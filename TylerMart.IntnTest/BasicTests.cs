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
	public class BasicTests : IClassFixture<MartFactory<Startup>> {
		private readonly MartFactory<Startup> Factory;
		public BasicTests(MartFactory<Startup> factory) {
			Factory = factory;
		}
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
		[Fact]
		public async void TestRegisterAndLogin() {
			var client = Factory.CreateClient();
			// client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
			// 	"Basic",
			// 	Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", )))
			// );

			var registerGet = await client.GetAsync("/Customer/Register");

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



//Assert.Equal("/Customer/Index", response3.RequestMessage.RequestUri.AbsolutePath);
