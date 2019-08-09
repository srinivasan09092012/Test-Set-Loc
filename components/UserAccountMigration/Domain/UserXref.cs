//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using FileHelpers;
using System;

namespace UserAccountMigration.Domain
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst(1)]
    public class UserXref
    {
        public UserXref() { }

        [FieldOrder(1)]
        public string PrimaryUserName { get; set; }

        [FieldOrder(2)]
        public string SecondaryUserName { get; set; }

        [FieldOrder(3)]
        public string AssociationId { get; set; }

        [FieldOrder(4)]
        public bool IsAssociationActive { get; set; }

        public void Validate(int delegateCount)
        {
            if (string.IsNullOrWhiteSpace(this.PrimaryUserName))
            {
                throw new ArgumentNullException("PrimaryUserName");
            }

            if (string.IsNullOrWhiteSpace(this.SecondaryUserName) && delegateCount == 0)
            {
                throw new ArgumentNullException("SecondaryUserName");
            }

            if (this.PrimaryUserName == this.SecondaryUserName)
            {
                throw new ArgumentException("PrimaryUserName cannot equal SecondaryUserName");
            }

            if (string.IsNullOrWhiteSpace(this.AssociationId))
            {
                throw new ArgumentNullException("AssociationId");
            }
            else
            {
                Guid id;
                if (!Guid.TryParse(this.AssociationId, out id))
                {
                    throw new ArgumentException("AssociationId invalid Guid");
                }
            }
        }
    }
}
