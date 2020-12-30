using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;

namespace Michaelsoft.ContentManager.Common.Encryption
{
    public static class RsaHelper
    {

        private static Pkcs1Encoding GetPkcs1RsaProvider(bool forEncryption,
                                                         string key,
                                                         bool isPrivateKey)
        {
            var encryptionEngine = new Pkcs1Encoding(new RsaEngine());
            using var stringReader = new StringReader(key);
            if (isPrivateKey)
            {
                var ackp = (AsymmetricCipherKeyPair) new PemReader(stringReader).ReadObject();
                encryptionEngine.Init(forEncryption, ackp.Private);
            }
            else
            {
                var akp = (AsymmetricKeyParameter) new PemReader(stringReader).ReadObject();
                encryptionEngine.Init(forEncryption, akp);
            }

            return encryptionEngine;
        }

        /// <summary>
        /// Encrypt using RSA algorithm. Long string requires large key-pair.
        /// </summary>
        /// <param name="plainText">This is your text to encrypt.</param>
        /// <param name="key">Private Key must be PKCS#1, Public Key can be either PKCS#1 or PKCS#8.</param>
        /// <param name="isPrivateKey">If you use you private key set this to true, otherwise to false.</param>
        /// <param name="toBase64">Set this to true if you want your result Base64 Encoded, to false if you want an UTF8 string.</param>
        /// <returns></returns>
        public static string EncryptString(string plainText,
                                           string key,
                                           bool isPrivateKey,
                                           bool toBase64)
        {
            try
            {
                var bytesToEncrypt = Encoding.UTF8.GetBytes(plainText);
                var encryptEngine = GetPkcs1RsaProvider(true, key, isPrivateKey);
                var length = bytesToEncrypt.Length;
                var blockSize = encryptEngine.GetInputBlockSize();
                var cipherTextBytes = new List<byte>();
                for (var chunkPosition = 0;
                     chunkPosition < length;
                     chunkPosition += blockSize)
                {
                    var chunkSize = Math.Min(blockSize, length - chunkPosition);
                    cipherTextBytes.AddRange(encryptEngine.ProcessBlock(bytesToEncrypt, chunkPosition, chunkSize));
                }

                return toBase64
                           ? Convert.ToBase64String(cipherTextBytes.ToArray())
                           : Encoding.UTF8.GetString(cipherTextBytes.ToArray());
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Decrypt using RSA algorithm.
        /// </summary>
        /// <param name="encryptedText">This is your encrypted text.</param>
        /// <param name="key">Private Key must be PKCS#1, Public Key can be either PKCS#1 or PKCS#8.</param>
        /// <param name="isPrivateKey">If you use you private key set this to true, otherwise to false.</param>
        /// <param name="fromBase64">Set this to true if you are going to decrypt a Base64 encoded string, to false if it's an UTF8 string.</param>
        /// <returns></returns>
        public static string DecryptString(string encryptedText,
                                           string key,
                                           bool isPrivateKey,
                                           bool fromBase64)
        {
            try
            {
                var bytesToDecrypt = fromBase64
                                         ? Convert.FromBase64String(encryptedText)
                                         : Encoding.UTF8.GetBytes(encryptedText);
                var decryptEngine = GetPkcs1RsaProvider(false, key, isPrivateKey);
                var length = bytesToDecrypt.Length;
                var blockSize = decryptEngine.GetInputBlockSize();
                var plainTextBytes = new List<byte>();
                for (var chunkPosition = 0;
                     chunkPosition < length;
                     chunkPosition += blockSize)
                {
                    var chunkSize = Math.Min(blockSize, length - chunkPosition);
                    plainTextBytes.AddRange(decryptEngine.ProcessBlock(bytesToDecrypt, chunkPosition, chunkSize));
                }

                return Encoding.UTF8.GetString(plainTextBytes.ToArray());
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.Write(ex.StackTrace);
                return null;
            }
        }

    }
}