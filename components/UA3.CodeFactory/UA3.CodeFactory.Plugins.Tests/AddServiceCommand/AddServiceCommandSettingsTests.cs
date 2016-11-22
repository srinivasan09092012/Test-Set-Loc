using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Plugins.AddServiceCommand;

namespace UA3.CodeFactory.Plugins.Tests.AddServiceCommand
{
    [TestClass]
    public class AddServiceCommandSettingsTests
    {
        [TestMethod]
        public void AddServiceCommandSettings_should_validate_projectionwriter_settings()
        {
            var settings = this.CreateValidSettings();

            settings.ProjectionWriterOption = ProjectionWriterOptions.CreateNew;
            settings.NewProjectionWriterClassName = null;
            settings.NewProjectionWriterEntityClass = new ClassModel("ExistingEntity", null);
            Assert.IsFalse(Validate(settings));

            settings = this.CreateValidSettings();

            settings.ProjectionWriterOption = ProjectionWriterOptions.CreateNew;
            settings.NewProjectionWriterClassName = "NewProjectionWriter";
            settings.NewProjectionWriterEntityClass = null;
            Assert.IsFalse(Validate(settings));

            settings = this.CreateValidSettings();

            settings.ProjectionWriterOption = ProjectionWriterOptions.UpdateExisting;
            settings.ExistingProjectionWriter = null;
            Assert.IsFalse(Validate(settings));
        }

        private bool Validate(AddServiceCommandSettings settings)
        {
            var validation = new List<ValidationResult>();

            return Validator.TryValidateObject(settings, new ValidationContext(settings), validation, true);
        }

        private AddServiceCommandSettings CreateValidSettings()
        {
            ProjectId commandHandlersProjId = this.GenerateProjectId();
            ProjectId contractsProjId = this.GenerateProjectId();

            var result = new AddServiceCommandSettings()
            {
                CommandHandlersProject = new ProjectModel()
                {
                    Id = commandHandlersProjId,
                    Name = "Whatever.Business"
                },
                CommandService = new ClassModel("WhateverCommandServide", DocumentId.CreateNewId(commandHandlersProjId)),
                CommandTypeName = "NewCommand",
                EventTypeName = "NewEvent",
                ServiceOperationName = "NewOperation",
                ProjectionWriterOption = ProjectionWriterOptions.Skip,
                ContractsProject = new ProjectModel() { Id = contractsProjId, Name = "Whatever.Contracts" }
            };

            Assert.IsTrue(this.Validate(result));

            return result;
        }

        private ProjectId GenerateProjectId()
        {
            return ProjectId.CreateNewId();
        }
    }
}
