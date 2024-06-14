using System.Security.Cryptography;
using System.Text;

namespace BankApp.crypts
{
    public class Cryptographer
    {
        public class AES256
        {
            public static void GenerateKeyIV(ref byte[] key, ref byte[] iv)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    key = sha256.ComputeHash(key);

                }
                using (MD5 md5 = MD5.Create())
                {
                    iv = md5.ComputeHash(iv);
                }
            }

            public static byte[] EncryptString(string plainText, byte[] key, byte[] iv)
            {
                if (plainText == null || plainText.Length <= 0)
                    throw new ArgumentNullException(nameof(plainText));
                if (key == null || key.Length <= 0)
                    throw new ArgumentNullException(nameof(key));
                if (iv == null || iv.Length <= 0)
                    throw new ArgumentNullException(nameof(iv));

                byte[] encrypted;

                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new())
                    {
                        using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new(cs))
                            {
                                sw.Write(plainText);
                            }
                            encrypted = ms.ToArray();
                        }
                    }
                }

                return encrypted;
            }

            public static string DecryptString(byte[] cipherText, byte[] key, byte[] iv)
            {
                if (cipherText == null || cipherText.Length <= 0)
                    throw new ArgumentNullException(nameof(cipherText));
                if (key == null || key.Length <= 0)
                    throw new ArgumentNullException(nameof(key));
                if (iv == null || iv.Length <= 0)
                    throw new ArgumentNullException(nameof(iv));

                string plaintext = "";

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new(cipherText))
                    {
                        using (CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new(cs))
                            {
                                plaintext = sr.ReadToEnd();
                            }
                        }
                    }
                }

                return plaintext;
            }
        }
    }
}