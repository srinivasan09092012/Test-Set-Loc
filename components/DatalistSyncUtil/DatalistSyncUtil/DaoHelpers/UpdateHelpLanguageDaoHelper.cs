//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Entities;
using System;
using System.Data.Entity;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateHelpLanguageDaoHelper
    {
        public UpdateHelpLanguageDaoHelper(HelpDbContext context)
        {
            this.Context = context;
        }

        public HelpDbContext Context { get; set; }

        public bool ExecuteProcedure(HelpContentLanguageModel cmd)
        {
            HelpNodeLocale helpNodeLocale = new HelpNodeLocale()
            {
                    HelpNodeId = cmd.HelpNodeId,
                    LocaleId = cmd.Language,
                    NodeTxt = cmd.HtmlContent,
                    NodeTitle = cmd.Title,
                    NodeSummary = null,
                    LastModifiedTimeStamp = DateTime.UtcNow,
                    OperatorID = cmd.OperatorId
            };

            this.Context.Entry(helpNodeLocale).State = EntityState.Modified;
            this.Context.SaveChanges();

            return true;
        }
    }
}
