using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using OpenQA.Selenium.DevTools.V125.Network;


namespace SeleniumTests.Pages.SYSMAN
{
    public class AccountCollection
    {
        public List<AccountType> Accounts { get; set; }
    }

    public class AccountType
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }

    public class accAdd
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public accAdd(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void CreateACcount()
        {
            var jsonFile = File.ReadAllText("accountData.json");
            var FetchedAccounts = JsonConvert.DeserializeObject<AccountCollection>(jsonFile);

            driver.goToURL("http://192.168.20.71:1026/");
            driver.FindElement(By.XPath("//*[@id='txtLoginUser']")).SendKeys("admin marla");
            driver.FindElement(By.XPath("//*[@id='txtLoginPass']")).SendKeys("adminP@ssw0rd");
            driver.ClickElement(wait, "//*[@id='btnLoginUser']");

            //--------------------------------------------------------------------------------
            driver.ClickElement(wait, "/html/body/div[13]/nav/div[2]/div/nav/ul/li[3]/a");
            driver.ClickElement(wait, "//*[@id='linkUserProfile']");
            // driver.Navigate().GoToUrl("http://192.168.20.71:1026/UserProfile/Index");

            foreach (var account in FetchedAccounts.Accounts)
            {
                wait.Until(d => d.FindElement(By.Id("txtLastName")).Displayed);
                driver.refactoredSendKey(wait, By.Id("txtFirstName"), account.FirstName);
                driver.refactoredSendKey(wait, By.Id("txtLastName"), account.LastName);
                driver.refactoredSendKey(wait, By.Id("txtMiddleName"), account.MiddleName);
                driver.refactoredSendKey(wait, By.Id("txtDepartment"), account.Department);
                driver.refactoredSendKey(wait, By.Id("txtPositionUser"), account.Position);
                driver.ClickElement(wait, "//*[@id='btnProfSubmit']");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                IAlert alertReceive = driver.SwitchTo().Alert();
                alertReceive.Accept();
            }
        }
    }
}