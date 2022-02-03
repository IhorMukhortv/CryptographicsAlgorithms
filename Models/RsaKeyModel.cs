using System.Numerics;

namespace CryptographicsAlgorithms.Models
{
	public class RsaKeyModel
	{
		public BigInteger FirstKey { get; }

		public BigInteger SecondKey { get; }

		public RsaKeyModel(BigInteger firstKey, BigInteger secondKey)
		{
			FirstKey = firstKey;
			SecondKey = secondKey;
		}
	}
}
