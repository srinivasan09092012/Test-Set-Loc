//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using FileHelpers;

namespace UserAccountMigration.Domain
{
    public class DelegateUserError : DelegateUser
    {
        public DelegateUserError() { }

        public DelegateUserError(DelegateUser delegateUser, string error)
        {
            this.UserName = delegateUser.UserName;
            this.Error = error;
        }

        [FieldOrder(99)]
        public string Error { get; set; }
    }
}
