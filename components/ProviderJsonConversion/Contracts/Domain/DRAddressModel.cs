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
    public class DRAddressModel
    {
        /// <summary>
        /// <para>ProviderAddressId</para>
        /// <para>Remarks: The id that identifies the provider address .
        /// </para>
        /// </summary>
        [DataMember]
        public Guid ProviderAddressId { get; set; }

        /// <summary>
        /// <para>ServiceLocationId</para>
        /// <para>Remarks: Identifies the service location for the address.</para>
        /// </summary>
        [DataMember]
        public Guid ServiceLocationId { get; set; }

        /// <summary>
        /// <para>Line1</para>
        /// <para>Remarks: The line 1 for the given address.
        /// </para>
        /// </summary>
        [DataMember]
        public string Line1 { get; set; }

        /// <summary>
        /// <para>Line2</para>
        /// <para>Remarks: The line 2 for the given address.
        /// </para>
        /// </summary>
        [DataMember]
        public string Line2 { get; set; }

        /// <summary>
        /// <para>City</para>
        /// <para>Remarks: The city for the given address.
        /// </para>
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// <para>State</para>
        /// <para>Remarks: The state for the given address.
        /// </para>
        /// </summary>
        [DataMember]
        public string State { get; set; }

        /// <summary>
        /// <para>PostalCode</para>
        /// <para>Remarks: The postal code for the given address.
        /// </para>
        /// </summary>
        [DataMember]
        public string PostalCode { get; set; }       

        /// <summary>
        /// <para>EffectiveDate</para>
        /// <para>Remarks: The effective date of the address.</para>
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
        /// <para>Remarks: The end date of the adress.</para>
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
        /// <para>IsActive</para>
        /// <para>Remarks: Specifies whether the address is active.</para>
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }       

        /// <summary>
        /// <para>OriginalEffectiveDate</para>
        /// <para>Remarks: The effective date for the given address.
        /// </para>
        /// </summary>
        public DateTime OriginalEffectiveDate { get; set; }

        /// <summary>
        /// <para>OriginalEndDate</para>
        /// <para>Remarks: The end date for the given address.</para>
        /// </summary>
        public DateTime OriginalEndDate { get; set; }
    }
}