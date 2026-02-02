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
            driver.goToURL("http://192.168.20.71:1026/UserProfile/Index");

            foreach (var account in FetchedAccounts.Accounts)
            {
                wait.Until(d => d.FindElement(By.Id("txtLastName")).Displayed);
                driver.FindElement(By.Id("txtLastName")).SendKeys(account.LastName);
                driver.FindElement(By.Id("txtFirstName")).SendKeys(account.FirstName);
                driver.FindElement(By.Id("txtMiddleName")).SendKeys(account.MiddleName);
                driver.FindElement(By.Id("txtDepartment")).SendKeys(account.Department);
                driver.FindElement(By.Id("txtPositionUser")).SendKeys(account.Position);
                driver.ClickElement(wait, "//*[@id='btnProfSubmit']");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                IAlert alertReceive = driver.SwitchTo().Alert();
                alertReceive.Accept();
            }
        }
    }
}