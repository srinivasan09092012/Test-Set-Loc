using System;

namespace InRuleRestLibrary
{
    public class ExecuteIndependentRuleSetRequest
    {
        public string RequestId { get; set; }
        public RuleApp RuleApp { get; set; }
        public RuleEngineServiceOptions RuleEngineServiceOptions { get; set; }
        public RuleEngineServiceOutputTypes RuleEngineServiceOutputTypes { get; set; }
        public string EntityState { get; set; }
        public string EntityStateFieldName { get; set; }
        public string RuleSetName { get; set; }
    }

    public class RuleApp
    {
        public int CacheTimeout { get; set; }
        public int ConnTimeout { get; set; }
        public string FileName { get; set; }
        public string Password { get; set; }
        public RepositoryRuleAppRevisionSpec RepositoryRuleAppRevisionSpec { get; set; }
        public string RepositoryServiceUri { get; set; }
        public bool UseIntegratedSecurity { get; set; }
        public string UserName { get; set; }
    }

    public class RepositoryRuleAppRevisionSpec
    {
        public string Guid { get; set; }
        public string Label { get; set; }
        public int Revision { get; set; }
        public string RuleApplicationName { get; set; }
    }

    public class RuleEngineServiceOptions
    {
        public RuleEngineServiceOverride[] Overrides { get; set; }
        public RuleSessionOverrides RuleSessionOverrides { get; set; }
    }

    public class RuleSessionOverrides
    {
        public string ExecutionTimeout { get; set; }
        public long MaxCycleCount { get; set; }
        public DateTime Now { get; set; }
    }

    public class RuleEngineServiceOverride
    {
        public bool AllowGetXmlWithDupInstances { get; set; }
        public bool AllowUntrustedCertificates { get; set; }
        public string AuthenticationType { get; set; }
        public string ConnectionString { get; set; }
        public string DefaultImportEntityDataType { get; set; }
        public string DisplayText { get; set; }
        public EmbeddedWsdl EmbeddedWsdl { get; set; }
        public bool EnableXsdValidation { get; set; }
        public string InlineXml { get; set; }
        public bool IsInternal { get; set; }
        public string LastImportedFileName { get; set; }
        public string OriginalSchemaLocation { get; set; }
        public string PortName { get; set; }
        public string Query { get; set; }
        public string RestServiceDomain { get; set; }
        public string RestServicePassword { get; set; }
        public string RestServiceRootUrl { get; set; }
        public string RestServiceUserName { get; set; }
        public string RestServiceX509CertificatePassword { get; set; }
        public string RestServiceX509CertificatePath { get; set; }
        public string Schema { get; set; }
        public bool SchemaIsActivated { get; set; }
        public string SchemaType { get; set; }
        public string ServerAddress { get; set; }
        public string ServiceName { get; set; }
        public string ServiceUriOverride { get; set; }
        public string Settings { get; set; }
        public string SortValue { get; set; }
        public string TableSettings { get; set; }
        public bool UseEmbeddedWsdl { get; set; }
        public bool UseEmbeddedXsd { get; set; }
        public string Value { get; set; }
        public ValuelistItem[] ValueListItems { get; set; }
        public int WebServiceMaxReceivedMessageSize { get; set; }
        public string WsdlUri { get; set; }
        public string X509Certificate { get; set; }
        public string XmlPath { get; set; }
        public string XsdPath { get; set; }
        public string OverrideType { get; set; }
        public string Name { get; set; }
    }

    public class EmbeddedWsdl
    {
        public string OriginalSchemaLocation { get; set; }
        public string Schema { get; set; }
        public int Type { get; set; }
    }

    public class ValuelistItem
    {
        public string DisplayText { get; set; }
        public string Value { get; set; }
    }

    public class RuleEngineServiceOutputTypes
    {
        public bool ActiveNotifications { get; set; }
        public bool ActiveValidations { get; set; }
        public bool EntityState { get; set; }
        public bool Overrides { get; set; }
        public bool RuleExecutionLog { get; set; }
    }

    public class ExecuteIndependentRuleSetResponse
    {
        public ActiveNotification[] ActiveNotifications { get; set; }
        public ActiveValidation[] ActiveValidations { get; set; }
        public string EntityState { get; set; }
        public bool HasRuntimeErrors { get; set; }
        public ResponseOverride[] Overrides { get; set; }
        public string RequestId { get; set; }
        public RuleExecutionLog RuleExecutionLog { get; set; }
        public string RuleSessionState { get; set; }
        public string SessionId { get; set; }
    }
    public class RuleExecutionLog
    {
        public Message[] Messages { get; set; }
        public long TotalEvaluationCycles { get; set; }
        public string TotalExecutionTime { get; set; }
        public int TotalTraceFrames { get; set; }
    }

    public class Message
    {
        public string Description { get; set; }
        public int ChangeType { get; set; }
        public int CollectionCount { get; set; }
        public string CollectionId { get; set; }
        public string MemberId { get; set; }
        public int MemberIndex { get; set; }
    }

    public class ActiveNotification
    {
        public string ElementId { get; set; }
        public bool IsActive { get; set; }
        public string Message { get; set; }
        public string NotificationType { get; set; }
    }

    public class ActiveValidation
    {
        public string ElementIdentifier { get; set; }
        public string InvalidMessageText { get; set; }
        public bool IsValid { get; set; }
        public Reason[] Reasons { get; set; }
    }

    public class Reason
    {
        public string FiringRuleId { get; set; }
        public string MessageText { get; set; }
    }

    public class ResponseOverride
    {
        public string Name { get; set; }
        public int OverrideType { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}
