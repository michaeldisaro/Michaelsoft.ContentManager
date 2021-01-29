namespace Michaelsoft.ContentManager.Client.Settings
{
    public class ContentManagerClientSettings : IContentManagerClientSettings
    {

        public string ServerBasePath { get; set; }
        
        public string ApplicationBasePath { get; set; }

    }

    public interface IContentManagerClientSettings
    {

        public string ServerBasePath { get; set; }
        
        public string ApplicationBasePath { get; set; }

    }
}