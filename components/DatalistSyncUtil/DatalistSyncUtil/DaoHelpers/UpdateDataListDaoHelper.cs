//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    /// <summary>
    /// UpdateDataListsDaoHelper
    /// </summary>
    public class UpdateDataListDaoHelper
    {
        public UpdateDataListDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(DataListMainModel cmd)
        {
            DataListModel dataListUpdated = new DataListModel()
            {
                ContentID = cmd.ContentID,
                DataListsName = cmd.Description,
                Description = cmd.Description,
                ID = cmd.ID,
                IsActive = cmd.IsActive,
                IsEditable = cmd.IsEditable,
                ReleaseStatus = cmd.ReleaseStatus,
                TenantID = cmd.TenantID,
                TenantModuleID = cmd.TenantModuleID
            };

            this.Context.Entry(dataListUpdated).State = EntityState.Modified;

            this.Context.SaveChanges();

            return true;
        }
    }
}
