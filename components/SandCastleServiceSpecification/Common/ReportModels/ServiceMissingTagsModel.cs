using FileHelpers;

namespace Common.ReportModels
{
    [DelimitedRecord(",")]
    public class ServiceMissingTagsModel
    {
        [FieldNullValue(typeof(string), null)]
        public string ModuleName; //Ex. ProviderManagement

        [FieldNullValue(typeof(string), null)]
        public string ServiceName; //Ex. ProviderSvc

        [FieldNullValue(typeof(string), null)]
        public string ServiceOperationName; //Ex. AddProvider

        [FieldNullValue(typeof(string), null)]
        public string ServiceOperationDescription; //Ex. Add a new provider to the system

        [FieldNullValue(typeof(string), null)]
        public string ActionNeeded; //Ex. Add a new provider to the system
    }
}
