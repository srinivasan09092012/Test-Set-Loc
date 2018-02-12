//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateMenuDaoHelper
    {
        public UpdateMenuDaoHelper(MenuDbContext context)
        {
            this.Context = context;
        }

        public MenuDbContext Context { get; private set; }

        public bool ExecuteProcedure(MenuListModel cmd)
        {
            MenuListModel updatedMenuList = new MenuListModel()
            {
                ID = cmd.ID,
                TenantModuleID = cmd.TenantModuleID,
                Name = cmd.Name,
                SecurityRightItemContentID = cmd.SecurityRightItemContentID,
                IsActive = cmd.IsActive,
                DisplaySize = cmd.DisplaySize,
                OperatorID = cmd.OperatorID,
                Level = cmd.Level,
                TenantId = cmd.TenantId
            };
            this.Context.Entry(updatedMenuList).State = EntityState.Modified;
            this.Context.SaveChanges();

            return true;
        }
    }
}
