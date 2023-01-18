using System;
using System.Xml.Serialization;

namespace ServiceSpecificationTool.Domain
{
    [Serializable]
    public class ServiceEnvironments
    {
        [XmlAttribute("environmentName")]
        public string EnvironmentName { get; set; }

        [XmlAttribute("active")]
        public string Active { get; set; }

        [XmlAttribute("serverpath")]
        public string Serverpath { get; set; } 

        public void ValidateEnvironment()
        {
            if (string.IsNullOrWhiteSpace(this.EnvironmentName))
            {
                throw new ArgumentNullException("Service EnvironmentName has not been specified.");
            }

            if (string.IsNullOrWhiteSpace(this.Serverpath.ToString()))
            {
                throw new ArgumentNullException("Service Environment Serverpath has not been specified.");
            } 
        }
    }                 
}
