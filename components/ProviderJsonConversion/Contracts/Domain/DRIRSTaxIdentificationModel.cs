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
    public class DRIRSTaxIdentificationModel
    {
        /// <summary>
        /// <para>_id</para>
        /// <para>Remarks: The id that identifies the IRS Tax Identification.</para>
        /// </summary>
        [DataMember]
        public Guid _id { get; set; }

        /// <summary>
        /// <para>TaxIdTypeCode</para>
        /// <para>Remarks: The type code that identifies the IRS Tax Identification.</para>
        /// </summary>
        [DataMember]
        public string TaxIdTypeCode { get; set; }

        /// <summary>
        /// <para>IRSTaxId</para>
        /// <para>Remarks: The id that identifies the IRS Tax Id.</para>
        /// </summary>
        [DataMember]
        public string IRSTaxId { get; set; }

        /// <summary>
        /// <para>TaxIdName</para>
        /// <para>Remarks: The id that identifies the Tax Name.</para>
        /// </summary>
        [DataMember]
        public string TaxIdName { get; set; }

        /// <summary>
        /// <para>EffectiveDate</para>
        /// <para>Remarks: The effective date of the IRS Tax Identification.</para>
        /// </summary>
        [DataMember]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// <para>EndDate</para>
        /// <para>Remarks: The end date of the IRS Tax Identification.</para>
        /// </summary>
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// <para>IsValid</para>
        /// <para>Remarks: Specifies whether the IRS Tax Identification is valid.</para>
        /// </summary>
        [DataMember]
        public bool IsValid { get; set; }

        /// <summary>
        /// <para>Version</para>
        /// <para>Remarks: The version number of the IRS Tax Identification</para>
        /// </summary>
        [DataMember]
        public int Version { get; set; }
    }
}