//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
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
    public class UpdateDataListItemLanguageDaoHelper
    {
        public UpdateDataListItemLanguageDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(ItemLanguage cmd)
        {
            DataListItemLanguage languageUpdate = new DataListItemLanguage()
            {
                DataListItemID = cmd.ItemID,
                Description = cmd.Description,
                LongDescription = cmd.LongDescription,
                IsActive = true,
                LastModified = DateTime.UtcNow,
                LocaleID = cmd.LocaleID
            };

            this.Context.Entry(languageUpdate).State = EntityState.Modified;
            this.Context.SaveChanges();

            return true;
        }
    }
}