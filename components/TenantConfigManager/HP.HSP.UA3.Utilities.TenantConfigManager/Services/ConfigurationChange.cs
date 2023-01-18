using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Services
{
    public enum ChangeType
    {
        Create = 0,
        Update = 1,
        Delete = 2
    }

    [DebuggerDisplay("{Description} in Module {Module} for Tenant {Tenant}")]
    public abstract class ConfigurationChange
    {
        public ChangeType ChangeType { get; protected set; }

        public string ItemType { get; set; }

        public string ItemId { get; set; }

        public int Order { get; set; }

        public string Tenant { get; set; }

        public string Module { get; set; }

        public static ConfigurationChange Created(object created)
        {
            return new CreateItemChange() { Added = created };            
        }

        public static ConfigurationChange Deleted(object deleted)
        {
            return new DeleteItemChange() { Deleted = deleted };
        }

        public static ConfigurationChange Updated(object original, object updated)
        {
            return new UpdateItemChange() { Updated = updated, Original = original };
        }

        public abstract object GetObject();
    }

    public class CreateItemChange : ConfigurationChange
    {
        public CreateItemChange()
        {
            base.ChangeType = Services.ChangeType.Create;
        }

        public object Added { get; set; }

        public override object GetObject()
        {
            return this.Added;
        }
    }

    public class DeleteItemChange : ConfigurationChange
    {
        public DeleteItemChange()
        {
            base.ChangeType = Services.ChangeType.Delete;
        }
        public object Deleted { get; set; }

        public override object GetObject()
        {
            return this.Deleted;
        }
    }

    public class UpdateItemChange : ConfigurationChange
    {
        public UpdateItemChange()
        {
            base.ChangeType = Services.ChangeType.Update;
        }

        public object Original { get; set; }
        public object Updated { get; set; }

        public override object GetObject()
        {
            return this.Original;
        }
    }
}
