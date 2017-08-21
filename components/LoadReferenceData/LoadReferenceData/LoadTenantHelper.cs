using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoadReferenceData
{
    public static class LoadTenantHelper
    {
        public static async Task<bool> ExecuteODataQueryAsync(string query)
        {
            string odataUrl = ConfigurationManager.AppSettings["ODataURL"]; ;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(odataUrl);
                client.Timeout = TimeSpan.FromMinutes(30);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var response = await client.GetAsync(query))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(query + " Loaded: " + response.IsSuccessStatusCode);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool ExecuteODataQuery(string query)
        {
            string odataUrl = ConfigurationManager.AppSettings["ODataURL"]; ;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(odataUrl);
                client.Timeout = TimeSpan.FromMinutes(20);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = client.GetAsync(query).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
        }
    }
}
