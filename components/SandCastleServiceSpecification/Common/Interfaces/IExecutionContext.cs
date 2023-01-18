namespace Common.Interfaces
{
    public interface IExecutionContext
    {
        string getSourcePath();

        string getTargetPath();

        string getRoutingTargetPath();

        ModuleSettings.ModuleSettingModel getModuleSettings();

        int getExecutionStage();
    }
}
