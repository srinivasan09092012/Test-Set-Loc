//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.CodeAnalysis.ItemSources;
using UA3.CodeFactory.Core.PluginModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Plugins.AddServiceCommand
{
    [DisplayName("Settings - Add Command")]
    public class AddServiceCommandSettings : SettingsModel, IValidatableObject
    {
        [Category("Service API")]
        [Description("This is the class and/or interface that will receive the generated operation")]
        [DisplayName("Command Service")]
        [ItemsSource(typeof(CommandServiceItemsSource))]
        [Required(ErrorMessage = "Command Service is required")]
        public ClassModel CommandService
        {
            get { return this.Get<ClassModel>(); }
            set { this.Set(value); }
        }

        [Category("Service API")]
        [DisplayName("Operation Name")]
        [Description("This is the name of the method that will be exposed on the command web-service")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operation Name is required")]
        public string ServiceOperationName
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        [Category("Handlers")]
        [DisplayName("Command Handlers Project")]
        [Description("Project that contains the BAS command handlers")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Command Handlers Project is required")]
        [ItemsSource(typeof(CommandHandlersProjectItemsSource))]
        public ProjectModel CommandHandlersProject
        {
            get { return this.Get<ProjectModel>(); }
            set { this.Set(value); }
        }

        [Category("Handlers")]
        [DisplayName("Projection Writer Option")]
        [Description("Option on generating new or updating Projection Writers for the new Event")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Projection Writer Option is required")]
        public ProjectionWriterOptions ProjectionWriterOption
        {
            get { return this.Get<ProjectionWriterOptions>(); }
            set
            {
                this.Set(value);

                switch (value)
                {
                    case ProjectionWriterOptions.CreateNew:
                        this.ExistingProjectionWriter = null;
                        break;

                    case ProjectionWriterOptions.Skip:
                        this.ExistingProjectionWriter = null;
                        this.NewProjectionWriterClassName = null;
                        break;

                    case ProjectionWriterOptions.UpdateExisting:
                        this.NewProjectionWriterClassName = null;
                        break;

                    default:
                        break;
                }
            }
        }

        [Category("Handlers")]
        [DisplayName("New Projection Writer Class Name")]
        [Description("Name of generated Projection Writer class")]
        public string NewProjectionWriterClassName
        {
            get { return this.Get<string>(); }
            set
            {
                this.Set(value);

                if (value != null)
                {
                    this.ProjectionWriterOption = ProjectionWriterOptions.CreateNew;
                }
            }
        }

        [Category("Handlers")]
        [DisplayName("Existing Projection Writer Class Name")]
        [Description("Existing projection writer class to receive new Handle method")]
        [ItemsSource(typeof(ProjectionWriterClassItemSource))]
        public ClassModel ExistingProjectionWriter
        {
            get { return this.Get<ClassModel>(); }
            set
            {
                this.Set(value);

                if (value != null)
                {
                    this.ProjectionWriterOption = ProjectionWriterOptions.UpdateExisting;
                }
            }
        }

        [Category("Contracts")]
        [DisplayName("Contracts Project")]
        [Description("Project that contains the BAS contract classes")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contracts Project is required")]
        [ItemsSource(typeof(ContractsProjectItemsSource))]
        public ProjectModel ContractsProject
        {
            get { return this.Get<ProjectModel>(); }
            set { this.Set(value); }
        }

        [Category("Contracts")]
        [DisplayName("New Event Name")]
        [Description("This is the name of the event class that will be created")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "New Event Name is required")]
        public string EventTypeName
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        [Category("Contracts")]
        [DisplayName("New Command Name")]
        [Description("This is the name of the command class that will be created")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "New Command Name is required")]
        public string CommandTypeName
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        [Category("Handlers")]
        [DisplayName("Projection Writer Entity Class")]
        [Description("This is the EF entity class the Projection Writer wraps (the T in ProjectionWriterBase<T>)")]
        [ItemsSource(typeof(EntityClassItemsSource))]
        public ClassModel NewProjectionWriterEntityClass
        {
            get { return this.Get<ClassModel>(); }
            set { this.Set(value); }
        }

        public static AddServiceCommandSettings Default()
        {
            return new AddServiceCommandSettings();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();

            switch(this.ProjectionWriterOption)
            {
                case ProjectionWriterOptions.CreateNew:
                    if (this.NewProjectionWriterEntityClass == null)
                    {
                        result.Add(new ValidationResult("Projection Writer Entity Class is required", new string[] { nameof(this.NewProjectionWriterEntityClass) }));
                    }
                    if (string.IsNullOrEmpty(this.NewProjectionWriterClassName))
                    {
                        result.Add(new ValidationResult("New Projection Writer Class Name is required", new string[] { nameof(this.NewProjectionWriterClassName) }));
                    }
                    break;
                case ProjectionWriterOptions.UpdateExisting:
                    if (this.ExistingProjectionWriter == null)
                    {
                        result.Add(new ValidationResult("Existing Projection Writer Class Name", new string[] { nameof(this.ExistingProjectionWriter) }));
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
