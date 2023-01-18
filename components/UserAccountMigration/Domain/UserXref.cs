//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [FieldOrder(5)]
        public bool IsAssociationAdmin { get; set; }

        [FieldOrder(6)]
        public string AssociatedFunctions { get; set; }

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
                else if (this.AssociationId != this.AssociationId.ToLower())
                {
                    throw new ArgumentException("AssociationId Guid must be lower case");
                }
            }

            if (this.IsAssociationAdmin)
            {
                if (!string.IsNullOrEmpty(this.AssociatedFunctions))
                {
                    throw new ArgumentException("Admin has should not have Associated Functions");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.AssociatedFunctions))
                {
                    if (this.IsAssociationActive)
                    {
                        throw new ArgumentException("Non-Admin has no Associated Functions");
                    }
                }
                else
                {
                    List<string> assocFunctionList = this.AssociatedFunctions.Split('|').ToList();
                    Guid id;
                    foreach (string assoc in assocFunctionList)
                    {
                        if (!Guid.TryParse(assoc, out id))
                        {
                            throw new ArgumentException("Associated Functions have invalid Guid(s)");
                        }
                        else if (assoc != assoc.ToLower())
                        {
                            throw new ArgumentException("Associated Functions Guid(s) must be lower case");
                        }
                    }
                }
            }
        }
    }
}