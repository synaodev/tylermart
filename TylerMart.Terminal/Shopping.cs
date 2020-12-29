using System;
using System.Collections.Generic;
using System.Linq;

using TylerMart.Domain.Models;
using TylerMart.Terminal.Services;

namespace TylerMart.Terminal {
	/// <summary>
	/// Functions for handling Orders
	/// </summary>
	public static class Shopping {
		private static Dictionary<Product, int> CloneInventory(Dictionary<Product, int> inventory) {
			Dictionary<Product, int> result = new Dictionary<Product, int>();
			foreach (var pc in inventory) {
				result.Add(pc.Key, pc.Value);
			}
			return result;
		}
		/// <summary>
		/// Create a new Order
		/// </summary>
		/// <param name="db">Database service</param>
		/// <param name="customer">Customer making the Order</param>
		public static void MakeOrder(DatabaseService db, Customer customer) {
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
					Console.WriteLine("\t{0}: {1} ({2})", pc.Key.Name, pc.Value, pc.Key.Description);
				}
				Console.WriteLine("Please choose one: (type 'rm' to remove something)");
				string input = Console.ReadLine();
				Product possible = null;
				if (String.Compare(input, "rm", true) != 0) {
					possible = inventory.Keys.SingleOrDefault(p => String.Compare(p.Name, input, true) == 0);
				}
				if (possible == null && String.Compare(input, "rm", true) != 0) {
					Console.WriteLine("That product doesn't exist!");
					Console.WriteLine("I'm sorry.");
				} else if (possible != null && inventory[possible] > 0) {
					--inventory[possible];
					shoppingCart.Add(possible);
					Console.WriteLine("Adding {0} to your shopping list", possible.Name);
				} else if (possible != null) {
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
					Console.WriteLine("Do you actually want to buy anything? (Y/N)");
					if (Console.ReadKey(true).Key == ConsoleKey.N) {
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
			Console.WriteLine("Order sent!");
		}
	}
}
