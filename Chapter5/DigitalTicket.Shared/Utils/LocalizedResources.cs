using Windows.ApplicationModel.Resources;

namespace DigitalTicket.Utils
{
    public static class LocalizedResources
    {
        public static string GetString(string name)
        {
            if (Windows.UI.Core.CoreWindow.GetForCurrentThread() != null)
            {
                return ResourceLoader.GetForCurrentView().GetString(name);
            }
            return null;
        }
    }
}
