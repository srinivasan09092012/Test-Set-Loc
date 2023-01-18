//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.CodeAnalysis.ItemSources;
using UA3.CodeFactory.Core.PluginModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Plugins.AddServiceQuery
{
    [DisplayName("Settings - Add Query")]
    public class AddServiceQuerySettings : SettingsModel, IValidatableObject
    {
        public AddServiceQuerySettings()
        {
            this.QueryEntityOption = QueryEntityOption.Unknown;
        }

        [Category("Service API")]
        [Description("This is the class and/or interface that will receive the generated operation")]
        [DisplayName("Query Service")]
        [Required(ErrorMessage = "Query Service is required")]
        [ItemsSource(typeof(QueryServiceItemsSource))]
        public ClassModel QueryService
        {
            get { return this.Get<ClassModel>(); }
            set { this.Set(value); }
        }

        [Category("Service API")]
        [DisplayName("Operation Name")]
        [Description("This is the name of the method that will be exposed on the command web-service")]
        [Required]
        public string ServiceOperationName
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        [Category("Contracts")]
        [DisplayName("Contracts Project")]
        [Description("Project that contains the BAS contract classes")]
        [Required]
        [ItemsSource(typeof(ContractsProjectItemsSource))]
        public ProjectModel ContractsProject
        {
            get { return this.Get<ProjectModel>(); }
            set { this.Set(value); }
        }

        [Category("Contracts")]
        [DisplayName("New Query Type")]
        [Description("This is the name of the query class that will be created")]
        [Required]
        public string QueryTypeName
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        [Category("Contracts")]
        [DisplayName("Query Result Type")]
        [Description("This is the type that will be returned as the query's result collection")]
        [Required]
        public string QueryResultTypeName
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        [Browsable(false)]
        public QueryEntityOption QueryEntityOption
        {
            get { return this.Get<QueryEntityOption>(); }
            internal set
            {
                this.Set(value);

                switch (value)
                {
                    case QueryEntityOption.CreateNew:
                        this.ExistingEntityType = null;
                        break;

                    case QueryEntityOption.UseExisting:
                        this.NewEntityTypeName = null;
                        break;

                    case QueryEntityOption.Unknown:
                        this.ExistingEntityType = null;
                        this.NewEntityTypeName = null;
                        break;

                    default:
                        break;
                }
            }
        }

        [Description("This is the existing entity class that the QueryExecutor queries over the database")]
        [DisplayName("Existing Entity Type")]
        [ItemsSource(typeof(QueryEntityItemsSource))]
        [Category("Entities")]
        public ClassModel ExistingEntityType
        {
            get { return this.Get<ClassModel>(); }
            set
            {
                this.Set<ClassModel>(value);
                if (value != null)
                {
                    this.QueryEntityOption = QueryEntityOption.UseExisting;
                }
            }
        }

        [Category("Entities")]
        [DisplayName("New Entity Type")]
        [Description("This is the new class name that the QueryExecutor queries of the database")]
        public string NewEntityTypeName
        {
            get { return this.Get<string>(); }
            set
            {
                this.Set<string>(value);
                if (value != null)
                {
                    this.QueryEntityOption = QueryEntityOption.CreateNew;
                }
            }
        }

        public static AddServiceQuerySettings Default()
        {
            return new AddServiceQuerySettings();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();

            if (this.QueryEntityOption == QueryEntityOption.Unknown)
            {
                var existingName = this.GetDisplayName(nameof(this.ExistingEntityType));
                var newName = this.GetDisplayName(nameof(this.NewEntityTypeName));

                result.Add(new ValidationResult($"{existingName} or {newName} must be specified to continue."));
            }
            else if (this.QueryEntityOption == QueryEntityOption.CreateNew)
            {
                var ctx = new ValidationContext(this)
                {
                    MemberName = nameof(this.NewEntityTypeName),
                    DisplayName = this.GetDisplayName(nameof(this.NewEntityTypeName))
                };

                var attr = new RequiredAttribute();
                result.Add(attr.GetValidationResult(this.NewEntityTypeName, ctx));
            }
            else if (this.QueryEntityOption == QueryEntityOption.UseExisting)
            {
                var ctx = new ValidationContext(this)
                {
                    MemberName = nameof(this.ExistingEntityType),
                    DisplayName = this.GetDisplayName(nameof(this.ExistingEntityType))
                };

                var attr = new RequiredAttribute();
                result.Add(attr.GetValidationResult(this.ExistingEntityType, ctx));
            }

            return result;
        }

        private string GetDisplayName(string propertyName)
        {
            var props = TypeDescriptor.GetProperties(this.GetType());
            var attr = props.Find(propertyName, true).Attributes.OfType<DisplayNameAttribute>().FirstOrDefault();

            return attr?.DisplayName;
        }
    }
}
