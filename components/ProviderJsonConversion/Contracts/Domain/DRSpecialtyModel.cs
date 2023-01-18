//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Extensions;
using System;
using System.Runtime.Serialization;

namespace Contracts.Domain
{
    /// <summary>
    /// <para>Specifies that the type defines or implements a data contract and is serializable by a serializer </para>
    /// </summary>
    [DataContract]
    public class DRSpecialtyModel
    {
        /// <summary>
        /// <para>ProviderSpecialtyId</para>
        /// <para>Remarks: The id that identifies the specialty.</para>
        /// </summary>
        [DataMember]
        public Guid ProviderSpecialtyId { get; set; }

        /// <summary>
        /// <para>SpecialtyCode</para>
        /// <para>Remarks: The code for the specialty.</para>
        /// </summary>
        [DataMember]
        public string SpecialtyCode { get; set; }        

        /// <summary>
        /// <para>EffectiveDate</para>
        /// <para>Remarks: The effective date of the specialty.</para>
        /// </summary>
        [DataMember]
        public DateTime EffectiveDate
        {
            get
            {
                return this.OriginalEffectiveDate < DateMethods.GetMinDate() ? DateMethods.GetMinDate() : this.OriginalEffectiveDate;
            }
        }

        /// <summary>
        /// <para>EndDate</para>
        /// <para>Remarks: The end date of the specialty.</para>
        /// </summary>
        [DataMember]
        public DateTime EndDate
        {
            get
            {
                return this.OriginalEndDate < DateMethods.GetMinDate() ? DateMethods.GetMinDate() : this.OriginalEndDate;
            }
        }

        /// <summary>
        /// <para>IsPrimary</para>
        /// <para>Remarks: Specifies wheter the specialty is primary.</para>
        /// </summary>
        [DataMember]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// <para>IsActive</para>
        /// <para>Remarks: Specifies wheter the specialty is active.</para>
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }        

        /// <summary>
        /// <para>TaxonomyCode</para>
        /// <para>Remarks: The taxonomy code associated with the specialty.</para>
        /// </summary>
        [DataMember]
        public string TaxonomyCode { get; set; }

        /// <summary>
        /// <para>OriginalEffectiveDate</para>
        /// <para>Remarks: The original effective date of the specialty before being validated with min date.</para>
        /// </summary>
        public DateTime OriginalEffectiveDate { get; set; }

        /// <summary>
        /// <para>OriginalEndDate</para>
        /// <para>Remarks: The original end date of the specialty before being validated with min date.</para>
        /// </summary>
        public DateTime OriginalEndDate { get; set; }
    }
}