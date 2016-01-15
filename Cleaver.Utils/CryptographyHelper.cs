using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Cleaver.Utils {
    public sealed class CryptographyHelper {

        public static string MD5(string source, int offset = 0, int count = -1) {
            using (var algorithm = System.Security.Cryptography.MD5.Create()) {
                return ComputeHash(algorithm, source, offset, count);
            }
        }

        public static string SHA1(string source, int offset = 0, int count = -1) {
            using (var algorithm = System.Security.Cryptography.SHA1.Create()) {
                return ComputeHash(algorithm, source, offset, count);
            }
        }

        public static string RSAEncrypt(string source, string xmlKey) {
            using (var rsa = new RSACryptoServiceProvider(1024)) {
                rsa.FromXmlString(xmlKey);
                var encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(source), false);
                return BytesToHex(encrypted);
            }
        }

        public static string ComputeHash(HashAlgorithm algorithm, string source, int offset = 0, int count = -1) {
            var bytes = Encoding.UTF8.GetBytes(source);
            if (count <= 0) {
                count = bytes.Length;
            }
            var data = algorithm.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var b in data) {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string BytesToHex(byte[] bytes) {
            return string.Concat(bytes.Select(b => b.ToString("x2")).ToArray());
        }

        public static byte[] HexStrToBytes(string hex) {
            return hex.Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
            //return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }
    }
}
