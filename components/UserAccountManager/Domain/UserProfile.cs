using System;
using System.Collections.Generic;

namespace UserAccountManager.Domain
{
    public class UserProfile
    {
        public UserProfile()
        {
            this.Initialize();
        }

        public Guid ProfileId { get; set; }

        public string ExternalId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string EmailAddress { get; set; }

        public string LocaleId { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public Guid TenantId { get; set; }

        public List<string> VosTags { get; set; }

        public void Initialize()
        {
            this.VosTags = new List<string>();
        }
    }
}