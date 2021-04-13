using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Xunit;

using TylerMart.Client;
using TylerMart.Client.Models;
using TylerMart.Selenium.Utility;

namespace TylerMart.Selenium {
	public class BasicTests : IClassFixture<MartFactory<Startup>>, IDisposable {
		private readonly MartFactory<Startup> Factory;
		private readonly HttpClient Http;
		private readonly RemoteWebDriver Driver;
		public BasicTests(MartFactory<Startup> factory) {
			Factory = factory;
			Http = factory.CreateCsrfAwareClient();
			var options = new ChromeOptions();
			options.AddArguments("--headless", "--ignore-certificate-errors");
			var location = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ChromeWebDriver")) ?
				System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) :
				Environment.GetEnvironmentVariable("ChromeWebDriver");
			Driver = new ChromeDriver(location, options);
		}
		[Fact]
		public void TestClient() {
			Assert.Equal("https://localhost:5001", Factory.RootUri);
			Driver.Navigate().GoToUrl($"{Factory.RootUri}/Home/Index");
			Assert.Equal("Home - TylerMart.Client", Driver.Title);
		}
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				Driver?.Dispose();
			}
		}
	}
}
