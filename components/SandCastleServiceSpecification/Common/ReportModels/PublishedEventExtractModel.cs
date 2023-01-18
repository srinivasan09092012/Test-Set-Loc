using FileHelpers;

namespace Common.ReportModels
{
    [DelimitedRecord(",")]
    public class PublishedEventExtractModel
    {
        [FieldNullValue(typeof(string), null)]
        public string MODULE_EVENT_ID;

        [FieldNullValue(typeof(string), null)]
        public string MODULE_ID;

        [FieldNullValue(typeof(string), null)]
        public string EVENT_TYPE_NAME;

        [FieldNullValue(typeof(string), null)]
        public string IS_ACTIVE;

        [FieldNullValue(typeof(string), null)]
        public string CREATED_TS;

        [FieldNullValue(typeof(string), null)]
        public string MODIFIED_TS;

        [FieldNullValue(typeof(string), null)]
        public string MODULE_NAME;
    }
}
