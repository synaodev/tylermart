using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TylerMart.Storage.Contexts;
using TylerMart.Client.Services;

namespace TylerMart.Client {
	/// <summary>
	/// Startup class
	/// </summary>
	public class Startup {
		/// <summary>
		/// Constructor that takes an instance of IConfiguration
		/// </summary>
		/// <param name="configuration">"appsettings.json" configuration file</param>
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}
		/// <summary>
		/// Configuration file
		/// </summary>
		public IConfiguration Configuration { get; }
		/// <summary>
		/// Configure application services
		/// </summary>
		/// <param name="services">Services builder</param>
		/// <remarks>
		/// This method gets called by the runtime. Use this method to add services to the container
		/// </remarks>
		public void ConfigureServices(IServiceCollection services) {
			services.AddLogging();
			services.AddControllersWithViews();
				//.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
				//.AddApplicationPart(typeof(Startup).Assembly);
			services.AddSession();
			services.AddHttpContextAccessor();
			services.AddDbContext<DatabaseContext>(builder => {
				builder.UseSqlServer(@"server=localhost,1433;database=TylerMart;user id=sa;password=Password12345;");
			});
			services.AddScoped<DatabaseService>();
		}
		/// <summary>
		/// Configure runtime settings
		/// </summary>
		/// <param name="app">Application runtime builder</param>
		/// <param name="env">Web host environment</param>
		/// <param name="context">Injected database context</param>
		/// <remarks>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
		/// </remarks>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext context) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}"
				);
			});
		}
	}
}
