//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using ViewProjectionLoader.Core.Services;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ViewProjectionLoader.ViewModel
{
    public abstract class MainEventViewModel : ViewModelBase
    {
        private bool isIdle;
        private IBusyTracker busyTracker;
        private IStatusTracker statusTracker;
        private TaskScheduler scheduler;

        public MainEventViewModel(IBusyTracker busyTracker, IStatusTracker statusTracker)
            : base()
        {
            this.busyTracker = busyTracker;
            this.statusTracker = statusTracker;
            this.IsIdle = busyTracker.IsIdle;

            this.busyTracker.OnBusy += this.OnBusy;
            this.busyTracker.OnIdle += this.OnIdle;

            this.statusTracker.StatusUpdated += this.OnStatusUpdated;
            this.SetTaskScheduler(TaskScheduler.Default);
        }

        public bool IsIdle
        {
            get { return this.isIdle; }
            set { this.SetValue(ref this.isIdle, value); }
        }

        public void SetTaskScheduler(TaskScheduler scheduler)
        {
            this.scheduler = scheduler ?? TaskScheduler.Default;
        }

        protected TaskScheduler Scheduler
        {
            get { return this.scheduler; }
        }

        protected IStatusTracker StatusTracker
        {
            get { return this.statusTracker; }
        }

        private void OnIdle()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this.IsIdle = true;
            });
        }

        private void OnBusy()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this.IsIdle = false;
            });
        }

        protected virtual void SetIdle()
        {
            this.busyTracker.SetIdle();
        }

        protected virtual void SetBusy()
        {
            this.busyTracker.SetBusy();
        }

        protected virtual void SetStatus(string format, params object[] args)
        {
            this.statusTracker.Set(format, args);
        }

        protected virtual void ClearStatus()
        {
            this.statusTracker.Clear();
        }

        protected void SetValue<T>(ref T field, T newValue, [CallerMemberName] string propertyname = "")
        {
            this.Set(propertyname, ref field, newValue);
        }

        protected virtual void OnStatusUpdated(string newStatus)
        {
        }
    }
}
