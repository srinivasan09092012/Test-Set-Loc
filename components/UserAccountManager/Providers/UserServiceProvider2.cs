using System;
using System.Net;
using svc = UserAccountManager.UserService2;
using UserAccountManager.Domain;

namespace UserAccountManager.Providers
{
    public class UserServiceProvider2 : BaseServiceDataProvider, IUserServiceProvider
    {
        public UserServiceProvider2(Domain.Environment envConfig)
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
                GenericId = userProfile.GeneralId,
                IsActive = userProfile.IsActive,
                IsAccountVerified = userProfile.IsAccountVerified,
                LastName = userProfile.LastName,
                LocaleId = userProfile.LocaleId,
                TenantId = userProfile.TenantId.ToString("D"),
                UserId = userProfile.UserName,
                UserVOSTags = new svc.UserVOSTagListModel()
            };

            foreach (var tag in userProfile.VOSTags)
            {
                svc.UserVOSTagModel newTag = new svc.UserVOSTagModel()
                {
                    Code = tag.Code,
                    TypeCode = tag.TypeCode,
                    EffectiveDate = tag.EffectiveDate,
                    EndDate = tag.EndDate
                };
                cmd.UserVOSTags.Add(newTag);
            }

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
                GenericId = userProfile.GeneralId,
                IsActive = userProfile.IsActive,
                IsAccountVerified = userProfile.IsAccountVerified,
                LastName = userProfile.LastName,
                LocaleId = userProfile.LocaleId,
                TenantId = userProfile.TenantId.ToString("D"),
                UserId = userProfile.UserName
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserService>(channelFactory);
                var svcResponse = svcProxy.UpdateProfile(cmd);

                foreach (var tag in userProfile.VOSTags)
                {
                    svc.UserVOSTagModel newTag = new svc.UserVOSTagModel()
                    {
                        UserVOSTagId = tag.UserVOSTagId,
                        Code = tag.Code,
                        TypeCode = tag.TypeCode,
                        EffectiveDate = tag.EffectiveDate,
                        EndDate = tag.EndDate
                    };
                    
                    if (newTag.UserVOSTagId == Guid.Empty)
                    {
                        svc.AddUserVOSTags cmd2 = new svc.AddUserVOSTags()
                        {
                            TenantID = cmd.TenantId,
                            Requestor = cmd.Requestor,
                            UserProfileID = svcResponse.UserProfileId,
                            UserVOSTags = new svc.UserVOSTagListModel()
                            {
                                newTag
                            }
                        };

                        svcProxy.AddUserVOSTags(cmd2);
                    }
                    else
                    {
                        svc.UpdateUserVOSTag cmd2 = new svc.UpdateUserVOSTag()
                        {
                            TenantID = cmd.TenantId,
                            Requestor = cmd.Requestor,
                            UserProfileID = svcResponse.UserProfileId,
                            UserVOSTag = newTag
                        };

                        svcProxy.UpdateUserVOSTag(cmd2);
                    }
                }
            }
        }

        public void ActiveProfile(string userId)
        {
            svc.ActivateProfile cmd = new svc.ActivateProfile()
            {
                Requestor = this.BuildRequestor(),
                TenantId = base.tenantId,
                UserId = userId
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserService>(channelFactory);
                var svcResponse = svcProxy.ActivateProfile(cmd);
            }
        }

        public void InactiveProfile(string userId)
        {
            svc.InactivateProfile cmd = new svc.InactivateProfile()
            {
                Requestor = this.BuildRequestor(),
                TenantId = base.tenantId,
                UserId = userId
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserService>(channelFactory);
                var svcResponse = svcProxy.InactivateProfile(cmd);
            }
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
