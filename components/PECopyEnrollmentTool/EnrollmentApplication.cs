using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PECopyEnrollment
{
    public class EnrollmentApplication
    {
        public Guid PROVIDER_ENROLLMENT_ID { get; set; }

        public long? PROVIDER_TAX_ID { get; set; }

        public string PROVIDER_NPID { get; set; }

        public decimal ENROLLMENT_STATUS_ID { get; set; }

        public string ENROLLMENT_STATUS_RSN { get; set; }

        public DateTime CREATED_TS { get; set; }

        public DateTime? SUBMITTED_TS { get; set; }

        public DateTime? RESPONSE_TS { get; set; }

        public byte[] APPLICATION_DTL { get; set; }

        public string APPLICATION_RESUME_CD { get; set; }

        public DateTime LAST_MODIFIED_TS { get; set; }

        public string LAST_UPDATE_BY { get; set; }

        public string APPLICATION_RESUME_ID { get; set; }

        public string EMAIL_ADR { get; set; }

        public string PROVIDER_STATE { get; set; }

        public string PROVIDER_CITY { get; set; }

        public string PROVIDER_FIRST_NAME { get; set; }

        public string PROVIDER_LAST_NAME { get; set; }

        public string PROVIDER_MIDDLE_NAME { get; set; }

        public string PROVIDER_BUSINESS_NAME { get; set; }

        public string APPLICATION_TYPE { get; set; }

        public string LEGACY_PROVIDER_ID { get; set; }

        public string PROVIDER_TYPE { get; set; }

        public string ENROLLMENT_TYPE { get; set; }

        public string POSTAL_CD { get; set; }

        public string REASON_EXPIRED { get; set; }

        public string TAX_ID_TYPE { get; set; }

        public string PREFERRED_COMM_LOCALE_ID { get; set; }

        public DateTime? FINALIZED_TS { get; set; }

        public bool? IS_RTP { get; set; }

        public DateTime ENROLLMENT_UPDATED_TS { get; set; }

        public DateTime? RTP_TS { get; set; }
    }
}
