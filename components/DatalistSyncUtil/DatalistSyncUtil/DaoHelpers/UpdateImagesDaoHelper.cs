//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateImagesDaoHelper
    {
        public UpdateImagesDaoHelper(ImageDbContext context)
        {
            this.Context = context;
        }

        public ImageDbContext Context { get; set; }

        public bool ExecuteProcedure(ImagesMainModel cmd)
        {
            Image imageUpdated = new Image()
            {
                ContentId = cmd.ContentId,
                Description = cmd.Description,
                ImageId = cmd.ImageId,
                IsActive = cmd.IsActive,
                LastModifiedTS = cmd.LastModifiedTS,
                OperatorId = cmd.OperatorId,
                TenantModuleId = cmd.TenantModuleId
            };

            this.Context.Entry(imageUpdated).State = EntityState.Modified;

            this.Context.SaveChanges();

            return true;
        }
    }
}
