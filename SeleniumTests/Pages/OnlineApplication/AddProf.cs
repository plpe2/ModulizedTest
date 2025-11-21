using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;

namespace SeleniumTests
{
    public class AddProf
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public AddProf(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void CreateProf()
        {
            driver.ClickElement(wait, "//*/button[normalize-space(text()) = 'Add New Professional']");

        }

        public void AssignProf()
        {

        }

    }
}

