using System.Runtime.InteropServices;
using Xunit;

using TylerMart.Client;
using TylerMart.Selenium.Utility;

namespace TylerMart.Selenium {
	public class BasicTests : IClassFixture<MartFactory<Startup>> {
		private readonly MartFactory<Startup> Factory;
		public BasicTests(MartFactory<Startup> factory) {
			Factory = factory;
		}
		[Theory]
		[InlineData(DriverName.Chrome)]
		[InlineData(DriverName.Firefox)]
		[InlineData(DriverName.Edge)]
		public void TestClient(DriverName name) {
			if (name == DriverName.Edge && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
				return;
			}
			using (var driver = Factory.CreateWebDriver(name)) {
				driver.Navigate().GoToUrl($"{Factory.RootUri}/Home/Index");

				Assert.Equal("Home - TylerMart.Client", driver.Title);

				driver.FindElementByXPath("/html/body/div/main/div[2]/div/a[2]").Click();

				Assert.Equal("Register - TylerMart.Client", driver.Title);

				driver.FindElementByXPath("//*[@id=\"FirstName\"]").SendKeys("Tyler");
				driver.FindElementByXPath("//*[@id=\"LastName\"]").SendKeys("Cadena");
				driver.FindElementByXPath("//*[@id=\"Email\"]").SendKeys("tyler.cadena@mail.com");
				driver.FindElementByXPath("//*[@id=\"Password\"]").SendKeys("tylercadena");
				driver.FindElementByXPath("//*[@id=\"PasswordConfirmation\"]").SendKeys("tylercadena");
				driver.FindElementByXPath("//*[@id=\"Address\"]").SendKeys("23222 Remington Way, West Hills, CA, 91307");
				driver.FindElementByXPath("//*[@id=\"DefaultLocation\"]").SendKeys("0");
				driver.FindElementByXPath("/html/body/div/main/div/div[2]/form/div[8]/input").Click();

				Assert.Equal("Login - TylerMart.Client", driver.Title);
			}
		}
	}
}
