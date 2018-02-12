//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddHtmlBlockDaoHelper
    {
        public AddHtmlBlockDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(HtmlBlockMainModel cmd)
        {
            Guid htmlBlockId = Guid.NewGuid();

            if (cmd.ID != Guid.Empty)
            {
                htmlBlockId = cmd.ID;
            }

            this.Context.HtmlBlock.Add(new HtmlBlock() 
            {
                ContentId = cmd.ContentId,
                Description = cmd.Description,
                HtmlBlockId = htmlBlockId,
                IsActive = cmd.IsActive,
                LastModifiedTS = cmd.LastModifiedTS,
                OperatorId = cmd.OperatorId,
                TenantModuleId = cmd.TenantModuleId
            });
        
            this.Context.SaveChanges();

            return true;
        }
    }
}
