using CryptographicsAlgorithms.Actions;
using CryptographicsAlgorithms.Actions.Base;
using CryptographicsAlgorithms.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptographicsAlgorithms.Managers
{
	public class MenuManager
	{
		private IList<BaseAction> _actions;

		public MenuManager()
		{
			_actions = new List<BaseAction>();
			_actions.Add(new CheckVigenereCipherAction());
			_actions.Add(new CheckSHA1Action());
			_actions.Add(new CheckRsaAction());
		}

		public void PrintMenu()
		{
			Console.WriteLine("This is program created for test implement algorithms (VigenereCipher, SHA1, Rsa)");
			Console.WriteLine("Enter one option from menu");
			Console.WriteLine("Choice one option from menu");
			Console.WriteLine("1 - Check Vigenere Cipher");
			Console.WriteLine("2 - Check SHA1");
			Console.WriteLine("3 - Check Rsa");
			Console.WriteLine("4 - Close");
		}

		public bool ChoiceAction(MenuAction menuAction)
		{
			var action = _actions.FirstOrDefault(x => x.Action == menuAction);
			if (action == null)
			{
				Console.WriteLine("Please select other menu option");
				return false;
			}

			return action.Execute();
		}

		public bool IsExit(MenuAction action)
		{
			return action == MenuAction.Exit;
		}
	}
}