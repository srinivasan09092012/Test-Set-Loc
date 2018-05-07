//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Command;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Config;
using MainEvent.Core;
using MainEvent.Core.Messages;
using MainEvent.Core.Services;
using Microsoft.Practices.Unity;
using NEventStore;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BAS = HP.HSP.UA3.Core.BAS.CQRS.Interfaces;

namespace MainEvent.ViewModel
{
    public class CommitsViewModel : MainEventViewModel
    {
        private DateTime? deleteFrom;
        private DateTime? deleteThrough;
        private IDbServiceFactory dbServiceFactory;
        private IWindowService windowService;
        private ConnectionStringSettings basPrimaryConnectionString;
        private TenantBasConfiguration basConfiguration;

        public CommitsViewModel(IBusyTracker busyTracker, IStatusTracker statusTracker, IDbServiceFactory dbServiceFactory, IWindowService windowService)
            : base(busyTracker, statusTracker)
        {
            this.dbServiceFactory = dbServiceFactory;
            this.windowService = windowService;
            this.deleteFrom = DateTime.Today;
            this.deleteThrough = DateTime.Today;
            this.DeleteAggregatesInRange = new RelayCommand(this.OnDeleteAggregates);
            MessengerInstance.Register<BasLoadedMessage>(this, this.OnBasLoaded);
        }

        public void OnDeleteAggregates()
        {
            Task.Factory.StartNew(() =>
            {
                this.SetBusy();

                this.SetStatus("Connecting to database");
                using (var svc = this.dbServiceFactory.CreateDbService(this.basPrimaryConnectionString))
                {
                    this.SetStatus("Querying for aggregates within range");

                    var ids = svc.GetAggregatesWithCommitsBetween(this.DeleteFrom.GetValueOrDefault(), this.DeleteThrough.GetValueOrDefault());

                    if (ids.Count == 0)
                    {
                        this.windowService.ShowMessageBox("No aggregates found within the specified date range", "UA3 Main Event", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        TenantContainerProvider containerProv = new TenantContainerProvider();

                        using (UnityContainer container = containerProv.GetContainer(this.basConfiguration))
                        {
                            var factory = container.Resolve<BAS.IEventStorageFactory>();
                            using (IStoreEvents store = factory.GetEventStorage(this.basConfiguration))
                            {
                                foreach (AggregateStorageData asd in ids)
                                {
                                    this.SetStatus("Deleting event stream with id {0} in bucket {1}", asd.StreamId, asd.BucketId);
                                    store.Advanced.DeleteStream(asd.BucketId, asd.StreamId);
                                }
                            }
                        }
                    }
                }
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    this.ShowErrorMessage("One or more errors occurred:", t.Exception);
                }

                this.SetIdle();
                this.ClearStatus();
            }).ConfigureAwait(false);
        }

        private void OnBasLoaded(BasLoadedMessage message)
        {
            this.basConfiguration = message.LoadResult.Configuration;
            this.basPrimaryConnectionString = this.basConfiguration.GetPrimaryConnectionString();
        }

        public ICommand DeleteAggregatesInRange { get; private set; }

        public DateTime? DeleteFrom
        {
            get { return this.deleteFrom; }
            set { this.SetValue(ref this.deleteFrom, value); }
        }

        public DateTime? DeleteThrough
        {
            get { return this.deleteThrough; }
            set { this.SetValue(ref this.deleteThrough, value); }
        }
    }
}
