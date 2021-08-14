//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Xml.Serialization;

namespace UserAccountMigration.Domain
{
    [Serializable]
    public class Environment
    {
        public Environment()
        {
            this.Initialize();
        }

        [XmlAttribute("tenantId")]
        public string TenantId { get; set; }

        [XmlAttribute("defaultLocale")]
        public string DefaultLocale { get; set; }

        public string ADContainer { get; set; }

        public string ADServer { get; set; }

        public string ADUser { get; set; }

        public string ADPassword { get; set; }

        public UserService UserService { get; set; }

        public UserQueryService UserQueryService { get; set; }

        public void Initialize()
        {
            this.DefaultLocale = "en-US";
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.TenantId))
            {
                throw new ArgumentNullException("TenantID");
            }

            if (string.IsNullOrWhiteSpace(this.DefaultLocale))
            {
                throw new ArgumentNullException("DefaultLocale");
            }

            if (string.IsNullOrWhiteSpace(this.ADContainer))
            {
                throw new ArgumentNullException("ADContainer");
            }

            if (string.IsNullOrWhiteSpace(this.ADServer))
            {
                throw new ArgumentNullException("ADServer");
            }

            if (string.IsNullOrWhiteSpace(this.ADUser))
            {
                throw new ArgumentNullException("ADUser");
            }

            if (string.IsNullOrWhiteSpace(this.ADPassword))
            {
                throw new ArgumentNullException("ADPassword");
            }

            if (this.UserQueryService == null)
            {
                throw new ArgumentNullException("UserQueryService");
            }
            else
            {
                this.UserQueryService.Validate();
            }

            if (this.UserService == null)
            {
                throw new ArgumentNullException("UserService");
            }
            else
            {
                this.UserService.Validate();
            }
        }
    }
}
