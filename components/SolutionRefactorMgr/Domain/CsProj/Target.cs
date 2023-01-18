using System.Collections.Generic;
using System.Xml.Serialization;

namespace SolutionRefactorMgr.Domain.CsProj
{
    public class Target
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("BeforeTargets")]
        public string BeforeTargets { get; set; }

        [XmlAttribute("AfterTargets")]
        public string AfterTargets { get; set; }

        [XmlAttribute("DependsOnTargets")]
        public string DependsOnTargets { get; set; }

        [XmlAttribute("Condition")]
        public string Condition { get; set; }

        public List<Error> ErrorList { get; set; }

        public List<PropertyElement> PropertyGroup { get; set; }

        public void Validate()
        {
        }
    }
}
