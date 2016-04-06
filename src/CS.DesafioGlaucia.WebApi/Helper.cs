using System;
using System.Security.Cryptography;
using System.Text;

namespace CS.DesafioGlaucia.WebApi
{
    public class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm temAlgoritmo = new SHA256CryptoServiceProvider();

            byte[] valorByte = Encoding.UTF8.GetBytes(input);
            byte[] byteHash = temAlgoritmo.ComputeHash(valorByte);

            return Convert.ToBase64String(byteHash);
        }
    }
}