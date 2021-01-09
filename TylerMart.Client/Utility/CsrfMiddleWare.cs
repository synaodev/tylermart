using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;

namespace TylerMart.Client.Utility {
	public class CsrfMiddleWare {
		public const string XsrfTokenHeaderName = "SNDV-XSRF-TOKEN";
		public const string XsrfCookieName = "SNDV-XSRF-TOKEN";
		private readonly RequestDelegate Next;
		private readonly IAntiforgery Antiforgery;
		public CsrfMiddleWare(RequestDelegate next, IAntiforgery antiforgery) {
			Next = next ?? throw new ArgumentNullException(nameof(next));
			Antiforgery = antiforgery;
		}
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
