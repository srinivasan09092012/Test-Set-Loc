//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPP.OneClick.XAML.Migration.DevContext
{
    [Table("UA3_DEVOPS.PRODUCT")]
    public partial class PRODUCT
    {
        public PRODUCT()
        {
            this.JOB_CONFIG = new HashSet<JOB_CONFIG>();
        }

        [Key]
        public Guid PRODUCT_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string PRODUCT_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string OPERATOR_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CREATED_TS { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LAST_MODIFIED_TS { get; set; }

        public virtual ICollection<JOB_CONFIG> JOB_CONFIG { get; set; }
    }
}
