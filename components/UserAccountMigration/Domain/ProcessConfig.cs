//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Xml.Serialization;

namespace UserAccountMigration.Domain
{
    [Serializable]
    public class ProcessConfig
    {
        public enum ProcessTypes
        {
            Undefined,
            Import,
            Export
        }

        public enum FileFormatType
        {
            Undefined,
            CSV
        }

        public enum ProcessStatusType
        {
            Initialized,
            Complete,
            Error
        }

        public ProcessConfig()
        {
            this.Initialize();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("processType")]
        public ProcessTypes ProcessType { get; set; }

        [XmlAttribute("userGroupFilter")]
        public string UserGroupFilter { get; set; }

        [XmlAttribute("filePath")]
        public string FilePath { get; set; }

        [XmlAttribute("fileName")]
        public string FileName { get; set; }

        [XmlAttribute("fileFormat")]
        public FileFormatType FileFormat { get; set; }

        [XmlAttribute("processAD")]
        public bool ProcessAD { get; set; }

        [XmlAttribute("processUserProfile")]
        public bool ProcessUserProfile { get; set; }

        [XmlAttribute("maxThreads")]
        public int MaxThreads { get; set; }

        [XmlIgnore]
        public ProcessStatusType ProcessedStatus { get; set; }

        [XmlIgnore]
        public int ProcessedCount { get; set; }

        [XmlIgnore]
        public int ErroredCount { get; set; }

        public void Initialize()
        {
            this.Name = string.Empty;
            this.ProcessType = ProcessTypes.Undefined;
            this.UserGroupFilter = string.Empty;
            this.FilePath = string.Empty;
            this.FileName = string.Empty;
            this.FileFormat = FileFormatType.Undefined;
            this.ProcessAD = false;
            this.ProcessUserProfile = false;
            this.MaxThreads = 1;
            this.ProcessedStatus = ProcessStatusType.Initialized;
            this.ProcessedCount = 0;
            this.ErroredCount = 0;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                throw new ArgumentNullException("ProcessName");
            }

            if (this.ProcessType == ProcessTypes.Undefined)
            {
                throw new ArgumentNullException("ProcessType");
            }

            if (string.IsNullOrEmpty(this.FilePath))
            {
                throw new ArgumentNullException("FilePath");
            }

            if (string.IsNullOrEmpty(this.FileName))
            {
                throw new ArgumentNullException("FileName");
            }
            else
            {
                if (this.FileName.Contains("\\") || this.FileName.Contains("//") || !this.FileName.Contains("."))
                {
                    throw new ArgumentNullException("FileName");
                }
            }

            if (this.FileFormat == FileFormatType.Undefined)
            {
                throw new ArgumentNullException("FileFormat");
            }

            switch (this.ProcessType)
            {
                case ProcessTypes.Export:
                    if (string.IsNullOrEmpty(this.UserGroupFilter))
                    {
                        throw new ArgumentNullException("UserGroupFilter");
                    }
                    break;

                case ProcessTypes.Import:
                    break;
            }
        }
    }
}
