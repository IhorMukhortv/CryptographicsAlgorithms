using CryptographicsAlgorithms.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CryptographicsAlgorithms.Algorithms
{
	public class DigitalSignaturesAlgorithms
	{
		public static (RsaKeyModel, RsaKeyModel) GenerateRsaKeys(BigInteger? q = null, BigInteger? p = null)
		{
			if (!(q.HasValue && IsSimple(q.Value) 
				&& p.HasValue && IsSimple(p.Value)
				&& p.Value != q.Value))
			{
				(q, p) = GetRandomPrimeNumbers();
			}

			BigInteger n = p.Value * q.Value;
			BigInteger m = (p.Value - 1) * (q.Value - 1);
			BigInteger d = SearchCoprimeNumber(m);
			BigInteger e = GetE(m, d);

			var privateKey = new RsaKeyModel(d, n);
			var publicKey = new RsaKeyModel(e, n);
			return (privateKey, publicKey);
		}

		public static string EncriptDecriptRsa(string message, RsaKeyModel key)
		{
			var result = new StringBuilder();

			for (int i = 0; i < message.Length; i++)
			{
				var messageBigInt = new BigInteger(message[i]);
				var c = Pow(messageBigInt, key.FirstKey) % key.SecondKey;
				
				result.Append((char)c);
			}

			return result.ToString();
		}

		private static BigInteger GetE(BigInteger m, BigInteger d)
		{
			for (int i = 1; ; i++)
			{
				var res = (i * d) % m;
				if (res == 1 && d != i)
				{
					return i;
				}
			}

			return 0;
		}

		private static (BigInteger, BigInteger) GetRandomPrimeNumbers()
		{
			BigInteger startPosition = 10;
			BigInteger endPosition = startPosition + new BigInteger(20);

			var result = new List<BigInteger>();
			for (var i = startPosition; i < endPosition; i++)
			{
				if (IsSimple(i))
				{
					result.Add(i);
				}
			}

			var rnd = new Random();

			var qIndex = rnd.Next(0, result.Count);
			var pIndex = rnd.Next(0, result.Count);

			BigInteger q = result[qIndex];
			BigInteger p = result[pIndex];

			return (q, p);
		}

		private static BigInteger SearchCoprimeNumber(BigInteger number)
		{
			var rnd = new Random();
			BigInteger coprimeNumber = 1;
			var results = new List<BigInteger>();
			while (true) {
				BigInteger nod = 0;

				if (coprimeNumber < number)
				{
					nod = NOD(coprimeNumber, number);
				}
				else
				{
					break;
				}

				if (nod == 1)
				{
					results.Add(coprimeNumber);
				}

				coprimeNumber++;
			}

			var resultIndex = rnd.Next(1, results.Count);
			return results[resultIndex];
		}

		private static BigInteger NOD(BigInteger val1, BigInteger val2)
		{
			if (val2 == 0)
			{
				return val1;
			}
			else
			{
				return NOD(val2, val1 % val2);
			}
		}
		
		private static bool IsSimple(BigInteger n)
		{
			for (BigInteger i = 2; i <= n / 2; i++)
			{
				if (n % i == 0)
				{
					return false;
				}
			}
			return true;
		}

		private static BigInteger Sqrt(BigInteger n)
		{
			if (n == 0)
			{
				return 0;
			}
			if (n > 0)
			{
				int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
				BigInteger root = BigInteger.One << (bitLength / 2);

				while (!IsSqrt(n, root))
				{
					root += n / root;
					root /= 2;
				}

				return root;
			}

			throw new ArithmeticException("NaN");
		}

		private static bool IsSqrt(BigInteger n, BigInteger root)
		{
			BigInteger lowerBound = root * root;
			BigInteger upperBound = (root + 1) * (root + 1);

			return (n >= lowerBound && n < upperBound);
		}

		private static BigInteger Pow(BigInteger a, BigInteger b)
		{
			BigInteger total = 1;
			while (b > int.MaxValue)
			{
				b -= int.MaxValue;
				total *= BigInteger.Pow(a, int.MaxValue);
			}
			total *= BigInteger.Pow(a, (int)b);
			return total;
		}
	}
}