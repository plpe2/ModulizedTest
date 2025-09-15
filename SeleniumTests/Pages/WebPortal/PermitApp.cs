using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace SeleniumTests
{
    public class PermitApp
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public PermitApp(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void ReceiveApp(string url)
        {
            driver.goToURL(url);
        }
    }
}