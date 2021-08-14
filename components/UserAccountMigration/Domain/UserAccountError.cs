//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using FileHelpers;

namespace UserAccountMigration.Domain
{
    public class UserAccountError : UserAccount
    {
        public UserAccountError() { }

        public UserAccountError(UserAccount userAccount, string error)
        {
            this.BirthDate = userAccount.BirthDate;
            this.EmailAddress = userAccount.EmailAddress;
            this.FirstName = userAccount.FirstName;
            this.Groups = userAccount.Groups;
            this.Last4SSN = userAccount.Last4SSN;
            this.LastName = userAccount.LastName;
            this.MiddleName = userAccount.MiddleName;
            this.Password = userAccount.Password;
            this.PhoneNumber = userAccount.PhoneNumber;
            this.UserName = userAccount.UserName;
            this.Error = error;
        }

        [FieldOrder(99)]
        public string Error { get; set; }
    }
}
