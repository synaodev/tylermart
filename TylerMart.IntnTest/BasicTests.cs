using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings;
using Xunit;

using TylerMart.Client;
using TylerMart.Client.Models;
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
		public async void TestEndpoints(string url) {
			var client = Factory.CreateClient();
			var response = await client.GetAsync(url);
			Assert.True(response.IsSuccessStatusCode);
			Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}
		/// <summary>
		/// Checks if user can register and login using CSRF-protected POST requests
		/// </summary>
		[Fact]
		public async void TestRegisterAndLogin() {
			var client = await Factory.CreateCsrfAwareClientAsync();
			var registerForm = new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("FirstName", "Tyler"),
				new KeyValuePair<string, string>("LastName", "Cadena"),
				new KeyValuePair<string, string>("Email", "tyler.cadena@revature.net"),
				new KeyValuePair<string, string>("Password", "tylercadena"),
				new KeyValuePair<string, string>("PasswordConfirmation", "tylercadena"),
				new KeyValuePair<string, string>("Address", "23222 Remington Way, West Hills, CA, 91307")
			};
			var registerResponse = await client.PostAsync(
				"/Customer/Register",
				new FormUrlEncodedContent(registerForm)
			);
			Assert.True(registerResponse.IsSuccessStatusCode);
			var loginForm = new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("Email", "tyler.cadena@revature.net"),
				new KeyValuePair<string, string>("Password", "tylercadena")
			};
			var loginResponse = await client.PostAsync(
				"/Customer/Login",
				new FormUrlEncodedContent(loginForm)
			);
			Assert.True(loginResponse.IsSuccessStatusCode);
			Assert.Equal("/Customer/Index", loginResponse.RequestMessage.RequestUri.AbsolutePath);
		}
	}
}
