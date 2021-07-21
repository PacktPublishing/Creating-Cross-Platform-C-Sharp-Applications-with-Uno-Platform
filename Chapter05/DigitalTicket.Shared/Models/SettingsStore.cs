using Windows.Storage;

namespace DigitalTicket.Models
{
    public static class SettingsStore
    {
        private const string AppLanguageKey = "Settings.AppLanguage";

        public static void StoreAppLanguageOption(string appTheme)
        {
            ApplicationData.Current.LocalSettings.Values[AppLanguageKey] = appTheme.ToString();
        }

        public static string GetAppLanguageOption()
        {
            if (ApplicationData.Current.LocalSettings.Values.Keys.Contains(AppLanguageKey))
            {
                return ApplicationData.Current.LocalSettings.Values[AppLanguageKey].ToString();
            }

            return "SystemDefault";
        }
    }
}
