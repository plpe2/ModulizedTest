using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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

        public void AppReceiving()
        {
            // wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            // driver.FindElement(By.XPath("//td[contains(text(), 'NBP2509-00019')]")).Click();
            // driver.FindElement(By.XPath("//td[contains(text(), 'NBP2509-00019')]/parent::tr//input[@type='checkbox']")).Click();
            // driver.FindElement(By.XPath("//*[@id='MainContent_btnDocMgr_batchAcceptance']")).Click();
            // wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            // driver.FindElement(By.XPath("//*[@id='MainContent_btnDocMgr_AcceptOk']")).Click();
            // IAlert alertReceive = driver.SwitchTo().Alert();
            // alertReceive.Accept();
            // wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            // driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();

            // wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            // driver.FindElement(By.XPath("//td[contains(text(), 'NBP2509-00019')]")).Click();
            // driver.FindElement(By.XPath("//td[contains(text(), 'NBP2509-00019')]/parent::tr//input[@type='checkbox']")).Click();
            // wait.Until(d => d.FindElement(By.Name("ctl00$MainContent$ctlDocMgr_OperatorsAdvice1$ddl_JumpTo_Steps")).Displayed);
            // // driver.selectDropdown("ctl00$MainContent$ctlDocMgr_OperatorsAdvice1$ddl_JumpTo_Steps", "257");
            // SelectElement phase = new SelectElement(driver.FindElement(By.Id("MainContent_ctlDocMgr_OperatorsAdvice1_ddl_JumpTo_Steps")));
            // ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", phase);
            // phase.SelectByValue("257");
            // var jumpbtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='btnJump']")));
            // ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", jumpbtn);
            // jumpbtn.Click();
            // IAlert alertJump = driver.SwitchTo().Alert();
            // alertJump.Accept();
            // wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            // driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='LinkButton1']"))).Click();
        }

        public void AppEval(){
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "evaluator");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();
        }
    }
}