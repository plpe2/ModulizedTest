using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void LoginTest(string url, string Username, string cNum)
        {
            driver.goToURL(url);

            //Initialize variables Selecting elements 
            driver.selectElement("Prefix", "BLG");
            driver.selectElement("pincode", "1234");
            driver.selectElement("year", "25");
            driver.selectElement("SeriesNo", cNum);
            driver.selectElement("Username", Username);
            driver.selectElement("Password", "P@ssw0rd");

            //Submitting Form
            driver.FindElement(By.XPath("//*[@id='formLogin']/div[6]/div[2]/button")).SendKeys(Keys.Return);

            wait.Until(d => d.FindElement(By.Id("frmVerification")).Displayed);

            var otpCode = driver.FindElement(By.Id("hidVerCode")).GetAttribute("value");
            driver.selectElement("VerificationCode", otpCode);

            IWebElement submitButton = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button.form-control.btn.btn-success")
                )
            );

            // Click the submit button
            submitButton.Click();
        }
    }
}