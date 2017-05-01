//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace MainEvent.Core.Services
{
    public interface IStatusTracker
    {
        string Status { get; }

        void Clear();

        void Set(string statusFormat, params object[] args);

        event Action<string> StatusUpdated;
    }
}