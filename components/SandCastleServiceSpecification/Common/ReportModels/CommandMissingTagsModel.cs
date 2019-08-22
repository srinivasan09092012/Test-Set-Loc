using FileHelpers;

namespace Common.ReportModels
{
    [DelimitedRecord(",")]
    public class CommandMissingTagsModel
    {
        [FieldNullValue(typeof(string), null)]
        public string ModuleName; 

        [FieldNullValue(typeof(string), null)]
        public string CommandName; 

        [FieldNullValue(typeof(string), null)]
        public string CommandDescription; 

        [FieldNullValue(typeof(string), null)]
        public string ActionNeeded; 
    }
}
