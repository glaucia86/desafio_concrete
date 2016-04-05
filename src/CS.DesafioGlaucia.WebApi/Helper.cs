using System;
using System.Security.Cryptography;
using System.Text;

namespace CS.DesafioGlaucia.WebApi
{
    public class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgoritmo = new SHA256CryptoServiceProvider();

            var valorByte = Encoding.UTF8.GetBytes(input);
            var byteHash = hashAlgoritmo.ComputeHash(valorByte);

            return Convert.ToBase64String(byteHash);
        }
    }
}