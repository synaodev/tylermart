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
			return password;
		}
		/// <summary>
		/// Login to a Customer's account
		/// </summary>
		/// <param name="db">Database service</param>
		public static Customer LoginCustomer(DatabaseService db) {
			Console.WriteLine("Please enter your email address: ");
			string emailAddress = Console.ReadLine();

			Console.WriteLine("Please enter your password: ");
			string password = ReadPasswordFromInput();

			Customer result = db.Customers.GetByEmailAddress(emailAddress);
			if (result != null && result.Password == password) {
				return result;
			}

			Console.WriteLine("Either your email address or your password were invalid!");
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
			string emailAddress = "";
			string password = "";

			while (firstName.Length == 0) {
				Console.WriteLine("What's your first name? ");
				firstName = Console.ReadLine();
				if (!nameRegex.Match(firstName).Success) {
					Console.WriteLine("First name should only contain letters!");
					firstName = "";
				} else if (firstName.Length < 2) {
					Console.WriteLine("First name must be at least two letters long!");
					firstName = "";
				} else if (firstName.Length > 50) {
					Console.WriteLine("First name must be under fifty letters long!");
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
				} else if (lastName.Length > 50) {
					Console.WriteLine("Last name must be under fifty letters long!");
					lastName = "";
				}
			}
			while (emailAddress.Length == 0) {
				Console.WriteLine("What's your email address? ");
				emailAddress = Console.ReadLine();
				if (!emailRegex.Match(emailAddress).Success) {
					Console.WriteLine("Email address must be in the proper format!");
					emailAddress = "";
				}
			}
			while (true) {
				if (password.Length == 0) {
					Console.WriteLine("Please enter your password: ");
					password = ReadPasswordFromInput();
					if (password.Length < 2) {
						Console.WriteLine("Password must be at least eight letters long!");
						password = "";
					} else if (password.Length > 50) {
						Console.WriteLine("Password must be under fifty letters long!");
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
			bool success = db.Customers.Create(new Customer() {
				FirstName = firstName,
				LastName = lastName,
				EmailAddress = emailAddress,
				Password = password
			});
			if (success) {
				Console.WriteLine("Registration successful!");
			} else {
				Console.WriteLine("Error! Registration failed!");
				Console.WriteLine("I'm sorry.");
			}
		}
	}
}
