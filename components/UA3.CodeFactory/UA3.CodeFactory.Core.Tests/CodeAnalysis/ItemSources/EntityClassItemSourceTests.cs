//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.CodeAnalysis.ItemSources;
using UA3.CodeFactory.Core.Services;
using UA3.CodeFactory.TestSupport;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Tests.CodeAnalysis.ItemSources
{
    [TestClass]
    public class EntityClassItemSourceTests
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
        public void EntityClassItemSource_should_get_entity_classes_from_solution()
        {
            EntityClassItemsSource inst = new EntityClassItemsSource(this.MockSolutionProvider);

            ItemCollection result = inst.GetValues();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            ClassModel pm = result[0].Value as ClassModel;
            Assert.IsNotNull(pm);
            Assert.AreEqual("TestEntity", pm.ClassTypeName);
        }
    }
}
