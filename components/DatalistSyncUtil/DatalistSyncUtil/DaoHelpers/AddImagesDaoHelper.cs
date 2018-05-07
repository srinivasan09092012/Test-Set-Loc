//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddImagesDaoHelper
    {
        public AddImagesDaoHelper(ImageDbContext context)
        {
            this.Context = context;
        }

        public ImageDbContext Context { get; set; }

        public bool ExecuteProcedure(ImagesMainModel cmd)
        {
            Guid imageID = Guid.NewGuid();

            if (cmd.ImageId != Guid.Empty)
            {
                imageID = cmd.ImageId;
            }

            this.Context.Image.Add(new Image()
            {
                ContentId = cmd.ContentId,
                Description = cmd.Description,
                ImageId = imageID,
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
