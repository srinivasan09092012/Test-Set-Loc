using Common.Interfaces;
using Common.ModuleSettings;

namespace Common.Implementations
{
    public class ExecutionContext : IExecutionContext
    {
        public ExecutionContext(ExecutionStages executionStages)
        {
            ExecutionStage = (short)executionStages;
        }

        public ExecutionContext(ExecutionStages executionStages, ModuleSettingModel moduleSettingFile){
            ExecutionStage = (short)executionStages;
            ModuleSetting = moduleSettingFile;
        }

        public ModuleSettingModel moduleSetting;

        private short ExecutionStage;

        public ModuleSettingModel ModuleSetting { get => moduleSetting; set => moduleSetting = value; }

        public enum ExecutionStages : short{
            HelpContent = 1,
            APIContent = 2,
        };

        public string getSourcePath() {
            switch (ExecutionStage){
                case 1:
                    return this.ModuleSetting.WebSourcePath;
                default:
                    return this.ModuleSetting.ModuleAPISourcePath;
            }

        }

        public string getTargetPath(){
            switch (ExecutionStage){
                case 1:
                    return this.ModuleSetting.WebTargetPath;
                default:
                    return this.ModuleSetting.ModuleAPITargetPath;
            }
        }

        public ModuleSettings.ModuleSettingModel getModuleSettings(){
            return moduleSetting;
        }

        public int getExecutionStage() {
            return ExecutionStage;
        }

    }
}
