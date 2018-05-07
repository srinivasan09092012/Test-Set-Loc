//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddImageLanguageDaoHelper
    {
        public AddImageLanguageDaoHelper(ImageDbContext context)
        {
            this.Context = context;
        }

        public ImageDbContext Context { get; set; }

        public bool ExecuteProcedure(ImageLanguage cmd)
        {
            if (cmd != null)
            {
                this.Context.ImageLanguages.Add(new ImageLanguages()
                {
                    ImageId = cmd.ImageId,
                    LocalId = cmd.LocaleId,
                    Source = cmd.Source,
                    Height = cmd.Height,
                    Width = cmd.Width,
                    ToolTip = cmd.ToolTip,
                    IsActive = cmd.IsActive,
                    OperatorId = cmd.OperatorId,
                    LastModifiedTS = cmd.LastModifiedTS
                });
            }

            this.Context.SaveChanges();

            return true;
        }
    }
}
