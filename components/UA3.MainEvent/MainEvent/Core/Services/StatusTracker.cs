//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace MainEvent.Core.Services
{
    public class StatusTracker : IStatusTracker
    {
        private object sync = new object();

        public void Set(string statusFormat, params object[] args)
        {
            lock (this.sync)
            {
                this.Status = string.Format(statusFormat, args);
                this.StatusUpdated?.Invoke(this.Status);
            }
        }

        public void Clear()
        {
            this.Set(string.Empty);
        }

        public string Status { get; private set; }

        public event Action<string> StatusUpdated;
    }
}