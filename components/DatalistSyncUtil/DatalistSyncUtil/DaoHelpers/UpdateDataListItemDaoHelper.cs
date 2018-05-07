//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace DatalistSyncUtil.DaoHelpers
{
    /// <summary>
    /// UpdateDataListItemDaoHelper
    /// </summary>
    public class UpdateDataListItemDaoHelper
    {
        public UpdateDataListItemDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(CodeItemModel cmd)
        {
            DataListItemModel dataListitemUpdated = new DataListItemModel()
            {
                ID = cmd.ID,
                DataListID = cmd.DatalistID,
                DataListItemName = cmd.Code,
                EffectiveDate = cmd.EffectiveStartDate.Value,
                EndDate = cmd.EffectiveEndDate.Value,
                IsActive = cmd.IsActive,
                IsEditable = cmd.IsEditable,
                OrderIndex = cmd.OrderIndex.Value,
                LastModified = DateTime.UtcNow,
            };

            this.Context.Entry(dataListitemUpdated).State = EntityState.Modified;

            this.Context.SaveChanges();

            return true;
        }
    }
}
