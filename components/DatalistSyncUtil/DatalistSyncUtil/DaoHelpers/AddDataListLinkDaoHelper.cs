//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddDataListLinkDaoHelper
    {
        public AddDataListLinkDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(DataListItemLink cmd)
        {
            Guid datalistItemID = Guid.NewGuid();

            if (cmd.DataListID != Guid.Empty)
            {
                datalistItemID = cmd.DataListID;
            }

            this.Context.DataListItemLinks.Add(new DataListItemLinkModel()
            {
                ParentID = cmd.ParentID,
                ChildID = cmd.ChildID,
                IsActive = cmd.IsActive
            });

            this.Context.SaveChanges();

            return true;
        }
    }
}
