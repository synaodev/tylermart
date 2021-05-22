using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings;
using Xunit;

using TylerMart.Client;
using TylerMart.IntnTest.Utility;

namespace TylerMart.IntnTest {
	/// <summary>
	/// Basic Tests of <see cref="TylerMart.Client.Startup"/>
	/// </summary>
	public class BasicTests : IClassFixture<MartFactory<Startup>> {
		private readonly MartFactory<Startup> Factory;
		/// <summary>
		/// Constructor that takes injected client factory
		/// </summary>
		/// <param name="factory">Client factory</param>
		public BasicTests(MartFactory<Startup> factory) {
			Factory = factory;
		}
		/// <summary>
		/// Checks if all endpoints are reached without incident
		/// </summary>
		/// <param name="url">URL</param>
		[Theory]
		[InlineData("/")]
		[InlineData("/Home/Index")]
		[InlineData("/Home/Privacy")]
		[InlineData("/Customer/Login")]
		[InlineData("/Customer/Register")]
		public void TestEndpoints(string url) {
			var client = Factory.CreateClient();
			var response = client.GetAsync(url)
				.GetAwaiter().GetResult();
			Assert.True(response.IsSuccessStatusCode);
			Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}
		/// <summary>
		/// Checks if user can register and login using CSRF-protected POST requests
		/// </summary>
		[Fact]
		public void TestRegisterAndLogin() {
			var client = Factory.CreateClient();
			var registerForm = new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("FirstName", "Tyler"),
				new KeyValuePair<string, string>("LastName", "Cadena"),
				new KeyValuePair<string, string>("Email", "tyler.cadena@revature.net"),
				new KeyValuePair<string, string>("Password", "tylercadena"),
				new KeyValuePair<string, string>("PasswordConfirmation", "tylercadena"),
				new KeyValuePair<string, string>("Address", "23222 Remington Way, West Hills, CA, 91307")
			};
			var registerResponse = client.PostAsync(
				"/Customer/Register",
				new FormUrlEncodedContent(registerForm)
			).GetAwaiter().GetResult();
			Assert.True(registerResponse.IsSuccessStatusCode);
			var loginForm = new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("Email", "tyler.cadena@revature.net"),
				new KeyValuePair<string, string>("Password", "tylercadena")
			};
			var loginResponse = client.PostAsync(
				"/Customer/Login",
				new FormUrlEncodedContent(loginForm)
			).GetAwaiter().GetResult();
			Assert.True(loginResponse.IsSuccessStatusCode);
			Assert.Equal("/Customer/Index", loginResponse.RequestMessage.RequestUri.AbsolutePath);
		}
	}
}
