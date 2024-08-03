using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Utils
{
    public static class HashUtil
    {
        // Tạo hàm băm MD5
        public static string ComputeMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ConvertToHexString(hashBytes);
            }
        }

        // Tạo hàm băm SHA-1
        public static string ComputeSHA1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ConvertToHexString(hashBytes);
            }
        }

        // Tạo hàm băm SHA-256
        public static string ComputeSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ConvertToHexString(hashBytes);
            }
        }

        // Tạo hàm băm SHA-512
        public static string ComputeSHA512(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ConvertToHexString(hashBytes);
            }
        }

        // Phương thức hỗ trợ: Chuyển đổi byte array sang chuỗi Hex
        private static string ConvertToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        // So sánh hai giá trị băm
        public static bool CompareHashes(string hash1, string hash2)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(hash1, hash2) == 0;
        }

        // Tạo hàm băm bằng Bcrypt (tùy chọn)
        // Yêu cầu thư viện bên ngoài như BCrypt.Net-Next
        public static string ComputeBCrypt(string input, int workFactor = 10)
        {
            return BCrypt.Net.BCrypt.HashPassword(input, workFactor);
        }

        // Kiểm tra một chuỗi với giá trị băm Bcrypt
        public static bool VerifyBCrypt(string input, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(input, hash);
        }
    }
}
