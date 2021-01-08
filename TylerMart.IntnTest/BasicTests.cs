using Xunit;

using TylerMart.Client;
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
			response.EnsureSuccessStatusCode();
			Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}
	}
}
