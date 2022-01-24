using System.Xml.Serialization;

namespace Common.ModuleSettings
{
    [XmlRoot(ElementName = "ServiceListPages")]
    public class ServiceListPages: ContentPage
    {
    }

    [XmlRoot(ElementName = "ContractListPages")]
    public class ContractListPages: ContentPage
    {
    }

    [XmlRoot(ElementName = "ModuleAPIPages")]
    public class ModuleAPIPages : ContentPage
    {
    }

    [XmlRoot(ElementName = "DocModuleSettingModel")]
    public class ModuleSettingModel
    {
        [XmlElement(ElementName = "ModuleName")]
        public string ModuleName;

        [XmlElement(ElementName = "ModuleNameDisplay")]
        public string ModuleNameDisplay;

        [XmlElement(ElementName = "StorageDrive")]
        public string StorageDrivePath;

        [XmlElement(ElementName = "webSourcePath")]
        public string WebSourcePath;

        [XmlElement(ElementName = "webTargetPath")]
        public string WebTargetPath;

        [XmlElement(ElementName = "WebRoutingTargetPath")]
        public string WebRoutingTargetPath;

        [XmlElement(ElementName = "webPortalPath")]
        public string WebPortalPath;

        [XmlElement(ElementName = "MainPageContent")]
        public string MainPageContent;

        [XmlElement(ElementName = "WebHost")]
        public string WebHost;

        [XmlElement(ElementName = "WebHostPhysicalPath")]
        public string WebHostPhysicalPath;

        [XmlElement(ElementName = "ServiceListPages")]
        public ServiceListPages ServiceListPages;

        [XmlElement(ElementName = "MainContractContent")]
        public string MainContractContent;

        [XmlElement(ElementName = "ContractListPages")]
        public ContractListPages ContractListPages;

        [XmlElement(ElementName = "OtherResourcesFolder")]
        public string OtherResourcesFolder;

        [XmlElement(ElementName = "ModuleHelpContentAvailable")]
        public bool ModuleHelpContentAvailable;

        [XmlElement(ElementName = "ModuleAPIAvailable")]
        public bool ModuleAPIAvailable;

        [XmlElement(ElementName = "ModuleAPISourcePath")]
        public string ModuleAPISourcePath;

        [XmlElement(ElementName = "ModuleAPITargetPath")]
        public string ModuleAPITargetPath;

        [XmlElement(ElementName = "ModuleAPIPages")]
        public ModuleAPIPages ModuleAPIPages;

        [XmlElement(ElementName = "MainAPIContent")]
        public string MainAPIContent;

        [XmlElement(ElementName = "EventsExtractFile")]
        public string EventsExtractFile;

        [XmlElement(ElementName = "BuildingDate")]
        public string BuildingDate;
    }
}
