using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using TylerMart.Storage.Contexts;

namespace TylerMart.IntnTest.Utility {
	public class MartFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {
		protected override void ConfigureWebHost(IWebHostBuilder builder) {
			builder.ConfigureServices(services => {
				var descriptor = services.SingleOrDefault(d =>
					d.ServiceType == typeof(DbContextOptions<DatabaseContext>)
				);
				services.Remove(descriptor);
				services.AddDbContext<DatabaseContext>(options => {
					options.UseInMemoryDatabase(databaseName: "InMemoryDb");
				});
				var sp = services.BuildServiceProvider();
				using (var scope = sp.CreateScope()) {
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<DatabaseContext>();
					var logger = scopedServices.GetRequiredService<ILogger<MartFactory<TStartup>>>();
					db.Database.EnsureCreated();
					try {
						// initialize database
					} catch (Exception exception) {
						logger.LogError(
							exception,
							"An error occurred while trying to seed the database! Error: {0}",
							exception.Message
						);
					}
				}
			});
		}
	}
}
