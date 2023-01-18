//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using FileHelpers;

namespace SQLToTaskConverter
{
    /// <summary>
    /// This class is used when the query is executed it returns a list of these objects
    /// that are then converted to the necessary input used by the TaskManagement TaskBulkUpload
    /// Process.
    /// </summary>
    [DelimitedRecord(",")]    
    public class Task
    {
        [FieldQuoted('"')]
        public string Id { get; set; }

        [FieldQuoted('"')]
        public string TaskMemberId { get; set; }

        [FieldQuoted('"')]
        public string TaskSourceModuleCode { get; set; }

        [FieldQuoted('"')]
        public string TaskTypeCode { get; set; }

        [FieldQuoted('"')]
        public string ExceptionErrorCode { get; set; }

        [FieldQuoted('"')]
        public string TaskComment { get; set; }
    }
}
