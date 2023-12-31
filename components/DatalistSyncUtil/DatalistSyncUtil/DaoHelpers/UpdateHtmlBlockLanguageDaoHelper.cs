//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateHtmlBlockLanguageDaoHelper
    {
        public UpdateHtmlBlockLanguageDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context { get; set; }

        public bool ExecuteProcedure(HtmlBlockLanguage cmd)
        {
            HtmlBlockLanguages htmlBlockUpdated = new HtmlBlockLanguages()
            {
                    HtmlBlockId = cmd.HtmlBlockId,
                    LocaleId = cmd.LocaleId,
                    Html = cmd.Html,
                    IsActive = cmd.IsActive,
                    OperatorId = cmd.OperatorId,
                    LastModifiedTS = cmd.LastModifiedTS
            };

            this.Context.Entry(htmlBlockUpdated).State = EntityState.Modified;
            this.Context.SaveChanges();

            return true;
        }
    }
}
