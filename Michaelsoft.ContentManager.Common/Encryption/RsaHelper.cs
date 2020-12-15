using System;
using System.Security.Cryptography;
using System.Text;

namespace Michaelsoft.ContentManager.Common.Encryption
{
    public static class RsaHelper
    {

        private const string RsaPrivateKeyHeader = "-----BEGIN RSA PRIVATE KEY-----";

        private const string RsaPrivateKeyFooter = "-----END RSA PRIVATE KEY-----";

        private const string RsaPublicKeyHeader = "ssh-rsa";

        private static RSACryptoServiceProvider GetRsaProvider(string key)
        {
            var rsa = new RSACryptoServiceProvider();

            if (key.StartsWith(RsaPrivateKeyHeader))
            {
                var endIdx = key.IndexOf(
                                         RsaPrivateKeyFooter,
                                         RsaPrivateKeyHeader.Length,
                                         StringComparison.Ordinal);

                var base64 = key.Substring(
                                           RsaPrivateKeyHeader.Length,
                                           endIdx - RsaPrivateKeyHeader.Length);

                var der = Convert.FromBase64String(base64);
                rsa.ImportRSAPrivateKey(der, out _);
            }

            if (key.StartsWith(RsaPublicKeyHeader))
            {
                var base64 = key.Split(" ")[1];
                var der = Convert.FromBase64String(base64);
                rsa.ImportRSAPublicKey(der, out _);
            }

            return rsa;
        }

        public static byte[] EncryptStringToBytes_Rsa(string plainText,
                                                      string key,
                                                      bool doOaepPadding)
        {
            try
            {
                var data = Encoding.Unicode.GetBytes(plainText);
                var rsa = GetRsaProvider(key);
                return rsa.Encrypt(data, doOaepPadding);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        public static string DecryptStringFromBytes_Rsa(byte[] data,
                                                        string key,
                                                        bool doOaepPadding)
        {
            try
            {
                var rsa = GetRsaProvider(key);
                var decrypted = rsa.Decrypt(data, doOaepPadding);
                return Encoding.Unicode.GetString(decrypted);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

    }
}