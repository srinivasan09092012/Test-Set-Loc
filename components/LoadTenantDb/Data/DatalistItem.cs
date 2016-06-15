﻿using HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities;

//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Utilities.LoadTenantDb.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WebRefDataListsMaint = HP.HSP.UA3.Utilities.LoadTenantDb.DataListsService;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class DatalistItem
    {
        private WebRefDataListsMaint.DataListsServiceClient clientLicense = new WebRefDataListsMaint.DataListsServiceClient();

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

            var datalistItemId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItems>() where dl.DataListsId == new Guid(datalist.Id) && dl.DataListsItemKey == dataListItem.Key select dl.DataListsItemId).FirstOrDefault();

            if (datalistItemId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
            {
                return null;
            }
            else
            {
                return datalistItemId.ToString();
            }
        }

        public void GetDataListItemTest(Datalist datalist, DatalistItem dataListItem)
        {
            DataListDBContext dataListDBContext = new DataListDBContext();
            //Dictionary<string, object> dataListItemDetail;

            var datalistItemId = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListsItems>() where dl.DataListsId == new Guid(datalist.Id) && dl.DataListsItemKey == dataListItem.Key select dl.DataListsItemId).FirstOrDefault();

            var datalistAttributes = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListAttributes>() where dl.DataListsId == new Guid(datalist.Id) select new { dl.TypeName, dl.DataListsAttributeId }).ToList();

            var datalistAttributeValues = (from dl in dataListDBContext.Set<HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities.DataListAttributeValues>() where dl.DataListsItemId == new Guid(dataListItem.Id) select dl.DataListAttributeId).ToList();

            //if (dataListItemDetail.ContainsKey("Attributes"))
            //{
            //    object attributeList = dataListItemDetail["Attributes"] as object;
            //    foreach (Dictionary<string, object> attributes in (IList)attributeList)
            //    {
            //        DatalistItemAttributeValue datalistItemAttributeValue = new DatalistItemAttributeValue();
            //        datalistItemAttributeValue.DataListsAttributeValueId = attributes["ID"].ToString();
            //        datalistItemAttributeValue.DataListsItemId = attributes["DataListItemID"].ToString();
            //        datalistItemAttributeValue.DataListAttributeId = attributes["DataListAttributeID"].ToString();
            //        datalistItemAttributeValue.DataListAttributeText = attributes["DataListAttributeName"].ToString();
            //        datalistItemAttributeValue.DataListsItemValueId = attributes["DataListValueID"].ToString();
            //        datalistItemAttributeValue.DataListsItemValueText = attributes["DataListAttributeValue"].ToString();
            //        dataListItem.DataListItemAttributeValues.Add(datalistItemAttributeValue);
            //    }
            //}

            foreach (Guid attribute in datalistAttributeValues)
            {
                //DatalistItemAttributeValue datalistItemAttributeValue = new DatalistItemAttributeValue();
                //datalistItemAttributeValue.DataListAttributeText = attribute;
                //dataListItem.DataListItemAttributeValues.Add(datalistItemAttributeValue);
            }
        }

        public string GetDataListItem(Datalist datalist, DatalistItem dataListItem)
        {
            int retryCount = 0;
            string objDataQuery = string.Format("ReferenceCodes?$filter=ContentID%20eq%20%27{0}%27%20and%20Code%20eq%20%27{1}%27&$expand=Children,Attributes", datalist.ContentId, dataListItem.Key);
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
                    EffectiveDate = dataListItem.EffectiveDate,
                    EndDate = dataListItem.EndDate,
                    Key = dataListItem.Key,
                    ItemIsActive = dataListItem.IsActive,
                    OrderIndex = dataListItem.OrderIndex,
                    ItemLastModified = DateTime.Now,
                    DataListItemLanguages = this.SetDataListItemLanguages(dataListItem.DataListItemLanguages),
                    DataListItemLinks = this.SetDataListItemLinks(dataListItem.DataListItemLinks),
                    DataListAttributeValues = this.SetDataListItemAttributeValues(dataListItem.DataListItemAttributeValues)
                },
                Requestor = new DataListsService.RequestorModel()
                {
                    IdentifierId = dataListItem.IdentifierId,
                    IdentifierIdType = HP.HSP.UA3.Utilities.LoadTenantDb.DataListsService.CoreEnumerationsMessagingIdentifierIdType.User,
                    TenantId = dataListItem.TenantId
                }
            };

            try
            {
                response = this.clientLicense.UpdateDataListItem(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListItemAttrKey, "false", "false", "false");
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheItemLinkerKey, "false", "false", "false");
                this.RefreshCache(string.Empty, "false", "false", "true");
            }
            catch
            {
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

        public DatalistItem AddDataListItem(DatalistItem dataListItem)
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
                Requestor = new DataListsService.RequestorModel()
                {
                    IdentifierId = dataListItem.IdentifierId,
                    IdentifierIdType = HP.HSP.UA3.Utilities.LoadTenantDb.DataListsService.CoreEnumerationsMessagingIdentifierIdType.User,
                    TenantId = dataListItem.TenantId
                }
            };

            try
            {
                response = this.clientLicense.AddDataListItem(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListItemAttrKey, "false", "false", "false");
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheItemLinkerKey, "false", "false", "false");
                this.RefreshCache(string.Empty, "false", "false", "true");
            }
            catch
            {
                return null;
            }

            if (response.DataListItemId != null)
            {
                dataListItem.Id = response.DataListItemId;
                return dataListItem;
            }
            else
            {
                return null;
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

        private void RefreshCache(string cacheKey, string clearAllCodeTableCacheFlag = "false", string reloadCache = "true", string reloadAllCodeTableCache = "false")
        {
            string objDataQuery = string.Empty;

            objDataQuery = string.Format("CacheRefresh(CacheKey='{0}',ClearAllCodeTableCache={1},ReloadCache={2},ReloadAllCodeTableCache={3})", cacheKey, clearAllCodeTableCacheFlag, reloadCache, reloadAllCodeTableCache);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
        }
    }
}