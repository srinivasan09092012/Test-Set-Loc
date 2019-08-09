//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using UserAccountMigration.Domain;
using svc = UserAccountMigration.UserService;

namespace UserAccountMigration.Providers
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

        public void AddProfileXref(UserXref userXref, string primaryUserId, string secondaryId, string relationshipCode)
        {
            svc.AddRegisteredUserXrefCommand cmd = new svc.AddRegisteredUserXrefCommand()
            {
                Requestor = this.BuildRequestor(),
                UserProfileId = primaryUserId,
                DelegateProfileId = secondaryId,
                RelationshipCode = relationshipCode,
                DelegateAssociations = new List<svc.RegisteredUserXrefAssociation>()
                {
                    new svc.RegisteredUserXrefAssociation()
                    {
                        AssociationId = userXref.AssociationId,
                        IsActive = userXref.IsAssociationActive,
                        IsAssociationAdministrator = true
                    }
                },
                IsActive = true,  
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserService>(channelFactory);
                var svcResponse = svcProxy.AddRegisteredUserXref(cmd);
            }
        }

        public void UpdateProfileXref(UserXref userXref, string xrefId, string xrefAssocId)
        {
            svc.UpdateRegisteredUserXrefCommand cmd = new svc.UpdateRegisteredUserXrefCommand()
            {
                Requestor = this.BuildRequestor(),
                UserProfileXrefId = xrefId,
                DelegateAssociations = new List<svc.RegisteredUserXrefAssociation>()
                {
                    new svc.RegisteredUserXrefAssociation()
                    {
                        AssociationId = userXref.AssociationId,
                        IsActive = userXref.IsAssociationActive,
                        IsAssociationAdministrator = true,
                        UserProfileXrefAssocId = xrefAssocId
                    }
                },
                IsActive = true,
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserService>(channelFactory);
                var svcResponse = svcProxy.UpdateRegisteredUserXref(cmd);
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
