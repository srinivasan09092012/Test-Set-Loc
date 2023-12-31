//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities;
using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Utilities.LoadTenantDb.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using WebRefDataListsMaint = HP.HSP.UA3.Utilities.LoadTenantDb.DataListsService;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class Datalist
    {
        private WebRefDataListsMaint.DataListsServiceClient clientLicense = new WebRefDataListsMaint.DataListsServiceClient();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Datalist()
        {
            this.DataListItemAttributes = new List<DatalistItemAttribute>();
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

        public List<DatalistItemAttribute> DataListItemAttributes { get; set; }

        public string GetDataListId(Datalist dataList)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var datalistId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataLists>() where dl.ContentId == dataList.ContentId select dl.DataListsId).FirstOrDefault();

            if (datalistId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
            {
                return null;
            }
            else
            {
                return datalistId.ToString();
            }
        }

        public static string GetAttributeId(Datalist dataList)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var datalistId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListAttributes>() 
                              where dl.DataListsId == new Guid(dataList.Id) select dl.DataListsAttributeId).FirstOrDefault();

            // TODO: Consider error logic.
            return datalistId.ToString();
        }

        public static string GetDataListId(string contentId)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var datalistId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataLists>()
                              where dl.ContentId == contentId
                              select dl.DataListsId).FirstOrDefault();

            // TODO: Consider error logic.
            return datalistId.ToString();
        }

        public string GetDataListDirect(Datalist dataList)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var result = from dl in dataListDBContext.DataLists
                         join dlAttributes in dataListDBContext.DataListAttributes on dl.DataListsId equals dlAttributes.DataListsId
                         where dl.ContentId == dataList.ContentId
                         select new { Id = dl.DataListsId,
                             ContentId = dl.ContentId,
                             Description = dl.Description,
                             Name = dl.DataListsName,
                             TenantId = dl.TenantId.ToString(),
                             TenantModuleId = dl.TenantModuleId,
                             IsActive = dl.IsActive,
                             AttributesDataListId = dlAttributes.DataListsId,
                             DataListsAttributeId = dlAttributes.DataListsAttributeId,
                             TypeName = dlAttributes.TypeName,
                             TypeDataListId = dlAttributes.TypeDataListsId,
                             TypeDefaultItemId = dlAttributes.TypeDefaultItemId,
                             AttributesIsActive = dlAttributes.IsActive
                         };

            string returnDatalistId = null;
            List<DatalistItemAttribute> allDatalistItemAttributes = new List<DatalistItemAttribute>();
            foreach (var singleResult in result)
            {
                returnDatalistId = singleResult.Id.ToString("D").ToUpper();
                dataList.Id = singleResult.Id.ToString("D").ToUpper();
                dataList.ContentId = singleResult.ContentId;
                dataList.Description = singleResult.Description;
                dataList.Name = singleResult.Name;
                dataList.TenantId = singleResult.TenantId;
                dataList.TenantModuleId = singleResult.TenantModuleId.ToString("D").ToUpper();
                dataList.IsActive = singleResult.IsActive;

                DatalistItemAttribute datalistItemAttributes = new DatalistItemAttribute();               
                datalistItemAttributes.DataListId = singleResult.AttributesDataListId.ToString("D").ToUpper();
                datalistItemAttributes.DataListsAttributeId = singleResult.DataListsAttributeId.ToString("D").ToUpper();
                datalistItemAttributes.TypeName = singleResult.TypeName;
                datalistItemAttributes.TypeDataListId = singleResult.TypeDataListId.ToString("D").ToUpper();
                datalistItemAttributes.TypeDefaultItemId = singleResult.TypeDefaultItemId.ToString("D").ToUpper();
                datalistItemAttributes.IsActive = singleResult.AttributesIsActive;

                allDatalistItemAttributes.Add(datalistItemAttributes);                                        
            }

            dataList.DataListItemAttributes = allDatalistItemAttributes;

            return returnDatalistId;
        }

        public bool DoesDataListExistsDirect(string contentId)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();
            bool dataListsExists = false;

            var result = from dl in dataListDBContext.DataLists
                         where dl.ContentId == contentId
                         select new
                         {
                             Id = dl.DataListsId
                         };

            foreach (var singleResult in result)
            {
                dataListsExists = true;
                break;
            }

            return dataListsExists;
        }

        public string GetDataList(Datalist dataList)
        {
            GetDataListId(dataList);

            int retryCount = 0;
            string objDataQuery = string.Format("DataList(TenantID={1})?$filter=ContentID%20eq%20%27{0}%27", dataList.ContentId, this.MainForm.TenantId);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
            while (!response.IsSuccessStatusCode && retryCount < 100)
            {
                response = client.GetAsync(objDataQuery).Result;
                retryCount++;
            }

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
                            dataList.DataListItemAttributes.Clear();

                            if (dataListDetail.ContainsKey("DataListAttributes"))
                            {
                                object attributeList = dataListDetail["DataListAttributes"] as object;
                                foreach (Dictionary<string, object> attributes in (IList)attributeList)
                                {
                                    DatalistItemAttribute datalistItemAttribute = new DatalistItemAttribute();
                                    datalistItemAttribute.DataListId = attributes["DataListID"].ToString();
                                    datalistItemAttribute.DataListsAttributeId = attributes["ID"].ToString();
                                    datalistItemAttribute.TypeName = attributes["TypeName"].ToString();
                                    datalistItemAttribute.TypeDataListId = attributes["DataListTypeID"].ToString();
                                    datalistItemAttribute.TypeDataListIdText = attributes["DataListTypeName"].ToString();
                                    datalistItemAttribute.TypeDefaultItemIdText = attributes["DefaultTypeText"].ToString();
                                    datalistItemAttribute.TypeDefaultItemId = attributes["DefaultTypeValue"].ToString();
                                    datalistItemAttribute.IsActive = (bool)attributes["IsActive"];
                                    dataList.DataListItemAttributes.Add(datalistItemAttribute);
                                }
                            }

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
                    TenantModuleId = dataList.TenantModuleId,
                    DataListAttributes = this.SetDataListAttributes(dataList.DataListItemAttributes)
                },
                Requestor = new Core.BAS.CQRS.UserMeta.RequestorModel()
                {
                    IdentifierId = "User1",
                    IdentifierIdType = Core.BAS.CQRS.UserMeta.CoreEnumerations.Messaging.IdentifierIdType.User,
                    TenantId = this.MainForm.TenantId,
                    RequestDate = DateTime.UtcNow
                }
            };

            try
            {
                response = this.clientLicense.UpdateDataList(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheItemLinkerKey, "false", "false", "false");
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheLocalLanguageKey, "false", "false", "false");
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListAttrKey, "false", "false", "false");
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListsKey, "false", "false", "true");
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
                    IsActive = true,
                    DataListAttributes = this.SetDataListAttributes(dataList.DataListItemAttributes)
                },
                Requestor = new Core.BAS.CQRS.UserMeta.RequestorModel()
                {
                    IdentifierId = "User1",
                    IdentifierIdType = Core.BAS.CQRS.UserMeta.CoreEnumerations.Messaging.IdentifierIdType.User,
                    TenantId = this.MainForm.TenantId,
                    RequestDate = DateTime.UtcNow
                }
            };

            try
            {
                response = this.clientLicense.AddDataList(command);
            }
            catch (Exception ex)
            {
                log.Error("Datalist.AddDataList ERROR Datalist Exception", ex);
                throw (ex);
            }

            try
            {
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheItemLinkerKey, "false", "false", "false");
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheLocalLanguageKey, "false", "false", "false");
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListAttrKey, "false", "false", "false");
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListsKey, "false", "false", "true");
            }
            catch (Exception ex)
            {
                log.Error("Datalist.AddDataList ERROR Refersh Cache Exception", ex);
                throw (ex);
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

        private DataListsService.DataListAttribute[] SetDataListAttributes(List<DatalistItemAttribute> attributeList)
        {
            if (attributeList != null)
            {
                DataListsService.DataListAttribute[] result = new DataListsService.DataListAttribute[] { };
                List<DataListsService.DataListAttribute> result2 = new List<DataListsService.DataListAttribute>();
                DataListsService.DataListAttribute attribute = null;

                foreach (DatalistItemAttribute itemAttr in attributeList)
                {
                    attribute = new DataListsService.DataListAttribute()
                    {
                        DataListId = itemAttr.DataListId,
                        DataListsAttributeId = itemAttr.DataListsAttributeId,
                        IsActive = itemAttr.IsActive,
                        TypeDataListId = itemAttr.TypeDataListId,
                        TypeDefaultItemId = itemAttr.TypeDefaultItemId,
                        TypeName = itemAttr.TypeName
                    };

                    result2.Add(attribute);
                }

                result = result2.ToArray();
                return result;
            }
            else
            {
                return null;
            }
        }

        private void RefreshCache(string cacheKey, string clearAllCodeTableCacheFlag = "false", string reloadCache = "true", string reloadAllCodeTableCache = "false")
        {
            string objDataQuery = string.Empty;

            objDataQuery = string.Format("CacheRefresh(CacheKey='{0}',ClearAllCodeTableCache={1},ReloadCache={2},ReloadAllCodeTableCache={3},TenantID={4})", cacheKey, clearAllCodeTableCacheFlag, reloadCache, reloadAllCodeTableCache, this.MainForm.TenantId);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
        }
    }
}
