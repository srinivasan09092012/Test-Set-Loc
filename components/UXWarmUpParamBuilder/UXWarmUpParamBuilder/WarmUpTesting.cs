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
        internal bool RunWarmUp(WarmUpPayloadModel warmUpPayloadModel, string domain)
        {
            string res = "";
            string url = "WarmUp/TestWarmUpForGet?ModuleName=" + warmUpPayloadModel.ModuleName + "&paramType=" + warmUpPayloadModel.ParamType + "&routeUrl=" + warmUpPayloadModel.RouteUrl + "&param=" + warmUpPayloadModel.Param;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(domain);
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

        internal bool RunWarmUpForPost(WarmUpPayloadModel warmUpPayloadModel, string domain)
        {
            string res = "";
            string url = "WarmUp/TestWarmUpForPost";

            var json = JsonConvert.SerializeObject(warmUpPayloadModel);
            var handler = new WinHttpHandler();
            handler.SendTimeout = TimeSpan.FromSeconds(300);
            handler.ReceiveDataTimeout = TimeSpan.FromSeconds(300);
            handler.ReceiveHeadersTimeout = TimeSpan.FromSeconds(300);
            try
            {
                using (var client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromSeconds(300);
                    var request = new HttpRequestMessage
                    {
                        RequestUri = new Uri(domain + url),
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };

                    using (HttpResponseMessage response = client.SendAsync(request).Result)
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
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
