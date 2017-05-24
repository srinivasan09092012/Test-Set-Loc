//---------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enteprise, Copyright (c) 2016. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent o the law.
//--------------------------------------------------------------------------------------------------
using DatalistSyncUtil.Commands;
using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using System;
using System.Linq;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddHelpContentDaoHelper : OracleDAOBase<string>
    {
        private const string Module = "MODULE";

        public AddHelpContentDaoHelper()
        {
        }

        public AddHelpContentDaoHelper(HelpDbContext context)
        {
            this.Context = context;
        }

        public HelpDbContext Context { get; set; }

        private Guid TenantModuleId { get; set; }

        private Guid ParentHelpNodeId { get; set; }

        private Guid TenantId { get; set; }

        private Guid HelpNodeId { get; set; }

        public void ExecuteProcedure(AddHelpContentCommand cmd)
        {
            this.TenantModuleId = cmd.HelpNodeModel.TenantModuleId;
            this.TenantId = cmd.HelpNodeModel.TenantId;

                if (cmd.HelpNodeModel.HelpNodeTypeCD == Module)
                {
                    ////Add Module
                    this.ParentHelpNodeId = this.AddHelpNodeAndLocale(cmd.HelpNodeModel);
                }
                else
                {
                    ////Add Topic
                    this.ParentHelpNodeId = this.GetNodeId(cmd.HelpNodeModel.ParentHelpNodeName, cmd.HelpNodeModel.HelpNodeTypeCD);
                    Guid child = this.AddHelpNodeAndLocale(cmd.HelpNodeModel);
                    this.CreateRelation(cmd.HelpNodeModel, this.ParentHelpNodeId, child);
                }

                this.Context.SaveChanges();
        }

        private Guid AddHelpNodeAndLocale(HelpNodeModel helpNodeModel)
        {
            Guid helpNodeID = Guid.NewGuid();
            HelpNode helpNode = new HelpNode()
            {
                HelpNodeId = helpNodeID,
                HelpNodeNM = helpNodeModel.HelpNodeNM,
                HelpNodeTypeCD = helpNodeModel.HelpNodeTypeCD,
                TenantId = this.TenantId,
                ModuleId = null,
                HelpMitaId = null,
                IsActive = helpNodeModel.IsActive,
                LastModifiedTimeStamp = DateTime.UtcNow,
                OperatorID = helpNodeModel.OperatorId,
                TenantModuleId = this.TenantModuleId,
                OrderIndex = 1
            };

            this.Context.HelpNode.Add(helpNode);

            foreach (HelpContentLanguageModel helpContentLanguageModel in helpNodeModel.HelpContentLanguages)
            {
                HelpNodeLocale helpNodeLocale = new HelpNodeLocale()
                {
                    HelpNodeId = helpNodeID,
                    LocaleId = helpContentLanguageModel.Language,
                    NodeTitle = helpContentLanguageModel.Title,
                    NodeSummary = null,
                    NodeTxt = helpContentLanguageModel.HtmlContent,
                    LastModifiedTimeStamp = DateTime.UtcNow,
                    OperatorID = helpNodeModel.OperatorId
                };

                this.Context.HelpNodeLocale.Add(helpNodeLocale);
            }

            return helpNodeID;
        }

        private void CreateRelation(HelpNodeModel helpNodeModel, Guid parent, Guid child)
        {
            HelpNodeLink helpNodeLink = new HelpNodeLink()
            {
                HelpNodeLinkId = Guid.NewGuid(),
                ParentNodeId = parent,
                ChildNodeId = child,
                IsActive = true,
                LastModifiedTimeStamp = DateTime.UtcNow,
                OperatorID = helpNodeModel.OperatorId,
                IsEnabled = true
            };

            this.Context.HelpNodeLink.Add(helpNodeLink);
        }

        private Guid GetNodeId(string helpNodeNM, string helpNodeTypeCD)
        {
            return (from helpNode in this.Context.HelpNode
                    where helpNode.HelpNodeNM == helpNodeNM 
                                && helpNode.HelpNodeTypeCD == helpNodeTypeCD
                    select helpNode.HelpNodeId).FirstOrDefault();
        }

        private string DotNetToOracle(string text)
        {
            Guid guid = new Guid(this.ConvertToGuid(text));
            return BitConverter.ToString(guid.ToByteArray()).Replace("-", string.Empty);
        }

        private string ConvertToGuid(string guidValue)
        {
            byte[] id = ConvertGuidToRaw(guidValue);
            string strHex = BitConverter.ToString(id);
            string gid = new Guid(strHex.Replace("-", string.Empty)).ToString();
            return gid;
        }
    }
}
