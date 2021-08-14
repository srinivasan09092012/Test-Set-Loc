//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace UserAccountMigration.Domain
{
    public class UserProfile
    {
        public UserProfile()
        {
            this.Initialize();
        }

        public Guid ProfileId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string EmailAddress { get; set; }

        public string GeneralId { get; set; }

        public bool IsActive { get; set; }

        public bool IsAccountVerified { get; set; }

        public string LocaleId { get; set; }

        public string PhoneNumber { get; set; }

        public string RelationshipCode { get; set; }

        public string UserName { get; set; }

        public Guid TenantId { get; set; }

        public List<RegistrationQualifier> RegQualifiers { get; set; }

        public List<UserVOSTag> VOSTags { get; set; }

        public void Initialize()
        {
            this.IsActive = true;
            this.RegQualifiers = new List<RegistrationQualifier>();
            this.VOSTags = new List<UserVOSTag>();
        }
    }
}