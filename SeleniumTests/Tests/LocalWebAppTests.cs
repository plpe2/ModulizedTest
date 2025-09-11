using System;
using System.Data.Common;
using System.Security.AccessControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    [TestClass]
    public class LocalWebAppTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless=new");
            options.AddArguments(["--start-maximized", "--ignore-certificate-errors"]);
            driver = new ChromeDriver(options);
        }

        IWebElement selectElement(string elementId, string tvalue)
        {
            var element = driver.FindElement(By.Name(elementId));
            element.Clear();
            element.SendKeys(tvalue);
            return element;
        }

        void goToURL(string UrlString)
        {
            driver.Navigate().GoToUrl(UrlString);
        }

        SelectElement selectDropdown(string titleName, string valueSelect)
        {
            var dropdown = new SelectElement(driver.FindElement(By.Name(titleName)));
            dropdown.SelectByText(valueSelect);
            return dropdown;
        }

        [TestMethod]
        [TestCategory("Login")]
        // [Ignore]
        public void LoginTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            // Navigation to local VM address
            goToURL("http://192.168.20.71:1021/BuildingPermit/Application#");

            //Initialize variables Selecting elements 
            selectElement("Prefix", "BLG");
            selectElement("pincode", "1234");
            selectElement("year", "25");
            selectElement("SeriesNo", "0000013");
            selectElement("Username", "RFGARCIA");
            selectElement("Password", "P@ssw0rd");

            //Submitting Form
            driver.FindElement(By.XPath("//*[@id='formLogin']/div[6]/div[2]/button")).SendKeys(Keys.Return);

            wait.Until(d => d.FindElement(By.Id("frmVerification")).Displayed);

            var otpCode = driver.FindElement(By.Id("hidVerCode")).GetAttribute("value");
            selectElement("VerificationCode", otpCode);

            IWebElement submitButton = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button.form-control.btn.btn-success")
                )
            );

            // Click the submit button
            submitButton.Click();
            //End of Login test

            //Start of Testing Building Permit Application module 
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalOwnBuilding']")).Displayed);

            // Function for Selecting Existing Records
            driver.FindElement(By.XPath("//table[@id='tblOwnBuilding']//td[normalize-space(text())='MADLA']")).Click();
            IWebElement btn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnSelectExisting")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btn);
            btn.Click();

            //Function for Creating new Building Permit
            // driver.FindElement(By.XPath("//*[@id='btnNewBuilding']")).Click();

            // SelectElement sexDropdown = new SelectElement(driver.FindElement(By.Id("Applicant_Person_Gender")));

            // sexDropdown.SelectByText("Male");

            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab1']")).Displayed);
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='loading']/img"));
                return elements.Count == 0 || !elements[0].Displayed;
            });

            IWebElement saveBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("li.save a.btn.btn-warning")));
            saveBtn.Click();

            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[3]/div/div[6]/button[1]")).Click();

            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[4]/a")).Click();


            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab2']")).Displayed);
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='loading']/img"));
                return elements.Count == 0 || !elements[0].Displayed;
            });

            //Building Information
            //Building Description
            selectElement("Building.Project.PIN", "13894432789");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/button"))).Click();
            // wait.Until(ExpectedConditions.ElementToBeClickable(
            //     By.CssSelector("button[data-id='Building_Project_BuildingProject']")
            // )).Click();

            // // 2. Wait until the dropdown menu is visible
            // wait.Until(ExpectedConditions.ElementIsVisible(
            //     By.CssSelector("div.dropdown-menu.show")
            // ));

            // // 3. Select the desired option by its text (e.g. "Proposed Three(3) Storey Residential")
            // wait.Until(ExpectedConditions.ElementToBeClickable(
            //     By.XPath("//div[contains(@class,'dropdown-menu') and contains(@class,'show')]//a[normalize-space()='Proposed Two(2) Storey Residential']")
            // )).Click();
            // var buttonTitle = wait.Until(ExpectedConditions.ElementToBeClickable(
            //     By.CssSelector("button[data-id='Building_Project_BuildingProject']")
            // ));
            // buttonTitle.Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/div")));
            driver.FindElement(By.XPath("//*[@id='formProjectInfo']/div/div/div/div[2]/div[3]/div[1]/div[1]/div[1]/div/div[2]/div/div[2]/ul/li[2]/a")).Click();
            selectElement("Building.Project.BaseBuildingName", "MADLA");
            selectElement("Building.Project.TDN", "98219826279");
            selectElement("Building.Project.TCTNo", "7154256907");
            selectDropdown("Building.Project.ScopeofWork", "New Construction");
            selectElement("Building.Project.EstimatedCost", "400000000");
            selectElement("Building.Project.FloorArea", "50");
            selectElement("Building.Project.UnitsPerFloor", "2");
            selectElement("Building.Project.LotArea", "60");
            selectElement("Building.Project.OpenSpace", "10");
            selectElement("Building.Project.Garrage", "10");
            selectElement("Building.Project.Terrace", "10");
            selectElement("Building.Project.Height", "25");
            selectElement("Building.Project.TotalUnits", "2");

            // Start Building Location
            selectElement("Building.Project.Address.HouseNo", "1");
            selectElement("Building.Project.Address.LotNo", "2");
            selectElement("Building.Project.Address.BlockNo", "3");
            selectElement("Building.Project.Address.PhaseNo", "4");
            selectDropdown("Building.Project.Address.SubdivisionName", "ADDAS 2A");
            selectDropdown("Building.Project.Address.CompoundComplexID", "Compound");

            saveBtn.Click();

            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[3]/div/div[6]/button[1]")).Click();

            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[4]/a")).Click();

            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='loading']/img"));
                return elements.Count == 0 || !elements[0].Displayed;
            });

            //Professional Information
            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab3']")).Displayed);

            // driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[2]/a")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab3']")).Displayed);

            driver.FindElement(By.XPath("//*[@id='btnSearchProfLicense']")).Click();
            wait.Until(d => d.FindElement(By.XPath("//*[@id='ModalExistingProf']/div")).Displayed);
            driver.FindElement(By.XPath("//*[@id='tblExistingProfLicense']/tbody/tr[1]/td[1]")).Click();
            driver.FindElement(By.XPath("//*[@id='btnSaveExistingProf']")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[4]/a")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab3']")).Displayed);
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='loading']/img"));
                return elements.Count == 0 || !elements[0].Displayed;
            });

            var disclaimerLabel = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("label[for='chkDisclaimer']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", disclaimerLabel);
            disclaimerLabel.Click();

            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[4]/a")).Click();

            wait.Until(d => d.FindElement(By.XPath("//*[@id='tab4']")).Displayed);
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='loading']/img"));
                return elements.Count == 0 || !elements[0].Displayed;
            });
            //Application Preview
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[5]/a"))).Click();
            wait.Until(d => d.FindElement(By.Id("mdlListAppliedPermit")).Displayed);
            driver.FindElement(By.XPath("//*[@id='mdlbtnOkayAP']")).Click();
            wait.Until(d => d.FindElement(By.Id("ModalSubmit")).Displayed);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ModalSubmit']/div/div/div[2]/div[3]/div/div/label"))).Click();
            driver.FindElement(By.Id("btnsavepayment")).Click();
            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]/div")).Displayed);
            driver.FindElement(By.XPath("/html/body/div[3]/div/div[6]/button[1]")).Click();     
        }   

        [TestMethod]
        [TestCategory("Register")]
        [Ignore]
        public void RegisterTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            goToURL("http://192.168.20.71:1024/");

            driver.FindElement(By.Id("rbOwner")).Click();

            selectDropdown("OwnershipTypeID", "Individual");

            selectDropdown("Owner.Title", "Mr.");

            //Owner Information
            driver.FindElement(By.Name("Application.IsOwner")).Click();
            selectElement("Owner.FirstName", "Ramon");
            selectElement("Owner.LastName", "Garcia");
            selectElement("Owner.MobileNo", "9391873976");
            selectElement("Owner.Email", "villanuevapv1@gmail.com");
            selectElement("OwnerAddress.FullAddress", "Blk 5 Lot 2 MOLINO HOMES MOLINO IV BACOOR, CAVITE");
            selectElement("OwnerAddress.Zipcode", "4102");

            //Login Credentials
            selectElement("AccountInfo.Username", "RFGARCIA");
            selectElement("AccountInfo.Password", "P@ssw0rd");
            selectElement("AccountInfo.ConfirmPassword", "P@ssw0rd");

            selectDropdown("AccountInfo.SecurityQuestionID", "What is your Mothers' mother maiden name?");

            selectElement("AccountInfo.SecurityAnswer", "Google");
            selectElement("AccountInfo.SecurityCode", "1234");

            // driver.FindElement(By.XPath("//*[@id='formRegister']/div[2]/div/div[2]/button")).Click();

            // wait.Until(d => d.FindElement(By.Id("ModalConfirmMessage")).Displayed);

            // driver.FindElement(By.XPath("//*[@id='ModalConfirmMessage']/div/div/div[3]/button[2]")).Click();
        }
        
        // [TestCleanup]
        // public void Teardown()
        // {
        //     driver?.Quit();
        // }
    }
}