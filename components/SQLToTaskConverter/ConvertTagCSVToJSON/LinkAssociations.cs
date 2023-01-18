//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace SQLToTaskConverter
{
    public class LinkAssociations
    {
        [JsonProperty("$id")]
        public string Id { get; set; }
        [JsonProperty("$values")]

        public List<LinkAssociation> Values { get; set; }
    }
}
