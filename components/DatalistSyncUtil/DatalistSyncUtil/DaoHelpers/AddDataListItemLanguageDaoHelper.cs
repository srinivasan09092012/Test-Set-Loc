//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
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
    public class AddDataListItemLanguageDaoHelper
    {
        public AddDataListItemLanguageDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(ItemLanguage cmd)
        {
            if (cmd != null)
            {
                this.Context.DataListItemLanguages.Add(new DataListItemLanguage()
                {
                    DataListItemID = cmd.ItemID,
                    Description = cmd.Description,
                    LongDescription = cmd.LongDescription,
                    IsActive = true,
                    LastModified = DateTime.UtcNow,
                    LocaleID = cmd.LocaleID
                });
            }

            this.Context.SaveChanges();

            return true;
        }
    }
}
