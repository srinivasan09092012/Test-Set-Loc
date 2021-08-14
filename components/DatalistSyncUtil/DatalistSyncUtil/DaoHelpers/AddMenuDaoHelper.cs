//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddMenuDaoHelper
    {
        public AddMenuDaoHelper(MenuDbContext context)
        {
            this.Context = context;
        }

        public MenuDbContext Context { get;  set; }

        public bool ExecuteProcedure(MenuListModel cmd)
        {
            Guid menuId = Guid.NewGuid();

            ///if (cmd.ID != Guid.Empty)
            ///{
            ///    menuId = cmd.ID;
            ///}

            this.Context.Menu.Add(new Menu()
            {
                MenuID = menuId,
                TenantModuleID = cmd.TenantModuleID,
                Name = cmd.Name,
                SecurityRightItemKey = cmd.SecurityRightItemContentID,
                IsActive = cmd.IsActive,
                DisplaySize = cmd.DisplaySize,
                OperatorID = cmd.OperatorID,
                LastModifiedTimeStamp = DateTime.UtcNow,
            });

            this.Context.SaveChanges();

            return true;
        }
    }
}
