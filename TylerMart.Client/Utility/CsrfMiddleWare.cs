using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;

namespace TylerMart.Client.Utility {
	/// <summary>
	/// Custom CSRF Middleware implementation courtesy of <see href="https://dasith.me/2020/02/03/integration-test-aspnetcore-api-with-csrf/">Dasith</see>
	/// </summary>
	public class CsrfMiddleWare {
		/// <summary>
		/// XSRF Token Header Name
		/// </summary>
		public const string XsrfTokenHeaderName = "SNDV-XSRF-TOKEN";
		/// <summary>
		/// XSRF Cookie Name
		/// </summary>
		public const string XsrfCookieName = "SNDV-XSRF-TOKEN";
		private readonly RequestDelegate Next;
		private readonly IAntiforgery Antiforgery;
		/// <summary>
		/// Constructor that takes a RequestDelegate instance and an Antiforgery instance
		/// </summary>
		/// <param name="next">Request Delegate</param>
		/// <param name="antiforgery">Antiforgery instance</param>
		public CsrfMiddleWare(RequestDelegate next, IAntiforgery antiforgery) {
			Next = next ?? throw new ArgumentNullException(nameof(next));
			Antiforgery = antiforgery;
		}
		/// <summary>
		/// Add cookies to request's HttpContext
		/// </summary>
		/// <param name="context">Client's Http Context</param>
		/// <remarks>
		/// Is Asynchronous
		/// </remarks>
		/// <returns>
		/// Asynchronous promise
		/// </returns>
		public async Task Invoke(HttpContext context) {
			var tokens = Antiforgery.GetAndStoreTokens(context);
			context.Response.Cookies.Append(
				XsrfCookieName,
				tokens.RequestToken,
				new CookieOptions() {
					HttpOnly = false,
					SameSite = SameSiteMode.Strict
				}
			);
			await Next.Invoke(context);
		}
	}
}
