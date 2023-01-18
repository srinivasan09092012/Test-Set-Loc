using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXWarmUpParamBuilder
{
    [DelimitedRecord("|")]
    public class TokenizedValue
    {
        public string TokenKey { get; set; }
        
        public string TokenValue { get; set; }

    }
}
