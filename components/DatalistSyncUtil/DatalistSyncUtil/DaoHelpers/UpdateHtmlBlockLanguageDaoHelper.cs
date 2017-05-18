﻿//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using System;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateHtmlBlockLanguageDaoHelper
    {
        public UpdateHtmlBlockLanguageDaoHelper(HtmlBlockDbContext context)
        {
            this.Context = context;
        }

        public HtmlBlockDbContext Context { get; set; }

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
