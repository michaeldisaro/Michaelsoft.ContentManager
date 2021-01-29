namespace Michaelsoft.ContentManager.Server.Settings
{
    public class AsymmetricEncryptionSettings : IAsymmetricEncryptionSettings
    {

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

    }

    public interface IAsymmetricEncryptionSettings
    {

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

    }
}