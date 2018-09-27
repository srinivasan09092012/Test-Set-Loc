using System;

namespace UserAccountManager.Domain
{
    [Serializable]
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
