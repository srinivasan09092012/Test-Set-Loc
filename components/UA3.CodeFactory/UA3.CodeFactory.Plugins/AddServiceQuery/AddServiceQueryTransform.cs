//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.PluginModel;

namespace UA3.CodeFactory.Plugins.AddServiceQuery
{
    public class AddServiceQueryTransform : SolutionTransformBase<AddServiceQuerySettings>
    {
        public AddServiceQueryTransform(AddServiceQuerySettings settings)
            : this(settings, new BasCodeGenerator())
        {
        }

        public AddServiceQueryTransform(AddServiceQuerySettings settings, IBasCodeGenerator codeGenerator)
            : base(settings, codeGenerator)
        {
        }

        protected override void ExecuteTransform(SolutionContext context)
        {
            this.AddServiceMethodToClass(context);

            this.AddServiceMethodToInterface(context);

            this.AddQueryParametersClass(context);

            this.AddQueryClass(context);

            this.AddQueryResultClass(context);

            this.AddQueryExecutorClass(context);

            this.AddQueryExecutorTestClass(context);
        }

        private void AddQueryExecutorTestClass(SolutionContext context)
        {
            TypeModel executorType = this.GetQueryExecutorType();
            TypeModel queryType = this.GetQueryType();
            TypeModel executorTestType = this.GetQueryExecutorTestType();
            TypeModel testType = new TypeModel($"{executorType.Name}Tests", context.GetNamespace(this.Conventions.QueryExecutorTestClassLocation));

            string stepName = $"Create query executor test class {executorType.Name}Tests";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.QueryExecutorTestClassLocation;

                var root = this.CodeGenerator.GenerateQueryExecutorTestClass(testType, executorType, queryType);

                context.AddDocument(location, $"{executorType.Name}Tests.cs", root);
            });
        }

        private void AddQueryParametersClass(SolutionContext context)
        {
            string stepName = $"Create query parameters class {this.Settings.QueryTypeName}Parameters";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.QueryParmsClassLocation;
                TypeModel resultType = this.GetQueryParmsType();

                var root = this.CodeGenerator.GenerateQueryParametersClass(resultType);

                context.AddDocument(location, $"{resultType.Name}.cs", root);
            });
        }

        private void AddQueryResultClass(SolutionContext context)
        {
            string stepName = $"Create query result class {this.Settings.QueryResultTypeName}";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.QueryResultClassLocation;
                TypeModel resultType = this.GetQueryResultType();

                var root = this.CodeGenerator.GenerateQueryResultClass(resultType);

                context.AddDocument(location, $"{resultType.Name}.cs", root);
            });
        }

        private void AddQueryExecutorClass(SolutionContext context)
        {
            string stepName = $"Create query executor class {this.Settings.QueryTypeName}Executor";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.QueryExecutorClassLocation;
                TypeModel inputType = this.GetInputType(context);
                TypeModel queryType = this.GetQueryType();
                TypeModel parmsType = this.GetQueryParmsType();
                TypeModel executorType = this.GetQueryExecutorType();

                var root = this.CodeGenerator.GenerateQueryExecutorClass(executorType, inputType, queryType, parmsType);

                context.AddDocument(location, $"{executorType.Name}.cs", root);
            });
        }

        private TypeModel GetInputType(SolutionContext context)
        {
            if (this.Settings.QueryEntityOption == QueryEntityOption.CreateNew)
            {
                return new TypeModel(this.Settings.NewEntityTypeName, this.Conventions.QueryEntityTypeNamespace);
            }

            if (this.Settings.QueryEntityOption == QueryEntityOption.UseExisting)
            {
                return new TypeModel(this.Settings.ExistingEntityType.ClassTypeName, context.GetNamespace(this.Conventions.QueryEntityClassLocation));
            }

            return null;
        }

        private void AddServiceMethodToInterface(SolutionContext context)
        {
            string stepName = $"Add or create method {this.Settings.QueryService.InterfaceTypeName}.{this.Settings.ServiceOperationName}";

            TypeModel queryType = this.GetQueryType();

            context.ExecuteStep(stepName, () =>
            {
                context.AddOrReplaceInterfaceMethod(
                    this.Settings.QueryService.InterfaceDocumentId,
                    this.CodeGenerator.GenerateQueryServiceInterfaceMethod(
                        this.Settings.ServiceOperationName,
                        queryType));

                context.AddUsingStatement(this.Settings.QueryService.InterfaceDocumentId, queryType.Namespace);
            });
        }

        private void AddServiceMethodToClass(SolutionContext context)
        {
            string stepName = $"Add or create method {this.Settings.QueryService.ClassTypeName}.{this.Settings.ServiceOperationName}";

            TypeModel queryType = this.GetQueryType();

            context.ExecuteStep(stepName, () =>
            {
                context.AddOrReplaceClassMethod(
                    this.Settings.QueryService.ClassDocumentId,
                    this.CodeGenerator.GenerateQueryServiceClassMethod(
                        this.Settings.ServiceOperationName,
                        queryType));

                context.AddUsingStatement(this.Settings.QueryService.ClassDocumentId, queryType.Namespace);
            });
        }

        private void AddQueryClass(SolutionContext context)
        {
            string stepName = $"Create query class {this.Settings.QueryTypeName}";

            context.ExecuteStep(stepName, () =>
            {
                ProjectItemLocation location = this.Conventions.QueryClassLocation;
                TypeModel queryType = this.GetQueryType();
                TypeModel parmsType = this.GetQueryParmsType();
                TypeModel resultType = this.GetQueryResultType();

                var root = this.CodeGenerator.GenerateQueryClass(queryType, parmsType, resultType);

                context.AddDocument(location, $"{this.Settings.QueryTypeName}.cs", root);
            });
        }

        private TypeModel GetQueryResultType()
        {
            return new TypeModel(this.Settings.QueryResultTypeName, this.Conventions.QueryResultClassNamespace);
        }

        // maybe these should be on BasConventions
        private TypeModel GetQueryType()
        {
            return new TypeModel(this.Settings.QueryTypeName, this.Conventions.QueryClassNamespace);
        }

        private TypeModel GetQueryParmsType()
        {
            return new TypeModel($"{this.Settings.QueryTypeName}Parms", this.Conventions.QueryParmsClassNamespace);
        }

        private TypeModel GetQueryExecutorType()
        {
            return new TypeModel($"{this.Settings.QueryTypeName}Executor", this.Conventions.QueryExecutorClassNamespace);
        }

        private TypeModel GetQueryExecutorTestType()
        {
            return new TypeModel($"{this.Settings.QueryTypeName}ExecutorTests", null);            
        }
    }
}
