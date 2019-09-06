using Common.ModuleSettings;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static bool IsPathValid(this string path)
        {
            return Directory.Exists(path) ? true : false;
        }

        public static string CreateDirectory(this string path)
        {
            Directory.CreateDirectory(path);
            return path;
        }

        public static string PrintComaSeparated(this List<ModuleSettingModel> settingList)
        {
            StringBuilder output = new StringBuilder();
           
            foreach (var setting in settingList)
            {
                output.Append(setting.ModuleNameDisplay + ",");
            };

            return output.ToString();
        }
    }
}
