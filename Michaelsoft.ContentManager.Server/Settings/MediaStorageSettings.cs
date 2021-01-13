namespace Michaelsoft.ContentManager.Server.Settings
{
    public class MediaStorageSettings : IMediaStorageSettings
    {

        public string Path { get; set; }

    }

    public interface IMediaStorageSettings
    {

        public string Path { get; set; }

    }
}