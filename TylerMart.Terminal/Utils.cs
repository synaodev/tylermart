using System;

namespace TylerMart.Terminal {
	/// <summary>
	/// Utilities for application
	/// </summary>
	public static class Utils {
		/// <summary>
		/// Reads password from console input
		/// </summary>
		/// <returns>
		/// The password
		/// </returns>
		public static string ReadPasswordFromInput() {
			string password = "";
			while (true) {
				ConsoleKeyInfo k = Console.ReadKey(true);
				if (k.Key == ConsoleKey.Enter) {
					break;
				} else if (k.Key == ConsoleKey.Backspace) {
					if (password.Length > 0) {
						password = password.Remove(password.Length - 1);
						Console.Write("\b \b");
					}
				} else if (k.KeyChar != '\u0000') {
					password += k.KeyChar;
					Console.Write("*");
				}
			}
			return password;
		}
	}
}
