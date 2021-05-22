using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using TylerMart.Storage.Contexts;

namespace TylerMart.IntnTest.Utility {
	/// <summary>
	/// Test Client Factory
	/// </summary>
	public class MartFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {
		/// <summary>
		/// Configure web host
		/// </summary>
		/// <param name="builder">Web host settings builder</param>
		/// <remarks>
		/// Changes database provider from SQL Server to In-Memory
		/// </remarks>
		protected override void ConfigureWebHost(IWebHostBuilder builder) {
			builder.UseContentRoot(".");
			base.ConfigureWebHost(builder);
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
					db.Database.EnsureDeleted();
					db.Database.EnsureCreated();
				}
			});
		}
	}
}
