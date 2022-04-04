using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXWarmUpParamBuilder
{
    public class WarmUpPayloadModel
    {
        public string ModuleName { get; set; }

        public string Priority { get; set; }

        public string ParamType { get; set; }

        public string RouteUrl { get; set; }

        public string Param { get; set; }
    }
}
