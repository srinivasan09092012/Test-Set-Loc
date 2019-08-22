using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.ModuleSettings
{
    [XmlRoot(ElementName = "ServiceListPages")]
    public class ServiceListPages
    {
        [XmlElement(ElementName = "ServiceListPage")]
        public List<string> ServiceListPage { get; set; }
    }

    [XmlRoot(ElementName = "ContractListPages")]
    public class ContractListPages
    {
        [XmlElement(ElementName = "ContractListPage")]
        public List<string> ContractListPage { get; set; }
    }

    [XmlRoot(ElementName = "DocModuleSettingModel")]
    public class ModuleSettingModel
    {
        [XmlElement(ElementName = "ModuleName")]
        public string ModuleName { get; set; }

        [XmlElement(ElementName = "ModuleNameDisplay")]
        public string ModuleNameDisplay { get; set; }

        [XmlElement(ElementName = "StorageDrive")]
        public string StorageDrive { get; set; }

        [XmlElement(ElementName = "webSourcePath")]
        public string WebSourcePath { get; set; }

        [XmlElement(ElementName = "webTargetPath")]
        public string WebTargetPath { get; set; }

        [XmlElement(ElementName = "MainPageContent")]
        public string MainPageContent { get; set; }

        [XmlElement(ElementName = "ServiceListPages")]
        public ServiceListPages ServiceListPages { get; set; }

        [XmlElement(ElementName = "MainContractContent")]
        public string MainContractContent { get; set; }

        [XmlElement(ElementName = "ContractListPages")]
        public ContractListPages ContractListPages { get; set; }

    }
}
