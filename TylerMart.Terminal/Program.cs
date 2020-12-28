using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using TylerMart.Domain.Models;
using TylerMart.Terminal.Services;

namespace TylerMart.Terminal {
	internal class Program {
		private static Dictionary<Product, int> CloneInventory(Dictionary<Product, int> inventory) {
			Dictionary<Product, int> result = new Dictionary<Product, int>();
			foreach (var pc in inventory) {
				result.Add(pc.Key, pc.Value);
			}
			return result;
		}
		private static void OrderMenu(DatabaseService db, Customer customer) {
			List<Location> locations = db.Locations.All();
			Console.WriteLine("Here are all the locations: ");
			locations.ForEach(l => Console.WriteLine("\t{0}", l.Name));

			Location location = null;
			while (location == null) {
				Console.WriteLine("Where would you like to order from?");
				string name = Console.ReadLine();
				location = locations.Find(l => String.Compare(l.Name, name, true) == 0);
				if (location == null) {
					Console.WriteLine("That location doesn't exist!");
					Console.WriteLine("I'm sorry.");
				}
			}

			Dictionary<Product, int> inventory = db.Products.CountAtLocation(location);
			Dictionary<Product, int> inventoryClone = CloneInventory(inventory);
			List<Product> shoppingCart = new List<Product>();
			while (true) {
				Console.WriteLine("Here are all of products available at {0}: ", location.Name);
				foreach (var pc in inventory) {
					Console.WriteLine("\t{0}: {1} ", pc.Key.Name, pc.Value);
				}
				Console.WriteLine("Please choose one: ");
				string input = Console.ReadLine();
				Product possible = inventory.Keys.SingleOrDefault(p => String.Compare(p.Name, input, true) == 0);
				if (possible == null) {
					Console.WriteLine("That product doesn't exist!");
					Console.WriteLine("I'm sorry.");
				} else if (inventory[possible] > 0) {
					--inventory[possible];
					shoppingCart.Add(possible);
					Console.WriteLine("Adding {0} to your shopping list", possible.Name);
				} else {
					Console.WriteLine("You can't take more than {0} of {1}!", inventoryClone[possible], possible.Name);
				}
				if (shoppingCart.Count > 0) {
					Console.WriteLine("Here's your list so far: ");
					shoppingCart.ForEach(p => {
						Console.WriteLine("\t{0}", p.Name);
					});
					Console.WriteLine("Would you like to remove anything? (Y/N)");
					if (Console.ReadKey(true).Key == ConsoleKey.Y) {
						Console.WriteLine("Which would you like to remove?");
						string input2 = Console.ReadLine();
						Product possible2 = shoppingCart.Find(p => String.Compare(p.Name, input2, true) == 0);
						if (possible2 == null) {
							Console.WriteLine("That's not a product on your list....");
						} else {
							++inventory[possible2];
							shoppingCart.Remove(possible2);
							Console.WriteLine("Removing {0} to your shopping list", possible2.Name);
						}
					} else {
						Console.WriteLine("Then are you done here? (Y/N)");
						if (Console.ReadKey(true).Key == ConsoleKey.Y) {
							break;
						}
					}
				} else {
					Console.WriteLine("Do you not want to buy anything?");
					if (Console.ReadKey(true).Key == ConsoleKey.Y) {
						Console.WriteLine("Okay.");
						return;
					}
				}
			}
			DateTime now = DateTime.Now;
			db.Orders.Create(new Order() {
				Complete = false,
				CreatedAt = now,
				CustomerID = customer.ID,
				LocationID = location.ID
			});
			Order order = db.Orders.GetByTimestamp(now);
			db.Orders.AddProducts(order, shoppingCart);
			db.Locations.RemoveProducts(location, shoppingCart);
		}
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
		private static Customer LoginCustomer(DatabaseService db) {
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
		private static void RegisterCustomer(DatabaseService db) {
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
		private static void Main(string[] args) {
			Console.WriteLine("**** Welcome to TylerMart ****\n");

			DatabaseService db = new DatabaseService();
			Customer customer = null;
			bool done = false;

			while (!done) {
				Console.WriteLine("[Main Menu]");
				Console.WriteLine("1 - Register");
				if (customer == null) {
					Console.WriteLine("2 - Login");
				} else {
					Console.WriteLine("2 - Logout");
				}
				Console.WriteLine("3 - Fill Order");
				Console.WriteLine("4 - My History");
				Console.WriteLine("4 - Quit");
				switch (Console.ReadKey(true).Key) {
				case ConsoleKey.D1:
					RegisterCustomer(db);
					break;
				case ConsoleKey.D2:
					if (customer == null) {
						customer = LoginCustomer(db);
					} else {
						customer = null;
					}
					break;
				case ConsoleKey.D3:
					if (customer == null) {
						Console.WriteLine("You must log in first!");
					} else {
						OrderMenu(db, customer);
					}
					break;
				case ConsoleKey.D4:
					if (customer == null) {
						Console.WriteLine("You must log in first!");
					} else {
						Console.WriteLine("{0} {1}'s orders:", customer.FirstName, customer.LastName);
						List<Order> orders = db.Orders.FindFromCustomer(customer);
						orders.ForEach(o => {
							Console.WriteLine(o.ToString());
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
