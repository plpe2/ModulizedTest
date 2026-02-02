using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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

        public void ReceiveApp(string appNo)
        {
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='tblPermitApplications_processing']"));
                return elements.Count == 0 || !elements[0].Displayed;
            });
            driver.FindElement(By.Id("txtApplicationNumber")).SendKeys(appNo);
            driver.FindElement(By.Id("btnSearchApplicant")).Click();
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='tblPermitApplications_processing']"));
                return elements.Count == 0 || !elements[0].Displayed;
            });
            var appLocation = string.Concat("//*/td[text()='" + appNo + "']");
            driver.FindElement(By.XPath(appLocation)).Click();
            wait.waitElementDisappear(driver);

            driver.selectDropdown(wait, "cmbApplicationKind", "Complex");
            driver.ClickElement(wait, "//*[@id='btnVerifySelectedApplication']");

            //Generic Container of Success and Failed modal message
            var modalContainer = wait.Until(d => d.FindElement(By.XPath("/html/body/div/div[4]/div/div/div[2]")).Displayed);
            // if (modalContainer)
            // {
            //     var msgBody = driver.FindElement(By.XPath("//*[@id='ModalMessage']/div/div/div[2]"));
            //     var txt_MsgBOdy = msgBody.Text;
            //     if (txt_MsgBOdy == "")
            //     {
            //     }
            //     else
            //     {
            //     }
            // }
            driver.FindElement(By.XPath("//*[@id='ModalMessage']/div/div/div[3]/button")).Click();
        }
    }
}