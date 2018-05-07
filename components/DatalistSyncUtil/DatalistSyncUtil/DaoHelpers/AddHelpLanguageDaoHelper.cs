//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Entities;
using System;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddHelpLanguageDaoHelper
    {
        public AddHelpLanguageDaoHelper(HelpDbContext context)
        {
            this.Context = context;
        }

        public HelpDbContext Context { get; set; }

        public bool ExecuteProcedure(HelpContentLanguageModel cmd)
        {
            if (cmd != null)
            {
                this.Context.HelpNodeLocale.Add(new HelpNodeLocale()
                {
                    HelpNodeId = cmd.HelpNodeId,
                    LocaleId = cmd.Language,
                    NodeTxt = cmd.HtmlContent,
                    NodeTitle = cmd.Title,
                    NodeSummary = null,
                    LastModifiedTimeStamp = DateTime.UtcNow,
                    OperatorID = cmd.OperatorId
                });
            }

            this.Context.SaveChanges();

            return true;
        }
    }
}
