//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight;

namespace ViewProjectionLoader.ViewModel
{
    public class TableViewModel : ViewModelBase
    {
        private bool isChecked = false;
        private string name = "";

        public bool IsChecked
        {
            get { return this.isChecked; }
            set { this.Set("IsChecked", ref this.isChecked, value); }
        }

        public string Name
        {
            get { return this.name; }
            set { this.Set("Name", ref this.name, value); }
        }
    }
}
