using System;
using System.Net;
using UserAccountManager.UserService;
using UserAccountManager.Domain;

namespace UserAccountManager.Providers
{
    public class UserServiceProvider : BaseServiceDataProvider
    {
        public UserServiceProvider(Domain.Environment envConfig)
            : base(
                  envConfig.UserService.BehaviorConfiguration,
                  envConfig.UserService.Binding,
                  envConfig.UserService.BindingConfiguration,
                  envConfig.UserService.EndpointAddress,
                  envConfig.TenantId)
        { }

        public void AddProfile(UserProfile userProfile)
        {
            AddProfile cmd = new AddProfile()
            {
                Requestor = this.BuildRequestor(),
                ContactNumber = userProfile.PhoneNumber,
                EmailAddress = userProfile.EmailAddress,
                FirstName = userProfile.FirstName,
                //GenericIdentifer = userProfile.ExternalId,
                LastName = userProfile.LastName,
                LocaleId = userProfile.LocaleId,
                TenantId = userProfile.TenantId.ToString("D"),
                UserId = userProfile.UserName//,
                //VosTags = userProfile.VosTags
            };

            using (var channelFactory = this.InitializeChannelFactory<IUserService>())
            {
                var svcProxy = this.CreateChannel<UserService.IUserService>(channelFactory);
                var svcResponse = svcProxy.AddProfile(cmd);
            }
        }

        public void UpdateProfile(UserProfile userProfile)
        {
            UpdateProfile cmd = new UpdateProfile()
            {
                Requestor = this.BuildRequestor(),
                ContactNumber = userProfile.PhoneNumber,
                EmailAddress = userProfile.EmailAddress,
                FirstName = userProfile.FirstName,
                //GenericIdentifer = userProfile.ExternalId,
                LastName = userProfile.LastName,
                LocaleId = userProfile.LocaleId,
                TenantId = userProfile.TenantId.ToString("D"),
                UserId = userProfile.UserName//,
                //VosTags = userProfile.VosTags
            };

            using (var channelFactory = this.InitializeChannelFactory<IUserService>())
            {
                var svcProxy = this.CreateChannel<UserService.IUserService>(channelFactory);
                var svcResponse = svcProxy.UpdateProfile(cmd);
            }
        }

        private RequestorModel BuildRequestor()
        {
            return new RequestorModel()
            {
                ApplicationName = Constants.AppName,
                CorrelationId = Guid.NewGuid().ToString("n"),
                IdentifierId = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                IdentifierIdType = CoreEnumerationsMessagingIdentifierIdType.User,
                IpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString(),
                RequestDate = DateTime.UtcNow,
                TenantId = base.tenantId
            };
        }
    }
}
