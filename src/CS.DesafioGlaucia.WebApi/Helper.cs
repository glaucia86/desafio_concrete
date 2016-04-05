using System;
using System.Security.Cryptography;

namespace CS.DesafioGlaucia.WebApi
{
    public class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgoritmo = new SHA256CryptoServiceProvider();

            byte[] valorByte = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgoritmo.ComputeHash(valorByte);

            return Convert.ToBase64String(byteHash);
        }
    }
}