//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using DatalistSyncUtil.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using System.Runtime.Serialization;

namespace DatalistSyncUtil.Commands
{
    [DataContract]
    public class AddHelpContentCommand : Command
    {
        public AddHelpContentCommand()
        {
        }

        [DataMember]
        public HelpNodeModel HelpNodeModel { get; set; }
    }
}