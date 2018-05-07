//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.CodeAnalysis.ItemSources;
using UA3.CodeFactory.Core.Services;
using UA3.CodeFactory.TestSupport;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Tests.CodeAnalysis.ItemSources
{
    [TestClass]
    public class ProjectionWriterClassItemSourceTests
    {
        public ISolutionProvider MockSolutionProvider { get; private set; }
        public Solution MockSolution { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.MockSolutionProvider = MockRepository.GenerateMock<ISolutionProvider>();
            this.MockSolution = SolutionUtil.CreateTypicalBASSolution("C:\\").Solution;
            this.MockSolutionProvider.Expect(p => p.GetSolution()).Return(this.MockSolution);
        }

        [TestMethod]
        public void ProjectionWriterClassItemSource_should_get_projection_writers_from_solution()
        {
            ProjectionWriterClassItemSource inst = new ProjectionWriterClassItemSource(this.MockSolutionProvider);

            ItemCollection result = inst.GetValues();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            ClassModel pm = result[0].Value as ClassModel;
            Assert.IsNotNull(pm);
            Assert.AreEqual("TestProjectionWriter", pm.ClassTypeName);
        }
    }
}
