//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight;
using ViewProjectionLoader.Core.Services;
using Ookii.Dialogs.Wpf;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace ViewProjectionLoader.ViewModel
{
    public static class ViewModelExtensions
    {
        private static void ShowTaskDialog(string title = "", string mainInstruction = "", string mainContent = "", string expandedInformation = "", TaskDialogIcon icon = TaskDialogIcon.Information, bool canMinimize = false)
        {
            TaskDialog dlg = new TaskDialog();
            dlg.AllowDialogCancellation = true;
            dlg.Buttons.Add(new TaskDialogButton(ButtonType.Ok)
            {
                Default = true,
                Enabled = true,
                Text = "OK"
            });
            dlg.Buttons.Add(new TaskDialogButton(ButtonType.Custom)
            {
                Default = false,
                Enabled = true,
                Text = "Copy to Clipboard",
            });

            dlg.ButtonClicked += OnButtonClicked;

            dlg.ExpandedByDefault = false;
            dlg.ExpandedInformation = expandedInformation;

            dlg.Content = mainContent;
            dlg.MainInstruction = mainInstruction;
            dlg.MinimizeBox = canMinimize;
            dlg.WindowTitle = title;
            dlg.MainIcon = icon;

            dlg.ShowDialog();
            dlg.ButtonClicked -= OnButtonClicked;
        }

        public static void ShowErrorMessage(this ViewModelBase source, string format, params object[] args)
        {
            ShowTaskDialog("UA3 Main Event - Error", "Error", string.Format(format, args), "", TaskDialogIcon.Error, false);
        }

        public static void ShowErrorMessage(this ViewModelBase source, string description, Exception exception)
        {
            string message = (exception is TargetInvocationException) ? exception.InnerException.Message : exception.Message;

            ShowTaskDialog("UA3 Main Event - Error", description, message, exception.StackTrace, TaskDialogIcon.Error, false);
        }

        public static void ShowErrorMessage(this ViewModelBase source, string description, AggregateException exception)
        {
            source.ShowErrorMessage(description, exception.InnerExceptions[0]);
        }

        private static void OnButtonClicked(object sender, TaskDialogItemClickedEventArgs e)
        {
            TaskDialog dlg = sender as TaskDialog;
            TaskDialogButton button = e.Item as TaskDialogButton;

            if (button != null && button.ButtonType == ButtonType.Custom)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
                sb.AppendLine();
                sb.AppendLine(dlg.MainInstruction);
                sb.AppendLine(dlg.Content);
                sb.AppendLine();

                if (!string.IsNullOrEmpty(dlg.ExpandedInformation))
                {
                    sb.AppendLine(dlg.ExpandedInformation);
                }

                Clipboard.SetText(sb.ToString());

                e.Cancel = true;
            }
        }
    }
}
