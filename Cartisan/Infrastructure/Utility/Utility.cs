using System;
using System.Security.Cryptography;
using System.Text;

namespace Cartisan.Infrastructure.Utility {
    public class Utility {
        public static string GetUniqueIdentifier(int length) {
            try {
                int maxSize = length;
                char[] chars = new char[62];
                string a;
                a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                chars = a.ToCharArray();
                int size = maxSize;
                byte[] data = new byte[1];
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                size = maxSize;
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data) {
                    result.Append(chars[b % (chars.Length - 1)]);
                }
                // Unique identifiers cannot begin with 0-9
                if (result[0] >= '0' && result[0] <= '9') {
                    return GetUniqueIdentifier(length);
                }
                return result.ToString();
            }
            catch (Exception ex) {
                throw new Exception("GENERATE_UID_FAIL", ex);
            }
        }
    }
}