using InstaSync.Models.Models;
using InstaSync.UOW.Helpers;

namespace InstaSync.UOW.Managers
{
    public class SettingsManager
    {
        public static void SaveSettings(Settings settings)
        {
            XmlHelper.SaveXml(settings);
        }

        public static Settings GetSettings()
        {
            return (Settings)XmlHelper.GetModel("Settings.xml", new Settings());
        }
    }
}
