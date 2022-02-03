using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CryptographicsAlgorithms.Algorithms
{
	public class HashAlgorithms
	{
		public static string GetSHA1Hash(string message)
		{
			uint h0 = 0x67452301,
				h1 = 0xEFCDAB89,
				h2 = 0x98BADCFE,
				h3 = 0x10325476,
				h4 = 0xC3D2E1F0;

			var messageBytesArray = Encoding.UTF8.GetBytes(message);
			var messageBitArray = new BitArray(messageBytesArray);
			var messageBites = MessagePadding(messageBitArray);

			var chanckCount = messageBites.Length / 512;

			for (int i = 0; i < chanckCount; i++)
			{
				BitArray blockMessage = new BitArray(512);
				for (int j = 0; j < 512; j++)
				{
					blockMessage[j] = messageBites[512 * i + j];
				}

				bool temp;
				for (int j = 0; j < 64; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						temp = blockMessage[8 * j + k];
						blockMessage[8 * j + k] = blockMessage[8 * (j + 1) - k - 1];
						blockMessage[8 * (j + 1) - k - 1] = temp;
					}
				}

				var u8 = new byte[64];
				blockMessage.CopyTo(u8, 0);
				var w = new List<uint>();

				for (var j = 0; j < 16; j++)
				{
					w.Add(Convert.ToUInt32(Hex(u8[4 * j]) + Hex(u8[4 * j + 1]) + Hex(u8[4 * j + 2]) + Hex(u8[4 * j + 3]), 16));
				}

				for (var j = 16; j < 80; j++)
				{
					w.Add(BinaryShift((w[j - 3] ^ w[j - 8] ^ w[j - 14] ^ w[j - 16]), 5));
				}

				uint a = h0,
					b = h1,
					c = h2,
					d = h3,
					e = h4;

				uint f = 0, kF = 0;
				for (int j = 0; j < 80; j++)
				{
					if (j < 20)
					{
						f = (b & c) | ((~b) & d);
						kF = 0x5A827999;
					}
					else if (j < 40)
					{
						f = b ^ c ^ d;
						kF = 0x6ED9EBA1;
					}
					else if (j < 60)
					{
						f = (b & c) | (b & d) | (c & d);
						kF = 0x8F1BBCDC;
					}
					else
					{
						f = b ^ c ^ d;
						kF = 0xCA62C1D6;
					}
					uint TEMP = (uint)((BinaryShift(a, 5) + f + e + w[j] + kF) % 4294967296);
			
					e = d;
					d = c;
					c = BinaryShift(b, 30);
					b = a;
					a = TEMP;
				}
				h0 = (uint)((h0 + a) % 4294967296);
				h1 = (uint)((h1 + b) % 4294967296);
				h2 = (uint)((h2 + c) % 4294967296);
				h3 = (uint)((h3 + d) % 4294967296);
				h4 = (uint)((h4 + e) % 4294967296);
			}
			return Hex(h0) + Hex(h1) + Hex(h2) + Hex(h3) + Hex(h4);
		}

		private static uint BinaryShift(uint x, int n)
		{
			return (x << n) | (x >> (32 - n));
		}

		private static string Hex(uint number)
		{
			var presetResult = @"";
			var i = 0;
			while (number != 0)
			{
				if (number % 16 < 10)
				{
					presetResult += (char)(number % 16 + 48);
				}
				else
				{
					switch (number % 16)
					{
						case 10:
							presetResult += @"A";
							break;
						case 11:
							presetResult += @"B";
							break;
						case 12:
							presetResult += @"C";
							break;
						case 13:
							presetResult += @"D";
							break;
						case 14:
							presetResult += @"E";
							break;
						case 15:
							presetResult += @"F";
							break;
					}
				}
				number /= 16;
				i++;
			}
			while (i < 8)
			{
				presetResult += @"0";
				i++;
			}
			var result = @"";
			for (i = 7; i >= 0; i--)
			{
				result += presetResult[i];
			}
			return result;
		}

		private static string Hex(byte Number)
		{
			var H1 = Number / 16;
			var H2 = Number % 16;
			var result = @"";
			if (H1 < 10)
			{
				result += (char)(H1 + 48);
			}
			else
			{
				switch (H1)
				{
					case 10:
						result += @"A";
						break;
					case 11:
						result += @"B";
						break;
					case 12:
						result += @"C";
						break;
					case 13:
						result += @"D";
						break;
					case 14:
						result += @"E";
						break;
					case 15:
						result += @"F";
						break;
				}
			}
			if (H2 < 10)
			{
				result += (char)(H2 + 48);
			}
			else
			{
				switch (H2)
				{
					case 10:
						result += @"A";
						break;
					case 11:
						result += @"B";
						break;
					case 12:
						result += @"C";
						break;
					case 13:
						result += @"D";
						break;
					case 14:
						result += @"E";
						break;
					case 15:
						result += @"F";
						break;
				}
			}
			return result;
		}

		private static BitArray MessagePadding(BitArray message)
		{
			var messageL = new int[1];
			messageL[0] = message.Count;

			message.Length += 1;
			message.Set(messageL[0], true);

			if (message.Count > 448)
			{
				message.Length += (message.Count - 448) % 512 == 0 ? 0 : 512 - (message.Count - 448) % 512;
			}
			else
			{
				message.Length = 448;
			}

			var length = new BitArray(messageL);
			message.Length += 64;
			for (var j = 0; j < 32; j++)
			{
				message[message.Count - j - 1] = length[j];
			}

			return message;
		}
	}
}