//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace UserAccountMigration.Domain
{
    public class UserVOSTag
    {
        public UserVOSTag() :
            base()
        {
        }

        public UserVOSTag(Guid tagId, string code, string typeCode, DateTime effectiveDate, DateTime endDate) :
            base()
        {
            this.UserVOSTagId = tagId;
            this.Code = code;
            this.TypeCode = typeCode;
            this.EffectiveDate = effectiveDate;
            this.EndDate = endDate;
        }

        public Guid UserVOSTagId { get; set; }

        public string Code { get; set; }

        public string TypeCode { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
