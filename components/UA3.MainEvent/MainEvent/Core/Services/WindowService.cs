//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MainEvent.Core.Services
{
    public class WindowService : IWindowService
    {
        public void MoveToMainWindow()
        {
            Window previousWindow = App.Current.MainWindow;
            MainWindow newWindow = new MainWindow();

            newWindow.Show();
            previousWindow?.Hide();

            App.Current.MainWindow = newWindow;

            previousWindow?.Close();
        }

        public bool BrowseForFolder(string description, out string selectedPath)
        {
            selectedPath = null;

            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.Description = description;
            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            dlg.ShowNewFolderButton = false;
            dlg.UseDescriptionForTitle = true;

            bool? dialogResult = dlg.ShowDialog(App.Current.MainWindow);
            bool result = dialogResult == null ? false : (bool)dialogResult;

            if (result == true)
            {
                selectedPath = dlg.SelectedPath;
            }

            return result;
        }

        public MessageBoxResult PromptAdditionalDeletes(IEnumerable<string> additionalDeletes)
        {
            string message = new StringBuilder("Due to dependencies, these additional tables must also have their data deleted:")
                        .AppendLine()
                        .AppendLine()
                        .AppendLine(string.Join("\n", additionalDeletes))
                        .AppendLine()
                        .AppendLine("Continue with delete?")
                        .ToString();

            return this.ShowMessageBox(message, "UA3 Main Event - Input Required", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public MessageBoxResult PromptAggregateDeletes(int total)
        {
            string message = "This will delete {0} aggregates from the commits table. Continue?";

            return this.ShowMessageBox(message, "UA3 Main Event - Input Required", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public virtual MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}