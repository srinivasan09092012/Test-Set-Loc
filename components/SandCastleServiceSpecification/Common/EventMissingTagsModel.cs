using FileHelpers;

namespace Common
{
    [DelimitedRecord(",")]
    public class EventMissingTagsModel
    {
        [FieldNullValue(typeof(string), null)]
        public string ModuleName; 

        [FieldNullValue(typeof(string), null)]
        public string EventName; 

        [FieldNullValue(typeof(string), null)]
        public string EventDescription;

        [FieldNullValue(typeof(string), null)]
        public string ActionNeeded; 
    }
}
