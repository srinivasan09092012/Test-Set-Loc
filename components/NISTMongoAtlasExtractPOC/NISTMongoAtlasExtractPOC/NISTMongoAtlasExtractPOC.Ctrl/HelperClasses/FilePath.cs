//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISTMongoAtlasExtractPOC.Ctrl.HelperClasses
{
    public class FilePath
    {
        public FilePath(string fileName, string path, string subDirectories)
        {
            this.FileName = fileName;
            this.Path = path;
            this.SubDirectories = subDirectories;
        }

        public string FileName { get; set; }

        public string Path { get; set; }

        public string SubDirectories { get; set; }
    }
}
