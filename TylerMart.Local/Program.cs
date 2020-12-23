using System;

using TylerMart.Local.Services;

namespace TylerMart.Local {
	internal class Program {
		private static void Main(string[] args) {
			DatabaseService service = new DatabaseService();
			Console.WriteLine("Hello, world!");
		}
	}
}
