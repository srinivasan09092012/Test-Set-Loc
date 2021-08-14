using System;

namespace Common
{
    public class Constants
    {
        public static class WebSolutionStructure
        {
            public class HtmlBlock
            {
                public const string linkBootStrap = "<link href ='../css/bootstrap.min.css' rel='stylesheet'>";
                public const string linkMainMdb = "<link href =\"../css/mdb.min.css\" rel=\"stylesheet\">";
                public const string linkStyles = "<link href =\"../css/style.css\" rel=\"stylesheet\">";
                public const string linkDataTablesMin = "<link href =\"../css/addons/datatables.min.css\" rel=\"stylesheet\">";
            }

            public static class Files
            {
                public const string IndexPage = @"\index.html";
                public const string UnderConstructionIcon = @"\underConstruction.jpg";
                public const string StartWithTokenCommandClassesIOServices = "_Contracts_Commands";////*
                public const string StartWithTokenEventsIOServices = "_Contracts_Events";////*
                public const string StartWithTokenQueryParams = "_Contracts_Queries_Parameters_"; ////*
                public const string StartWithTokenQueryDomain = "_Contracts_Domain_"; ////*

                public static class Extensions
                {
                    public const string htmlExtension = ".htm";
                    public const string xmlExtension = ".xml";
                }
            }

            public static class Folders
            {
                //sandcastle output
                public const string Html = @"\html\";
                public const string Icons = "icons";
                public const string outPutFolderName = @"\API Sevice Specification\";// used by default constructor

                //bootstrap folder structure:
                public const string BootStrap = @"\BootStrap";
                public const string img = @"\img\";
            }
        }

        public static class HtmlInventory
        {
            public const string PageFooterDiv = "pageFooter"; // All pages uses this div to display copy right contact information
            public const string PageHeaderDiv = "PageHeader";
            public const string PageSyntaxisClassDiv = "codeSnippetContainer"; // All pages uses this div to display syntaxis information
            public const string PageSyntaxiHeadersClassSpan = "collapsibleRegionTitle"; //header
        }

        public static class Labels
        {
            public static string CopyRight = string.Format("This document is the property of Gainwell Technologies, Copyright (c) {0}. All rights reserved.", DateTime.Now.Year.ToString());
            public static string ProductName = "DXC Payer Portfolio";
        }
    }
}
