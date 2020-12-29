using System;
using System.Collections.Generic;

using TylerMart.Domain.Models;
using TylerMart.Terminal.Services;

namespace TylerMart.Terminal {
	internal class Program {
		public static void Main(string[] args) {
			Console.WriteLine("\n**** Welcome to TylerMart ****");

			DatabaseService db = new DatabaseService();
			Customer customer = null;
			bool done = false;

			while (!done) {
				Console.WriteLine("\n[Main Menu]");
				Console.WriteLine("1 - Register");
				if (customer == null) {
					Console.WriteLine("2 - Login");
				} else {
					Console.WriteLine("2 - Logout");
				}
				Console.WriteLine("3 - Fill Order");
				Console.WriteLine("4 - My History");
				Console.WriteLine("5 - Quit");
				switch (Console.ReadKey(true).Key) {
				case ConsoleKey.D1:
					Accounts.RegisterCustomer(db);
					break;
				case ConsoleKey.D2:
					if (customer == null) {
						customer = Accounts.LoginCustomer(db);
					} else {
						customer = null;
					}
					break;
				case ConsoleKey.D3:
					if (customer == null) {
						Console.WriteLine("You must log in first!");
					} else {
						Shopping.MakeOrder(db, customer);
					}
					break;
				case ConsoleKey.D4:
					if (customer == null) {
						Console.WriteLine("You must log in first!");
					} else {
						Console.WriteLine("{0} {1}'s orders:", customer.FirstName, customer.LastName);
						List<Order> orders = db.Orders.FindFromCustomer(customer);
						orders.ForEach(o => {
							Console.Write(o);
						});
					}
					break;
				case ConsoleKey.D5:
					Console.WriteLine("Goodbye!");
					done = true;
					break;
				default:
					Console.WriteLine("Invalid key pressed!");
					break;
				}
			}
		}
	}
}
