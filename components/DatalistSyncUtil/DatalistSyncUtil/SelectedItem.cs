using System;

namespace DatalistSyncUtil
{
    public class SelectedItem
    {
        public string ContentID { get; set; }

        public string Code { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }

        public bool Selected { get; set; }

        public Guid ID { get; set; }
    }
}
