//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using MainEvent.Core.Messages;
using MainEvent.Core.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainEvent.ViewModel
{
    public class BasLoaderViewModel : MainEventViewModel
    {
        private string basPath;
        private bool basSelected = false;
        private IWindowService windowService;
        private IBasLoader basLoader;
        private string loadStatus;
        private bool loadingInProgress = false;
        private bool canLoadBas = false;
        private bool canSelectBas = true;

        public BasLoaderViewModel(IWindowService windowService, IBasLoader basLoader, IStatusTracker statusTracker)
            : base(new BusyTracker(), statusTracker)
        {
            this.windowService = windowService;
            this.basLoader = basLoader;

            this.SelectBas = new RelayCommand(this.OnSelectBas);
            this.LoadBas = new RelayCommand(this.OnLoadBas);
        }

        protected override void OnStatusUpdated(string newStatus)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this.LoadStatus = newStatus;
            });
        }

        private void OnLoadBas()
        {
            this.OnBasLoadStarted();

            Task.Factory.StartNew<BasLoaderResult>(() =>
            {
                return this.basLoader.Load(this.BasPath, this.StatusTracker);
            }, CancellationToken.None, TaskCreationOptions.None, this.Scheduler).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    this.ShowErrorMessage("One or more errors occurred loading the specified BAS solution:", t.Exception);
                }
                else
                {
                    MessengerInstance.Send(new BasLoadedMessage(t.Result));

                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        this.windowService.MoveToMainWindow();
                        this.OnBasLoadCompleted();
                    });
                }
            }, this.Scheduler).ConfigureAwait(false);
        }

        private void OnBasLoadCompleted()
        {
            this.LoadingInProgress = false;
            this.CanSelectBas = true;
            this.CanLoadBas = true;
        }

        private void OnBasLoadStarted()
        {
            this.LoadingInProgress = true;
            this.CanSelectBas = false;
            this.CanLoadBas = false;
        }

        private void OnSelectBas()
        {
            string path = "";
            if (this.windowService.BrowseForFolder("Select BAS Service Folder", out path))
            {
                this.BasSelected = true;
                this.BasPath = path;
                this.CanLoadBas = true;
            }
        }

        public ICommand SelectBas { get; private set; }

        public ICommand LoadBas { get; private set; }

        public bool LoadingInProgress
        {
            get { return this.loadingInProgress; }
            set { this.SetValue(ref this.loadingInProgress, value); }
        }

        public bool BasSelected
        {
            get { return this.basSelected; }
            set { this.SetValue(ref this.basSelected, value); }
        }

        public string LoadStatus
        {
            get { return this.loadStatus; }
            set { this.SetValue(ref this.loadStatus, value); }
        }

        public string BasPath
        {
            get { return this.basPath; }
            set { this.SetValue(ref this.basPath, value); }
        }

        public bool CanLoadBas
        {
            get { return this.canLoadBas; }
            set { this.SetValue(ref this.canLoadBas, value); }
        }

        public bool CanSelectBas
        {
            get { return this.canSelectBas; }
            set { this.SetValue(ref this.canSelectBas, value); }
        }
    }
}
