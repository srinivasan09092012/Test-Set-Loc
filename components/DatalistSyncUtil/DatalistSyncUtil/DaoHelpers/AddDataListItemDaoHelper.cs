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
    /// <summary>
    /// AddDataListsItemDaoHelper
    /// </summary>
    public class AddDataListItemDaoHelper
    {
        public AddDataListItemDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(CodeItemModel cmd)
        {
            Guid datalistItemID = Guid.NewGuid();

            if (cmd.ID != Guid.Empty)
            {
                datalistItemID = cmd.ID;
            }
            
            this.Context.DataListItems.Add(new DataListItemModel()
            {
                ID = datalistItemID,
                DataListID = cmd.DatalistID,
                DataListItemName = cmd.Code,
                EffectiveDate = cmd.EffectiveStartDate.Value,
                EndDate = cmd.EffectiveEndDate.Value,
                IsActive = cmd.IsActive,
                IsEditable = cmd.IsEditable,
                OrderIndex = cmd.OrderIndex.Value,
                LastModified = DateTime.UtcNow,
            });

            this.Context.SaveChanges();

            if (cmd.LanguageList != null)
            {
                foreach (ItemLanguage datalistLanguage in cmd.LanguageList)
                {
                    this.Context.DataListItemLanguages.Add(new DataListItemLanguage()
                    {
                        DataListItemID = datalistItemID,
                        Description = datalistLanguage.Description,
                        LongDescription = datalistLanguage.LongDescription,
                        IsActive = true,
                        LastModified = DateTime.UtcNow,
                        LocaleID = datalistLanguage.LocaleID
                    });
                }
            }

            this.Context.SaveChanges();

            return true;
        }
    }
}
