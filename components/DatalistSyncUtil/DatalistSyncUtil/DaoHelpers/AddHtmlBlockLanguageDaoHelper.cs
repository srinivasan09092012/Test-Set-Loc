//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddHtmlBlockLanguageDaoHelper
    {
        public AddHtmlBlockLanguageDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(HtmlBlockLanguage cmd)
        {
            if (cmd != null)
            {
                this.Context.HtmlBlockLanguages.Add(new HtmlBlockLanguages()
                {
                    HtmlBlockId = cmd.HtmlBlockId,
                    LocaleId = cmd.LocaleId,
                    Html = cmd.Html,
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
