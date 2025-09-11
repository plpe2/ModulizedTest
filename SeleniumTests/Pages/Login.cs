using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Helpers;
using System;

namespace SeleniumTests.Pages
{
    public class Login
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public Login(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void LoginTest(string url, string Username, string Password)
        {
            driver.goToURL("http://192.168.20.71:1021/BuildingPermit/Application#");
            driver.selectElement("Prefix", "BLG");
        }
    }
}