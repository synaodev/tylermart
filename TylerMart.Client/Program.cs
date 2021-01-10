using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TylerMart.Client {
	/// <summary>
	/// Contains Main method
	/// </summary>
	public class Program {
		/// <summary>
		/// Main
		/// </summary>
		/// <param name="args">Argument list</param>
		public static void Main(string[] args) {
			CreateHostBuilder(args).Build().Run();
		}
		/// <summary>
		/// Creates web host settings builder
		/// </summary>
		/// <param name="args">Argument list</param>
		/// <returns>
		/// Web host settings builder
		/// </returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
				});
	}
}
