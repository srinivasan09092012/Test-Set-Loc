//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Command;
using ViewProjectionLoader.Core;
using ViewProjectionLoader.Core.Messages;
using ViewProjectionLoader.Core.Model;
using ViewProjectionLoader.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BAS = HP.HSP.UA3.Core.BAS.CQRS.Interfaces;

namespace ViewProjectionLoader.ViewModel
{
    public class ViewProjectionsViewModel : MainEventViewModel
    {
        private ConnectionStringSettings connectionString;
        private IDbServiceFactory dbServiceFactory;
        private IWindowService windowService;
        private IBasLoader basLoader;
        private BasLoaderResult basLoadResult;

        public ViewProjectionsViewModel(IBusyTracker busyTracker, IStatusTracker statusTracker, IDbServiceFactory dbServiceFactory, IWindowService windowService, IBasLoader basLoader)
            : base(busyTracker, statusTracker)
        {
            this.dbServiceFactory = dbServiceFactory;
            this.windowService = windowService;
            this.basLoader = basLoader;

            MessengerInstance.Register<BasLoadedMessage>(this, this.OnBasLoaded);

            this.ClearViewProjectionSelections = new RelayCommand(this.OnClearViewProjectionSelections);
            this.SelectAllViewProjections = new RelayCommand(this.OnSelectAllViewProjections);
            this.TruncateViewProjections = new RelayCommand(this.OnTruncateViewProjections);

            this.ViewProjectionTableNames = new ObservableCollection<TableViewModel>();
        }

        public ICommand ClearViewProjectionSelections { get; private set; }

        public ICommand SelectAllViewProjections { get; private set; }

        public ICommand TruncateViewProjections { get; private set; }

        public ObservableCollection<TableViewModel> ViewProjectionTableNames { get; private set; }

        private void OnTruncateViewProjections()
        {
            Task.Factory.StartNew(() =>
            {
                this.SetBusy();

                IDbService svc = this.dbServiceFactory.CreateDbService(this.connectionString);
                DependencyWalker dw = new DependencyWalker();

                List<string> selectedToDelete = (from item in this.ViewProjectionTableNames
                                                 where item.IsChecked
                                                 select item.Name).ToList();

                this.SetStatus("Querying table dependencies");
                var tableSchemas = svc.GetTableSchemas();

                List<string> othersToDelete = dw.GetChildDependencies(tableSchemas, selectedToDelete);

                if (othersToDelete.Count > 0)
                {
                    MessageBoxResult shouldContinue = this.windowService.PromptAdditionalDeletes(othersToDelete);

                    if (shouldContinue != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }

                List<string> allToDelete = new List<string>();
                allToDelete.AddRange(selectedToDelete);
                allToDelete.AddRange(othersToDelete);

                List<TableSchemaModel> ordered = dw.SortByChildDependencies(tableSchemas, allToDelete);

                ordered.Reverse();

                var orderToDelete = ordered.Select(p => p.Name).ToArray();

                svc.DeleteTableData(ordered.Select(p => p.Name), this.StatusTracker);
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    this.ShowErrorMessage("One or more errors occured clearing view projections:", t.Exception);
                }

                this.SetIdle();
                this.ClearStatus();
            }).ConfigureAwait(false);
        }

        private void OnBasLoaded(BasLoadedMessage message)
        {
            this.basLoadResult = message.LoadResult;
            this.connectionString = this.basLoadResult.Configuration.GetPrimaryConnectionString();

            try
            {
                this.SetBusy();

                this.SetStatus("Connecting to database");
                IDbService dbService = this.dbServiceFactory.CreateDbService(this.connectionString);

                this.SetStatus("Querying for view projection tables");
                var names = dbService.GetViewProjectionTableNames(this.connectionString.ConnectionString);

                names.ForEach(p => this.ViewProjectionTableNames.Add(new TableViewModel() { Name = p }));
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage("Exception while querying for table names: {0}", ex.Message);
            }
            finally
            {
                this.SetIdle();
                this.ClearStatus();
            }
        }

        private void OnSelectAllViewProjections()
        {
            foreach (TableViewModel tvm in this.ViewProjectionTableNames)
            {
                tvm.IsChecked = true;
            }
        }

        private void OnClearViewProjectionSelections()
        {
            foreach (TableViewModel tvm in this.ViewProjectionTableNames)
            {
                tvm.IsChecked = false;
            }
        }
    }
}
