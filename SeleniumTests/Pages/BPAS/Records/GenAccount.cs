using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Helpers;
using System;

namespace SeleniumTests.Pages.BPAS.Records
{
    public class GenAccount
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public GenAccount(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void MigrateAccount(string accName)
        {
            var table_Record = String.Concat("//*/table[@id='tblEncodeEdit']/*/tr[1]/td[normalize-space(text()) = '", accName, "']");

            driver.goToURL("http://192.168.20.71:1027/Records/EncodeOrEditExistingBuilding");
            driver.refactoredSelect(wait, By.XPath("/html/body/div[116]/div/section/div[1]/div[2]/div[1]/div[2]/select"), "2");
            driver.FindElement(By.Id("txtEvalKeyword")).SendKeys(accName);
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div[1]/div[2]/div[1]/div[3]/div/div/span/button");
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, table_Record);
            driver.selectDropdown(wait, "RightOverLandID", "Owned");
            driver.selectDropdown(wait, "BldgTypeID", "Base");
            driver.FindElement(By.Id("txtZipCode")).SendKeys("4102");
            driver.selectDropdown(wait, "ZoningUseId", "RESIDENTIAL");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgExist']");
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[57]")).Displayed);
            driver.ClickElement(wait, "/html/body/div[57]/div/div/div[2]/div[2]/div/div/button");
        }
    }
}