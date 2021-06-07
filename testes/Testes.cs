using System;
using Microsoft.Extensions.Configuration;
using Xunit;
using System.IO;
using Selenium;
using testes;
using OpenQA.Selenium;

namespace Testes
{
    public class Testes
    {
        private IConfiguration _configuration;
        public Testes()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        [Fact]
        public void TestarFireFox()
        {
            ExecutaTesteGoogle(Browser.FireFox);
        }

        [Fact]
        public void TestarChrome()
        {
            ExecutaTesteGoogle(Browser.Chrome);
        }

        private void ExecutaTesteGoogle(Browser browser)
        {
            TesteGoogle testeGoogle = new TesteGoogle(_configuration, browser);
            testeGoogle.CarregarPagina();
            var results = testeGoogle.BuscaGoogle("mfrinfo");
            // foreach (IWebElement result in results)
            // {
            //     Console.WriteLine(result.Text);
            // }
            Assert.True(results.Count > 0);
            testeGoogle.FecharPagina();
        }
    }
}
