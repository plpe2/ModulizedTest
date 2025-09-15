using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;

namespace SeleniumTests
{
    public class SubmitApp
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SubmitApp(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void SubmitTest()
        {
            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab4']")).Displayed);
            wait.UntilLoadingDisappears(driver);
            //Application Preview
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[2]/div/ul/li[5]/a"))).Click();
            wait.Until(d => d.FindElement(By.Id("mdlListAppliedPermit")).Displayed);
            driver.FindElement(By.XPath("//*[@id='mdlbtnOkayAP']")).Click();
            wait.Until(d => d.FindElement(By.Id("ModalSubmit")).Displayed);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ModalSubmit']/div/div/div[2]/div[3]/div/div/label"))).Click();
            driver.FindElement(By.Id("btnsavepayment")).Click();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]/div")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[3]/div/div[6]/button[1]")).Click();


            driver.FindElement(By.Id("txtAppNoNew")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.Id("txtAppNoNew")).SendKeys(Keys.Control + "c");
        }
    }
}