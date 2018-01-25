using System;
using System.Net;
using UserAccountManager.Domain;
using UserAccountManager.UserQueryService;

namespace UserAccountManager.Providers
{
    public class UserQueryServiceProvider : BaseServiceDataProvider
    {
        public UserQueryServiceProvider(Domain.Environment envConfig)
            : base(
                  envConfig.UserQueryService.BehaviorConfiguration,
                  envConfig.UserQueryService.Binding,
                  envConfig.UserQueryService.BindingConfiguration,
                  envConfig.UserQueryService.EndpointAddress,
                  envConfig.TenantId)
        { }

        public UserProfile LoadUserProfile(string userName)
        {
            UserProfile userProfile = null;
            UserProfileQuery query = new UserProfileQuery()
            {
                Requestor = this.BuildRequestor(),
                Where = new UserQueryService.UserIdParms()
                {
                    TenantId = base.tenantId,
                    UserId = userName
                }
            };

            using (var channelFactory = this.InitializeChannelFactory<IUserQueryService>())
            {
                var svcProxy = this.CreateChannel<UserQueryService.IUserQueryService>(channelFactory);
                var svcResponse = svcProxy.GetUserProfile(query);
                if (svcResponse.QueryResult != null)
                {
                    userProfile = new UserProfile()
                    {
                        ProfileId = svcResponse.QueryResult.UserProfileId,
                        DisplayName = svcResponse.QueryResult.FirstName + " " + svcResponse.QueryResult.LastName,
                        EmailAddress = svcResponse.QueryResult.EMail,
                        FirstName = svcResponse.QueryResult.FirstName,
                        LastName = svcResponse.QueryResult.LastName,
                        LocaleId = svcResponse.QueryResult.LocalId,
                        //MiddleName = svcResponse.QueryResult.MiddleName,
                        PhoneNumber = svcResponse.QueryResult.ContactNumber,
                        TenantId = svcResponse.QueryResult.TenantId,
                        UserName = svcResponse.QueryResult.UserId//,
                        //VosTags = svcResponse.QueryResult.VosTags
                    };
                }
            }

            return userProfile;
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
