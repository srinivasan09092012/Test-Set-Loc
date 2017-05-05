//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Windows;

namespace MainEvent.Core.Services
{
    public interface IWindowService
    {
        void MoveToMainWindow();

        MessageBoxResult PromptAdditionalDeletes(IEnumerable<string> additionalDeletes);

        MessageBoxResult PromptAggregateDeletes(int total);

        MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);

        bool BrowseForFolder(string description, out string selectedPath);
    }
}