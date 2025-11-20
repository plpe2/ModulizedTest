using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;
using System.Net.Http;
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
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void FillUserAppInfo(string appName, Boolean accType, string appType)
        {

            switch (appType)
            {
                case "Create":
                    if (!accType) //Detect if Registered account
                    {
                        wait.UntilLoadingDisappears(driver);
                        wait.Until(d => d.FindElement(By.XPath("//*[@id='modalOwnBuilding']")).Displayed);
                    }
                    else { break; } //Break the case if the account is New
                    //Function for New Building
                    driver.ClickElement(wait, "//*[@id='btnNewBuilding']");
                    wait.Until(d => d.FindElement(By.XPath("//*[@id='tab1']")).Displayed);
                    break;
                case "Existing":
                    if (!accType) //Detect if Registered account
                    {
                        wait.UntilLoadingDisappears(driver);
                        wait.Until(d => d.FindElement(By.XPath("//*[@id='modalOwnBuilding']")).Displayed);
                    }
                    // Function for Selecting Existing Records
                    var appLocator = string.Concat("//table[@id='tblOwnBuilding']//td[normalize-space(text())='", appName, "']");
                    driver.FindElement(By.XPath(appLocator)).Click();
                    IWebElement btn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnSelectExisting")));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btn);
                    btn.Click();
                    break;
                default:
                    break;
            }


            wait.UntilLoadingDisappears(driver);

            IWebElement saveBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("li.save a.btn.btn-warning")));
            saveBtn.Click();

            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]")).Displayed);
            driver.ClickElement(wait, "/html/body/div[3]/div/div[6]/button[1]");
            //Next Button
            driver.ClickElement(wait, "/html/body/div[1]/div[2]/div/ul/li[3]/a");

            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab2']")).Displayed);
            wait.UntilLoadingDisappears(driver);

            //Building Information
            //Building Description
            driver.ProjInfoGens("Building.Project.PIN", "LPIN");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/button"))).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/div")));
            driver.FindElement(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/div/div[2]/ul/li[2]/a")).Click();
            driver.selectElement("Building.Project.BaseBuildingName", appName);
            driver.ProjInfoGens("Building.Project.TDN", "TDN");
            driver.ProjInfoGens("Building.Project.TCTNo", "TCT");
            driver.selectDropdown(wait, "Building.Project.ScopeofWork", "New Construction");
            driver.selectElement("Building.Project.EstimatedCost", "30000000");
            driver.selectElement("Building.Project.FloorArea", "30");
            driver.selectElement("Building.Project.UnitsPerFloor", "2");
            driver.selectElement("Building.Project.LotArea", "40");
            driver.selectElement("Building.Project.OpenSpace", "10");
            driver.selectElement("Building.Project.Garrage", "10");
            driver.selectElement("Building.Project.Terrace", "10");
            driver.selectElement("Building.Project.Height", "25");
            driver.selectElement("Building.Project.TotalUnits", "1");

            // Start Building Location
            driver.selectDropdown(wait, "Building.Project.ConstructionProgressDescription", "To Start");
            driver.selectElement("Building.Project.ConstructionDate", "06152025");
            driver.selectElement("Building.Project.CompletionDate", "11272026");
            driver.addressGens("Building.Project.Address.HouseNo");
            driver.addressGens("Building.Project.Address.LotNo");
            driver.addressGens("Building.Project.Address.BlockNo");
            driver.addressGens("Building.Project.Address.PhaseNo");
            // driver.selectDropdown("Building.Project.Address.SubdivisionName", "LOTE");
            driver.ClickElement(wait, "//*[@id='formProjectInfo']/div/div/div/div[2]/div[5]/div[1]/div[3]/div/div[2]/button");
            driver.selectDropdown(wait, "Building.Project.Address.BarangayName", "ALIMA");
            // wait.Until(d => d.FindElement(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[5]/div[1]/div[3]/div/div[2]/div")).Displayed);
            driver.selectDropdown(wait, "Building.Project.Address.CompoundComplexID", "Compound");

            // driver.ClickElement(wait, "//*[@id='formProjectInfo']/div/div/div/div[2]/div[5]/div[2]/div[1]/div/div[2]/button");
            // wait.Until(d => d.FindElement(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[5]/div[2]/div[1]/div/div[2]/div")).Displayed);
            // driver.ClickElement(wait, "//*[@id='formProjectInfo']/div/div/div/div[2]/div[5]/div[2]/div[1]/div/div[2]/div/div[2]/ul/li[7]/a");

            saveBtn.Click();

            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[3]/div/div[6]/button[1]")).Click();

            driver.ClickElement(wait, "/html/body/div[1]/div[2]/div/ul/li[3]/a");
        }
    }
}