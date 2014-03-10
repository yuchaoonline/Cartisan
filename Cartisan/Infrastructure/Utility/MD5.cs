using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cartisan.Infrastructure.Utility {
    public static class MD5Util {
        public static string MD5Encrypt(string pToEncrypt, CipherMode mode = CipherMode.CBC, string key = "CARTISAN") {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = mode;
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray()) {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();

        }

        public static string MD5Decrypt(string pToDecrypt, CipherMode mode = CipherMode.CBC, string key = "CARTISAN") {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = mode;
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++) {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.ASCII.GetString(ms.ToArray());

        }

        public static string GetMd5Hash(string input) {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}