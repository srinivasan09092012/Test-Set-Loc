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
    public class UpdateDataListItemDaoHelper
    {
        public UpdateDataListItemDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        ////public Guid ExecuteProcedure(CodeItemModel cmd)
        ////{
        ////    Guid existingGuid = new Guid();

        ////    existingGuid = new Guid(cmd.UpdateDataListItem.DataListItemId);

        ////    if (cmd.UpdateDataListItem.DataListItemLanguages != null)
        ////    {
        ////        foreach (DataListItemLanguage datalistLanguage in cmd.UpdateDataListItem.DataListItemLanguages)
        ////        {
        ////            Entities.DataListsLanguages datalistItemLanguage = new Entities.DataListsLanguages()
        ////            {
        ////                DataListsItemId = existingGuid,
        ////                Description = datalistLanguage.Description,
        ////                LongDescription = datalistLanguage.LongDescription,
        ////                IsActive = datalistLanguage.IsActive,
        ////                LastModified = DateTime.UtcNow,
        ////                LocalId = datalistLanguage.Local
        ////            };

        ////            // If DataListItemId is empty, do an insert. Otherwise, an update.
        ////            if (datalistLanguage.DataListItemId == null)
        ////            {
        ////                this.Context.Entry(datalistItemLanguage).State = EntityState.Added;
        ////            }
        ////            else
        ////            {
        ////                this.Context.Entry(datalistItemLanguage).State = EntityState.Modified;
        ////            }
        ////        }
        ////    }

        ////    Entities.DataListsItems dataListitemUpdated = new Entities.DataListsItems()
        ////    {
        ////        DataListsItemId = existingGuid,
        ////        DataListsId = new Guid(cmd.UpdateDataListItem.DataListId),
        ////        DataListsItemKey = cmd.UpdateDataListItem.Key,
        ////        EffectiveDate = cmd.UpdateDataListItem.EffectiveDate,
        ////        EndDate = cmd.UpdateDataListItem.EndDate,
        ////        IsActive = cmd.UpdateDataListItem.ItemIsActive,
        ////        IsEditable = cmd.UpdateDataListItem.ItemIsEditable ?? true,
        ////        OrderIndex = cmd.UpdateDataListItem.OrderIndex,
        ////        LastModified = DateTime.UtcNow,
        ////    };

        ////    this.Context.Entry(dataListitemUpdated).State = EntityState.Modified;

        ////    this.Context.SaveChanges();

        ////    return existingGuid;
        ////}

        ////public void ExecuteProcedure(string dataListId, bool? isEditable)
        ////{
        ////    Guid id = Guid.Parse(dataListId);
        ////    this.Context.DataListsItems.Where(x => x.DataListsId == id).ToList()
        ////        .ForEach(x => x.IsEditable = isEditable ?? true);
        ////    this.Context.SaveChanges();
        ////}       
    }
}