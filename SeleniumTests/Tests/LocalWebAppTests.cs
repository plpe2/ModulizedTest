using System;
using System.Data.Common;
using System.Security.AccessControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Pages;

namespace SeleniumTests
{
    [TestClass]
    public class LocalWebAppTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless=new");
            options.AddArguments(["--start-maximized", "--ignore-certificate-errors"]);
            driver = new ChromeDriver(options);
        }

        [TestMethod]
        public void OLTesting()
        {
            var UserLog = new Login(driver, wait);
            UserLog.LoginTest("http://192.168.20.71:1024/Account/Login?statusCode=0", "RFGARCIA", "P@ssw0rd");
        }

        // [TestCleanup]
        // public void Teardown()
        // {
        //     driver?.Quit();
        // }
    }
}