using System;
using System.Configuration;

namespace BatchDirectoryCheck
{
        public class CustomConfigurationElementDbServerCollection : ConfigurationElementCollection
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new CustomConfigurationElementDbServers();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                CustomConfigurationElementDbServers customElement = element as CustomConfigurationElementDbServers;
                if (customElement != null)
                {
                    return customElement.Environment;
                }

                return String.Empty;
            }

            public CustomConfigurationElementDbServers this[int index]
            {
                get { return base.BaseGet(index) as CustomConfigurationElementDbServers; }
                set
                {
                    if (base.BaseGet(index) != null)
                    {
                        base.BaseRemoveAt(index);
                    }

                    base.BaseAdd(index, value);
                }
            }
        }

        public class CustomConfigurationSection : ConfigurationSection
        {
            [ConfigurationProperty("dbServers")]
            public CustomConfigurationElementDbServerCollection CustomConfigurationsDbServers
            {
                get
                {
                    CustomConfigurationElementDbServerCollection collection = base["dbServers"] as CustomConfigurationElementDbServerCollection;
                    return collection ?? new CustomConfigurationElementDbServerCollection();
                }
            }
        }

        public class CustomConfigurationElementDbServers : ConfigurationElement
        {
            [ConfigurationProperty("type", IsRequired = true)]
            public string Type
            {
                get
                {
                    string value = base["type"] as String;
                    return value ?? String.Empty;
                }
            }

            [ConfigurationProperty("environment", IsRequired = true)]
            public string Environment
            {
                get
                {
                    string value = base["environment"] as String;
                    return value ?? String.Empty;
                }
            }

            [ConfigurationProperty("databaseService", IsRequired = true)]
            public string DatabaseService
            {
                get
                {
                    string value = base["databaseService"] as String;
                    return value ?? String.Empty;
                }
            }

            [ConfigurationProperty("databaseHost", IsRequired = true)]
            public string DatabaseHost
            {
                get
                {
                    string value = base["databaseHost"] as String;
                    return value ?? String.Empty;
                }
            }

            [ConfigurationProperty("databasePort", IsRequired = false)]
            public string DatabasePort
            {
                get
                {
                    string value = base["databasePort"] as String;
                    return value ?? String.Empty;
                }
            }

            [ConfigurationProperty("databaseInitialCatalog", IsRequired = false)]
            public string DatabaseInitialCatalog
            {
                get
                {
                    string value = base["databaseInitialCatalog"] as String;
                    return value ?? String.Empty;
                }
            }

            [ConfigurationProperty("serviceUrl", IsRequired = true)]
            public string ServiceUrl
            {
                get
                {
                    string value = base["serviceUrl"] as String;
                    return value ?? String.Empty;
                }
            }

            [ConfigurationProperty("serviceVersion", IsRequired = false)]
            public string ServiceVersion
            {
                get
                {
                    string value = base["serviceVersion"] as String;
                    return value ?? String.Empty;
                }
            }

            [ConfigurationProperty("protectDataSource", IsRequired = false)]
            public string ProtectDataSource
            {
                get
                {
                    string value = base["protectDataSource"] as String;
                    return value ?? String.Empty;
                }
            }
        }
}
