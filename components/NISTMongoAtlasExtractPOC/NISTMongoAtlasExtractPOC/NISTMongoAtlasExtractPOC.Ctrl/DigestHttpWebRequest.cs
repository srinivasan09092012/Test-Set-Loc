//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class DigestHttpWebRequest
    {
        private string user;
        private string password;
        private string realm;
        private string nonce;
        private string qop;
        private string cnonce;
        private Algorithm md5;
        private DateTime cnonceDate;
        private int nc;

        private string requestMethod = WebRequestMethods.Http.Get;
        private string contentType;
        private byte[] postData;

        public DigestHttpWebRequest(string user, string password)
        {
            this.user = user;
            this.password = password;
        }

        private enum Algorithm
        {
            MD5 = 0, // Apache Default
            MD5sess = 1 //IIS Default
        }

        public string Method
        {
            get { return this.requestMethod; }
            set { this.requestMethod = value; }
        }

        public string ContentType
        {
            get { return this.contentType; }
            set { this.contentType = value; }
        }

        public byte[] PostData
        {
            get { return this.postData; }
            set { this.postData = value; }
        }

        public HttpWebResponse GetResponse(Uri uri)
        {
            HttpWebResponse response = null;
            int infiniteLoopCounter = 0;
            int maxNumberAttempts = 2;

            while ((response == null ||
                response.StatusCode != HttpStatusCode.Accepted) &&
                infiniteLoopCounter < maxNumberAttempts)
            {
                try
                {
                    var request = this.CreateHttpWebRequestObject(uri);

                    // If we've got a recent Auth header, re-use it!
                    if (!string.IsNullOrEmpty(this.cnonce) &&
                        DateTime.Now.Subtract(this.cnonceDate).TotalHours < 1.0)
                    {
                        request.Headers.Add("Authorization", this.ComputeDigestHeader(uri));
                    }

                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (WebException webException)
                    {
                        // Try to fix a 401 exception by adding a Authorization header
                        if (webException.Response != null &&
                            ((HttpWebResponse)webException.Response).StatusCode == HttpStatusCode.Unauthorized)
                        {
                            var wwwAuthenticateHeader = webException.Response.Headers["WWW-Authenticate"];
                            this.realm = this.GetDigestHeaderAttribute("realm", wwwAuthenticateHeader);
                            this.nonce = this.GetDigestHeaderAttribute("nonce", wwwAuthenticateHeader);
                            this.qop = this.GetDigestHeaderAttribute("qop", wwwAuthenticateHeader);
                            this.md5 = this.GetMD5Algorithm(wwwAuthenticateHeader);

                            this.nc = 0;
                            this.cnonce = new Random().Next(123400, 9999999).ToString();
                            this.cnonceDate = DateTime.Now;

                            request = this.CreateHttpWebRequestObject(uri, true);
                            infiniteLoopCounter++;
                            response = (HttpWebResponse)request.GetResponse();
                        }
                        else
                        {
                            throw webException;
                        }
                    }

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Accepted:
                            return response;
                        case HttpStatusCode.Redirect:
                        case HttpStatusCode.Moved:
                            uri = new Uri(response.Headers["Location"]);

                            // We decrement the loop counter, as there might be a variable number of redirections which we should follow
                            infiniteLoopCounter--;
                            break;
                    }
                }
                catch (WebException ex)
                {
                    throw ex;
                }
            }

            throw new Exception("Error: Either authentication failed, authorization failed or the resource doesn't exist");
        }

        private HttpWebRequest CreateHttpWebRequestObject(Uri uri, bool addAuthenticationHeader)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.Method = this.Method;

            if (!string.IsNullOrEmpty(this.ContentType))
            {
                request.ContentType = this.ContentType;
            }

            if (addAuthenticationHeader)
            {
                request.Headers.Add("Authorization", this.ComputeDigestHeader(uri));
            }

            if (this.PostData != null && this.PostData.Length > 0)
            {
                request.ContentLength = this.PostData.Length;
                Stream postDataStream = request.GetRequestStream(); //open connection
                postDataStream.Write(this.PostData, 0, this.PostData.Length); // Send the data.
                postDataStream.Close();
            }
            else if (this.Method == WebRequestMethods.Http.Post && (this.PostData == null || this.PostData.Length == 0))
            {
                request.ContentLength = 0;
            }

            return request;
        }

        private HttpWebRequest CreateHttpWebRequestObject(Uri uri)
        {
            return this.CreateHttpWebRequestObject(uri, false);
        }

        private string ComputeDigestHeader(Uri uri)
        {
            this.nc = this.nc + 1;
            string ha1, ha2;
            switch (this.md5)
            {
                case Algorithm.MD5sess:
                    var secret = this.ComputeMd5Hash(string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", this.user, this.realm, this.password));
                    ha1 = this.ComputeMd5Hash(string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", secret, this.nonce, this.cnonce));
                    ha2 = this.ComputeMd5Hash(string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Method, uri.PathAndQuery));
                    var data = string.Format(CultureInfo.InvariantCulture, "{0}:{1:00000000}:{2}:{3}:{4}", this.nonce, this.nc, this.cnonce, this.qop, ha2);
                    var kd = this.ComputeMd5Hash(string.Format(CultureInfo.InvariantCulture, "{0}:{1}", ha1, data));

                    return string.Format("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", " + "algorithm=MD5-sess, response=\"{4}\", qop={5}, nc={6:00000000}, cnonce=\"{7}\"", this.user, this.realm, this.nonce, uri.PathAndQuery, kd, this.qop, this.nc, this.cnonce);
                case Algorithm.MD5:
                    ha1 = this.ComputeMd5Hash(string.Format("{0}:{1}:{2}", this.user, this.realm, this.password));
                    ha2 = this.ComputeMd5Hash(string.Format("{0}:{1}", this.Method, uri.PathAndQuery));
                    var digestResponse = this.ComputeMd5Hash(string.Format("{0}:{1}:{2:00000000}:{3}:{4}:{5}", ha1, this.nonce, this.nc, this.cnonce, this.qop, ha2));
                    return string.Format("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", " + "algorithm=MD5, response=\"{4}\", qop={5}, nc={6:00000000}, cnonce=\"{7}\"", this.user, this.realm, this.nonce, uri.PathAndQuery, digestResponse, this.qop, this.nc, this.cnonce);
            }

            throw new Exception("The digest header could not be generated");
        }

        private string GetDigestHeaderAttribute(string attributeName, string digestAuthHeader)
        {
            var regHeader = new Regex(string.Format(@"{0}=""([^""]*)""", attributeName));
            var matchHeader = regHeader.Match(digestAuthHeader);
            if (matchHeader.Success)
            {
                return matchHeader.Groups[1].Value;
            }

            throw new ApplicationException(string.Format("Header {0} not found", attributeName));
        }

        private Algorithm GetMD5Algorithm(string digestAuthHeader)
        {
            var md5Regex = new Regex(@"algorithm=(?<algo>.*)[,]", RegexOptions.IgnoreCase);
            var md5Attribute = md5Regex.Match(digestAuthHeader);
            if (md5Attribute.Success)
            {
                char[] charSeparator = new char[] { ',' };
                string algorithm = md5Attribute.Result("${algo}").ToLower().Split(charSeparator)[0];
                switch (algorithm)
                {
                    case "md5-sess":
                    case "\"md5-sess\"":
                        return Algorithm.MD5sess;
                    case "md5":
                    case "\"md5\"":
                    default:
                        return Algorithm.MD5;
                }
            }

            throw new ApplicationException("Could not determine Digest algorithm to be used from the server response.");
        }

        private string ComputeMd5Hash(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = MD5.Create().ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
