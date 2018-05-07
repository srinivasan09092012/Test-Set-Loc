//---------------------------------------------------------------------------------------------
// This code is the property of DXC Technology Enteprise, Copyright (c) 2018. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent o the law.
//--------------------------------------------------------------------------------------------------
using DatalistSyncUtil.Configs;
using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatalistSyncUtil.DaoHelpers
{
    public class GetHelpContentDaoHelper
    {
        private const string Module = "MODULE";

        public GetHelpContentDaoHelper(HelpDbContext context)
        {
            this.Context = context;
        }

        public HelpDbContext Context { get; set; }

        public List<HelpNodeModel> ExecuteProcedure(Guid tenantID)
        {
            var result = from hn in this.Context.HelpNode
                          join helpNodeLink_List in this.Context.Set<HelpNodeLink>() on hn.HelpNodeId equals helpNodeLink_List.ChildNodeId into hps
                          from hhi in hps.DefaultIfEmpty()
                            where hn.TenantId.Equals(tenantID)
                          select new HelpNodeModel()
                          {
                              HelpNodeId = hn.HelpNodeId,
                              HelpNodeNM = hn.HelpNodeNM,
                              HelpNodeTypeCD = hn.HelpNodeTypeCD,
                              NodeDepth = hn.HelpNodeNM.Length - hn.HelpNodeNM.Replace(".", string.Empty).Length,
                              IsActive = hn.IsActive,
                              LastModifiedTS = hn.LastModifiedTimeStamp,
                              TenantId = hn.TenantId,
                              TenantModuleId = hn.TenantModuleId,
                              OperatorId = hn.OperatorID,
                              HowToHelpNodeNM = hn.HowToHelpNodeNM,
                              ParentId = hhi == null ? System.Guid.Empty : hhi.ParentNodeId,
                          };
            var results = result.ToList();
            results.ForEach(x =>
            {
                if (x.HelpNodeTypeCD != Module)
                {
                    string ParentHelpNodeName, HelpNodeTypeCD;
                    GetNodeNameAndType(x.ParentId, out ParentHelpNodeName, out HelpNodeTypeCD);
                    x.ParentHelpNodeName = ParentHelpNodeName;
                    x.ParentHelpNodeTypeCD = HelpNodeTypeCD;
                }
            });
            return results;
        }

        public void GetNodeNameAndType(Guid helpNodeId, out string helpNodeNM, out string helpNodeTypeCD)
        {
            var query = (from helpNode in this.Context.HelpNode
                    where helpNode.HelpNodeId == helpNodeId
                    select new { helpNodeName = helpNode.HelpNodeNM, helpNodeType = helpNode.HelpNodeTypeCD }).FirstOrDefault();
            helpNodeNM = query.helpNodeName;
            helpNodeTypeCD = query.helpNodeType;
            return;
        }

        public string GetNodeName(Guid helpNodeId)
        {
           return (from helpNode in this.Context.HelpNode
             where helpNode.HelpNodeId == helpNodeId
             select helpNode.HelpNodeNM).FirstOrDefault();
        }

        public IQueryable<AddHelpContentModel> GetNodeNameAndLanguages(Guid helpNodeId)
        {
            return from helpNode in this.Context.HelpNode
                   join local in this.Context.HelpNodeLocale
                   on helpNode.HelpNodeId equals local.HelpNodeId
                   where helpNode.HelpNodeId == helpNodeId
                   && helpNode.IsActive == true
                   select new AddHelpContentModel()
                   {
                       FullHelpNodeName = helpNode.HelpNodeNM,
                       Languages = this.Context.HelpNodeLocale.Where(x => x.HelpNodeId == helpNodeId).Select(x => x.LocaleId).ToList()
                   };
        }
    }
}
