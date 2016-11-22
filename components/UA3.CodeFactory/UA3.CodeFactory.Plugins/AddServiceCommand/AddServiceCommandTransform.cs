//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.PluginModel;

namespace UA3.CodeFactory.Plugins.AddServiceCommand
{
    public class AddServiceCommandTransform : SolutionTransformBase<AddServiceCommandSettings>
    {
        public AddServiceCommandTransform(AddServiceCommandSettings settings)
            : this(settings, new BasCodeGenerator())
        {
        }

        public AddServiceCommandTransform(AddServiceCommandSettings settings, IBasCodeGenerator codeGenerator)
            : base(settings, codeGenerator)
        {
        }

        protected override void ExecuteTransform(SolutionContext context)
        {
            this.AddServiceMethodToClass(context);

            this.AddServiceMethodToInterface(context);

            this.AddCommandClass(context);

            this.AddEventClass(context);

            this.AddCommandHandlerClass(context);

            this.AddCommandHandlerTestClass(context);

            if (this.Settings.ProjectionWriterOption == ProjectionWriterOptions.CreateNew)
            {
                this.AddProjectionWriterClass(context);

                this.AddProjectionWriterTestClass(context);
            }
            else if (this.Settings.ProjectionWriterOption == ProjectionWriterOptions.UpdateExisting)
            {
                this.UpdateProjectionWriterClass(context);

                this.UpdateOrCreateProjectionWriterTests(context);
            }
        }

        private void UpdateOrCreateProjectionWriterTests(SolutionContext context)
        {
            Document existing = null;

            if (context.TryFindClassDocument($"{this.Settings.ExistingProjectionWriter.ClassTypeName}Tests", out existing))
            {
                this.UpdateProjectionWriterTestClass(context, existing);
            }
            else
            {
                this.AddProjectionWriterTestClass(context);
            }
        }

        private void UpdateProjectionWriterTestClass(SolutionContext context, Document testClassDocument)
        {
            string stepName = $"Add test method to {this.Settings.ExistingProjectionWriter.ClassTypeName}Tests";

            context.ExecuteStep(stepName, () =>
            {
                TypeModel testType = new TypeModel(this.Settings.ExistingProjectionWriter.ClassTypeName, null);
                TypeModel eventType = this.GetEventType();

                context.AddOrReplaceClassMethod(
                    testClassDocument.Id,
                    this.CodeGenerator.GenerateProjectionWriterTestMethod(testType, eventType));
            });
        }

        private void UpdateProjectionWriterClass(SolutionContext context)
        {
            string stepName = $"Update projection writer {this.Settings.ExistingProjectionWriter.ClassTypeName}";

            context.ExecuteStep(stepName, () =>
            {
                TypeModel eventType = this.GetEventType();

                context.AddOrReplaceClassMethod(
                    this.Settings.ExistingProjectionWriter.ClassDocumentId,
                    this.CodeGenerator.GenerateProjectionWriterMethod(eventType));
            });
        }

        private void AddProjectionWriterClass(SolutionContext context)
        {
            string stepName = $"Create projection writer class {this.Settings.NewProjectionWriterClassName}";

            context.ExecuteStep(stepName, () =>
            {
                TypeModel writerType = this.GetProjectionWriterType(this.Settings.NewProjectionWriterClassName);
                TypeModel eventType = this.GetEventType();
                TypeModel entityType = this.GetEntityType();

                var root = this.CodeGenerator.GenerateProjectionWriterClass(
                    writerType,
                    eventType,
                    entityType);

                context.AddDocument(this.Conventions.ProjectionWriterClassLocation, $"{writerType.Name}.cs", root);
            });
        }

        private void AddProjectionWriterTestClass(SolutionContext context)
        {
            string stepName = $"Create projection writer test class {this.Settings.NewProjectionWriterClassName}Tests";

            context.ExecuteStep(stepName, () =>
            {
                TypeModel testType = new TypeModel(this.Settings.NewProjectionWriterClassName + "Tests", context.GetNamespace(this.Conventions.ProjectionWriterTestClassLocation));
                TypeModel eventType = this.GetEventType();
                TypeModel entityType = this.GetEntityType();

                var root = this.CodeGenerator.GenerateProjectionWriterTestClass(
                    testType,
                    eventType,
                    entityType);

                context.AddDocument(this.Conventions.ProjectionWriterClassLocation, $"{testType.Name}.cs", root);
            });
        }

        private void AddCommandHandlerClass(SolutionContext context)
        {
            string stepName = $"Create command handler class {this.Settings.CommandTypeName}Handler";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.CommandHandlerClassLocation;
                TypeModel handlerType = this.GetCommandHandlerType();
                TypeModel cmdType = this.GetCommandType();

                var root = this.CodeGenerator.GenerateCommandHandlerClass(
                    handlerType,
                    cmdType);

                context.AddDocument(this.Conventions.CommandHandlerClassLocation, $"{handlerType.Name}.cs", root);
            });
        }

        private void AddCommandHandlerTestClass(SolutionContext context)
        {
            string stepName = $"Create command handler test class {this.Settings.CommandTypeName}HandlerTests";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.CommandHandlerTestClassLocation;
                TypeModel handlerType = this.GetCommandHandlerType();
                TypeModel cmdType = this.GetCommandType();
                TypeModel testType = new TypeModel(cmdType.Name + "HandlerTests", context.GetNamespace(this.Conventions.CommandHandlerTestClassLocation));

                var root = this.CodeGenerator.GenerateCommandHandlerTestClass(
                    testType,
                    handlerType,
                    cmdType);

                context.AddDocument(location, $"{testType.Name}.cs", root);
            });
        }

        private void AddEventClass(SolutionContext context)
        {
            string stepName = $"Create event class {this.Settings.EventTypeName}";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.EventClassLocation;
                TypeModel type = this.GetEventType();

                var root = this.CodeGenerator.GenerateEventClass(type);

                context.AddDocument(location.Project, location.Folders, $"{this.Settings.EventTypeName}.cs", root);
            });
        }

        private void AddCommandClass(SolutionContext context)
        {
            string stepName = $"Create command class {this.Settings.CommandTypeName}";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.CommandClassLocation;
                TypeModel type = this.GetCommandType();

                var root = this.CodeGenerator.GenerateCommandClass(type);

                context.AddDocument(location.Project, location.Folders, $"{this.Settings.CommandTypeName}.cs", root);
            });
        }

        private void AddServiceMethodToInterface(SolutionContext context)
        {
            string stepName = $"Add or create method {this.Settings.CommandService.InterfaceTypeName}.{this.Settings.ServiceOperationName}";

            TypeModel cmdType = new TypeModel(this.Settings.CommandTypeName, this.Conventions.CommandClassNamespace);
            TypeModel evtType = new TypeModel(this.Settings.EventTypeName, this.Conventions.EventClassNamespace);

            context.ExecuteStep(stepName, () =>
            {
                context.AddOrReplaceInterfaceMethod(
                    this.Settings.CommandService.InterfaceDocumentId,
                    this.CodeGenerator.GenerateCommandServiceInterfaceMethod(
                        this.Settings.ServiceOperationName,
                        cmdType,
                        evtType));
            });
        }

        private void AddServiceMethodToClass(SolutionContext context)
        {
            string stepName = $"Add or create method {this.Settings.CommandService.ClassTypeName}.{this.Settings.ServiceOperationName}";

            TypeModel cmdType = new TypeModel(this.Settings.CommandTypeName, this.Conventions.CommandClassNamespace);
            TypeModel evtType = new TypeModel(this.Settings.EventTypeName, this.Conventions.EventClassNamespace);

            context.ExecuteStep(stepName, () =>
            {
                context.AddOrReplaceClassMethod(
                    this.Settings.CommandService.ClassDocumentId,
                    this.CodeGenerator.GenerateCommandServiceClassMethod(
                        this.Settings.ServiceOperationName,
                        cmdType,
                        evtType));
            });
        }

        private TypeModel GetCommandType()
        {
            return new TypeModel(this.Settings.CommandTypeName, this.Conventions.CommandClassNamespace);
        }

        private TypeModel GetEventType()
        {
            return new TypeModel(this.Settings.EventTypeName, this.Conventions.EventClassNamespace);
        }

        private TypeModel GetCommandHandlerType()
        {
            return new TypeModel($"{this.Settings.CommandTypeName}Handler", this.Conventions.CommandHandlerClassNamespace);
        }

        private TypeModel GetProjectionWriterType(string typeName)
        {
            return new TypeModel(typeName, this.Conventions.ProjectionWriterNamespace);
        }

        private TypeModel GetEntityType()
        {
            return new TypeModel(this.Settings.NewProjectionWriterEntityClass.ClassTypeName, this.Conventions.EntityClassNamespace);
        }
    }
}