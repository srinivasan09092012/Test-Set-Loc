//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddServicesDaoHelper
    {
        public AddServicesDaoHelper(ServiceDbContext context)
        {
            this.Context = context;
        }

        public ServiceDbContext Context { get; set; }

        public bool ExecuteProcedure(ServicesMainModel cmd)
        {
            Guid serviceID = Guid.NewGuid();

            this.Context.Service.Add(new Service()
            {
                Name = cmd.Name,
                SecurityRightItemKey = cmd.SecurityRightItemContentID,
                LabelItemKey = cmd.LabelItemKey,
                ServiceID = serviceID,
                DefaultText = cmd.DefaultText,
                BaseUrl = cmd.BaseURL,
                IocContainer = cmd.IOCContainer,
                IsActive = cmd.IsActive,
                LastModifiedTimeStamp = cmd.LastModifiedDate,
                OperatorID = cmd.OperatorID,
                TenantModuleID = cmd.TenantModuleID
            });

            this.Context.SaveChanges();

            return true;
        }
    }
}
