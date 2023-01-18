//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Threading;
using ViewProjectionLoader.Core.Messages;
using ViewProjectionLoader.Core.Services;

namespace ViewProjectionLoader.ViewModel
{
    public class MainViewModel : MainEventViewModel
    {
        private string status;

        public MainViewModel(IBusyTracker busyTracker, IStatusTracker statusTracker)
            : base(busyTracker, statusTracker)
        {
            MessengerInstance.Register<BasLoadedMessage>(this, this.OnBasLoaded);
        }

        protected override void OnStatusUpdated(string newStatus)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this.Status = newStatus;
            });
        }

        private void OnBasLoaded(BasLoadedMessage message)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this.Title = string.Format("UA3 Main Event - {0}\\{1}", message.LoadResult.ModuleName, message.LoadResult.ApplicationName);
            });
        }

        public string Title { get; private set; }

        public string Status
        {
            get { return this.status; }
            set { this.SetValue(ref this.status, value); }
        }
    }
}
