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
    public class DRIRSTaxIdAssociationsModel
    {
        /// <summary>
        /// <para>ProviderIRSTaxIdAssociationId</para>
        /// <para>Remarks: The id that identifies the IRS Tax Association.</para>
        /// </summary>
        [DataMember]
        public Guid ProviderIRSTaxIdAssociationId { get; set; }

        /// <summary>
        /// <para>IRSTaxIdentificationId</para>
        /// <para>Remarks: The id that identifies the IRS Tax Identification.</para>
        /// </summary>
        [DataMember]
        public Guid IRSTaxIdentificationId { get; set; }

        /// <summary>
        /// <para>EffectiveDate</para>
        /// <para>Remarks: The effective date of the IRS Tax Association.</para>
        /// </summary>
        [DataMember]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// <para>EndDate</para>
        /// <para>Remarks: The end date of the IRS Tax Association.</para>
        /// </summary>
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// <para>IsActive</para>
        /// <para>Remarks: Specifies whether the IRS Tax Association is active.</para>
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }
    }
}