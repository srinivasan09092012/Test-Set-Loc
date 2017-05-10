//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;

namespace DatalistSyncUtil.Models
{
    public class TreeSyncData
    {
       public List<DataListMainModel> DataListItems { get; set; }

      public List<MenuListModel> MenuListItems { get; set; }
    }
}