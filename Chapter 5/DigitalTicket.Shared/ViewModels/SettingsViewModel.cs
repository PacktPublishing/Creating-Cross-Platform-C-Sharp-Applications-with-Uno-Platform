using Microsoft.Toolkit.Mvvm.ComponentModel;
using DigitalTicket.Models;
using Windows.UI.Xaml;
using System.Collections.Generic;
using Windows.Globalization;
using Windows.UI.Xaml.Controls;
using DigitalTicket.Utils;
using DigitalTicket.Views;

namespace DigitalTicket.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        public List<string> LanguageOptions = new List<string>()
        {
            "Deutsch",
            "English",
            LocalizedResources.GetString("UseSystemLanguageName")
        };

        private int selectedLanguageIndex;
        public int SelectedLanguageIndex
        {
            get
            {
                return selectedLanguageIndex;
            }
            set
            {
                SetProperty(ref selectedLanguageIndex, value);
                SettingsStore.StoreAppLanguageOption(value == LanguageOptions.IndexOf(LocalizedResources.GetString("UseSystemLanguageName")) ? "SystemDefault" : LanguageOptions[value]);
                UpdateAppLanguageOverride();
            }
        }

        public void UpdateAppLanguageOverride()
        {
            ApplicationLanguages.PrimaryLanguageOverride = GetPrimaryLanguageOverrideFromLanguage(LanguageOptions[selectedLanguageIndex]);

            (Window.Current.Content as Frame).BackStack.Clear();
            (Window.Current.Content as Frame).Navigate(typeof(MainPage),typeof(SettingsPage));
        }

        public static string GetPrimaryLanguageOverrideFromLanguage(string language)
        {
            string actualLanguage = language == LocalizedResources.GetString("UseSystemLanguageName") ?  ResolveClosestSupportedLanguage() : language;
            switch (actualLanguage)
            {
                case "Deutsch":
                    return "de-DE";
                case "English":
                    return "en";
                default:
                    return "en";
            }
        }


        private static string ResolveClosestSupportedLanguage()
        {
            string languageIdentifier = Windows.System.UserProfile.GlobalizationPreferences.Languages[0].ToString();

            if (languageIdentifier.StartsWith("de"))
            {
                return "Deutsch";
            }
            else if (languageIdentifier.StartsWith("en"))
            {
                return "English";
            }
            return "English";
        }

        public SettingsViewModel()
        {
            var languageOption = SettingsStore.GetAppLanguageOption();
            if (languageOption == "SystemDefault")
            {
                selectedLanguageIndex = LanguageOptions.IndexOf(LocalizedResources.GetString("UseSystemLanguageName"));
            }
            else
            {
                selectedLanguageIndex = LanguageOptions.IndexOf(languageOption);
            }
        }
    }
}
