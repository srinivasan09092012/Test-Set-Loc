using FileHelpers;

namespace Common.ReportModels
{
    [DelimitedRecord(",")]
    public class DTOMissingTagsModel
    {
        [FieldNullValue(typeof(string), null)]
        public string ModuleName; 

        [FieldNullValue(typeof(string), null)]
        public string DTOName; 

        [FieldNullValue(typeof(string), null)]
        public string DTODescription; 

        [FieldNullValue(typeof(string), null)]
        public string ActionNeeded; 
    }
}
