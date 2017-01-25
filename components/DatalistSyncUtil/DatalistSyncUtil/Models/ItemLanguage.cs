using System;

namespace DatalistSyncUtil
{
    [Serializable]
    public class ItemLanguage
    {
        public string ContentID { get; set; }

        public string Code { get; set; }

        public string LocaleID { get; set; }

        public string Description { get; set; }

        public bool DescriptionModified { get; set; }

        public string LongDescription { get; set; }

        public bool LongDescriptionModified { get; set; }

        public string Status { get; set; }
    }
}