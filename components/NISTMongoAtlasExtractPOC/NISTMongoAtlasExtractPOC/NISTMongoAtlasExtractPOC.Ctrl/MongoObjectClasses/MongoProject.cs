//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class MongoProject
    {
        public MongoProject(string projectID, string clusterName)
        {
            this.ProjectID = projectID;
            this.ClusterName = clusterName;
        }

        public string ProjectID { get; set; }

        public string ClusterName { get; set; }
    }
}