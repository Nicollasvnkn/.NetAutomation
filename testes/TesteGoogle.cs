using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Selenium;
using selenium;
using System;
using System.Collections.ObjectModel;

namespace testes
{

    public class TesteGoogle
    {
        private IConfiguration _configuration;
        private Browser _browser;

        private IWebDriver _driver;

        public TesteGoogle(IConfiguration configuration, Browser browser)
        {
            _configuration = configuration;
            _browser = browser;

            string pathDriver = null;

            if (browser == Browser.FireFox)
            {
                pathDriver = _configuration.GetSection("Selenium:PathDriverFireFox").Value;
            }
            else if (_browser == Browser.Chrome)
            {
                pathDriver = _configuration.GetSection("Selenium:PathDriverChrome").Value;
            }

            _driver = WebDriverFactory.CreateWebDriver(_browser, pathDriver);
        }

        public void CarregarPagina()
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl("http://www.google.com.br");
        }

        public ReadOnlyCollection<IWebElement> BuscaGoogle(string conteudo)
        {
            IWebElement webElement = _driver.FindElement(By.Name("q"));
            webElement.SendKeys(conteudo);
            webElement.SendKeys(Keys.Enter);

            IWebElement resultsSearch = _driver.FindElement(By.Id("search"));
            return resultsSearch.FindElements(By.XPath(".//a"));
        }

        public void FecharPagina()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
