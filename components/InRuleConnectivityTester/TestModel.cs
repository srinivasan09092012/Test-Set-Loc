//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.BusinessRules.Interface;
using HPE.HSP.UA3.Core.API.BusinessRules.Interface.Domain;
using System;

namespace HPE.HSP.UA3.Core.API.BusinessRules.Providers.Tests.Domain
{
    [Serializable]
    public class TestModel : IBusinessRulesObject
    {
        public TestModel()
        {
            this.Rules = new BaseValidationModel();
        }

        public TestModel(string name, int priority) : this()
        {
            this.Name = name;
            this.Priority = priority;
        }

        public string Name { get; set; }

        public int Priority { get; set; }

        public int Validation { get; set; }

        public BaseValidationModel Rules { get; set; }
    }
}
