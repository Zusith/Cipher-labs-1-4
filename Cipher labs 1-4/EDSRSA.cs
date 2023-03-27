using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_labs_1_4
{
    class EDSRSA
    {
        public void Check(string text)
        {
            RSA rsa = RSA.Create();
            byte[] publicKey = rsa.ExportRSAPublicKey();
            byte[] privateKey = rsa.ExportRSAPrivateKey();
            string message = text;
            byte[] hash = SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(message));
            byte[] signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            Console.WriteLine("Подпись: {0}", Convert.ToBase64String(signature));
            bool verified = rsa.VerifyHash(hash, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            if (verified)
            {
                Console.WriteLine("Подпись верна.");
            }
            else
            {
                Console.WriteLine("Подпись неверна.");
            }
        }
    }
}
