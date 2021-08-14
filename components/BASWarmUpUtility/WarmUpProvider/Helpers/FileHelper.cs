//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using WarmUpProvider.Domain;

namespace WarmUpProvider.Helpers
{
    public class FileHelper
    {
        private JsonSerializer typedSerializer = new JsonSerializer
        {
            TypeNameHandling = TypeNameHandling.None,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        public List<TenantWarmUpModel> ReadEndpointDataFromJSON()
        {
            string fileName = ConfigurationManager.AppSettings["JSONFileName"];
            List<TenantWarmUpModel> endpoints = new List<TenantWarmUpModel>();

            try
            {
                using (Stream stream = new FileStream("Configs/" + fileName, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        using (JsonReader jsonReader = new JsonTextReader(reader))
                        {
                            endpoints = this.typedSerializer.Deserialize<List<TenantWarmUpModel>>(jsonReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogError("Error deserializing JSON file!!!!", ex);
                throw ex;
            }

            return endpoints;
        }

        public void WriteEndpointDataToJSON(List<TenantWarmUpModel> tenantWarmUpData)
        {
            string file = ConfigurationManager.AppSettings["JSONFileName"];

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(file))
                {
                    using (JsonTextWriter jsonWriter = new JsonTextWriter(streamWriter))
                    {
                        this.typedSerializer.Serialize(jsonWriter, tenantWarmUpData);
                    }
                };
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogError("Could not write to file!!!!" + ex);
                throw ex;
            }
        }
    }
}