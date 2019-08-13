using FileHelpers;

namespace Common
{
    [DelimitedRecord(",")]
    public class ModelMissingTagsModel
    {
        [FieldNullValue(typeof(string), null)]
        public string ModuleName; 

        [FieldNullValue(typeof(string), null)]
        public string ModelName; 

        [FieldNullValue(typeof(string), null)]
        public string ModelDescription; 

        [FieldNullValue(typeof(string), null)]
        public string ActionNeeded; 
    }
}
