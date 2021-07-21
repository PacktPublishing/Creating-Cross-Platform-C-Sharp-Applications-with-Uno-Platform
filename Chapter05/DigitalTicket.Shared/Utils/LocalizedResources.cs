using Windows.ApplicationModel.Resources;

namespace DigitalTicket.Utils
{
    public static class LocalizedResources
    {
        private static ResourceLoader cachedResourceLoader;
        public static string GetString(string name)
        {
            if (cachedResourceLoader == null)
            {
                cachedResourceLoader = ResourceLoader.GetForViewIndependentUse();
            }
            if (cachedResourceLoader != null)
            {
                return cachedResourceLoader.GetString(name);
            }
            return null;
        }
    }
}
