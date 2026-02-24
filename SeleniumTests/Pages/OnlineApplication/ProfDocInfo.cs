using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V125.Network;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeleniumTests
{
    public class ProfDocInfo
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public ProfDocInfo(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void ProfDocTest()
        {
            wait.UntilLoadingDisappears(driver);

            //Professional Information
            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab3']")).Displayed);

            // driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[2]/a")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab3']")).Displayed);

            driver.FindElement(By.XPath("//*[@id='btnSearchProfLicense']")).Click();
            wait.Until(d => d.FindElement(By.XPath("//*[@id='ModalExistingProf']/div")).Displayed);
            var profrecord = driver.FindElement(By.XPath("//*[@id='tblExistingProfLicense']/tbody/tr[1]/td[1]"));
            var WaitedRecord = profrecord.GetAttribute("value");
            profrecord.Click();
            driver.selectDropdown(wait, "Designationmodal", "Plans and Specification");
            driver.FindElement(By.XPath("//*[@id='btnSaveExistingProf']")).Click();
            wait.UntilLoadingDisappears(driver);
            // Need to refactor in order to work
            // wait.Until(d => d.FindElement(By.XPath("/*//tr/td[normalize-space(text()) ='" + WaitedRecord + "']")).Displayed);
            // For Sweetalert in Prof
            driver.ClickElement(wait, "/html/body/div[1]/div[2]/div/ul/li[3]/a");
            // driver.ClickElement(wait, "/html/body/div[3]/div/div[6]/button[1]");
            // driver.ClickElement(wait, "/html/body/div[1]/div[2]/div/ul/li[3]/a");

            wait.UntilLoadingDisappears(driver);

            driver.DocReqClick(wait, "//*[@id='fileUploadForm']/div[1]/div[1]/div[1]/div[2]/div[1]/div/div/input[1]");
            driver.DocReqClick(wait, "//*[@id='fileUploadForm']/div[1]/div[1]/div[1]/div[2]/div[2]/div/div/input[1]");
            driver.DocReqClick(wait, "//*[@id='fileUploadForm']/div[1]/div[1]/div[1]/div[2]/div[3]/div/div/input[1]");

            driver.DocReqClick(wait, "//*[@id='fileUploadForm']/div[1]/div[1]/div[2]/div[2]/div[1]/div/div/input[1]");
            driver.DocReqClick(wait, "//*[@id='fileUploadForm']/div[1]/div[1]/div[2]/div[2]/div[2]/div/div/input[1]");
            driver.DocReqClick(wait, "//*[@id='fileUploadForm']/div[1]/div[1]/div[2]/div[2]/div[3]/div/div/input[1]");

            var disclaimerLabel = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("label[for='chkDisclaimer']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", disclaimerLabel);
            disclaimerLabel.Click();

            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[3]/a")).Click();
        }
    }
}