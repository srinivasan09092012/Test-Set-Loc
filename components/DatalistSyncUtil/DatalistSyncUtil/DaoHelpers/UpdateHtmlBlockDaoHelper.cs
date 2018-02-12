//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateHtmlBlockDaoHelper
    {
        public UpdateHtmlBlockDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(HtmlBlockMainModel cmd)
        {
            HtmlBlock htmlBlockUpdated = new HtmlBlock()
            {
                ContentId = cmd.ContentId,
                Description = cmd.Description,
                HtmlBlockId = cmd.ID,
                IsActive = cmd.IsActive,
                LastModifiedTS = cmd.LastModifiedTS,
                OperatorId = cmd.OperatorId,
                TenantModuleId = cmd.TenantModuleId
            };

            this.Context.Entry(htmlBlockUpdated).State = EntityState.Modified;

            this.Context.SaveChanges();

            return true;
        }
    }
}
