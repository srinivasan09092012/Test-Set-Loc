//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using FileHelpers;
using System;

namespace UserAccountMigration.Domain
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst(1)]
    public class DelegateUser
    {
        public DelegateUser() { }

        [FieldOrder(1)]
        public string UserName { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
            {
                throw new ArgumentNullException("UserName");
            }
        }
    }
}
