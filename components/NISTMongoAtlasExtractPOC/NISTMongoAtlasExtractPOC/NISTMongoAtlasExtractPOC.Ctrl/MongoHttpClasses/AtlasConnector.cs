//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//---------using System;
using HPE.HSP.UA3.Core.API.Logger;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class AtlasConnector
    {
        private string opsMgrBaseUrl;
        private string apiUser;
        private string apiKey;

        public AtlasConnector(string opsMgrBaseUrl, string apiUser, string apiKey)
        {
            this.opsMgrBaseUrl = opsMgrBaseUrl;
            this.apiUser = apiUser;
            this.apiKey = apiKey;
        }

        public enum ResponseType
        {
            HTTPResponse,
            FileResponse
        }

        public string GetRequest(string apiEndpoint)
        {
            string endpointUriStr = this.opsMgrBaseUrl + apiEndpoint;
            Uri endpointUri = new Uri(endpointUriStr);
            LoggerManager.Logger.LogInformational("Attempting to send HTTP request to " + endpointUriStr);

            // HTTP Digest Auth Request properties
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            string content = string.Empty;

            try
            {
                DigestHttpWebRequest request = new DigestHttpWebRequest(this.apiUser, this.apiKey);
            
                HttpWebResponse httpResponse = request.GetResponse(endpointUri);

                Stream stream = httpResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.Default);

                content = reader.ReadToEnd();
                stream.Close();
                httpResponse.Close();
                LoggerManager.Logger.LogInformational("Succesfully retrieved response and processed results in " + stopwatch.ElapsedMilliseconds.ToString() + " MS from " + endpointUriStr);
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogInformational("Response failed in " + stopwatch.ElapsedMilliseconds.ToString() + " milliseconds from " + endpointUriStr);
                throw ex;
            }

            return content;
        }

        public Stream GetRequestStream(string apiEndpoint)
        {
            string endpointUriStr = this.opsMgrBaseUrl + apiEndpoint;
            Uri endpointUri = new Uri(endpointUriStr);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            LoggerManager.Logger.LogInformational("Attempting to send HTTP request to " + endpointUriStr);

            // HTTP Digest Auth Request properties
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            try
            {
                DigestHttpWebRequest request = new DigestHttpWebRequest(this.apiUser, this.apiKey);
                HttpWebResponse httpResponse = request.GetResponse(endpointUri);
                Stream stream = httpResponse.GetResponseStream();
                LoggerManager.Logger.LogInformational("Succesfully retrieved response and processed results in " + stopwatch.ElapsedMilliseconds.ToString() + " MS from " + endpointUriStr);
                return stream;
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogInformational("Response failed in " + stopwatch.ElapsedMilliseconds.ToString() + " MS from " + endpointUriStr);
                throw ex;
            }
        }
    }
}