//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Runtime.Serialization;

namespace SQLToTaskConverter
{
    [DataContract]
    public class Parameters
    {
        [DataMember]
        public string ParameterName { get; set; }

        [DataMember]
        public string ParameterValue { get; set; }
    }
}
