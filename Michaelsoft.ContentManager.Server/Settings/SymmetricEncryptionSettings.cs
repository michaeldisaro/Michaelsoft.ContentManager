namespace Michaelsoft.ContentManager.Server.Settings
{
    public class SymmetricEncryptionSettings : ISymmetricEncryptionSettings
    {

        public string DataEncryptionKey { get; set; }

        public string DataEncryptionIv { get; set; }

    }

    public interface ISymmetricEncryptionSettings
    {

        public string DataEncryptionKey { get; set; }

        public string DataEncryptionIv { get; set; }

    }
}