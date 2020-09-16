//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using System;

namespace WarmUpProvider.Domain
{
    [Serializable]
    public class ModuleEndpointModel
    {
        public string ModuleName { get; set; }

        public string ApplicationName { get; set; }

        public string EndPoint { get; set; }

        public string Binding { get; set; }

        public bool CheckHealthStatus { get; set; }
    }
}
