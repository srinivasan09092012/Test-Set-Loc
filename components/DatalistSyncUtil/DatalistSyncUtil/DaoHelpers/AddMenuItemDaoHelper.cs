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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddMenuItemDaoHelper
    {
        public AddMenuItemDaoHelper(MenuDbContext context)
        {
            this.Context = context;
        }

        public MenuDbContext Context { get; private set; }

        public bool ExecuteProcedure(MenuItemModel cmd)
        {
            Guid menuItemID = Guid.NewGuid();

            ///if (cmd.ID != Guid.Empty)
            ///{
            ///    menuItemID = cmd.ID;
            ///}

            this.Context.MenuItem.Add(new MenuItem()
            {
                MenuItemID = menuItemID,
                MenuID = cmd.MenuID,
                ParentMenuItemID = cmd.ParentMenuItemID,
                SecurityRightItemID = cmd.SecurityRightItemID,
                Name = cmd.Name,
                OrderIndex = Convert.ToInt16(cmd.OrderIndex),
                IsVisible = cmd.IsVisible,
                DefaultText = cmd.DefaultText,
                CSSClass = cmd.CSSClass,
                IOCContainer = cmd.IOCContainer,
                LabelItemKey = cmd.LabelItemContentID,
                ModuleSectionItemKey = cmd.ModuleSectionContentID,
                BaseURL = cmd.BaseURL,
                ReportsContentURL = cmd.ReportContentsURL,
                PrintPreviewContentURL = cmd.PrintPreviewContentURL,
                PageHelpHTMLContentID = cmd.PageHelpHTMLContentID,
                MITAHelpHTMLContentID = cmd.MITAHelpHTMLContentID,
                IsActive = cmd.IsActive,
                OperatorID = cmd.OperatorID,
                LastModifiedTimeStamp = cmd.LastModifiedDate,
                AuditContentURL = cmd.AuditContentURL,
                ContactUsContentURL = cmd.ContactUsContentURL,
            });

            this.Context.SaveChanges();

            return true;
        }
    }
}
