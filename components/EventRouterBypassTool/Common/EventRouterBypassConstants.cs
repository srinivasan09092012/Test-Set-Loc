//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

namespace EventRouterByPassTool.Common
{
    public static class EventRouterBypassConstants
    {
        public static class DatabaseRelated
        {
            public const string Commercial = "Commercial";
            public const string ConnectionStringWrk = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = mapsdevdbo02.dev.mapshc.com)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = AWRK_PDBB)));User ID=UA3_INTEGRATION;Password=UA3_INTEGRATION;Self Tuning=false;PERSIST SECURITY INFO=True";
            public const string DeleteQueryOracle = "DELETE FROM {0}.{1} WHERE GROUP_ID = HEXTORAW(('{2}'))";
            public const string DeleteQuerySql = "DELETE FROM {0}.{1} WHERE GROUP_ID = ('{2}')";
            public const string EventSubscriptionErrorEntitySet = "EventSubscriptionErrors";
            public const string EventSubscriptionsErrorTable = "EVENT_SUBSCRIPTIONS_ERR";
            public const string ExternalModule = "External";
            public const string ExternalReqEntity = "ExternalReqs";
            public const string ExternalReqTable = "EXTERNAL_REQ";
            public const string INXSchema = "UA3_INTEGRATION";
            public const string Medicaid = "Medicaid";
            public const string Module = "MODULE";
            public const string ModuleEntitySet = "Modules";
            public const string ModuleEventEntitySet = "ModuleEvents";
            public const string ModuleEvents = "MODULE_EVENTS";
            public const string ModuleEventSubscriptionFilter = "ModuleEventSubscriptionFilter";
            public const string ModuleEventSubscriptionFilterTable = "MODULE_EVENTS_SUBSCR_FILTER";
            public const string ModuleEventSubscriptions = "ModuleEventSubscription";
            public const string ModuleEventSubscriptionTable = "MODULE_EVENTS_SUBSCRIPTION";
            public const string MyConnectionStringsSection = "myConnectionStrings";
            public const string MyModulesSection = "myModules";
            public const string InvalidModule = "Extaernal";
            public const string OracleProviderName = "Oracle.ManagedDataAccess.Client";
            public const string ResTable = "RES";
            public const string ReqTable = "REQ";
            public const string SqlServerProviderName = "System.Data.SqlClient";
            public const string UA3InxSchema = "UA3_INTEGRATION";
        }

        public static class DataListConstants
        {
            public const string Add = "add";
            public const string Clear = "clear";
            public const string Name = "name";
            public const string Remove = "remove";
            public const string Value = "value";
            public const string Values = "values";
        }

        public static class Messages
        {
            public const string AllowMultipleAppSetting = "AllowMultiple";
            public const string BulkInserSuccessful = "Bulk insert successful";
            public const string DeleteData = "Data deleted";
            public const string DeleteDataMessage = "Data deleted successfully.";
            public const string ErrorCodeBusinessException = "504";
            public const string ErrorMessage = "Error simulated through the EventRouter bypass tool";
            public const string ErrorReadingXml = "Error loading {0}: ";
            public const string FailStatus = "Fail: {0}";
            public const string FileDirectoryAppSetting = "FileDirectory";
            public const string SuccessStatus = "Success";
            public const string TestConstant = "Test";
            public const string TimeTakenOverall = "Overall time taken: {0}";
            public const string TimeTakenReq = "Time taken for Req table: {0}";
            public const string TimeTakenRes = "Time taken for Res table: {0}";
        }

        public static class XmlAttributes
        {
            public const string AggregateRootId = "AggregateRootID";
            public const string AggregateRootIdPayloadFormat = "a:AggregateRootID";
            public const string AggregateRootType = "AggregateRootType";
            public const string AggregateRootTypeDefault = "TestAggregate";
            public const string AggregateRootTypePayloadFormat = "a:AggregateRootType";
            public const string AggregateRootTypeLower = "aggregateroottype";
            public const string CommitIdLower = "commitid";
            public const string EventFilterMetadata = "EventFilterMetadata";
            public const string EventFilterMetadataPayloadFormat = "a:EventFilterMetadata";
            public const string EventNameAttribute = "EventNameAttribute";
            public const string EventNameSpaceAttribute = "EventNameSpaceAttribute";
            public const string INGroupId = "INGroupId";
            public const string INGroupIdPayloadFormat = "a:INGroupId";
            public const string INGroupIdToLower = "ingroupid";
            public const string IsLastFromGroup = "IsLastFromGroup";
            public const string IsLastFromGroupPayloadFormat = "a:IsLastFromGroup";
            public const string XmlFilter = "Xml Files (*.xml)|*.xml";
            public const string XmlnsAttribute = "xmlns";
        }
    }
}
