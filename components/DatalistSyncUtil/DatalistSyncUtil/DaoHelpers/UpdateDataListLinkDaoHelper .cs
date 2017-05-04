//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateDataListLinkDaoHelper
    {
        public UpdateDataListLinkDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(DataListItemLink cmd)
        {
            DataListItemLinkModel dataListLinkUpdated = new DataListItemLinkModel()
            {
                ParentID = cmd.ParentID,
                ChildID = cmd.ChildID,
                IsActive = cmd.IsActive,
                LastModified = DateTime.UtcNow,
            };

            this.Context.Entry(dataListLinkUpdated).State = EntityState.Modified;

            this.Context.SaveChanges();

            return true;
        }
    }
}