//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
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
    public class UpdateMenuItemDaoHelper
    {
        public UpdateMenuItemDaoHelper(MenuDbContext context)
        {
            this.Context = context;
        }

        public MenuDbContext Context { get; private set; }

        public bool ExecuteProcedure(MenuItemModel cmd)
        {
            MenuItem updatedMenuItem = new MenuItem()
            {
                MenuItemID  = cmd.ID,
                MenuID = cmd.MenuID,
                ParentMenuItemID = cmd.ParentMenuItemID,
                SecurityRightItemKey = cmd.SecurityRightItemContentID,
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
                AuditContentURL = cmd.AuditContentURL,
                ContactUsContentURL = cmd.ContactUsContentURL,
                LastModifiedTimeStamp = cmd.LastModifiedDate,
            };

            this.Context.Entry(updatedMenuItem).State = EntityState.Modified;
            this.Context.SaveChanges();
            return true;
        }
    }
}
