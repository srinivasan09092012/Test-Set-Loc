using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPP.OneClick.XAML.Migration.DevContext
{
    class ProcessTM
    {
        [JsonProperty(PropertyName = "x:Array")]
        public List<ArrayElm> XArray { get; set; }

        //[JsonProperty(PropertyName = "mtbc:BuildParameter")]
        //public List<ArrayElm> BluildParameter { get; set; }

        [JsonProperty(PropertyName = "x:String")]
        public List<ArrayElm> XString { get; set; }

        [JsonProperty(PropertyName = "x:Boolean")]
        public List<ArrayElm> XBoolean { get; set; }
    }

    class ProcessTMNA
    {
        [JsonProperty(PropertyName = "x:Array")]
        public List<ArrayElm> XArray { get; set; }

        //[JsonProperty(PropertyName = "mtbc:BuildParameter")]
        //public List<ArrayElm> BluildParameter { get; set; }

        [JsonProperty(PropertyName = "x:String")]
        public List<ArrayElm> XString { get; set; }

        [JsonProperty(PropertyName = "x:Boolean")]
        public ArrayElm XBoolean { get; set; }
    }

    class ProcessTMbuildparam
    {
        //[JsonProperty(PropertyName = "x:Array")]
        //public List<ArrayElm> XArray { get; set; }

        [JsonProperty(PropertyName = "mtbc:BuildParameter")]
        public ArrayElm BluildParameter { get; set; }

        //[JsonProperty(PropertyName = "x:String")]
        //public List<ArrayElm> XString { get; set; }

        //[JsonProperty(PropertyName = "x:Boolean")]
        //public List<ArrayElm> XBoolean { get; set; }
    }
    class ProcessTMbuildparamarray
    {
        //[JsonProperty(PropertyName = "x:Array")]
        //public List<ArrayElm> XArray { get; set; }

        [JsonProperty(PropertyName = "mtbc:BuildParameter")]
        public List<ArrayElm> BluildParameter { get; set; }

        //[JsonProperty(PropertyName = "x:String")]
        //public List<ArrayElm> XString { get; set; }

        //[JsonProperty(PropertyName = "x:Boolean")]
        //public List<ArrayElm> XBoolean { get; set; }
    }

    public class ArrayElm
    {
        [JsonProperty(PropertyName = "@x:Key")]
        public string XKey { get; set; }
        [JsonProperty(PropertyName = "@Type")]
        public string XType { get; set; }
        [JsonProperty(PropertyName = "x:String")]
        public string XString { get; set; }
        [JsonProperty(PropertyName = "#text")]
        public string XText { get; set; }
        [JsonProperty(PropertyName = "mtbc:BuildParameter")]
        public string MTBCBuildParameter { get; set; }
    }
    public class BuildArgs
    {
        public string MSBuildArguments { get; set; }
        public string SFMSBuildArguments { get; set; }
    }
}
