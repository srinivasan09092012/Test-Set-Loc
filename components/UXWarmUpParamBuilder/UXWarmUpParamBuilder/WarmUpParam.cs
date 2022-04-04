using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXWarmUpParamBuilder
{
    [DelimitedRecord("|")]
    public class WarmUpParam
    {
        public string ModuleName { get; set; }

        public string ParamRequired { get; set; }

        public string ControllerName { get; set; }

        public string RouteUrl { get; set; }

        public string Param { get; set; }

        public string ParamType { get; set; }

        [FieldHidden]
        public string Status { get; set; }
    }
}
