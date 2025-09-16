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

        public void ReceiveApp()
        {
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='tblPermitApplications_processing']"));
                return elements.Count == 0 || !elements[0].Displayed;
            });
            driver.FindElement(By.Id("txtApplicationNumber")).SendKeys("EXU2509-00001");
            driver.FindElement(By.Id("btnSearchApplicant")).Click();
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='tblPermitApplications_processing']"));
                return elements.Count == 0 || !elements[0].Displayed;
            });
            driver.FindElement(By.XPath("//*/td[text()='EXU2509-00001']")).Click();
        }
    }
}