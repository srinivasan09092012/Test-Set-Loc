using System;
using System.Collections.Generic;
using System.Net;
using UserAccountManager.Domain;
using svc = UserAccountManager.UserQueryService1;

namespace UserAccountManager.Providers
{
    public class UserQueryServiceProvider1 : BaseServiceDataProvider, IUserQueryServiceProvider
    {
        public UserQueryServiceProvider1(Domain.Environment envConfig)
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
            svc.UserProfileQuery query = new svc.UserProfileQuery()
            {
                Requestor = this.BuildRequestor(),
                Where = new svc.UserIdParms()
                {
                    UserId = userName,
                    TenantId = base.tenantId
                }
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserQueryService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserQueryService>(channelFactory);
                var svcResponse = svcProxy.GetUserProfile(query);
                if (svcResponse.QueryResult != null)
                {
                    userProfile = new UserProfile()
                    {
                        ProfileId = svcResponse.QueryResult.UserProfileId,
                        DisplayName = svcResponse.QueryResult.FirstName + " " + svcResponse.QueryResult.LastName,
                        EmailAddress = svcResponse.QueryResult.EMail,
                        GeneralId = svcResponse.QueryResult.GenericIdentifier,
                        FirstName = svcResponse.QueryResult.FirstName,
                        IsAccountVerified = svcResponse.QueryResult.IsAccountVerified.HasValue ? svcResponse.QueryResult.IsAccountVerified.Value : true,
                        IsActive = true,
                        LastName = svcResponse.QueryResult.LastName,
                        LocaleId = svcResponse.QueryResult.LocalId,
                        PhoneNumber = svcResponse.QueryResult.ContactNumber,
                        TenantId = svcResponse.QueryResult.TenantId,
                        UserName = svcResponse.QueryResult.UserId,
                        VOSTags = new List<UserVOSTag>()
                    };
                }
            }

            return userProfile;
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
