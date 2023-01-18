//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts.Domain
{
    /// <summary>
    /// <para>Specifies that the type defines or implements a data contract and is serializable by a serializer </para>
    /// </summary>
    [DataContract]
    public class DRProviderServiceLocationModel
    {
        public DRProviderServiceLocationModel()
        {
            this.ProviderAddresses = new List<DRAddressModel>();
            this.ProviderSpecialties = new List<DRSpecialtyModel>();
            this.ProviderTaxonomies = new List<DRTaxonomyModel>();
            this.ProviderIRSTaxIdAssociations = new List<DRIRSTaxIdAssociationsModel>();
        }

        /// <summary>
        /// <para>ProviderServiceLocationId</para>
        /// <para>Remarks: The id that identifies the Provider Service Location.</para>
        /// </summary>
        [DataMember]
        public Guid ProviderServiceLocationId { get; set; }

        /// <summary>
        /// <para>ProviderTypeCode</para>
        /// <para>Remarks: The type code for the provider service location.</para>
        /// </summary>
        [DataMember]
        public string ProviderTypeCode { get; set; }

        /// <summary>
        /// <para>ProviderBillingId</para>
        /// <para>Remarks: The billing id for the provider service location.</para>
        /// </summary>
        [DataMember]
        public string ProviderBillingId { get; set; }        

        /// <summary>
        /// <para>EffectiveDate</para>
        /// <para>Remarks: The effective date of the provider service location.</para>
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
        /// <para>Remarks: The end date of the provider service location.</para>
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
        /// <para>Remarks: Specifies whether the provider service location is active.</para>
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// <para>LongNPI</para>
        /// <para>Remarks: The NPI of the provider service location with the long type</para>
        /// </summary>
        public long? LongNPI { get; set; }

        /// <summary>
        /// <para>NPI</para>
        /// <para>Remarks: The NPI of the provider service location with the string version of longNPI</para>
        /// </summary>
        [DataMember]
        public string NPI
        {
            get
            {
                return this.LongNPI != null ? this.LongNPI.Value.ToString() : string.Empty;
            }
        }

        /// <summary>
        /// <para>ProviderAddresses</para>
        /// <para>Remarks: The list of addresses for the provider service location.</para>
        /// </summary>
        [DataMember]
        public List<DRAddressModel> ProviderAddresses { get; set; }

        /// <summary>
        /// <para>ProviderSpecialties</para>
        /// <para>Remarks: The list of specialties for the provider service location.</para>
        /// </summary>
        [DataMember]
        public List<DRSpecialtyModel> ProviderSpecialties { get; set; }

        /// <summary>
        /// <para>ProviderTaxonomies</para>
        /// <para>Remarks: The list of taxonomies for the provider service location.</para>
        /// </summary>
        [DataMember]
        public List<DRTaxonomyModel> ProviderTaxonomies { get; set; }

        /// <summary>
        /// <para>ProviderIRSTaxIdAssociations</para>
        /// <para>Remarks: The list of IRS Tax Id Associations for the provider service location.</para>
        /// </summary>
        [DataMember]
        public List<DRIRSTaxIdAssociationsModel> ProviderIRSTaxIdAssociations { get; set; }

        /// <summary>
        /// <para>PracticeLocationId</para>
        /// <para>Remarks: The practice location Id of the provider service location.\</para>
        /// </summary>
        public Guid PracticeLocationId { get; set; }

        /// <summary>
        /// <para>OriginalEffectiveDate</para>
        /// <para>Remarks: The effective date of the provider service location.</para>
        /// </summary>
        public DateTime OriginalEffectiveDate { get; set; }

        /// <summary>
        /// <para>OriginalEndDate</para>
        /// <para>Remarks: The end date of the provider service location.</para>
        /// </summary>
        public DateTime OriginalEndDate { get; set; }
    }
}