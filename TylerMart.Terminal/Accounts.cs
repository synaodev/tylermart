using System;
using System.Text.RegularExpressions;

using TylerMart.Domain.Models;
using TylerMart.Terminal.Services;

namespace TylerMart.Terminal {
	/// <summary>
	/// Functions for handling Customer data
	/// </summary>
	public static class Accounts {
		private static string ReadPasswordFromInput() {
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
			Console.Write('\n');
			return password;
		}
		/// <summary>
		/// Login to a Customer's account
		/// </summary>
		/// <param name="db">Database service</param>
		public static Customer LoginCustomer(DatabaseService db) {
			Console.WriteLine("Please enter your email: ");
			string email = Console.ReadLine();

			Console.WriteLine("Please enter your password: ");
			string password = ReadPasswordFromInput();

			Customer result = db.Customers.GetByEmail(email);
			if (result != null && result.Password == password) {
				Console.WriteLine("\nLogin successful!");
				return result;
			}

			Console.WriteLine("\nEither your email or your password were invalid!");
			Console.WriteLine("I'm sorry.");
			return null;
		}
		/// <summary>
		/// Create new Customer
		/// </summary>
		/// <param name="db">Database service</param>
		public static void RegisterCustomer(DatabaseService db) {
			Regex nameRegex = new Regex(@"^[a-zA-Z]+$");
			Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
			string firstName = "";
			string lastName = "";
			string email = "";
			string password = "";
			string address = "";

			while (firstName.Length == 0) {
				Console.WriteLine("What's your first name? ");
				firstName = Console.ReadLine();
				if (!nameRegex.Match(firstName).Success) {
					Console.WriteLine("First name should only contain letters!");
					firstName = "";
				} else if (firstName.Length < 2) {
					Console.WriteLine("First name must be at least two letters long!");
					firstName = "";
				}
			}
			while (lastName.Length == 0) {
				Console.WriteLine("What's your last name? ");
				lastName = Console.ReadLine();
				if (!nameRegex.Match(lastName).Success) {
					Console.WriteLine("Last name should only contain letters!");
					lastName = "";
				} else if (lastName.Length < 2) {
					Console.WriteLine("Last name must be at least two letters long!");
					lastName = "";
				}
			}
			while (email.Length == 0) {
				Console.WriteLine("What's your email? ");
				email = Console.ReadLine();
				if (!emailRegex.Match(email).Success) {
					Console.WriteLine("Email must be in the proper format!");
					email = "";
				}
			}
			while (true) {
				if (password.Length == 0) {
					Console.WriteLine("Please enter your password: ");
					password = ReadPasswordFromInput();
					if (password.Length < 8) {
						Console.WriteLine("Password must be at least eight letters long!");
						password = "";
					}
				} else {
					Console.WriteLine("Please enter your password again: ");
					string passwordTwo = ReadPasswordFromInput();
					if (passwordTwo != password) {
						Console.WriteLine("Error! This is different from your previous attempt! Try again.");
						password = "";
					} else {
						break;
					}
				}
			}
			while (address.Length == 0) {
				Console.WriteLine("What's your real address? ");
				address = Console.ReadLine();
				if (address.Length < 5) {
					Console.WriteLine("Real address must be at least five letters long!");
					address = "";
				}
			}
			bool success = db.Customers.Create(new Customer() {
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				Password = password,
				Address = address
			});
			if (success) {
				Console.WriteLine("\nRegistration successful!");
			} else {
				Console.WriteLine("\nError! Registration failed!");
				Console.WriteLine("I'm sorry.");
			}
		}
	}
}
