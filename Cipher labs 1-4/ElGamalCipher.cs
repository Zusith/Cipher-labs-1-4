using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_labs_1_4
{
    class ElGamalCipher : Cipher
    {
        Hashtable keyopen = new Hashtable();
        string DecryptedMessage1 = "";
        double keysecret = 0;
        int maxprimenum;
        List<int> primesnumlist;

        public ElGamalCipher(string message = "", int maxprimenum = 3000)
            : base(message)
        {
            this.maxprimenum = maxprimenum;
            primesnumlist = SieveEratosthenes(this.maxprimenum);
        }


        public double KeySecret
        {
            get { return keysecret; }
            set { keysecret = value; }
        }

        public Hashtable Keyopen
        {
            get { return keyopen; }
        }

        public void KeyGeneration()
        {
            Random rnd = new Random();
            double p = 0;

            int rndindex = rnd.Next(0, primesnumlist.Count);
            p = primesnumlist[rndindex];

            double g = 0;

            //g = (int)Math.Pow(1 % p, 1 / (p - 1));

            bool check = true;
            Console.WriteLine(check);
            g = 1;
            List<int> primeFactors = GetPrimeFactors((int)p - 1);
            g = FindPrimitiveRoot((int)p, primeFactors, rnd);

            double x = rnd.Next(2, (int)p - 1);

            double y = Math.Pow(g, x) % p;
            //int y = (int)ydouble;

            keyopen.Add("p", p);
            keyopen.Add("g", g);
            keyopen.Add("y", y);

            keysecret = x;
        }

        public static List<int> SieveEratosthenes(int n) //генерация списка простых чисел в диапозоне от 2 до n
        {
            var numbers = new List<int>();
            //заполнение списка числами от 2 до n-1
            for (int i = 2; i < n; i++)
            {
                numbers.Add(i);
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = 2; j < n; j++)
                {
                    //удаляем кратные числа из списка
                    numbers.Remove(numbers[i] * j);
                }
            }
            return numbers;
        }

        public static bool IsCoprime(int num1, int num2)
        {
            if (num1 == num2)
            {
                return num1 == 1;
            }
            else
            {
                if (num1 > num2)
                {
                    return IsCoprime(num1 - num2, num2);
                }
                else
                {
                    return IsCoprime(num2 - num1, num1);
                }
            }
        }

        public override string Encrypt()
        {
            Random rnd = new Random();

            EncryptedMessage = "";

            double a = 0, b = 0;
            for (int cell = 0; cell < Message.Length; cell++)
            {
                int k = 0;
                double y = (double)keyopen["y"], p = (double)keyopen["p"], g = (double)keyopen["g"];
                Console.WriteLine(y);
                while (true)
                {
                    k = rnd.Next(2, (int)p - 1);
                    if (IsCoprime(k, (int)p - 1))
                    {
                        break;
                    }
                }

                a = Math.Pow(g, k) % p;
                b = (Math.Pow(y, k) * Convert.ToInt32(Message[cell])) % p;
                EncryptedMessage += a + " " + b + " ;";
            }
            return EncryptedMessage;
        }

        public override string Decrypt()
        {
            double p = (double)keyopen["p"];
            double x = keysecret;
            string[] messmass = Message.Split(' ');
            double a = Convert.ToDouble(messmass[0]);
            double b = Convert.ToDouble(messmass[1]);
            double m = (b * Math.Pow(a, (x * (p - 2)))) % p;
            DecryptedMessage1 = Convert.ToString(m);
            return DecryptedMessage = "5";
        }

        static int FindPrimitiveRoot(int prime, List<int> primeFactors, Random rand)
        {
            int primitiveRoot;
            do
            {
                primitiveRoot = rand.Next(2, prime - 1); // выбираем случайное число между 2 и p-1 
            } while (!IsPrimitiveRoot(prime, primitiveRoot, primeFactors)); // повторяем, пока не найдем первообразный корень 

            return primitiveRoot;
        }

        static bool IsPrimitiveRoot(int prime, int primitiveRoot, List<int> primeFactors)
        {
            foreach (int factor in primeFactors)
            {
                BigInteger factorPower = (prime - 1) / factor;
                BigInteger residue = BigInteger.ModPow(primitiveRoot, factorPower, prime);
                if (residue == 1)
                {
                    return false; // если residue равно 1, то primitiveRoot не является первообразным корнем 
                }
            }
            return true;
        }

        static List<int> GetPrimeFactors(int number)
        {
            List<int> primeFactors = new List<int>();
            int factor = 2;
            while (factor * factor <= number)
            {
                if (number % factor == 0)
                {
                    primeFactors.Add(factor);
                    number /= factor;
                }
                else
                {
                    factor++;
                }
            }
            if (number > 1)
            {
                primeFactors.Add(number);
            }
            return primeFactors.Distinct().ToList();
        }
    }
}
