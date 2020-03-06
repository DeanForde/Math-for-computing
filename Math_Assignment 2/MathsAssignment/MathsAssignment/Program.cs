using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MathsAssignment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ProcessMenu();
        }

        static int DisplayMenu()
        {
            int option;
            Console.WriteLine("1) sum of all multiples of 3 and 5 below a specified value.");
            Console.WriteLine("2) prime factorization of a positive integer.");
            Console.WriteLine("3) sum of all even Fibonacci numbers.");
            Console.WriteLine("4) extended Euclidean algorithm.");
            Console.WriteLine("5) RSA encryption.");
            Console.WriteLine("6) RSA decryption.");
            Console.WriteLine("7) Quit console application");
            Console.WriteLine("Select: ");
            option = int.Parse(Console.ReadLine());
            return option;
        }

        static void ProcessMenu()
        {
            int option; 
            do
            {
                option = DisplayMenu();
                switch (option)
                {
                    case 1:
                        MultiplesOf3And5();
                        break;
                    case 2:
                        PrimeFactors();
                        break;
                    case 3:
                        EvenFib();
                        break;
                    case 4:
                        ExtEucAlg();
                        break;
                    case 5:
                        RSAEncrypt();
                        break;
                    case 6:
                        RSADecrypt();
                        break;
                    case 7:
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Error, enter a valid option");
                        break;
                }
            } while (option != 99);
        }

        // Used a binary array instead of the 'mod' operator as it is a faster
        // solution and helps prevent duplication of common numbers.
        static void MultiplesOf3And5()
        {
            int sum = 0;
            Console.WriteLine("Enter a number <= 50,000: ");
            int n = int.Parse(Console.ReadLine());
            bool[] nums = new bool[n];
            for (int i = 1; i<n; ++i)
                nums[i] = Convert.ToBoolean(0);
            for (int i = 3; i<n; i += 3)
                nums[i] = Convert.ToBoolean(1);
            for (int i = 5; i<n; i += 5)
                nums[i] = Convert.ToBoolean(1);
            for (int i = 1; i<n; ++i)
                if (nums[i])
                    sum = sum + i;
        Console.WriteLine(sum);
        }

    static void PrimeFactors()
        {
            Console.WriteLine("Enter integer n (0 to stop): ");
            BigInteger a = BigInteger.Parse(Console.ReadLine());
            while (a > 0)
            {
                List<BigInteger> primeFactors = PrimeFactorization(a);
                FactorList(primeFactors);
                a = BigInteger.Parse(Console.ReadLine());
            }
        }

        /// Find prime factors.
        static List<BigInteger> PrimeFactorization(BigInteger a)
        {
            List<BigInteger> retval = new List<BigInteger>();
            for (BigInteger b = 2; a > 1; b++)
            {
                while (a % b == 0)
                {
                    a /= b;
                    retval.Add(b);
                }
            }
            return retval;
        }

        static void FactorList(List<BigInteger> FactorList)
        {
            if (FactorList.Count == 1)
            {
                Console.WriteLine("{0} is Prime", FactorList[0]);
            }
            else
            { 
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < FactorList.Count; ++i)
                {
                    if (i > 0)
                    {
                        sb.Append('*');
                    }
                    sb.AppendFormat("{0}", FactorList[i]);
                }
                Console.WriteLine(sb.ToString());
            }
        }

        static void EvenFib()
        {
            BigInteger back = 0;
            BigInteger front = 2;
            BigInteger current = 0;
            BigInteger sum = front;
            BigInteger n;
            Console.WriteLine("Enter n: ");
            n = Convert.ToUInt64(Console.ReadLine());
            while (front <= n)
            {
                sum += current;
                current = 4 * front + back;
                back = front;
                front = current;
            }
            Console.WriteLine(sum);
        }

        static void ExtEucAlg()
        {
            BigInteger a;
            BigInteger b;
            Console.WriteLine("Enter a: ");
            a = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Enter b: ");
            b = BigInteger.Parse(Console.ReadLine());
            XEuc(a, b);
        }

        static void XEuc(BigInteger x, BigInteger y)
        {
            BigInteger[] U = {x, 1, 0};
            BigInteger[] V = {y, 0, 1};
            BigInteger[] W = {0, 0, 0};
            BigInteger a;
            BigInteger floor = 0;
            while(V[0] > 0)
            {
                floor = U[0] / V[0];
                for (int i = 0; i <= 2; i++)
                {
                    W[i] = U[i]-(V[i]*floor);
                    U[i] = V[i];
                    V[i] = W[i];
                }
            }
            a = U[0];
            if (a == (x * U[1]) + (y * U[2]))
                Console.WriteLine("GCD = " + a + "; " + "x = " + U[1] + "; " + "y =" + U[2] + ";");
            else
                Console.WriteLine("Does not satisfy d = ax + by");
        }

        static void RSAEncrypt()
        {
            BigInteger P;
            Console.WriteLine("Enter P: ");
            P = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Enter n: ");
            BigInteger n = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Enter e");
            BigInteger e = BigInteger.Parse(Console.ReadLine());
            BigInteger C = ModPow(P, e, n);
            Console.WriteLine(C);
        }

        static void RSADecrypt()
        {
            Console.WriteLine("Enter C: ");
            BigInteger C = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Enter d: ");
            BigInteger d = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Enter n: ");
            BigInteger n = BigInteger.Parse(Console.ReadLine());
            BigInteger P = ModPow(C, d, n);
            Console.WriteLine("Plaintext: " + P);
        }

        private static BigInteger ModPow(BigInteger baseNum, BigInteger exponent, BigInteger modulus)
        {
            BigInteger pow = 1;
            if (modulus == 1)
                return 0;
            BigInteger curPow = baseNum % modulus;
            BigInteger res = 1;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                    res = (res * curPow) % modulus;
                exponent = exponent / 2;
                curPow = (curPow * curPow) % modulus;  // square curPow
            }
            return res;
        }

        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
