//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class MongoAtlasApiKey
    {
        public MongoAtlasApiKey(string id, string publicKey)
        {
            this.ID = id;
            this.PublicKey = publicKey;
        }

        public string ID { get; set; }

        public string PublicKey { get; set; }
    }
}