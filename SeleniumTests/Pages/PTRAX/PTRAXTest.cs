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

        public void Receiving_into_Eval(string AppNumber)
        {
            string selColRecord = String.Concat("//td[contains(text(), '", AppNumber, "')]");
            string chkBox = String.Concat("//td[contains(text(), '", AppNumber, "')]/parent::tr//input[@type='checkbox']");

            driver.goToURL("http://192.168.20.71:1023/Account/DtraxLogin.aspx");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "receiving");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.ClickElement(wait, "//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']");

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_batchAcceptance']");
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_AcceptOk']");
            IAlert alertReceive = driver.SwitchTo().Alert();
            alertReceive.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "/html/body/div[13]/div[1]/a/span");

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, chkBox);
            driver.ClickElement(wait, selColRecord);
            driver.refactoredSelect(wait, By.XPath("//*[@id='MainContent_ctlDocMgr_OperatorsAdvice1_ddl_JumpTo_Steps']"), "542");
            driver.ClickElement(wait, "//*[@id='btnJump']");
            IAlert alertJump = driver.SwitchTo().Alert();
            alertJump.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();
            
            // ------------------------------------------------------------------------------------------------------
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='LinkButton1']"))).Click();

            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "evaluator");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_batchAcceptance']");
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_AcceptOk']");
            alertReceive.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();
        }

        public void AppEval(string AppNumber)
        {
            string selColRecord = String.Concat("//td[contains(text(), '", AppNumber, "')]");
            string chkBox = String.Concat("//td[contains(text(), '", AppNumber, "')]/parent::tr//input[@type='checkbox']");

            driver.goToURL("http://192.168.20.71:1023/Account/DtraxLogin.aspx"); //Temporary for solo testing
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "evaluator");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_batchAcceptance']");
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_AcceptOk']");
            IAlert alertReceive = driver.SwitchTo().Alert();
            alertReceive.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();

            // wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            // driver.ClickElement(wait, selColRecord);
            // driver.ClickElement(wait, chkBox);
            // wait.Until(d => d.FindElement(By.Name("ctl00$MainContent$ctlDocMgr_OperatorsAdvice1$ddl_JumpTo_Steps")).Displayed);
            // // driver.selectDropdown("ctl00$MainContent$ctlDocMgr_OperatorsAdvice1$ddl_JumpTo_Steps", "257");
            // driver.selectDropdown(wait, "ctl00$MainContent$ctlDocMgr_OperatorsAdvice1$ddl_JumpTo_Steps", "Step 8 : BILLING (ISSUANCE OF ORDER OF PAYMENT)");
            // driver.ClickElement(wait, "//*[@id='btnJump']");
            // IAlert alertJump = driver.SwitchTo().Alert();
            // alertJump.Accept();
            // wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            // driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();
            // wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='LinkButton1']"))).Click();
        }

        public void Eval_into_Billing(string AppNumber)
        {
            string selColRecord = String.Concat("//td[contains(text(), '", AppNumber, "')]");
            string chkBox = String.Concat("//td[contains(text(), '", AppNumber, "')]/parent::tr//input[@type='checkbox']");

            driver.goToURL("http://192.168.20.71:1023/Account/DtraxLogin.aspx"); //Temporary for solo testing

            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "evaluator");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();

            wait.Until(d => d.FindElement(By.Name("ctl00$MainContent$ctlDocMgr_Inc_SanPedro1$txtDocMgr_Inc_DIN")).Displayed);
            driver.selectElement("ctl00$MainContent$ctlDocMgr_Inc_SanPedro1$txtDocMgr_Inc_DIN", AppNumber);
            driver.ClickElement(wait, "//*[@id='MainContent_ctlDocMgr_Inc_SanPedro1_btnDocMgr_Search']");

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);

            driver.refactoredSelect(wait, By.XPath("//*[@id='MainContent_ctlDocMgr_OperatorsAdvice1_ddl_JumpTo_Steps']"), "546");
            driver.ClickElement(wait, "//*[@id='btnJump']");
            IAlert alertJump = driver.SwitchTo().Alert();
            alertJump.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='LinkButton1']"))).Click();

            driver.goToURL("http://192.168.20.71:1023/Account/DtraxLogin.aspx");

            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "billingdbo");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_batchAcceptance']");
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_AcceptOk']");
            IAlert alertReceive = driver.SwitchTo().Alert();
            alertReceive.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "/html/body/div[13]/div[1]/a/span");

        }

        public void Billing_into_Treasy(string AppNumber)
        {
            string selColRecord = String.Concat("//td[contains(text(), '", AppNumber, "')]");
            string chkBox = String.Concat("//td[contains(text(), '", AppNumber, "')]/parent::tr//input[@type='checkbox']");

            driver.goToURL("http://192.168.20.71:1023/Account/DtraxLogin.aspx");

            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "billingdbo");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();

            wait.Until(d => d.FindElement(By.Name("ctl00$MainContent$ctlDocMgr_Inc_SanPedro1$txtDocMgr_Inc_DIN")).Displayed);
            driver.selectElement("ctl00$MainContent$ctlDocMgr_Inc_SanPedro1$txtDocMgr_Inc_DIN", AppNumber);
            driver.ClickElement(wait, "//*[@id='MainContent_ctlDocMgr_Inc_SanPedro1_btnDocMgr_Search']");

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);

            driver.refactoredSelect(wait, By.XPath("//*[@id='MainContent_ctlDocMgr_OperatorsAdvice1_ddl_JumpTo_Steps']"), "547");
            driver.ClickElement(wait, "//*[@id='btnJump']");
            IAlert alertJump = driver.SwitchTo().Alert();
            alertJump.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='LinkButton1']"))).Click();

            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "treasury");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_batchAcceptance']");
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_AcceptOk']");
            IAlert alertReceive = driver.SwitchTo().Alert();
            alertReceive.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "/html/body/div[13]/div[1]/a/span");
        }

        public void Treasury_into_Releasing(string AppNumber)
        {
            string selColRecord = String.Concat("//td[contains(text(), '", AppNumber, "')]");
            string chkBox = String.Concat("//td[contains(text(), '", AppNumber, "')]/parent::tr//input[@type='checkbox']");

            driver.goToURL("http://192.168.20.71:1023/Account/DtraxLogin.aspx");

            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "treasury");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);
            driver.refactoredSelect(wait, By.XPath("//*[@id='MainContent_ctlDocMgr_OperatorsAdvice1_ddl_JumpTo_Steps']"), "549");
            driver.ClickElement(wait, "//*[@id='btnJump']");
            IAlert alertJump = driver.SwitchTo().Alert();
            alertJump.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[13]/div[1]/a/span")).Click();
            
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='LinkButton1']"))).Click();

            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtUser", "releasingdbo");
            driver.selectElement("ctl00$ContentPlaceHolder1$ctlLogin1$txtPass", "P@ssw0rd");
            driver.FindElement(By.XPath("//*[@id='ContentPlaceHolder1_ctlLogin1_btnLogin']")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='gbox_grdMailbox_Procurement']")).Displayed);
            driver.ClickElement(wait, selColRecord);
            driver.ClickElement(wait, chkBox);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_batchAcceptance']");
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "//*[@id='MainContent_btnDocMgr_AcceptOk']");
            IAlert alertReceive = driver.SwitchTo().Alert();
            alertReceive.Accept();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[12]")).Displayed);
            driver.ClickElement(wait, "/html/body/div[13]/div[1]/a/span");
        }
    }
}