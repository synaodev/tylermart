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
				Console.WriteLine("4 - Search for Customers");
				Console.WriteLine("5 - Customer History");
				Console.WriteLine("6 - Location History");
				Console.WriteLine("7 - Quit");
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
						Console.WriteLine("\nYou must log in first!");
					} else {
						Shopping.MakeOrder(db, customer);
					}
					break;
				case ConsoleKey.D4:
					if (customer == null) {
						Console.WriteLine("\nYou must log in first!");
					} else {
						Console.WriteLine("By first or last name? (F/L)");
						if (Console.ReadKey(true).Key == ConsoleKey.L) {
							Console.WriteLine("Enter last name: ");
							string name = Console.ReadLine();
							db.Customers.FindByLastName(name).ForEach(c => {
								Console.WriteLine(c);
							});
						} else {
							Console.WriteLine("Enter first name: ");
							string name = Console.ReadLine();
							db.Customers.FindByFirstName(name).ForEach(c => {
								Console.WriteLine(c);
							});
						}
					}
					break;
				case ConsoleKey.D5:
					if (customer == null) {
						Console.WriteLine("\nYou must log in first!");
					} else {
						Console.WriteLine("{0} {1}'s orders:", customer.FirstName, customer.LastName);
						List<Order> orders = db.Orders.FindFromCustomerWithDetails(customer);
						orders.ForEach(o => {
							Console.Write(o);
						});
					}
					break;
				case ConsoleKey.D6:
					if (customer == null) {
						Console.WriteLine("\nYou must log in first!");
					} else {
						List<Location> locations = db.Locations.All();
						Console.WriteLine("Here are all the locations: ");
						locations.ForEach(l => Console.WriteLine("\t{0}", l.Name));

						Location location = null;
						while (location == null) {
							Console.WriteLine("Which location would you like to see orders from?");
							string name = Console.ReadLine();
							location = locations.Find(l => String.Compare(l.Name, name, true) == 0);
							if (location == null) {
								Console.WriteLine("That location doesn't exist!");
								Console.WriteLine("I'm sorry.");
							}
						}

						Console.WriteLine("{0} location's orders:", location.Name);
						List<Order> orders = db.Orders.FindFromLocationWithDetails(location);
						orders.ForEach(o => {
							Console.Write(o);
						});
					}
					break;
				case ConsoleKey.D7:
					Console.WriteLine("\nGoodbye!");
					done = true;
					break;
				default:
					Console.WriteLine("\nInvalid key pressed!");
					break;
				}
			}
		}
	}
}
