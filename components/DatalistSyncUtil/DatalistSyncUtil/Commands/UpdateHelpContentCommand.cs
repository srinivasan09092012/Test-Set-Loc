//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using DatalistSyncUtil.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using System.Runtime.Serialization;

namespace DatalistSyncUtil.Commands
{
    [DataContract]
    public class UpdateHelpContentCommand : Command
    {
        public UpdateHelpContentCommand()
        {
            this.HelpNodeModel = new HelpNodeModel();
        }

        [DataMember]
        public HelpNodeModel HelpNodeModel { get; set; }
       }
}
