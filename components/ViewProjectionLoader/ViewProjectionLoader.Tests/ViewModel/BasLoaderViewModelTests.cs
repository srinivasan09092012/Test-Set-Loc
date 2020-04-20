//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Messaging;
using ViewProjectionLoader.Core.Messages;
using ViewProjectionLoader.Core.Services;
using ViewProjectionLoader.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ViewProjectionLoader.Tests.ViewModel
{
    [TestClass]
    public class BasLoaderViewModelTests
    {
        public IStatusTracker MockStatusTracker { get; set; }

        public IWindowService MockWindowService { get; set; }

        public IBusyTracker MockBusyTracker { get; set; }

        public IBasLoader MockBasLoader { get; set; }

        public IMessenger MockMessenger { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.MockStatusTracker = MockRepository.GenerateMock<IStatusTracker>();
            this.MockWindowService = MockRepository.GenerateMock<IWindowService>();
            this.MockBusyTracker = MockRepository.GenerateMock<IBusyTracker>();
            this.MockBasLoader = MockRepository.GenerateMock<IBasLoader>();
            this.MockMessenger = MockRepository.GenerateMock<IMessenger>();
        }

        [TestMethod]
        public void BasLoaderViewModel_should_load_bas_from_ui_command()
        {
            Messenger.OverrideDefault(this.MockMessenger);
            BasLoaderViewModel vm = this.CreateInstance();
            vm.BasPath = "c:\\baspath";
            vm.SetTaskScheduler(new CurrentThreadTaskScheduler());

            this.MockBasLoader.Expect(p => p.Load("c:\\baspath", this.MockStatusTracker)).Return(
                new BasLoaderResult("c:\\baspath")
                {
                    ApplicationName = "ApplicationName",
                    ModuleName = "ModuleName"
                });

            this.MockMessenger.Expect(p => p.Send<BasLoadedMessage>(
                Arg<BasLoadedMessage>.Is.NotNull));

            vm.LoadBas.Execute(null);

            this.MockWindowService.Expect(p => p.MoveToMainWindow());
            this.MockBasLoader.VerifyAllExpectations();
            this.MockMessenger.VerifyAllExpectations();
        }

        public BasLoaderViewModel CreateInstance()
        {
            return new BasLoaderViewModel(this.MockWindowService, this.MockBasLoader, this.MockStatusTracker);
        }
    }
}
