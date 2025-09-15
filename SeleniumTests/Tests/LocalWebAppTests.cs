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
        [TestCategory("Online Application")]
        [Ignore]
        public void OnlineAppTesting()
        {
            var UserLog = new Login(driver, wait);
            var UserAppInfo = new AppProjInfo(driver, wait);
            var ProfDoc = new ProfDocInfo(driver, wait);
            var Submit = new SubmitApp(driver, wait);
            UserLog.LoginTest("http://192.168.20.71:1024/Account/Login?statusCode=0", "RFGARCIA", "P@ssw0rd");
            UserAppInfo.FillUserAppInfo("KWLDGE");
            ProfDoc.ProfDocTest();
            Submit.SubmitTest();
        }

        [TestMethod]
        public void WebPortalTesting()
        {
            var WebPLogin = new WebPLogin(driver,wait);
            var PermitApp = new PermitApp(driver, wait);
            WebPLogin.WebPLoginTesting("http://192.168.20.71:1025/");
            PermitApp.ReceiveApp("http://192.168.20.71:1025/Permits/Building");
        }

        // [TestCleanup]
        // public void Teardown()
        // {
        //     driver?.Quit();
        // }
    }
}