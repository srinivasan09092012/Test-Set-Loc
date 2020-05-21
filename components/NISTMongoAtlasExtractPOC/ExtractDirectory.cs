﻿//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

public class Class1
{
	public Class1()
	{
	}
}



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