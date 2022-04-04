using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UXWarmUpParamBuilder
{
    public class WarmUpTesting
    {
        internal bool RunWarmUp(WarmUpPayloadModel warmUpPayloadModel)
        {
            string res = "";
            string url = "WarmUp/TestWarmUp?paramType=" + warmUpPayloadModel.ParamType + "&routeUrl=" + warmUpPayloadModel.RouteUrl + "&param=" + warmUpPayloadModel.Param;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost.dev.mapshc.com/");
                    client.Timeout = TimeSpan.FromSeconds(300);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        
                        using (HttpContent content = response.Content)
                        {
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;
                        }
                    }
                }
                return Convert.ToBoolean(res);
            }
            catch
            {
                return false;
            }
        }

        internal bool RunWarmUpForPost(WarmUpPayloadModel warmUpPayloadModel)
        {
            string res = "";
            string url = "WarmUp/TestWarmUp";

            var json = JsonConvert.SerializeObject(warmUpPayloadModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost.dev.mapshc.com/");
                    client.Timeout = TimeSpan.FromSeconds(300);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage response = client.PostAsync(url,data).Result)
                    {

                        using (HttpContent content = response.Content)
                        {
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;
                        }
                    }
                }
                return Convert.ToBoolean(res);
            }
            catch
            {
                return false;
            }
        }
    }
}
