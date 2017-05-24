// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.Help.DataAccess.DataAccess.Entities;
using System.Data.Entity;

namespace DatalistSyncUtil.Entities
{
    public interface IHelpDbContext
    {
        /// <summary>
        /// The Help MITA table holds information related to MITA guidelines
        /// such as the title and url
        /// <uol>
        /// <li><b>Table:</b>HELP_MITA</li> 
        /// </uol>
        /// </summary>
        IDbSet<HelpMita> HelpMita { get; set; }

        /// <summary>
        /// The Help Node table holds information related to Help Node having Tenant and Module information
        /// such as the Tenant and Module information
        /// <uol>
        /// <li><b>Table:</b>HELP_NODE</li> 
        /// </uol>
        /// </summary>
        IDbSet<HelpNode> HelpNode { get; set; }

        /// <summary>
        /// The Help Node Link table holds information related to parent and child node
        /// such as the ParentNodeId
        /// <uol>
        /// <li><b>Table:</b>HELP_NODE_LINK</li> 
        /// </uol>
        /// </summary>
        IDbSet<HelpNodeLink> HelpNodeLink { get; set; }

        /// <summary>
        /// The Help Node Locale table holds information related to the actuall text to be displayed
        /// such as the NodeSummary and NodeTitle
        /// <uol>
        /// <li><b>Table:</b>HELP_NODE_LOCALE</li> 
        /// </uol>
        /// </summary>
        IDbSet<HelpNodeLocale> HelpNodeLocale { get; set; }
    }
}