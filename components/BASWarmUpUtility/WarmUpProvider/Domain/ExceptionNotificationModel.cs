//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using System.Collections.Generic;

namespace WarmUpProvider.Domain
{
    public class ExceptionNotificationModel
    {
        public string Endpoint { get; set; }

        public List<BusinessExceptionMessage> BusinessExceptionMessage { get; set; }
    }
}
