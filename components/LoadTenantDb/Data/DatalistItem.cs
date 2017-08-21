//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities;
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
    public class DatalistItem
    {
        private WebRefDataListsMaint.DataListsServiceClient clientLicense = new WebRefDataListsMaint.DataListsServiceClient();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DatalistItem()
        {
            this.DataListItemLanguages = new List<DatalistItemLanguage>();
            this.DataListItemAttributeValues = new List<DatalistItemAttributeValue>();
            this.DataListItemLinks = new List<DatalistItemLink>();
        }

        public MainForm MainForm { get; set; }

        public string DataListId { get; set; }

        public string Id { get; set; }

        public string Key { get; set; }

        public string ContentId { get; set; }

        public int OrderIndex { get; set; }

        public bool IsActive { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime EndDate { get; set; }

        public string IdentifierId { get; set; }

        public string TenantId { get; set; }

        public List<DatalistItemLanguage> DataListItemLanguages { get; set; }

        public List<DatalistItemLink> DataListItemLinks { get; set; }

        public List<DatalistItemAttributeValue> DataListItemAttributeValues { get; set; }

        public string GetDataListItemId(Datalist datalist, DatalistItem dataListItem)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var datalistItemId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItems>() 
                                  where dl.DataListsId == new Guid(datalist.Id) && dl.DataListsItemKey == dataListItem.Key 
                                  select dl.DataListsItemId).FirstOrDefault();

            if (datalistItemId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
            {
                return null;
            }
            else
            {
                return datalistItemId.ToString();
            }
        }

        public List<Object> GetDataListItemLanguages(string datalistItemId)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var localList = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsLanguages>() 
                                         where dl.DataListsItemId == new Guid(datalistItemId) 
                                         select new { dl.LocalId }).ToList<Object>();

            return localList;
        }

        public static List<DataListsItems> GetMessageTypeDatalistItems(string datalistId)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var itemList = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItems>()
                             where dl.DataListsId == new Guid(datalistId)
                             select new { dl.DataListsItemId, dl.DataListsItemKey }).ToList();

            List<DataListsItems> datalistItems = new List<DataListsItems>();
            foreach (var item in itemList)
            {
                DataListsItems datalistItem = new DataListsItems();
                datalistItem.DataListsItemId = item.DataListsItemId;
                datalistItem.DataListsItemKey = item.DataListsItemKey;
                datalistItems.Add(datalistItem);
            }

            return datalistItems;
        }

        public bool DoesLinkExists(string datalistId, string childDatalistItemKey, string parentDatalistItemKey)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var parentDatalistItemId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItems>() 
                                        where dl.DataListsId == new Guid(datalistId) && dl.DataListsItemKey == parentDatalistItemKey 
                                        select dl.DataListsItemId).FirstOrDefault();

            var childDatalistItemId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItems>() 
                                       where dl.DataListsItemKey == childDatalistItemKey 
                                       select dl.DataListsItemId).FirstOrDefault();

            var datalistLinks = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItemsLinks>() 
                                 where dl.ParentId == parentDatalistItemId && dl.ChildId == childDatalistItemId 
                                 select dl.ChildId).ToList();

            if (datalistLinks.Count != 0)
            {
                return false;
            }

            return true;
        }

        public int GetLinksCount(string datalistId, string parentDatalistItemKey)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();

            var parentDatalistItemId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItems>() 
                                        where dl.DataListsId == new Guid(datalistId) && dl.DataListsItemKey == parentDatalistItemKey 
                                        select dl.DataListsItemId).FirstOrDefault();

            var datalistLinks = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItemsLinks>() 
                                 where dl.ParentId == parentDatalistItemId 
                                 select dl.ChildId).ToList();

            return datalistLinks.Count;
        }

        public string GetDataListItem(Datalist datalist, DatalistItem dataListItem)
        {
            int retryCount = 0;
            string objDataQuery = string.Format("ReferenceCodes({2})?$filter=ContentID%20eq%20%27{0}%27%20and%20Code%20eq%20%27{1}%27&$expand=Children,Attributes", datalist.ContentId, dataListItem.Key, this.MainForm.TenantId);
            string baseUrl = this.MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
            while (!response.IsSuccessStatusCode && retryCount < 9999)
            {
                response = client.GetAsync(objDataQuery).Result;
                retryCount++;
            }

            if (response.IsSuccessStatusCode)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = 99999999;
                Dictionary<string, object> parameters = serializer.Deserialize<Dictionary<string, object>>(response.Content.ReadAsStringAsync().Result);

                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    if (parameter.Value is IList)
                    {
                        IList dataListItems = (IList)parameter.Value;

                        foreach (Dictionary<string, object> dataListItemDetail in dataListItems)
                        {
                            dataListItem.Id = dataListItemDetail["ID"].ToString();
                            dataListItem.ContentId = dataListItemDetail["ContentID"].ToString();
                            dataListItem.Key = dataListItemDetail["Code"].ToString();
                            dataListItem.TenantId = dataListItemDetail["TenantID"].ToString();
                            dataListItem.OrderIndex = (int)dataListItemDetail["OrderIndex"];
                            dataListItem.EffectiveDate = Convert.ToDateTime(dataListItemDetail["EffectiveStartDate"].ToString());
                            dataListItem.EndDate = Convert.ToDateTime(dataListItemDetail["EffectiveEndDate"].ToString());
                            dataListItem.IsActive = (bool)dataListItemDetail["IsActive"];
                            dataListItem.DataListId = datalist.Id;
                            dataListItem.DataListItemLanguages.Clear();
                            dataListItem.DataListItemLinks.Clear();
                            dataListItem.DataListItemAttributeValues.Clear();

                            if (dataListItemDetail.ContainsKey("LanguageList"))
                            {
                                object languageList = dataListItemDetail["LanguageList"] as object;
                                foreach (Dictionary<string, object> language in (IList)languageList)
                                {
                                    DatalistItemLanguage datalistItemLanguage = new DatalistItemLanguage();
                                    datalistItemLanguage.DataListItemId = dataListItem.Id;
                                    datalistItemLanguage.Description = language["Description"].ToString();
                                    datalistItemLanguage.IsActive = (bool)language["IsActive"];
                                    datalistItemLanguage.Locale = language["LocaleID"].ToString();
                                    dataListItem.DataListItemLanguages.Add(datalistItemLanguage);
                                }
                            }

                            if (dataListItemDetail.ContainsKey("Children"))
                            {
                                object linkList = dataListItemDetail["Children"] as object;
                                foreach (Dictionary<string, object> link in (IList)linkList)
                                {
                                    DatalistItemLink datalistItemLink = new DatalistItemLink();
                                    datalistItemLink.DataListItemId = dataListItem.Id;
                                    datalistItemLink.ParentId = dataListItem.Id;
                                    datalistItemLink.Active = (bool)link["IsActive"];
                                    datalistItemLink.ChildId = link["ID"].ToString();
                                    datalistItemLink.Code = link["Code"].ToString();
                                    datalistItemLink.Link = link["ContentID"].ToString();
                                    dataListItem.DataListItemLinks.Add(datalistItemLink);
                                }
                            }

                            if (dataListItemDetail.ContainsKey("Attributes"))
                            {
                                object attributeList = dataListItemDetail["Attributes"] as object;
                                foreach (Dictionary<string, object> attributes in (IList)attributeList)
                                {
                                    DatalistItemAttributeValue datalistItemAttributeValue = new DatalistItemAttributeValue();
                                    datalistItemAttributeValue.DataListsAttributeValueId = attributes["ID"].ToString();
                                    datalistItemAttributeValue.DataListsItemId = attributes["DataListItemID"].ToString();
                                    datalistItemAttributeValue.DataListAttributeId = attributes["DataListAttributeID"].ToString();
                                    datalistItemAttributeValue.DataListAttributeText = attributes["DataListAttributeName"].ToString();
                                    datalistItemAttributeValue.DataListsItemValueId = attributes["DataListValueID"].ToString();
                                    datalistItemAttributeValue.DataListsItemValueText = attributes["DataListAttributeValue"].ToString();
                                    dataListItem.DataListItemAttributeValues.Add(datalistItemAttributeValue);
                                }
                            }

                            return dataListItem.Id;
                        }
                    }
                }
            }

            return null;
        }

        public bool UpdateDataListItem(DatalistItem dataListItem)
        {
            DataListsService.DataListItemUpdated response = null;

            DataListsService.UpdateDataListItemCommand command = new DataListsService.UpdateDataListItemCommand()
            {
                UpdateDataListItem = new DataListsService.UpdateDataListItem()
                {
                    DataListItemId = dataListItem.Id,
                    DataListId = dataListItem.DataListId,
                    EffectiveDate = Convert.ToDateTime("1899-12-31"),
                    EndDate = Convert.ToDateTime("9999-12-31"),
                    Key = dataListItem.Key,
                    ItemIsActive = true,
                    OrderIndex = 0,
                    DataListItemLanguages = this.SetDataListItemLanguages(dataListItem.DataListItemLanguages),
                    DataListItemLinks = this.SetDataListItemLinks(dataListItem.DataListItemLinks),
                    DataListAttributeValues = this.SetDataListItemAttributeValues(dataListItem.DataListItemAttributeValues)
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
                response = this.clientLicense.UpdateDataListItem(command);
            }
            catch(Exception ex)
            {
                log.Error("ERROR UpdateDataListItem ContentId=" + dataListItem.ContentId, ex);
                log.Error("StackTrace=" + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    log.Error("Inner Exception Message=" + ex.InnerException);
                }
                log.Error("DataListItem=" + dataListItem.ToString());
                return false;
            }

            if (response.DataListItemId != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddDataListItem(DatalistItem dataListItem)
        {
            DataListsService.DataListItemAdded response = null;

            DataListsService.AddDataListItemCommand command = new DataListsService.AddDataListItemCommand()
            {
                AddDataListItem = new DataListsService.AddDataListItem()
                {
                    DataListItemId = dataListItem.Id,
                    DataListId = dataListItem.DataListId,
                    Key = dataListItem.Key,
                    OrderIndex = dataListItem.OrderIndex,
                    ItemIsActive = true,
                    EffectiveDate = Convert.ToDateTime("1899-12-31"),
                    EndDate = Convert.ToDateTime("9999-12-31"),
                    DataListItemLanguages = this.SetDataListItemLanguages(dataListItem.DataListItemLanguages),
                    DataListItemLinks = this.SetDataListItemLinks(dataListItem.DataListItemLinks),
                    DataListAttributeValues = this.SetDataListItemAttributeValues(dataListItem.DataListItemAttributeValues)
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
                response = this.clientLicense.AddDataListItem(command);
            }

            catch(Exception ex)
            {
                log.Error("ERROR AddDataListItem ContentId=" + dataListItem.ContentId, ex);
                log.Error("StackTrace=" + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    log.Error("Inner Exception Message=" + ex.InnerException);
                }
                log.Error("DataListItem=" + dataListItem.ToString());
                return false;                
            }

            if (response.DataListItemId != null)
            {
                dataListItem.Id = response.DataListItemId;
                return true;
            }
            else
            {
                return false;
            }
        }

        private DataListsService.DataListItemLanguage[] SetDataListItemLanguages(List<DatalistItemLanguage> langList)
        {
            if (langList != null)
            {
                DataListsService.DataListItemLanguage[] result = new DataListsService.DataListItemLanguage[] { };
                List<DataListsService.DataListItemLanguage> result2 = new List<DataListsService.DataListItemLanguage>();
                DataListsService.DataListItemLanguage lang = null;

                foreach (DatalistItemLanguage itemLang in langList)
                {
                    lang = new DataListsService.DataListItemLanguage()
                    {
                        DataListItemId = itemLang.DataListItemId,
                        Description = itemLang.Description,
                        LongDescription = itemLang.LongDescription,
                        IsActive = itemLang.IsActive,
                        Local = itemLang.Locale
                    };

                    result2.Add(lang);
                }

                result = result2.ToArray();
                return result;
            }
            else
            {
                return null;
            }
        }

        private DataListsService.DataListItemLink[] SetDataListItemLinks(List<DatalistItemLink> linkList)
        {
            if (linkList != null)
            {
                DataListsService.DataListItemLink[] result = new DataListsService.DataListItemLink[] { };
                List<DataListsService.DataListItemLink> result2 = new List<DataListsService.DataListItemLink>();
                DataListsService.DataListItemLink link = null;

                foreach (DatalistItemLink itemLink in linkList)
                {
                    link = new DataListsService.DataListItemLink()
                    {
                        ChildId = itemLink.ChildId,
                        IsActive = itemLink.Active,
                        ParentId = itemLink.ParentId
                    };

                    result2.Add(link);
                }

                result = result2.ToArray();
                return result;
            }
            else
            {
                return null;
            }
        }

        private DataListsService.DataListAttributeValue[] SetDataListItemAttributeValues(List<DatalistItemAttributeValue> attributeList)
        {
            if (attributeList != null)
            {
                DataListsService.DataListAttributeValue[] result = new DataListsService.DataListAttributeValue[] { };
                List<DataListsService.DataListAttributeValue> result2 = new List<DataListsService.DataListAttributeValue>();
                DataListsService.DataListAttributeValue attribute = null;

                foreach (DatalistItemAttributeValue itemAttr in attributeList)
                {
                    attribute = new DataListsService.DataListAttributeValue()
                    {
                        DataListsItemValueId = itemAttr.DataListsItemValueId,
                        DataListsItemId = itemAttr.DataListsItemId,
                        DataListAttributeId = itemAttr.DataListAttributeId,
                        DataListsAttributeValueId = itemAttr.DataListsAttributeValueId
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

        public void RefreshCache(string cacheKey, string clearAllCodeTableCacheFlag = "false", string reloadCache = "true", string reloadAllCodeTableCache = "false")
        {
            string objDataQuery = string.Empty;

            objDataQuery = string.Format("CacheRefresh(CacheKey='{0}',ClearAllCodeTableCache={1},ReloadCache={2},ReloadAllCodeTableCache={3}, TenantID={4})", cacheKey, clearAllCodeTableCacheFlag, reloadCache, reloadAllCodeTableCache, this.MainForm.TenantId);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
        }
       
        public override string ToString()
        {           
            String returnString = string.Concat("DataListId=[", this.DataListId, "]\n",
                "Id=[", this.Id, "]\n",
                "Key=[" , this.Key, "]\n",
                "ContentId=[", this.ContentId, "]\n",
                "OrderIndex=[", this.OrderIndex, "]\n",
                "IsActive=[", this.IsActive, "]\n",
                "EffectiveDate=[", this.EffectiveDate, "]\n",
                "EndDate=[", this.EndDate, "]\n",
                "IdentifierId=[", this.IdentifierId, "]\n",
                "TenantId=[", this.TenantId, "]\n",
                "DataListId=[", this.DataListId, "]\n");

            if(this.DataListItemLinks != null)
            {
                foreach (DatalistItemLink datalistItemLink in this.DataListItemLinks)
                {
                    String returnLinkString = string.Concat("LINK\n", "ChilId=[", datalistItemLink.ChildId, "]\n",
                        "ParentId=[", datalistItemLink.ParentId, "]\n",
                        "Link=[", datalistItemLink.Link, "]\n",
                        "Description=[", datalistItemLink.Description, "]\n",
                        "Code=[", datalistItemLink.Code, "]\n",
                        "Active=[", datalistItemLink.Active, "]\n",
                        "LastModified=[", datalistItemLink.LastModified, "]\n",
                        "DataListItemId=[", datalistItemLink.DataListItemId, "]\n");
                    returnString += returnLinkString;
                }
            }

            if (this.DataListItemLanguages != null)
            {
                foreach (DatalistItemLanguage datalistItemLanguage in this.DataListItemLanguages)
                {
                    String returnLangString = string.Concat("LANGUAGE\n", "Locale=[", datalistItemLanguage.Locale, "]\n",
                        "Description=[", datalistItemLanguage.Description, "]\n",
                        "IsActive=[", datalistItemLanguage.IsActive, "]\n",
                        "Description=[", datalistItemLanguage.Description, "]\n",
                        "DataListItemId=[", datalistItemLanguage.DataListItemId, "]\n",
                        "LongDescription=[", datalistItemLanguage.LongDescription, "]\n");
                    returnString += returnLangString;
                }
            }  

            if(this.DataListItemAttributeValues != null)
            {
                foreach (DatalistItemAttributeValue datalistItemAttributeValue in this.DataListItemAttributeValues)
                {
                    String returnAttributeString = string.Concat("ATTRIBUTE\n", "DataListsAttributeValueId=[", datalistItemAttributeValue.DataListsAttributeValueId, "]\n",
                        "DataListsItemId=[", datalistItemAttributeValue.DataListsItemId, "]\n",
                        "DataListAttributeId=[", datalistItemAttributeValue.DataListAttributeId, "]\n" +
                        "DataListAttributeText=[", datalistItemAttributeValue.DataListAttributeText, "]\n",
                        "DataListsItemValueId=[", datalistItemAttributeValue.DataListsItemValueId, "]\n",
                        "DataListsItemValueText=[", datalistItemAttributeValue.DataListsItemValueText, "]\n");
                    returnString += returnAttributeString;
                }
            }

            return returnString;
        }
    }
}