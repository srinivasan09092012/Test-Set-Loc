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

namespace DatalistSyncUtil.DaoHelpers
{
    /// <summary>
    /// UpdateDataListsDaoHelper
    /// </summary>
    public class UpdateDataListDaoHelper
    {
        public UpdateDataListDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        ////public Guid ExecuteProcedure(UpdateDataListCommand cmd)
        ////{
        ////    Guid existingGuid = new Guid(cmd.UpdateDataList.Id);

        ////    if (cmd.UpdateDataList.DataListAttributes != null)
        ////    {
        ////        foreach (DataListAttribute datalistAttribute in cmd.UpdateDataList.DataListAttributes)
        ////        {
        ////            Guid attributeGuid;

        ////            if (datalistAttribute.DataListsAttributeId == null)
        ////            {
        ////                attributeGuid = Guid.NewGuid();
        ////            }
        ////            else
        ////            {
        ////                attributeGuid = new Guid(datalistAttribute.DataListsAttributeId);
        ////            }

        ////            Entities.DataListAttributes entityDataListAttributes = new Entities.DataListAttributes()
        ////            {
        ////                DataListsAttributeId = attributeGuid,
        ////                DataListsId = existingGuid,
        ////                TypeName = datalistAttribute.TypeName,
        ////                TypeDataListsId = new Guid(datalistAttribute.TypeDataListId),
        ////                TypeDefaultItemId = new Guid(datalistAttribute.TypeDefaultItemId),
        ////                IsActive = datalistAttribute.IsActive,
        ////                LastModified = DateTime.UtcNow
        ////            };

        ////            // If DataListsAttributeId is empty, do an insert. Otherwise, an update.
        ////            if (datalistAttribute.DataListsAttributeId == null)
        ////            {
        ////                this.Context.Entry(entityDataListAttributes).State = EntityState.Added;
        ////            }
        ////            else
        ////            {
        ////                this.Context.Entry(entityDataListAttributes).State = EntityState.Modified;
        ////            }
        ////        }
        ////    }

        ////    Entities.DataLists dataListUpdated = new Entities.DataLists();
        ////    dataListUpdated.ContentId = cmd.UpdateDataList.ContentId;
        ////    dataListUpdated.DataListsName = cmd.UpdateDataList.DataListsName;
        ////    dataListUpdated.Description = cmd.UpdateDataList.Description;
        ////    dataListUpdated.DataListsId = existingGuid;
        ////    dataListUpdated.IsActive = cmd.UpdateDataList.IsActive;
        ////    dataListUpdated.IsEditable = cmd.UpdateDataList.IsEditable ?? true;
        ////    dataListUpdated.ReleaseStatus = cmd.UpdateDataList.ReleaseStatus;
        ////    dataListUpdated.TenantId = new Guid(cmd.Requestor.TenantId);
        ////    dataListUpdated.TenantModuleId = new Guid(cmd.UpdateDataList.TenantModuleId);
        ////    dataListUpdated.LastModified = DateTime.UtcNow;

        ////    this.Context.Entry(dataListUpdated).State = EntityState.Modified;

        ////    if (string.IsNullOrEmpty(dataListUpdated.ReleaseStatus))
        ////    {
        ////        this.Context.Entry(dataListUpdated).Property(x => x.ReleaseStatus).IsModified = false;
        ////    }
        ////    else
        ////    {
        ////        this.Context.Entry(dataListUpdated).Property(x => x.ReleaseStatus).IsModified = true;
        ////    }

        ////    this.Context.SaveChanges();

        ////    return existingGuid;
        ////}
    }
}