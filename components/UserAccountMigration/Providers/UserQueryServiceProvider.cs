﻿//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net;
using UserAccountMigration.Domain;
using svc = UserAccountMigration.UserQueryService;

namespace UserAccountMigration.Providers
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
            svc.UserProfileQuery query = new svc.UserProfileQuery()
            {
                Requestor = this.BuildRequestor(),
                Where = new svc.UserIdParms()
                {
                    UserId = userName
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
                        EmailAddress = svcResponse.QueryResult.EmailAddress,
                        GeneralId = svcResponse.QueryResult.GenericId,
                        IsAccountVerified = svcResponse.QueryResult.IsAccountVerified.HasValue ? svcResponse.QueryResult.IsAccountVerified.Value : true,
                        IsActive = svcResponse.QueryResult.IsActive.HasValue ? svcResponse.QueryResult.IsActive.Value : true,
                        FirstName = svcResponse.QueryResult.FirstName,
                        LastName = svcResponse.QueryResult.LastName,
                        LocaleId = svcResponse.QueryResult.LocalId,
                        PhoneNumber = svcResponse.QueryResult.ContactNumber,
                        RelationshipCode = svcResponse.QueryResult.RelationshipCode,
                        TenantId = svcResponse.QueryResult.TenantId,
                        UserName = svcResponse.QueryResult.UserId,
                        RegQualifiers = new List<RegistrationQualifier>(),
                        VOSTags = new List<UserVOSTag>()
                    };

                    foreach (svc.UserVOSTagModel tag in svcResponse.QueryResult.UserVOSTags)
                    {
                        userProfile.VOSTags.Add(new UserVOSTag(tag.UserVOSTagId, tag.Code, tag.TypeCode, tag.EffectiveDate, tag.EndDate));
                    }
                }
            }

            return userProfile;
        }

        public void GetUserXref(UserXref xref, ref string xrefId, ref string xrefAssocId, ref bool identicalXrefExists)
        {
            identicalXrefExists = false;
            svc.UserDelegateQuery query = new svc.UserDelegateQuery()
            {
                Requestor = this.BuildRequestor(),
                Where = new svc.UserDelegateParms()
                {
                    AssociationId = xref.AssociationId,
                    UserId = xref.PrimaryUserName,
                    IsRegistered = true,
                    DelegateFilters = new List<svc.UserDelegateFilter>()
                    {
                        new svc.UserDelegateFilter()
                        {
                            FieldName = svc.FilterModeCriteriaFilterFields.DelegateUserId,
                            FilteredMode = svc.FilterModeCriteriaFilterModeType.EqualTo,
                            FilterValue = xref.SecondaryUserName
                        }
                    }
                }
            };

            using (var channelFactory = this.InitializeChannelFactory<svc.IUserQueryService>())
            {
                var svcProxy = this.CreateChannel<svc.IUserQueryService>(channelFactory);
                var svcResponse = svcProxy.GetUserDelegates(query);
                if (svcResponse.Results != null && svcResponse.Results.Count > 0)
                {
                    if (svcResponse.Results[0].IsAssociationActive == xref.IsAssociationActive &&
                        svcResponse.Results[0].IsAssociationAdministrator)
                    {
                        identicalXrefExists = true;
                    }
                    else
                    {
                        xrefId = svcResponse.Results[0].XREFId.ToString();
                        xrefAssocId = svcResponse.Results[0].XrefAssocId.ToString();
                    }
                }
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
