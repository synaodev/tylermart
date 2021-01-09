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
using TylerMart.Client.Utility;

namespace TylerMart.Client {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddLogging();
			services.AddControllersWithViews(options => {
				options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
			}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
				.AddApplicationPart(typeof(Startup).Assembly);
			services.AddAntiforgery(options => {
				options.HeaderName = CsrfMiddleWare.XsrfTokenHeaderName;
			});
			services.AddSession();
			services.AddHttpContextAccessor();
			services.AddDbContext<DatabaseContext>(builder => {
				builder.UseSqlServer(@"server=localhost,1433;database=TylerMart;user id=sa;password=Password12345;");
			});
			services.AddScoped<DatabaseService>();
		}
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext context) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseMiddleware<CsrfMiddleWare>();
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
			//context.Database.Migrate();
		}
	}
}
