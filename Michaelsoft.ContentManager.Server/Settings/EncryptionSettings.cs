namespace Michaelsoft.ContentManager.Server.Settings
{
    public class EncryptionSettings : IEncryptionSettings
    {

        public string DataEncryptionKey { get; set; }

        public string DataEncryptionIv { get; set; }

    }

    public interface IEncryptionSettings
    {

        public string DataEncryptionKey { get; set; }

        public string DataEncryptionIv { get; set; }

    }
}