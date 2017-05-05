//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using MainEvent.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace MainEvent.Tests
{
    [TestClass]
    public class DialogTests
    {
        [TestMethod]
        [TestCategory("IgnoreOnBuild")]
        public void ErrorMessage_dialog_test()
        {
            try
            {
                this.Throw();
            }
            catch (Exception ex)
            {
                ViewModelExtensions.ShowErrorMessage(null, "Description", new AggregateException(ex));
            }
        }

        private void Throw()
        {
            var ex = new InvalidOperationException();
            throw new TargetInvocationException(ex);
        }
    }
}