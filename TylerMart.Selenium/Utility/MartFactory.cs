using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Edge.SeleniumTools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

using TylerMart.Storage.Contexts;

// Example Page: https://blog-bertrand-thomas.devpro.fr/2020/01/27/fix-breaking-change-asp-net-core-3-integration-tests-selenium/

namespace TylerMart.Selenium.Utility {
	public enum DriverName {
		Chrome,
		Firefox,
		Edge
	}
	public class MartFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {
		private IWebHost Host;
		public string RootUri { get; private set; }
		public MartFactory() {
			CreateServer(CreateWebHostBuilder());
		}
		protected override TestServer CreateServer(IWebHostBuilder builder) {
			Host = builder.Build();
			Host.Start();
			RootUri = Host.ServerFeatures
				.Get<IServerAddressesFeature>()
				.Addresses.LastOrDefault();
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
		public RemoteWebDriver CreateWebDriver(DriverName name) {
			switch (name) {
			case DriverName.Chrome: {
				var options = new ChromeOptions();
				options.AddArguments("--headless", "--ignore-certificate-errors");
				var location = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ChromeWebDriver")) ?
					System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) :
					Environment.GetEnvironmentVariable("ChromeWebDriver");

				return new ChromeDriver(location, options);
			}
			case DriverName.Firefox: {
				var options = new FirefoxOptions();
				options.AcceptInsecureCertificates = true;
				options.AddArguments("-headless");
				var location = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FirefoxWebDriver")) ?
					System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) :
					Environment.GetEnvironmentVariable("FirefoxWebDriver");

				return new FirefoxDriver(location, options);
			}
			case DriverName.Edge: {
				var options = new EdgeOptions();
				options.AcceptInsecureCertificates = true;
				options.UseChromium = true;
				options.AddArguments("headless", "disable-gpu");
				var location = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EdgeWebDriver")) ?
					System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) :
					Environment.GetEnvironmentVariable("EdgeWebDriver");

				return new EdgeDriver(location, options);
			}
			default:
				return null;
			}
		}
		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			if (disposing) {
				Host?.Dispose();
			}
		}
	}
}
