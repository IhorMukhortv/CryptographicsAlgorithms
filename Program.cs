using CryptographicsAlgorithms.Algorithms;
using CryptographicsAlgorithms.Enums;
using CryptographicsAlgorithms.Managers;
using CryptographicsAlgorithms.Models;
using System;
using System.Text;

namespace CryptographicsAlgorithms
{
	class Program
	{
		static void Main(string[] args)
		{
			var menuManager = new MenuManager();

			var choice = MenuAction.None;
			while (!menuManager.IsExit(choice))
			{
				Console.WriteLine();

				menuManager.PrintMenu();
				Enum.TryParse(Console.ReadLine(), out choice);

				Console.Clear();

				menuManager.ChoiceAction(choice);
			}

		}
	}
}