using System;
using System.Net;
using svc = UserAccountManager.UserService1;
using UserAccountManager.Domain;

namespace UserAccountManager.Providers
{
    public class UserServiceProvider1 : BaseServiceDataProvider, IUserServiceProvider
    {
        public UserServiceProvider1(Domain.Environment envConfig)
            : base(
                  envConfig.UserService.BehaviorConfiguration,
                  envConfig.UserService.Binding,
                  envConfig.UserService.BindingConfiguration,
                  envConfig.UserService.EndpointAddress,
                  envConfig.TenantId)
        { }

        public void AddProfile(UserProfile userProfile)
        {
            svc.AddProfile cmd = new svc.AddProfile()
            {
                Requestor = this.BuildRequestor(),
                ContactNumber = userProfile.PhoneNumber,
                EmailAddress = userProfile.EmailAddress,
                FirstName = userProfile.FirstName,
                //GenericId = userProfile.GeneralId,
                LastName = userProfile.LastName,
                LocaleId = userProfile.LocaleId,
                TenantId = userProfile.TenantId.ToString("D"),
                UserId = userProfile.UserName//,
                //VosTags = userProfile.VosTags
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserService>(channelFactory);
                var svcResponse = svcProxy.AddProfile(cmd);
            }
        }

        public void UpdateProfile(UserProfile userProfile)
        {
            svc.UpdateProfile cmd = new svc.UpdateProfile()
            {
                Requestor = this.BuildRequestor(),
                ContactNumber = userProfile.PhoneNumber,
                EmailAddress = userProfile.EmailAddress,
                FirstName = userProfile.FirstName,
                //GenericId = userProfile.GeneralId,
                LastName = userProfile.LastName,
                LocaleId = userProfile.LocaleId,
                TenantId = userProfile.TenantId.ToString("D"),
                UserId = userProfile.UserName//,
                //VosTags = userProfile.VosTags
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserService>(channelFactory);
                var svcResponse = svcProxy.UpdateProfile(cmd);
            }
        }

        public void ActiveProfile(string userId)
        {
            throw new NotImplementedException("This operation is not available in this environment.");
        }

        public void InactiveProfile(string userId)
        {
            throw new NotImplementedException("This operation is not available in this environment.");
        }

        private svc.RequestorModel BuildRequestor()
        {
            return new svc.RequestorModel()
            {
                ApplicationName = Constants.AppName,
                CorrelationId = Guid.NewGuid().ToString("n"),
                IdentifierId = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                IdentifierIdType = svc.CoreEnumerationsMessagingIdentifierIdType.User,
                IpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString(),
                RequestDate = DateTime.UtcNow,
                TenantId = base.tenantId
            };
        }
    }
}
