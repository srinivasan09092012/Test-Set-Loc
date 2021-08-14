//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UA3.CodeFactory.Core;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.PluginModel;
using UA3.CodeFactory.Core.Services;
using UA3.CodeFactory.Services;

namespace UA3.CodeFactory.ViewModel
{
    public class CodeFactoryWindowViewModel : ViewModelBase
    {
        private ISolutionProvider solutionProvider;
        private ICodeFactoryPluginProvider pluginProvider;
        private ICodeFactoryPluginMetadata selectedPlugin;
        private IWindowService windowService;
        private SettingsModel settings;
        private Visibility optionsVisibility = Visibility.Collapsed;
        private Visibility outputVisibility = Visibility.Collapsed;
        private Visibility pluginVisibility = Visibility.Visible;
        private UIStep uiStep = UIStep.SelectingPlugin;
        private string nextButtonText = "Configure";
        private bool canMoveNext = false;
        private SolutionTransformStep selectedStep;
        private string selectedStepLogText;
        private SolutionTransformStep currentStep;

        public CodeFactoryWindowViewModel()
        {
            this.pluginProvider = ServiceLocator.Current.GetInstance<ICodeFactoryPluginProvider>();
            this.solutionProvider = ServiceLocator.Current.GetInstance<ISolutionProvider>();
            this.windowService = ServiceLocator.Current.GetInstance<IWindowService>();
            this.Initialize();
        }

        [PreferredConstructor]
        public CodeFactoryWindowViewModel(ICodeFactoryPluginProvider pluginProvider, ISolutionProvider solutionProvider, IWindowService windowService)
        {
            this.pluginProvider = pluginProvider;
            this.solutionProvider = solutionProvider;
            this.windowService = windowService;
            this.Initialize();
        }

        public Visibility PluginVisibility
        {
            get { return this.pluginVisibility; }
            set { this.Set("PluginVisibility", ref this.pluginVisibility, value); }
        }

        public Visibility OptionsVisibility
        {
            get { return this.optionsVisibility; }
            set { this.Set("OptionsVisibility", ref this.optionsVisibility, value); }
        }

        public Visibility OutputVisibility
        {
            get { return this.outputVisibility; }
            set { this.Set("OutputVisibility", ref this.outputVisibility, value); }
        }

        public ICommand NextButtonCommand { get; private set; }

        public string NextButtonText
        {
            get { return this.nextButtonText; }
            set { this.Set("NextButtonText", ref this.nextButtonText, value); }
        }

        public bool CanMoveNext
        {
            get { return this.canMoveNext; }
            set { this.Set("CanMoveNext", ref this.canMoveNext, value); }
        }

        public string SelectedStepLogText
        {
            get { return this.selectedStepLogText; }
            set { this.Set("SelectedStepLogText", ref this.selectedStepLogText, value); }
        }

        public ObservableCollection<SolutionTransformStep> Steps { get; set; }

        public SolutionTransformStep SelectedStep
        {
            get { return this.selectedStep; }
            set
            {
                this.Set("SelectedStep", ref this.selectedStep, value);
                if (value == null)
                {
                    this.SelectedStepLogText = string.Empty;
                }
                else
                {
                    this.SelectedStepLogText = this.selectedStep.LogBuffer ?? string.Empty;
                }
            }
        }

        public void MoveToNextStep()
        {
            switch (this.uiStep)
            {
                case UIStep.SelectingPlugin:
                    this.PluginVisibility = Visibility.Collapsed;
                    this.OptionsVisibility = Visibility.Visible;
                    this.uiStep = UIStep.SettingOptions;
                    break;

                case UIStep.SettingOptions:
                    this.OptionsVisibility = Visibility.Collapsed;
                    this.OutputVisibility = Visibility.Visible;
                    this.uiStep = UIStep.ViewingOutput;
                    break;

                case UIStep.ViewingOutput:
                    this.OutputVisibility = Visibility.Collapsed;
                    this.PluginVisibility = Visibility.Visible;
                    this.uiStep = UIStep.SelectingPlugin;
                    break;

                default:
                    break;
            }
        }

        private void Initialize()
        {
            this.Steps = new ObservableCollection<SolutionTransformStep>();

            var pluginData = this.pluginProvider.GetPluginMetadata();
            this.Plugins = new ObservableCollection<ICodeFactoryPluginMetadata>(pluginData);
            this.NextButtonCommand = new RelayCommand(this.OnNextButtonClicked);
        }

        private void OnNextButtonClicked()
        {
            if (this.uiStep == UIStep.SelectingPlugin)
            {
                var plugin = this.pluginProvider.GetPlugin(this.selectedPlugin);
                this.PluginSettings = plugin.GetDefaultSettings();
                this.NextButtonText = "Run";
                this.MoveToNextStep();
            }
            else if (this.uiStep == UIStep.SettingOptions)
            {
                var errors = this.PluginSettings.Validate();

                if (errors.Count > 0)
                {
                    var msg = string.Join("\r\n", errors.Select(p => p.ErrorMessage).ToArray());

                    string message = "One or more settings are invalid, please fix to continue:\r\n\r\n" + msg;
                    string title = "Error";
                    this.windowService.ShowErrorMessage(title, message);
                }
                else
                {
                    this.NextButtonText = "Finish";
                    this.MoveToNextStep();
                    TransformSolution();
                }
            }
            else if (this.uiStep == UIStep.ViewingOutput)
            {
                this.NextButtonText = "Configure";
                this.MoveToNextStep();
            }
        }

        private void TransformSolution()
        {
            SolutionContext context = null;

            try
            {
                var solution = this.solutionProvider.GetSolution();
                context = new SolutionContext(solution);
                context.StepInitialized += OnStepInitialized;
                var plugin = this.pluginProvider.GetPlugin(this.SelectedPlugin);
                var transform = plugin.GetTransform(this.PluginSettings);
                transform.Execute(context);                
            }
            catch (Exception ex)
            {
                this.currentStep.Write("Error: {0}", ex.Message);
                this.currentStep.Write("Full Exception: {0}", ex);
            }
            finally
            {
                context.StepInitialized -= OnStepInitialized;
                this.currentStep = null;
            }

            Exception saveException = null;

            if (!context.TrySave(out saveException))
            {
                string text = $"There was an error while committing changes to the solution: {saveException.Message}.\r\nFull Exception: {saveException}";
                this.windowService.ShowErrorMessage("Error", text);

                foreach (var step in this.Steps)
                {
                    step.Succeeded = false;
                    step.LogBuffer = text;
                }                
            }
        }

        private void OnStepInitialized(SolutionTransformStep step)
        {
            this.Steps.Add(step);
            this.currentStep = step;
        }

        public ObservableCollection<ICodeFactoryPluginMetadata> Plugins { get; set; }

        public ICommand ConfigurePluginCommand { get; set; }

        public SettingsModel PluginSettings
        {
            get { return this.settings; }
            set { this.Set("PluginSettings", ref this.settings, value); }
        }

        public ICodeFactoryPluginMetadata SelectedPlugin
        {
            set
            {
                base.Set("SelectedPlugin", ref this.selectedPlugin, value);
                this.CanMoveNext = (value != null);
            }
            get
            {
                return this.selectedPlugin;
            }
        }
    }
}
