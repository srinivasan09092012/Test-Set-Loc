//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight;
using System.Text;

namespace UA3.CodeFactory.Core
{
    public class SolutionTransformStep : ViewModelBase
    {
        private string name;
        private bool? succeeded;
        private StringBuilder log = new StringBuilder();
        private string logBuffer = null;

        public SolutionTransformStep(string name)
        {
            this.Name = name;
            this.succeeded = null;
        }

        public string Name
        {
            get { return this.name; }
            set { this.Set("Name", ref this.name, value); }
        }

        public bool? Succeeded
        {
            get { return this.succeeded; }
            set { this.Set("Succeeded", ref this.succeeded, value); }
        }

        public string LogBuffer
        {
            get { return this.logBuffer; }
            set { this.Set("Logbuffer", ref this.logBuffer, value); }
        }

        public void Write(string format, params object[] args)
        {
            this.log.AppendLine(string.Format(format, args));

            this.logBuffer = this.log.ToString();
        }
    }
}
