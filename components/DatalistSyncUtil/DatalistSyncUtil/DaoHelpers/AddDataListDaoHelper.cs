//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
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
            Guid newGuidDatalist = Guid.NewGuid();
            List<string> attributes = new List<string>();

            this.Context.DataLists.Add(new DataListModel()
            {
                ContentID = cmd.ContentID,
                DataListsName = cmd.Description,
                Description = cmd.Description,
                ID = newGuidDatalist,
                IsActive = cmd.IsActive,
                IsEditable = cmd.IsEditable,
                ReleaseStatus = cmd.ReleaseStatus,
                TenantID = cmd.TenantID,
                TenantModuleID = cmd.TenantModuleID
            });

            ////this.Context.SaveChanges();

            return true;
        }
    }
}