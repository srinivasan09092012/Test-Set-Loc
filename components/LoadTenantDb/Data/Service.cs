//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HP.HSP.UA3.Utilities.LoadTenantDb.Forms;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using WebRefServiceMaint = HP.HSP.UA3.Utilities.LoadTenantDb.ServiceService;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class Service
    {
        private WebRefServiceMaint.ServiceServiceClient clientLicense = new WebRefServiceMaint.ServiceServiceClient();

        public Service()
            : base()
        {
        }

        public MainForm MainForm { get; set; }

        public Guid ServiceId { get; set; }

        public Guid TenantModuleId { get; set; }

        public string Name { get; set; }

        public Guid SecurityRightItemId { get; set; }

        public string LabelItemContentId { get; set; }

        public string DefaultText { get; set; }

        public string IocContainer { get; set; }

        public string BaseUrl { get; set; }

        private IDbSession session;

        public ConnectionStringSettings ConnectionStringSettings { get; set; }

        public string GetServiceId(Service service)
        {
            this.ConnectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            this.session = new DbSession(this.ConnectionStringSettings.ProviderName, this.ConnectionStringSettings.ConnectionString);

            ServiceDbContext serviceDbContext = new ServiceDbContext(this.session);

            var serviceId = (from s in serviceDbContext.Set<HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities.Service>() where s.ServiceID == service.ServiceId select s.ServiceID).FirstOrDefault();

            if (serviceId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
            {
                return null;
            }
            else
            {
                return serviceId.ToString();
            }
        }

        public bool AddService(Service service)
        {
            ServiceService.ServiceAdded response = null;

            ServiceService.AddServiceCommand command = new ServiceService.AddServiceCommand()
            {
                AddService = new ServiceService.AddService()
                {
                    ServiceId = service.ServiceId.ToString(),
                    TenantModuleId = service.TenantModuleId.ToString(),
                    SecurityRightItemId = service.SecurityRightItemId.ToString(),
                    Name = service.Name,
                    DefaultText = service.DefaultText,
                    IocContainer = service.IocContainer,
                    LabelItemKey = service.LabelItemContentId,
                    BaseUrl = service.BaseUrl,
                    IsActive = true
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
                response = this.clientLicense.AddService(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheFullServiceTableKey);
            }
            catch
            {
                return false;
            }

            if (response.ServiceId != null)
            {
                service.ServiceId = Guid.Parse(response.ServiceId);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateService(Service service)
        {
            ServiceService.ServiceUpdated response = null;

            ServiceService.UpdateServiceCommand command = new ServiceService.UpdateServiceCommand()
            {
                UpdateService = new ServiceService.UpdateService()
                {
                    ServiceId = service.ServiceId.ToString(),
                    TenantModuleId = service.TenantModuleId.ToString(),
                    SecurityRightItemId = service.SecurityRightItemId.ToString(),
                    Name = service.Name,
                    DefaultText = service.DefaultText,
                    IocContainer = service.IocContainer,
                    LabelItemKey = service.LabelItemContentId,
                    BaseUrl = service.BaseUrl,
                    IsActive = true
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
                response = this.clientLicense.UpdateService(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheFullServiceTableKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (response.ServiceId != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RefreshCache(string cacheKey, string reloadCache = "true", string cacheType = "Service")
        {
            string objDataQuery = string.Format("CacheRefreshTable(CacheKey='{0}',ReloadCache={1},CacheType='{2}')", cacheKey, reloadCache, cacheType);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
        }

        public override string ToString()
        {
            String returnString = string.Concat("ServiceId=[", this.ServiceId, "]\n",
                "TenantModuleId=[", this.TenantModuleId, "]\n",
                "Name=[", this.Name, "]\n",
                "SecurityRightItemId=[", this.SecurityRightItemId, "]\n",
                "LabelItemContentId=[", this.LabelItemContentId, "]\n",
                "DefaultText=[", this.DefaultText, "]\n",
                "IocContainer=[", this.IocContainer, "]\n",
                "BaseUrl=[", this.BaseUrl, "]\n");

            return returnString;
        }
    }
}