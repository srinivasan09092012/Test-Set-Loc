using System.Collections.Generic;
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

        [XmlElement(ElementName = "MainPageContent")]
        public string MainPageContent;

        [XmlElement(ElementName = "ServiceListPages")]
        public ServiceListPages ServiceListPages;

        [XmlElement(ElementName = "MainContractContent")]
        public string MainContractContent;

        [XmlElement(ElementName = "ContractListPages")]
        public ContractListPages ContractListPages;

    }
}
