//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace WarmUpProvider.Domain
{
    [Serializable]
    public class TenantWarmUpModel
    {
        public string TenantId { get; set; }

        public string RootURL { get; set; }

        public List<ModuleEndpointModel> Endpoints { get; set; }
    }
}
