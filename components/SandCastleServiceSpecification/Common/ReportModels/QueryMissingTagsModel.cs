using FileHelpers;

namespace Common.ReportModels
{
    [DelimitedRecord(",")]
    public class QueryMissingTagsModel
    {
        [FieldNullValue(typeof(string), null)]
        public string ModuleName; 

        [FieldNullValue(typeof(string), null)]
        public string QueryName; 

        [FieldNullValue(typeof(string), null)]
        public string QueryDescription; 

        [FieldNullValue(typeof(string), null)]
        public string ActionNeeded; 
    }
}
