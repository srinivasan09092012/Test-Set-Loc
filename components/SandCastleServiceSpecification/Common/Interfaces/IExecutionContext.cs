namespace Common.Interfaces
{
    public interface IExecutionContext
    {
        string getSourcePath();

        string getTargetPath();

        ModuleSettings.ModuleSettingModel getModuleSettings();

        int getExecutionStage();
    }
}
