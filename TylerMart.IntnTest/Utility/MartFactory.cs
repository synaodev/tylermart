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
using TylerMart.Client.Utility;

namespace TylerMart.IntnTest.Utility {
	public class MartFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {
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
		// https://dasith.me/2020/02/03/integration-test-aspnetcore-api-with-csrf/
		public async Task<HttpClient> CreateCsrfAwareClientAsync() {
			const string CookieName = CsrfMiddleWare.XsrfCookieName;
			const string HeaderName = CsrfMiddleWare.XsrfTokenHeaderName;
			var client = this.CreateClient();
			var testResult = await client.GetAsync("/");
			var cookies = testResult.Headers.GetValues("Set-Cookie").ToList();
			var token = cookies.Single(x => x.StartsWith(CookieName))?.Substring($"{CookieName}=".Length).Split(";")[0];
			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Add(HeaderName, new[] { token });
			client.DefaultRequestHeaders.Add("Cookie", cookies);
			return client;
		}
	}
}
