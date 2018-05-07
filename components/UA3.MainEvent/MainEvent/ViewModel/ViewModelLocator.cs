//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MainEvent"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using MainEvent.Core.Services;
using Microsoft.Practices.Unity;

namespace MainEvent.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            DispatcherHelper.Initialize();

            Container = new UnityContainer();

            Container.RegisterType<IDbServiceFactory, DbServiceFactory>();
            Container.RegisterType<IWindowService, WindowService>();
            Container.RegisterType<IBasLoader, BasLoader>();
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<BasLoaderViewModel>();
            Container.RegisterType<CommitsViewModel>();
            Container.RegisterType<ReplayViewModel>();
            Container.RegisterType<ViewProjectionsViewModel>();

            Container.RegisterType<IBusyTracker, BusyTracker>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IStatusTracker, StatusTracker>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance<IMessenger>(Messenger.Default, new ContainerControlledLifetimeManager());

            this.Main = Container.Resolve<MainViewModel>();
            this.BasLoad = Container.Resolve<BasLoaderViewModel>();
            this.Commits = Container.Resolve<CommitsViewModel>();
            this.Replay = Container.Resolve<ReplayViewModel>();
            this.ViewProjections = Container.Resolve<ViewProjectionsViewModel>();
        }

        public MainViewModel Main { get; private set; }

        public BasLoaderViewModel BasLoad { get; private set; }

        public CommitsViewModel Commits { get; private set; }

        public ReplayViewModel Replay { get; private set; }

        public ViewProjectionsViewModel ViewProjections { get; private set; }

        public static UnityContainer Container { get; private set; }

        public static void Cleanup()
        {
            try
            {
                Container.Dispose();
            }
            catch
            {
            }
        }
    }
}
