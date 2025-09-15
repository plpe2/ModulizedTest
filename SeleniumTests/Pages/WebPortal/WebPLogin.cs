using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Helpers;
using System;

namespace SeleniumTests
{
    public class WebPLogin
    {
        private readonly WebDriverWait wait;
        private readonly IWebDriver driver;

        public WebPLogin(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void WebPLoginTesting(string url)
        {
            driver.goToURL(url);
        }

    }
}