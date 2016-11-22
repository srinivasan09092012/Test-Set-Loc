using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Plugins.AddServiceQuery;

namespace UA3.CodeFactory.Plugins.Tests.AddServiceQuery
{
    [TestClass]
    public class AddServiceQuerySettingsTests
    {
        [TestMethod]
        public void AddServiceQuerySettings_should_validate_entity_settings()
        {
            var settings = this.CreateValidSettings();
            Assert.IsTrue(this.Validate(settings));

            settings.NewEntityTypeName = null;
            settings.ExistingEntityType = null;
            Assert.IsFalse(this.Validate(settings));

            settings.NewEntityTypeName = "QueryType";
            settings.ExistingEntityType = null;
            Assert.IsTrue(this.Validate(settings));

            settings.NewEntityTypeName = null;
            settings.ExistingEntityType = new ClassModel("ExistingEntity", DocumentId.CreateNewId(ProjectId.CreateNewId()));
            Assert.IsTrue(this.Validate(settings));
        }

        public AddServiceQuerySettings CreateValidSettings()
        {
            ProjectId contractsId = ProjectId.CreateNewId();
            ProjectId businessProjId = ProjectId.CreateNewId();

            var result = new AddServiceQuerySettings()
            {
                ContractsProject = new ProjectModel() { Id = contractsId, Name = "Whatever.Contracts" },
                NewEntityTypeName = "QueryEntityType",
                QueryResultTypeName = "QueryResultType",
                QueryService = new ClassModel("WhateverQueryService", DocumentId.CreateNewId(businessProjId)),
                ServiceOperationName = "NewQuery",
                QueryTypeName = "QueryType"
            };

            Assert.IsTrue(this.Validate(result));

            return result;
        }

        private bool Validate(AddServiceQuerySettings settings)
        {
            var validation = new List<ValidationResult>();

            return Validator.TryValidateObject(settings, new ValidationContext(settings), validation, true);
        }
    }
}
