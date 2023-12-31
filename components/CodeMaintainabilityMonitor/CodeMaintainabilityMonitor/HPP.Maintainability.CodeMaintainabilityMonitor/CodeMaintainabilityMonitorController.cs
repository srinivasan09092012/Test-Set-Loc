﻿//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Windows.Forms;

namespace HPP.Maintainability.CodeMaintainabilityMonitor
{
    public static class CodeMaintainabilityMonitorController
    {
        #region Public Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CodeMaintainabilityMonitor());
        }
        #endregion
    }
}