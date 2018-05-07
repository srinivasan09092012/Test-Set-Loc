//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Ioc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UA3.CodeFactory.Core.Services;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace UA3.CodeFactory.Core.UX
{
    /// <summary>
    /// Interaction logic for TypeSelector.xaml
    /// </summary>
    public partial class TypeEditor : UserControl, ITypeEditor
    {
        public TypeEditor()
        {
            InitializeComponent();
            this.SolutionProvider = SimpleIoc.Default.GetInstance<ISolutionProvider>();
        }

        public ISolutionProvider SolutionProvider { get; private set; }

        public Node Value
        {
            get { return (Node)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Node), typeof(TypeEditor), new PropertyMetadata(0));

        protected Func<ClassDeclarationSyntax, bool> Filter { get; set; }

        private void OnClick(object sender, RoutedEventArgs args)
        {
            var sln = this.SolutionProvider.GetSolution();

            //var types = sln.GetAllClasses(this.Filter);

            TypeBrowser win = new TypeBrowser();

            //temp.ForEach(p => win.AvailableTypes.Add(p));

            if (win.ShowDialog() == true)
            {
                this.Value = win.SelectedType;
                if (this.Value != null)
                {
                    MessageBox.Show($"Selected {this.Value.Name}");
                }
            }
        }

        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            Binding binding = new Binding("Value");
            binding.Source = propertyItem;
            binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(this, TypeEditor.ValueProperty, binding);

            return this;
        }
    }
}
