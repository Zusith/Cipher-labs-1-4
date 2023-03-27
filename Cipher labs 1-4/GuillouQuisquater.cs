using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_labs_1_4
{
    public class GuillouQuisquater
    {

        static BigInteger p = BigInteger.Parse("1180591620717411303424");
        static BigInteger g = BigInteger.Parse("2");
        static BigInteger x = BigInteger.Parse("123456789");
        static BigInteger y = BigInteger.ModPow(g, x, p);

        public void Cipher()
        {
            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();

            BigInteger k = GenerateRandomNumber();
            BigInteger r = BigInteger.ModPow(g, k, p);
            BigInteger e = CalculateHash(message + r.ToString());
            BigInteger s = (k - x * e) % (p - 1);

            Console.WriteLine("Зашифрованное сообщение: ({0}, {1})", r, s);

            Console.WriteLine("Введите первое число сообщения для проверки подписи:");
            BigInteger r1 = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Введите второе число сообщения для проверки подписи:");
            BigInteger s1 = BigInteger.Parse(Console.ReadLine());

            BigInteger e1 = CalculateHash(message + r1.ToString());
            BigInteger v = BigInteger.ModPow(y, e1, p) * BigInteger.ModPow(r1, s1, p) % p;

            if (v == r1)
            {
                Console.WriteLine("Подпись верна.");
            }
            else
            {
                Console.WriteLine("Подпись несоответствует.");
            }

            Console.ReadLine();
        }

        static BigInteger GenerateRandomNumber()
        {
            Random random = new Random();
            byte[] bytes = new byte[p.ToByteArray().Length];
            random.NextBytes(bytes);
            return new BigInteger(bytes) % (p - 2) + 1;
        }

        static BigInteger CalculateHash(string message)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
            BigInteger hash = 0;

            foreach (byte b in bytes)
            {
                hash = (hash * 256 + b) % (p - 1);
            }

            return hash;
        }
    }
}
