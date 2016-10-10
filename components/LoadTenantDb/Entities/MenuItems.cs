using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TM_MENU_ITEM")]
    public partial class MenuItem
    {
        [Key]
        [Column("TM_MENU_ITEM_ID")]
        public Guid MenuItemId { get; set; }

        [Required]
        [Column("TM_MENU_ID")]
        public Guid MenuId { get; set; }

        [Column("PARENT_MENU_ITEM_ID")]
        public Guid ParentMenuItemId { get; set; }

        [Required]
        [Column("SECURITY_RIGHT_ITEM_ID")]
        public Guid SecurityRightItemId { get; set; }

        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Column("ORDER_INDEX")]
        [StringLength(100)]
        public string OrderIndex { get; set; }

        [Required]
        [Column("IS_VISIBLE")]
        public bool IsVisible { get; set; }

        [Column("DEFAULT_TEXT")]
        [StringLength(4000)]
        public string DefaultText { get; set; }

        [Required]
        [Column("CSS_CLASS")]
        [StringLength(100)]
        public string CssClass { get; set; }

        [Column("IOC_CONTAINER")]
        [StringLength(100)]
        public string IocContainer { get; set; }

        [Column("LABEL_ITEM_KEY")]
        [StringLength(200)]
        public string LabelItemContentId { get; set; }

        [Required]
        [Column("MODULE_SECTION_ITEM_KEY")]
        [StringLength(200)]
        public string ModuleSectionContentId { get; set; }

        [Required]
        [Column("BASE_URL")]
        [StringLength(100)]
        public string BaseUrl { get; set; }

        [Required]
        [Column("REPORTS_CONTENT_URL")]
        [StringLength(100)]
        public string ReportsContentUrl { get; set; }

        [Required]
        [Column("PRINT_PREVIEW_CONTENT_URL")]
        [StringLength(100)]
        public string Print_Preview_ContentUrl { get; set; }

        [Required]
        [Column("PAGE_HELP_HTML_CONTENT_ID")]
        [StringLength(100)]
        public string PageHelpContentId { get; set; }

        [Required]
        [Column("MITA_HELP_HTML_CONTENT_ID")]
        [StringLength(100)]
        public string MitaHelpContentId { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Column("OPERATOR_ID")]
        [StringLength(50)]
        public string OperatorId { get; set; }

        [Column("LAST_MODIFIED_TS")]
        public DateTime LastModified { get; set; }

        [Column("TENANT_ID")]
        public Guid TenantId { get; set; }
    }
}
