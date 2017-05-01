//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Windows;

namespace MainEvent.ViewModel
{
    public static class ViewModelExtensions
    {
        public static void ShowErrorMessage(this ViewModelBase source, string format, params object[] args)
        {
            MessageBox.Show(string.Format(format, args), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowErrorMessage(this ViewModelBase source, string description, AggregateException exception)
        {
            string[] errors = exception.InnerExceptions.Select(p => p.Message).ToArray();
            string err = string.Join("\n\n", errors);
            source.ShowErrorMessage("{0}:\n\n{1}", description, err);
        }
    }
}