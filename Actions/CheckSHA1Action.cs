using CryptographicsAlgorithms.Actions.Base;
using CryptographicsAlgorithms.Algorithms;
using CryptographicsAlgorithms.Enums;
using System;

namespace CryptographicsAlgorithms.Actions
{
	public class CheckSHA1Action : BaseAction
	{
		public override MenuAction Action => MenuAction.CheckSHA1;

		protected override bool ExecuteInternal()
		{
			Console.WriteLine("Please enter value: ");
			var text = Console.ReadLine();

			var hash = HashAlgorithms.GetSHA1Hash(text);

			Console.WriteLine($"Hash data for {text} : ");
			Console.WriteLine(hash);

			return true;
		}
	}
}