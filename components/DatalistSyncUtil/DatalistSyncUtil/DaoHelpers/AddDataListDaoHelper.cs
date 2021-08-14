//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Collections.Generic;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddDataListDaoHelper
    {
        public AddDataListDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(DataListMainModel cmd)
        {
            Guid datalistID = Guid.NewGuid();

            if (cmd.ID != Guid.Empty)
            {
                datalistID = cmd.ID;
            }

            this.Context.DataLists.Add(new DataListModel()
            {
                ContentID = cmd.ContentID,
                DataListsName = cmd.Description,
                Description = cmd.Description,
                ID = datalistID,
                IsActive = cmd.IsActive,
                IsEditable = cmd.IsEditable,
                ReleaseStatus = cmd.ReleaseStatus,
                TenantID = cmd.TenantID,
                TenantModuleID = cmd.TenantModuleID
            });

            this.Context.SaveChanges();

            return true;
        }
    }
}
