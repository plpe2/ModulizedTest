using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Helpers;
using System;

namespace SeleniumTests
{
    public class PTRAXTest
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public PTRAXTest(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void PTRAXLogin()
        {
            driver.goToURL("http://192.168.20.71:1023/Account/DtraxLogin.aspx");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "receiving");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();
        }
    }
}