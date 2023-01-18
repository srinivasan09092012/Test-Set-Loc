using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Plugins.AddServiceQuery;
using UA3.CodeFactory.TestSupport;

namespace UA3.CodeFactory.Plugins.Tests.AddServiceQuery
{
    [TestClass]
    public class AddServiceQueryTransformTests
    {
        [TestMethod]
        public void AddServiceQuery_should_add_query_elements_to_solution()
        {
            var solution = SolutionUtil.CreateTypicalBASSolution("c:\\");

            var settings = this.CreateSettings(solution);

            AddServiceQueryTransform transform = new AddServiceQueryTransform(settings, new BasCodeGenerator());
            SolutionContext ctx = new SolutionContext(solution.Solution);

            transform.Execute(ctx);

            ctx.CurrentSolution.AllClassDocuments().ToList().ForEach(doc =>
            {
                Console.WriteLine(doc.Name.PadRight(32, '*'));
                Console.WriteLine(doc.GetTextAsync().Result);
                Console.WriteLine("".PadRight(32, '*'));
            });

            var conventions = BasConventions.CreateFrom(ctx.CurrentSolution);
            var projects = BasProjects.CreateFrom(ctx.CurrentSolution);

            var serviceClassDoc = projects.ServicesProject.GetDocument("TypicalQueryService.cs");
            Assert.IsNotNull(serviceClassDoc);
            Assert.IsTrue(serviceClassDoc.GetClass().HasMethodWithSignature("NewQueryOperation", new Tuple<string, string>("query", "NewQueryType")));

            var serviceInterfaceDoc = projects.ServicesProject.GetDocument("ITypicalQueryService.cs");
            Assert.IsNotNull(serviceInterfaceDoc);
            Assert.IsTrue(serviceInterfaceDoc.GetInterface().HasMethodWithSignature("NewQueryOperation", new Tuple<string, string>("query", "NewQueryType")));

            var paramsClassDoc = projects.ContractsProject.GetDocument("NewQueryTypeParms.cs");
            Assert.IsNotNull(paramsClassDoc);

            var queryClassDoc = projects.ContractsProject.GetDocument("NewQueryType.cs");
            Assert.IsNotNull(queryClassDoc);

            var resultClassDoc = projects.ContractsProject.GetDocument("NewQueryResultType.cs");
            Assert.IsNotNull(resultClassDoc);

            var executorClassDoc = projects.DataAccessProject.GetDocument("NewQueryTypeExecutor.cs");
            Assert.IsNotNull(executorClassDoc);            
        }       

        private AddServiceQuerySettings CreateSettings(SolutionSummary solution)
        {
            BasConventions conventions = BasConventions.CreateFrom(solution.Solution);

            return new AddServiceQuerySettings()
            {
                ContractsProject = conventions.ContractsProject,
                ServiceOperationName = "NewQueryOperation",
                NewEntityTypeName = "NewEntityType",
                QueryResultTypeName = "NewQueryResultType",
                QueryTypeName = "NewQueryType",
                QueryService = new ClassModel("TypicalQueryService",
                    solution.Solution.AllDocuments().FirstOrDefault(p => p.Name == "TypicalQueryService.cs").Id)
                {
                    InterfaceDocumentId = solution.Solution.AllDocuments().FirstOrDefault(p => p.Name == "ITypicalQueryService.cs").Id,
                    InterfaceTypeName = "ITypicalQueryService"
                }
            };
        }
    }
}
