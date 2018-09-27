using System;

namespace UserAccountManager.Domain
{
    [Serializable]
    public class RegistrationQualifier
    {
        public RegistrationQualifier() :
            base()
        {
        }

        public RegistrationQualifier(string key, string value) :
            base()
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
