//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
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
