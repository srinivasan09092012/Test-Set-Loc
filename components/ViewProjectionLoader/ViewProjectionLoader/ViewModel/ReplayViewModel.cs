//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Command;
using HP.HSP.UA3.Core.BAS.CQRS.Base.Replay;
using ViewProjectionLoader.Core;
using ViewProjectionLoader.Core.Messages;
using ViewProjectionLoader.Core.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using CoreInterfaces = HP.HSP.UA3.Core.BAS.CQRS.Interfaces;

namespace ViewProjectionLoader.ViewModel
{
    public class ReplayViewModel : MainEventViewModel
    {
        private BasLoaderResult basLoadResult;

        public ReplayViewModel(IBusyTracker busyTracker, IStatusTracker statusTracker)
            : base(busyTracker, statusTracker)
        {
            this.ReplayAllEvents = new RelayCommand(this.OnReplayAllEvents);
            MessengerInstance.Register<BasLoadedMessage>(this, this.OnBasLoaded);
        }

        private void OnBasLoaded(BasLoadedMessage message)
        {
            this.basLoadResult = message.LoadResult;
        }

        public ICommand ReplayAllEvents { get; private set; }

        private void OnReplayAllEvents()
        {
            Task.Factory.StartNew(() =>
            {
                this.SetBusy();

                var configuratorType = this.basLoadResult.CreateReflector().GetConfiguratorType();
                CoreInterfaces.IModuleConfigurator configurator = null;

                if (configuratorType != null)
                {
                    object configuratorObj = configuratorType.Assembly.CreateInstance(configuratorType.FullName, true);
                    configurator = configuratorObj as CoreInterfaces.IModuleConfigurator;
                }

                string tenantId = LocalSettings.TenantId;
                EventReplayContext context = EventReplayContext.ForTenant(this.basLoadResult.Configuration).Configure(cfg =>
                {
                    cfg.SpecifyAggregates(p => p.All());
                    cfg.SpecifyEvents(p => p.All());
                    cfg.SpecifyExternalDistribution(p => p.Disabled());
                    cfg.SpecifyHandlers(p => p.All());
                    cfg.SetReplayMode(ReplayMode.LongRunning);

                    if (configurator != null)
                    {
                        cfg.UseConfigurator(configurator);
                    }
                });

                this.SetStatus("Replaying events...");

                new EventReplayer().Replay(context);

                this.SetStatus("Event replay completed successfully");
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    this.ShowErrorMessage("One or more errors occurred while replaying events:", t.Exception);
                }

                this.ClearStatus();
                this.SetIdle();
            }).ConfigureAwait(false);
        }
    }
}
