using System;
using System.Xml.Serialization;

namespace UserAccountManager.Domain
{
    [Serializable]
    public class Environment
    {
        public Environment()
        {
            this.Initialize();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("tenantId")]
        public string TenantId { get; set; }
        
        [XmlAttribute("serviceVersion")]
        public int ServiceVersion { get; set; }

        [XmlAttribute("isDeleteAllowed")]
        public bool IsDeleteAllowed { get; set; }

        public string ADContainer { get; set; }

        public string ADServer { get; set; }

        public string ADUser { get; set; }

        public string ADPassword { get; set; }

        public UserService UserService { get; set; }

        public UserQueryService UserQueryService { get; set; }

        public void Initialize()
        {
            this.IsDeleteAllowed = false;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                throw new ArgumentNullException("Environment Name is required.");
            }

            if (string.IsNullOrWhiteSpace(this.TenantId))
            {
                throw new ArgumentNullException("Tenant ID is required.");
            }

            if (string.IsNullOrWhiteSpace(this.ADContainer))
            {
                throw new ArgumentNullException("Environment ADContainer is required.");
            }

            if (string.IsNullOrWhiteSpace(this.ADServer))
            {
                throw new ArgumentNullException("Environment ADServer is required.");
            }

            if (string.IsNullOrWhiteSpace(this.ADUser))
            {
                throw new ArgumentNullException("Environment ADUser is required.");
            }

            if (string.IsNullOrWhiteSpace(this.ADPassword))
            {
                throw new ArgumentNullException("Environment ADPassword is required.");
            }

            if (this.UserQueryService == null)
            {
                throw new ArgumentNullException("Environment UserQueryService is required.");
            }
            else
            {
                this.UserQueryService.Validate();
            }

            if (this.UserService == null)
            {
                throw new ArgumentNullException("Environment UserService is required.");
            }
            else
            {
                this.UserService.Validate();
            }
        }
    }
}
