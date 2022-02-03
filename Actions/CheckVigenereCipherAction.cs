using CryptographicsAlgorithms.Actions.Base;
using CryptographicsAlgorithms.Algorithms;
using CryptographicsAlgorithms.Enums;
using System;

namespace CryptographicsAlgorithms.Actions
{
	public class CheckVigenereCipherAction : BaseAction
	{
		public override MenuAction Action => MenuAction.CheckVigenereCipher;

		protected override bool ExecuteInternal()
		{
			Console.WriteLine("Please enter key: ");
			var key = Console.ReadLine();

			Console.WriteLine("Please enter value: ");
			var text = Console.ReadLine();

			var encryption = text.VigenereCipher(key);
			var dencryption = encryption.VigenereCipher(key, false);

			Console.WriteLine($"Encryption data: {encryption}");
			Console.WriteLine($"Dencryption data: {dencryption}");

			return text == dencryption;
		}
	}
}
