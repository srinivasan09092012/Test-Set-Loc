//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Runtime.Serialization;

namespace Contracts.Domain
{
    /// <summary>
    /// <para>Specifies that the type defines or implements a data contract and is serializable by a serializer </para>
    /// </summary>
    [DataContract]
    public class DRTaxonomyModel
    {
        /// <summary>
        /// <para>ProviderTaxonomyId</para>
        /// <para>Remarks: The id that identifies the taxonomy.</para>
        /// </summary>
        [DataMember]
        public Guid ProviderTaxonomyId { get; set; }

        /// <summary>
        /// <para>EffectiveDate</para>
        /// <para>Remarks: The effective date of the taxonomy.</para>
        /// </summary>
        [DataMember]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// <para>EndDate</para>
        /// <para>Remarks: The end date of the taxonomy.</para>
        /// </summary>
        [DataMember]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// <para>IsActive</para>
        /// <para>Remarks: Specifies wheter the taxonomy is active.</para>
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// <para>TaxonomyCode</para>
        /// <para>Remarks: The code of the taxonomy.</para>
        /// </summary>
        [DataMember]
        public string TaxonomyCode { get; set; }
    }
}