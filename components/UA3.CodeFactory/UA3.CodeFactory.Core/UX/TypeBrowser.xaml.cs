//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.ObjectModel;
using System.Windows;

namespace UA3.CodeFactory.Core.UX
{
    /// <summary>
    /// Interaction logic for TypeBrowser.xaml
    /// </summary>
    public partial class TypeBrowser : Window
    {
        public TypeBrowser()
        {
            InitializeComponent();
            this.AvailableTypes = new ObservableCollection<Node>();
            this.DataContext = this;
        }

        public Node SelectedType
        {
            get
            {
                return this.tvItems.SelectedItem as Node;
            }
        }

        public ObservableCollection<Node> AvailableTypes { get; set; }

        private void OnOK(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
