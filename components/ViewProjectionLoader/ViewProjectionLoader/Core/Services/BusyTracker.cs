//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace ViewProjectionLoader.Core.Services
{
    public class BusyTracker : IBusyTracker
    {
        private object sync = new object();

        public BusyTracker()
        {
            this.IsIdle = true;
        }

        public bool IsIdle { get; private set; }

        public void SetBusy()
        {
            lock (this.sync)
            {
                this.IsIdle = false;
                this.OnBusy?.Invoke();
            }
        }

        public void SetIdle()
        {
            lock (this.sync)
            {
                this.IsIdle = true;
                this.OnIdle?.Invoke();
            }
        }

        public event Action OnBusy;

        public event Action OnIdle;
    }
}
