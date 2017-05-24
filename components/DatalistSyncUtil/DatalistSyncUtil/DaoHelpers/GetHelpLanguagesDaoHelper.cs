﻿//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DatalistSyncUtil.DaoHelpers
{
    public class GetHelpLanguagesDaoHelper
    {
        public GetHelpLanguagesDaoHelper(HelpDbContext context)
        {
            this.Context = context;
        }

        public HelpDbContext Context { get; set; }

        public List<HelpContentLanguageModel> ExecuteProcedure()
        {
            var results = from help in this.Context.HelpNode
                          join hnl in this.Context.HelpNodeLocale on help.HelpNodeId equals hnl.HelpNodeId
                          select new HelpContentLanguageModel()
                          {
                              HelpNodeId = hnl.HelpNodeId,
                              HelpNodeNM = help.HelpNodeNM,
                              Language = hnl.LocaleId,
                              Title = hnl.NodeTitle,
                              HtmlContent = hnl.NodeTxt,
                              OperatorId = hnl.OperatorID
                          };

            return results.ToList();
        }
    }
}
