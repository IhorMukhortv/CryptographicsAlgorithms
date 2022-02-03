using CryptographicsAlgorithms.Actions.Base;
using CryptographicsAlgorithms.Algorithms;
using CryptographicsAlgorithms.Enums;
using CryptographicsAlgorithms.Models;
using System;

namespace CryptographicsAlgorithms.Actions
{
	public class CheckRsaAction : BaseAction
	{
		public override MenuAction Action => MenuAction.CheckRsa;

		protected override bool ExecuteInternal()
		{
			(RsaKeyModel privateKey, RsaKeyModel publicKey) = DigitalSignaturesAlgorithms.GenerateRsaKeys();
			Console.WriteLine($"Private key {privateKey.FirstKey} : {privateKey.SecondKey}");
			Console.WriteLine($"Public key {publicKey.FirstKey} : {publicKey.SecondKey}");

			Console.WriteLine("Please enter value: ");
			var text = Console.ReadLine();

			var resultEncript = DigitalSignaturesAlgorithms.EncriptDecriptRsa(text, publicKey);
			var resultDecript = DigitalSignaturesAlgorithms.EncriptDecriptRsa(resultEncript, privateKey);

			Console.WriteLine($"Encript : {resultEncript}");
			Console.WriteLine($"Decript : {resultDecript}");

			return true;
		}
	}
}