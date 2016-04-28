//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Utilities.LoadTenantDb.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WebRefDataListsMaint = HP.HSP.UA3.Utilities.LoadTenantDb.DataListsService;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class Datalist
    {
        private WebRefDataListsMaint.DataListsServiceClient clientLicense = new WebRefDataListsMaint.DataListsServiceClient();

        public Datalist()
        {
        }

        public MainForm MainForm { get; set; }

        public string Id { get; set; }

        public string ContentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IdentifierId { get; set; }

        public bool IsActive { get; set; }

        public string TenantId { get; set; }

        public string TenantModuleId { get; set; }

        public string GetDataList(Datalist dataList)
        {
            string objDataQuery = string.Format("DataList?$filter=ContentID%20eq%20%27{0}%27", dataList.ContentId);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;

            if (response.IsSuccessStatusCode)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> parameters = serializer.Deserialize<Dictionary<string, object>>(response.Content.ReadAsStringAsync().Result);

                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    if (parameter.Value is IList)
                    {
                        IList dataLists = (IList)parameter.Value;

                        foreach (Dictionary<string, object> dataListDetail in dataLists)
                        {
                            dataList.Id = dataListDetail["ID"].ToString();
                            dataList.ContentId = dataListDetail["ContentID"].ToString();
                            dataList.Description = (string)dataListDetail["Description"];
                            dataList.Name = dataListDetail["DataListsName"].ToString();
                            dataList.TenantId = dataListDetail["TenantID"].ToString();
                            dataList.TenantModuleId = dataListDetail["TenantModuleID"].ToString();
                            dataList.IsActive = (bool)dataListDetail["IsActive"];
                            return dataList.Id;
                        }
                    }
                }
            }

            return null;
        }

        public bool UpdateDataList(Datalist dataList)
        {
            DataListsService.DataListsUpdated response = null;

            DataListsService.UpdateDataListCommand command = new DataListsService.UpdateDataListCommand()
            {
                UpdateDataList = new DataListsService.UpdateDataList()
                {
                    Id = dataList.Id,
                    ContentId = dataList.ContentId,
                    DataListsName = dataList.Name,
                    Description = dataList.Description,
                    IsActive = dataList.IsActive,
                    TenantModuleId = dataList.TenantModuleId
                },
                Requestor = new DataListsService.RequestorModel()
                {
                    IdentifierId = dataList.IdentifierId,
                    IdentifierIdType = HP.HSP.UA3.Utilities.LoadTenantDb.DataListsService.CoreEnumerationsMessagingIdentifierIdType.User,
                    TenantId = dataList.TenantId
                }
            };

            try
            {
                response = this.clientLicense.UpdateDataList(command);
            }
            catch
            {
                return false;
            }

            if (response.DataListId != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Datalist AddDataList(Datalist dataList)
        {
            DataListsService.DataListsAdded response = null;

            DataListsService.AddDataListCommand command = new DataListsService.AddDataListCommand()
            {
                AddDataList = new DataListsService.AddDataList()
                {
                    ContentId = dataList.ContentId,
                    DataListsName = dataList.Name,
                    Description = dataList.Description,
                    TenantModuleId = dataList.TenantModuleId,
                    IsActive = true
                },
                Requestor = new DataListsService.RequestorModel()
                {
                    IdentifierId = dataList.IdentifierId,
                    IdentifierIdType = HP.HSP.UA3.Utilities.LoadTenantDb.DataListsService.CoreEnumerationsMessagingIdentifierIdType.User,
                    TenantId = dataList.TenantId
                }
            };

            try
            {
                response = this.clientLicense.AddDataList(command);
            }
            catch
            {
                return null;
            }

            if (response.DataListId != null)
            {
                dataList.Id = response.DataListId;
                return dataList;
            }
            else
            {
                return null;
            }
        }
    }
}
