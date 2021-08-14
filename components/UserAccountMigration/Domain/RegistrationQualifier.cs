//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//------------------------------------------------------------------------------------------

namespace UserAccountMigration.Domain
{
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
