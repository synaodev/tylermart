using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using Xunit;

using TylerMart.Client;
using TylerMart.Client.Models;
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
				Assert.NotNull(driver);

				driver.Navigate().GoToUrl($"{Factory.RootUri}/Home/Index");
				Assert.Equal("Home - TylerMart.Client", driver.Title);

				driver.FindElementByXPath("/html/body/div/main/div[2]/div/a[1]").Click();
				Assert.Equal("Login - TylerMart.Client", driver.Title);
			}
		}
	}
}
