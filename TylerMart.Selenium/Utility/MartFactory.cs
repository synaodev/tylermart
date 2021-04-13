using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using TylerMart.Storage.Contexts;
using TylerMart.Client.Utility;

// Example Page: https://blog-bertrand-thomas.devpro.fr/2020/01/27/fix-breaking-change-asp-net-core-3-integration-tests-selenium/

namespace TylerMart.Selenium.Utility {
	public class MartFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {
		private IWebHost _host;
		public string RootUri { get; private set; }
		public MartFactory() {
			ClientOptions.BaseAddress = new Uri("https://localhost");
			CreateServer(CreateWebHostBuilder());
		}
		protected override TestServer CreateServer(IWebHostBuilder builder) {
			_host = builder.Build();
			_host.Start();
			RootUri = _host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault();
			return new TestServer(new WebHostBuilder().UseStartup<TStartup>());
		}
		protected override IWebHostBuilder CreateWebHostBuilder() {
			var builder = WebHost.CreateDefaultBuilder(Array.Empty<string>());
			builder.UseStartup<TStartup>();
			return builder;
		}
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
		public HttpClient CreateCsrfAwareClient() {
			const string CookieName = CsrfMiddleWare.XsrfCookieName;
			const string HeaderName = CsrfMiddleWare.XsrfTokenHeaderName;
			var client = this.CreateClient();
			var result = client.GetAsync("/").GetAwaiter().GetResult();
			var cookies = result.Headers.GetValues("Set-Cookie").ToList();
			var token = cookies.Single(x => x.StartsWith(CookieName))?.Substring($"{CookieName}=".Length).Split(";")[0];
			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Add(HeaderName, new[] { token });
			client.DefaultRequestHeaders.Add("Cookie", cookies);
			return client;
		}
		[ExcludeFromCodeCoverage]
		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			if (disposing) {
				_host?.Dispose();
			}
		}
	}
}
