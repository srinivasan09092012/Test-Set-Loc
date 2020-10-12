using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConfigLoader
{
    [DelimitedRecord(",")]
    public class AppSettingsResult : AppSettings
    {
        [FieldCaption("Error")]
        public string Error { get; set; }
    }
}
