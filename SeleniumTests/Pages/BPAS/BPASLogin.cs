using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;

namespace SeleniumTests
{
    public class BPASLogin
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public BPASLogin(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void BPASLoginTest()
        {
            driver.goToURL("http://192.168.20.71:1027/");
            driver.FindElement(By.XPath("//*[@id='formRegister']/div/div[1]/input")).SendKeys("admin marla");
            driver.FindElement(By.XPath("//*[@id='password-field']")).SendKeys("adminP@ssw0rd");
            driver.FindElement(By.Id("btnLog")).Click();
            wait.UntilLoadingDisappears(driver);
            wait.Until(d => d.FindElement(By.XPath("//*[@id='ConfirmedLogin']")).Displayed);
            driver.FindElement(By.XPath("//*[@id='btnLogInOK']")).Click();
            wait.UntilLoadingDisappears(driver);
        }

        public void NBPEvalTest()
        {
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationGeodetic");
        }
    }
}