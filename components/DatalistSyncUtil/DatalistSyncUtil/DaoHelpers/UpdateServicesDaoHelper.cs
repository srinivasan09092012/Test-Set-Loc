//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateServicesDaoHelper
    {
        public UpdateServicesDaoHelper(ServiceDbContext context)
        {
            this.Context = context;
        }

        public ServiceDbContext Context { get; set; }

        public bool ExecuteProcedure(ServicesMainModel cmd)
        {
            Service serviceUpdated = new Service()
            {
                Name = cmd.Name,
                SecurityRightItemKey = cmd.SecurityRightItemContentID,
                LabelItemKey = cmd.LabelItemKey,
                ServiceID = cmd.ServiceID,
                DefaultText = cmd.DefaultText,
                BaseUrl = cmd.BaseURL,
                IocContainer = cmd.IOCContainer,
                IsActive = cmd.IsActive,
                LastModifiedTimeStamp = cmd.LastModifiedDate,
                OperatorID = cmd.OperatorID,
                TenantModuleID = cmd.TenantModuleID
            };

            this.Context.Entry(serviceUpdated).State = EntityState.Modified;

            this.Context.SaveChanges();

            return true;
        }
    }
}
