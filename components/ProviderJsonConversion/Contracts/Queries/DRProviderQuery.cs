﻿//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using Contracts.Domain;
using Contracts.Queries.Parameters;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using System.Runtime.Serialization;

namespace Contracts.Queries
{
    [DataContract]
    [KnownType(typeof(DRProviderModel))]
    [KnownType(typeof(DRProviderQueryParms))]
    public class DRProviderQuery : Query<DRProviderModel>
    {
        [DataMember]
        public DRProviderQueryParms Where { get; set; }

        public long TotalMiliseconds { get; set; }

        public void Validate()
        {
        }
    }
}