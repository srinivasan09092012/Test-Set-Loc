using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConfigLoader
{
    [DelimitedRecord(",")]
    public class AppSettings
    {
        [FieldCaption("ModuleName")]
        public string ModuleName { get; set; }

        [FieldCaption("ApplicationName")]
        public string ApplicationName { get; set; }

        [FieldCaption("AppSettingKey")]
        public string AppSettingKey { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldCaption("AppSettingValue")]
        public string AppSettingValue { get; set; }
    }
}
