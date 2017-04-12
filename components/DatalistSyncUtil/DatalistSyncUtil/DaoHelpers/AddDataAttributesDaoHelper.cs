//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddDataAttributesDaoHelper
    {
        public AddDataAttributesDaoHelper(DataListAttributeDbContext context)
        {
            this.Context = context;
        }

        public DataListAttributeDbContext Context { get; set; }

        public bool ExecuteProcedure(ItemAttribute cmd)
        {
            Guid datalistItemID = Guid.NewGuid();

            if (cmd.ID != Guid.Empty)
            {
                datalistItemID = cmd.ID;
            }

            this.Context.DataListAttributes.Add(new DataListAttribute()
            {
                ID = datalistItemID,
                DataListID = cmd.DataListID,
                TypeName = cmd.ContentID,
                DataListTypeID = cmd.DataListTypeID,
                DefaultTypeValue = cmd.DefaultTypeValue,
                DefaultTypeText = cmd.Code,
                IsActive = cmd.IsActive,
                LastModifiedDate = DateTime.UtcNow,
            });

            this.Context.SaveChanges();

            return true;
        }
    }
}