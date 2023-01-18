//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateDataListAttributesDaoHelper
    {
        public UpdateDataListAttributesDaoHelper(DataListAttributeDbContext context)
        {
            this.Context = context;
        }

        public DataListAttributeDbContext Context { get; set; }

        public bool ExecuteProcedure(ItemAttribute cmd)
        {
            DataListAttribute dataListAttributeUpdated = new DataListAttribute()
            {
                ID = cmd.ID,
                DataListID = cmd.DataListID,
                TypeName = cmd.ContentID,
                //DataListTypeID=,
                //DefaultTypeValue=
                DefaultTypeText = cmd.Code,
                IsActive = cmd.IsActive,
                LastModifiedDate = DateTime.UtcNow,
            };

            this.Context.Entry(dataListAttributeUpdated).State = EntityState.Modified;

            this.Context.SaveChanges();

            return true;
        }
    }
}
