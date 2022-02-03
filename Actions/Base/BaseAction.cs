using CryptographicsAlgorithms.Enums;
using System;

namespace CryptographicsAlgorithms.Actions.Base
{
	public abstract class BaseAction
	{
		public abstract MenuAction Action { get; }

		public virtual bool Execute()
		{
			try
			{
				return ExecuteInternal();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		protected abstract bool ExecuteInternal();
	}
}