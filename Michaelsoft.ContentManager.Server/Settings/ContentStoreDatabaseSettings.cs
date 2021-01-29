namespace Michaelsoft.ContentManager.Server.Settings
{
    public class ContentStoreDatabaseSettings : IContentStoreDatabaseSettings
    {

        public string ContentsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }

    public interface IContentStoreDatabaseSettings
    {

        public string ContentsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}