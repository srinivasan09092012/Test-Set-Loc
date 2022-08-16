//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace SQLToTaskConverter
{
    public class LinkAssociation
    {
        [JsonProperty("$id")]
        public string Id { get; set; }

        public string LinkConfigId { get; set; }

        public string LinkParameter { get; set; }

        public string LinkTypeCode { get; set; }
    }
}
