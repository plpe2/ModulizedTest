using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;
using System.Threading;

namespace SeleniumTests.Pages
{
    public class AppProjInfo
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        public AppProjInfo(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void FillUserAppInfo()
        {
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalOwnBuilding']")).Displayed);

            // Function for Selecting Existing Records
            
            driver.FindElement(By.XPath("//table[@id='tblOwnBuilding']//td[normalize-space(text())='LODGE']")).Click();
            IWebElement btn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnSelectExisting")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btn);
            btn.Click();

            wait.UntilLoadingDisappears(driver);

            // Function for Creating new Building Permit
            // var newBldg = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='btnNewBuilding']")));
            // ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", newBldg);
            // newBldg.Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab1']")).Displayed);
            wait.UntilLoadingDisappears(driver);

            //For Newly Created Account
            // driver.selectElement sexDropdown = new driver.selectElement(driver.FindElement(By.Id("Applicant_Person_Gender")));

            // sexDropdown.SelectByText("Male");

            IWebElement saveBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("li.save a.btn.btn-warning")));
            saveBtn.Click();

            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[3]/div/div[6]/button[1]")).Click();
            //Next Button
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[4]/a")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab2']")).Displayed);
            wait.UntilLoadingDisappears(driver);

            //Building Information
            //Building Description
            driver.selectElement("Building.Project.PIN", "13894432789");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/button"))).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/div")));
            driver.FindElement(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/div/div[2]/ul/li[2]/a")).Click();
            driver.selectElement("Building.Project.BaseBuildingName", "LODGE");
            driver.selectElement("Building.Project.TDN", "98219826279");
            driver.selectElement("Building.Project.TCTNo", "7154256907");
            driver.selectDropdown("Building.Project.ScopeofWork", "New Construction");
            driver.selectElement("Building.Project.EstimatedCost", "40000000");
            driver.selectElement("Building.Project.FloorArea", "50");
            driver.selectElement("Building.Project.UnitsPerFloor", "2");
            driver.selectElement("Building.Project.LotArea", "60");
            driver.selectElement("Building.Project.OpenSpace", "10");
            driver.selectElement("Building.Project.Garrage", "10");
            driver.selectElement("Building.Project.Terrace", "10");
            driver.selectElement("Building.Project.Height", "25");
            driver.selectElement("Building.Project.TotalUnits", "2");

            // Start Building Location
            driver.addressGens("Building.Project.Address.HouseNo");
            driver.addressGens("Building.Project.Address.LotNo");
            driver.addressGens("Building.Project.Address.BlockNo");
            driver.addressGens("Building.Project.Address.PhaseNo");
            driver.selectDropdown("Building.Project.Address.SubdivisionName", "ADDAS 2A");
            driver.selectDropdown("Building.Project.Address.CompoundComplexID", "Compound");

            saveBtn.Click();

            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[3]/div/div[6]/button[1]")).Click();

            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[4]/a")).Click();
        }
    }
}