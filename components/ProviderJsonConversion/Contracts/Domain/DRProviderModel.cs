//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts.Domain
{
    /// <summary>
    /// <para>Specifies that the type defines or implements a data contract and is serializable by a serializer </para>
    /// </summary>
    [DataContract]
    public class DRProviderModel
    {
        public DRProviderModel()
        {
            this.ProviderServiceLocations = new List<DRProviderServiceLocationModel>();
        }

        /// <summary>
        /// <para>_id</para>
        /// <para>Remarks: The id that identifies the Provider.</para>
        /// </summary>
        [DataMember]
        public Guid _id { get; set; }        

        /// <summary>
        /// <para>BaseId</para>
        /// <para>Remarks: The Base Id for the Provider.</para>
        /// </summary>
        [DataMember]
        public string BaseId { get; set; }

        /// <summary>
        /// <para>BusinessName</para>
        /// <para>Remarks: The business name of the Provider.</para>
        /// </summary>
        [DataMember]
        public string BusinessName { get; set; }

        /// <summary>
        /// <para>IsActive</para>
        /// <para>Remarks: Specifies whether the Provider is active.</para>
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// <para>ProviderCategoryCode</para>
        /// <para>Remarks: The provider category code for the provider.</para>
        /// </summary>
        [DataMember]
        public string ProviderCategoryCode { get; set; }

        /// <summary>
        /// <para>ProviderServiceLocations</para>
        /// <para>Remarks: The list of the service locations for the provider.</para>
        /// </summary>
        [DataMember]
        public List<DRProviderServiceLocationModel> ProviderServiceLocations { get; set; }
    }
}