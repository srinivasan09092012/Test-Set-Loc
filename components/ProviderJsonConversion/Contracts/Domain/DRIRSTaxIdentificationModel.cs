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
        public DateTime EffectiveDate
        {
            get
            {
                return this.OriginalEffectiveDate < DateMethods.GetMinDate() ? DateMethods.GetMinDate() : this.OriginalEffectiveDate;
            }
        }

        /// <summary>
        /// <para>EndDate</para>
        /// <para>Remarks: The end date of the IRS Tax Identification.</para>
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

        /// <summary>
        /// <para>OriginalEffectiveDate</para>
        /// <para>Remarks: The effective date of the IRS Tax Identification.</para>
        /// </summary>
        public DateTime OriginalEffectiveDate { get; set; }

        /// <summary>
        /// <para>OriginalEndDate</para>
        /// <para>Remarks: The end date of the IRS Tax Identification.</para>
        /// </summary>
        public DateTime OriginalEndDate { get; set; }
    }
}