using System;
using System.Collections.Generic;

namespace UserAccountManager.Domain
{
    [Serializable]
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