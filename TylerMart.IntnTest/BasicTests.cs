using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

using TylerMart.Client;
using TylerMart.IntnTest.Utility;

namespace TylerMart.IntnTest {
	public class BasicTests : IClassFixture<MartFactory<Startup>> {
		private readonly HttpClient Client;
		private readonly MartFactory<Startup> Factory;
		public BasicTests(MartFactory<Startup> factory) {
			Factory = factory;
			Client = factory.CreateClient(new WebApplicationFactoryClientOptions() {
				AllowAutoRedirect = false
			});
		}
		[Fact]
		public async void TestInitialization() {
			var defaultPage = await Client.GetAsync("/");
		}
	}
}
