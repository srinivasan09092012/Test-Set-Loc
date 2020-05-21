//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class MongoHostProcess

    //// "Host" in Mongo API terms
    {
        public MongoHostProcess(DateTime created, string hostname, DateTime lastPing, int port, string typeName, string version)
        {
            this.DateCreated = created;
            this.Hostname = hostname;
            this.LastPing = lastPing;
            this.Port = port;
            this.TypeName = typeName;
            this.Version = version;
        }

        public DateTime DateCreated { get; set; }

        public string Hostname { get; set; }

        public DateTime LastPing { get; set; }

        public int Port { get; set; }

        public string TypeName { get; set; }

        public string Version { get; set; }
    }
}