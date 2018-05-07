//---------------------------------------------------------------------------------------------
// This code is the property of DXC Technology Enteprise, Copyright (c) 2018. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent o the law.
//--------------------------------------------------------------------------------------------------
using DatalistSyncUtil.Commands;
using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatalistSyncUtil.DaoHelpers
{
    public class UpdateHelpContentDaoHelper : OracleDAOBase<int>
    {
        public UpdateHelpContentDaoHelper(HelpDbContext context)
        {
            this.Context = context;
            this.AddHelpContentDaoHelper = new AddHelpContentDaoHelper();
            this.Languages = new List<string>();
            this.GetHelpContentDaoHelper = new GetHelpContentDaoHelper(this.Context);
        }

        public HelpDbContext Context { get; set; }

        private Guid ExistingHelpNodeId { get; set; }

        private AddHelpContentDaoHelper AddHelpContentDaoHelper { get; set; }

        private List<string> Languages { get; set; }

        private GetHelpContentDaoHelper GetHelpContentDaoHelper { get; set; }

        public Guid ExecuteProcedure(UpdateHelpContentCommand cmd)
        {
            this.ExistingHelpNodeId = cmd.HelpNodeModel.HelpNodeId;

            var updateHelpNode = this.Context.HelpNode.FirstOrDefault(f => f.HelpNodeId == this.ExistingHelpNodeId);

            if (updateHelpNode != null)
            {
                updateHelpNode.IsActive = cmd.HelpNodeModel.IsActive;
                this.Context.Entry(updateHelpNode).State = EntityState.Modified;
            }

            this.Context.SaveChanges();
            return this.ExistingHelpNodeId;
        }
    }
}
