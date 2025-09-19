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
            wait.UntilLoadingDisappears(driver);
            // wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), '')]"))).Click();
            // GeodeticTest();
            ArchiTest();
        }

        public void GeodeticTest(){
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'NBP2509-00019')]"))).Click();
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='frmComplianceGeodetic']/div[1]/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='frmComplianceGeodetic']/div[2]/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='frmComplianceGeodetic']/div[3]/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveGeodeticPlans']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='step-2']/div/div[2]/div/div/div[2]/div/div/div/div[1]/div/h3[2]/a/label");
            driver.ClickElement(wait, "//*[@id='btnSaveGeodeticEval']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
        }

        public void ArchiTest()
        {
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationArchitectural");
            wait.UntilLoadingDisappears(driver);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'NBP2509-00019')]"))).Click();
            wait.UntilLoadingDisappears(driver);
            driver.EvalGens("FireAveCover");
            driver.EvalGens("FireOverAll");
            driver.EvalGens("SolidAveCover");
            driver.EvalGens("SolidOverAll");
            driver.EvalGens("PartitionsSolidConcrete");
            driver.EvalGens("PartitionsSolidMasonry");
            driver.EvalGens("PartitionsHollowUnit");
            driver.EvalGens("ProtectConcrete");
            driver.EvalGens("ProtectMasonry");
            driver.EvalGens("ProtectLathPlaster");
            driver.EvalGens("EWSolidConcrete");
            driver.EvalGens("EWSolidMasonry");
            driver.EvalGens("EWHollowUnit");
            driver.EvalGens("ColumnReinforcedConrete");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalFire']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");
        }
    }
}