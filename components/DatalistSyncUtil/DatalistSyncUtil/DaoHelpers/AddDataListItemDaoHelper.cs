//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Collections.Generic;

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

        ////public AddDataListItemResults ExecuteProcedure(AddDataListItemCommand cmd)
        ////{
        ////    Guid newGuidDatalistItem = Guid.NewGuid(); 
        ////    Guid newGuidDatalistItemAttributeValue;
        ////    Guid newGuidDatalistItemLanguage;
        ////    Guid newGuidDatalistItemLink;
        ////    List<string> attributeValues = new List<string>();
        ////    List<string> itemLinks = new List<string>();
        ////    List<string> languages = new List<string>();

        ////    foreach (DataListItemLanguage datalistLanguage in cmd.AddDataListItem.DataListItemLanguages)
        ////    {
        ////        //If a guid is passed in the request for the datalist item id it will be used
        ////        //Otherwise a new guid will be created.
        ////        if (cmd.AddDataListItem.DataListItemId != null)
        ////        {
        ////            newGuidDatalistItem = new Guid(cmd.AddDataListItem.DataListItemId);
        ////        }                

        ////        newGuidDatalistItemLanguage = Guid.NewGuid();
        ////        languages.Add(newGuidDatalistItemLanguage.ToString());
        ////        this.Context.DataListsLanguages.Add(new Entities.DataListsLanguages()
        ////        {
        ////            DataListsItemId = newGuidDatalistItem,
        ////            Description = datalistLanguage.Description,
        ////            LongDescription = datalistLanguage.LongDescription,
        ////            IsActive = datalistLanguage.IsActive,
        ////            LastModified = DateTime.UtcNow,
        ////            LocalId = datalistLanguage.Local
        ////        });
        ////    }

        ////    if ((cmd.AddDataListItem.DataListItemLinks != null) && (cmd.AddDataListItem.DataListItemLinks.Count > 0))
        ////    {
        ////        foreach (DataListItemLink datalistLink in cmd.AddDataListItem.DataListItemLinks)
        ////        {
        ////            newGuidDatalistItemLink = Guid.NewGuid();
        ////            languages.Add(newGuidDatalistItemLink.ToString());

        ////            ////If Parent Id is set use it, otherwise use the DataListItemId
        ////            Guid parentId = newGuidDatalistItem;
        ////            if (datalistLink.ParentId != null)
        ////            {
        ////                parentId = new Guid(datalistLink.ParentId);
        ////            }

        ////            this.Context.DataListsItemsLinks.Add(new Entities.DataListsItemsLinks()
        ////            {
        ////                ParentId = parentId, 
        ////                ChildId = new Guid(datalistLink.ChildId),
        ////                IsActive = datalistLink.IsActive,
        ////                LastModified = DateTime.UtcNow
        ////            });
        ////        }
        ////    }

        ////    if ((cmd.AddDataListItem.DataListAttributeValues != null) && (cmd.AddDataListItem.DataListAttributeValues.Count > 0))
        ////    {
        ////        foreach (DataListAttributeValue datalistAttributeValue in cmd.AddDataListItem.DataListAttributeValues)
        ////        {
        ////            newGuidDatalistItemAttributeValue = Guid.NewGuid();
        ////            attributeValues.Add(newGuidDatalistItemAttributeValue.ToString());
        ////            this.Context.DataListsAttributeValues.Add(new Entities.DataListAttributeValues()
        ////            {
        ////                DataListsItemId = newGuidDatalistItem,
        ////                DataListAttributeId = new Guid(datalistAttributeValue.DataListAttributeId),
        ////                DataListsItemValueId = new Guid(datalistAttributeValue.DataListsItemValueId),
        ////                LastModified = DateTime.UtcNow,
        ////                DataListsAttributeValueId = newGuidDatalistItemAttributeValue
        ////            });
        ////        }
        ////    }

        ////    this.Context.DataListsItems.Add(new Entities.DataListsItems()
        ////    {
        ////        DataListsItemId = newGuidDatalistItem,
        ////        DataListsId = new Guid(cmd.AddDataListItem.DataListId),
        ////        DataListsItemKey = cmd.AddDataListItem.Key,
        ////        EffectiveDate = cmd.AddDataListItem.EffectiveDate,
        ////        EndDate = cmd.AddDataListItem.EndDate,
        ////        IsActive = cmd.AddDataListItem.ItemIsActive,
        ////        IsEditable = cmd.AddDataListItem.ItemIsEditable ?? true,
        ////        OrderIndex = cmd.AddDataListItem.OrderIndex,
        ////        LastModified = DateTime.UtcNow,
        ////    });

        ////    this.Context.SaveChanges();

        ////    AddDataListItemResults results = new AddDataListItemResults(
        ////        newGuidDatalistItem.ToString(),
        ////        languages, 
        ////        itemLinks, 
        ////        attributeValues);

        ////    return results;
        ////}
    }
}