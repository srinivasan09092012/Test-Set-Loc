//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using CommandLine;
using CommandLine.Text;
using HP.HSP.UA3.BatchProcessingFactory.Core.Batch;
using System;
using System.Collections.Generic;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class CommandLineOptions : OptionsBase
    {
        [Usage(ApplicationAlias = "NISTMongoAtlasExtractPOC.Ctrl.Main.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>()
                {
                new Example(
                    "Runs Batch Job",
                    new CommandLineOptions
                    {
                        TenantId = new Guid("77B50320-5F06-5740-84F4-18D4A8CDA51D")
                    }),
                };
            }
        }

        [Option("StartDate", Required = false, HelpText = "StartDate must be valid date in format MM/DD/YYYY")]
        public DateTime? StartDate { get; set; } ////Todo: Remove Example 
    }
}