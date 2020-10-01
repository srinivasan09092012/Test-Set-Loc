//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

namespace WorkflowInstanceCleanupUtil.Models
{
    public class ProcessInfo
    {
        public ProcessInfo(int id, string folio)
        {
            this.ID = id;
            this.Folio = folio;
        }

        public int ID { get; set; }

        public string Folio { get; set; }
    }
}
