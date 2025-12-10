using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace SeleniumTests
{

    public class UserCollection
    {
        public List<UserData> users { get; set; }
    }

    public class UserData
    {
        public string Title { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Mname { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public string Mobile { get; set; }
        public string PRC { get; set; }
        public string IssuedDate { get; set; }
        public string Expiration { get; set; }
        public string PTR { get; set; }
        public string IssuedDatePTR { get; set; }
        public string ExpirationPTR { get; set; }
        public string Address { get; set; }
    }

    public class AddProf
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public AddProf(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void CreateProf(string appName)
        {
            var json = File.ReadAllText("professionalData.json");
            var userData = JsonConvert.DeserializeObject<UserCollection>(json);

            driver.ClickElement(wait, "//table[@id='tblOwnBuilding']//td[normalize-space(text())='" + appName + "']");
            IWebElement btn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnSelectExisting")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btn);
            btn.Click();

            wait.UntilLoadingDisappears(driver);

            IWebElement saveBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("li.save a.btn.btn-warning")));
            saveBtn.Click();

            wait.Until(d => d.FindElement(By.XPath("/html/body/div[3]")).Displayed);
            driver.ClickElement(wait, "/html/body/div[3]/div/div[6]/button[1]");

            //Next button
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[3]/a")).Click();

            //Next button
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/ul/li[3]/a")).Click();

            //Adding Professional
            driver.ClickElement(wait, "//*/button[normalize-space(text()) = 'Add New Professional']");

            wait.Until(d => d.FindElement(By.Id("ModalAddExistingProf")).Displayed);


            foreach (var profs in userData.users)
            {
                Console.WriteLine($"Running test for: {profs.Title}");

                driver.refactoredSelect(wait, By.XPath("/html/body/div[1]/div[1]/div[17]/div/div/div[2]/form/div/div/div/div/div[1]/div[1]/select"), profs.Title);
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[1]/div[2]/input[2]")).SendKeys(profs.Fname);
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[1]/div[4]/input")).SendKeys(profs.Lname);
                driver.refactoredSelect(wait, By.XPath("/html/body/div[1]/div[1]/div[17]/div/div/div[2]/form/div/div/div/div/div[2]/div[1]/select"), profs.Gender);
                driver.refactoredSelect(wait, By.XPath("/html/body/div[1]/div[1]/div[17]/div/div/div[2]/form/div/div/div/div/div[2]/div[2]/select"), profs.Profession);
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[2]/div[3]/div/div[2]/input")).SendKeys(profs.Mobile);
                //PRC
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[4]/div[1]/input")).SendKeys(profs.PRC);
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[4]/div[2]/input")).SendKeys(profs.IssuedDate);
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[4]/div[3]/input")).SendKeys("Bacoor");
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[4]/div[4]/input")).SendKeys(profs.Expiration);
                //PTR
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[5]/div[1]/input")).SendKeys(profs.PTR);
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[5]/div[2]/input")).SendKeys(profs.IssuedDatePTR);
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[5]/div[3]/input")).SendKeys("Bacoor");
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[5]/div[4]/input")).SendKeys(profs.ExpirationPTR);
                //Address
                driver.FindElement(By.XPath("//*[@id='divModalProf']/div/div/div[6]/div[2]/input")).SendKeys(profs.Address);

                driver.ClickElement(wait, "//*[@id='btnAddProf']");

                wait.Until(d => d.FindElement(By.XPath("/html/body/div[4]/div")).Displayed);
                driver.ClickElement(wait, "/html/body/div[4]/div/div[6]/button[1]");
            }

        }

        public void AssignProf()
        {

        }

    }
}

