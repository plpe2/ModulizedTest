using System;
using System.Data.Common;
using System.Security.AccessControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
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
            options.AddArguments(["--start-maximized", "--ignore-certificate-errors", "--incognito"]);
            driver = new ChromeDriver(options);
        }

        [TestMethod]
        [TestCategory("Register")]
        [Ignore]
        public void RegisterTest()
        {
            var Register = new Register(driver, wait);
            Register.RegisterTest("http://192.168.20.71:1024/", "JOLLY", "BHAN");
        }

        [TestMethod]
        [TestCategory("OnlineApplication")]
        [Ignore]
        public void OnlineAppTesting()
        {
            var UserLog = new Login(driver, wait);
            var UserAppInfo = new AppProjInfo(driver, wait);
            var ProfDoc = new ProfDocInfo(driver, wait);
            var Submit = new SubmitApp(driver, wait);
            UserLog.LoginTest("http://192.168.20.71:1024/Account/Login?statusCode=0", "CLOWNE", "0000045");
            UserAppInfo.FillUserAppInfo("SCKS");
            ProfDoc.ProfDocTest();
            Submit.SubmitTest();
        }

        [TestMethod]
        [TestCategory("WebPortal")]
        [Ignore]
        public void WebPortalTesting()
        {
            var WebPLogin = new WebPLogin(driver, wait);
            var PermitApp = new PermitApp(driver, wait);
            WebPLogin.WebPLoginTesting("http://192.168.20.71:1025/");
            PermitApp.ReceiveApp("NBP2509-00020");
        }

        [TestMethod]
        [TestCategory("BPAS")]
        [Ignore]
        public void BPASTesting()
        {
            var BPASLogin = new BPASLogin(driver, wait);
            BPASLogin.BPASLoginTest();
            BPASLogin.NBPEvalTest();
        }

        [TestMethod]
        [TestCategory("PTRAX")]
        // [Ignore]

        public void PTRAXTesting()
        {
            var PTRAXTest = new PTRAXTest(driver, wait);
            PTRAXTest.PTRAXLogin();
            PTRAXTest.AppReceiving();
            PTRAXTest.AppEval();
        }

        // [TestCleanup]
        // public void Teardown()
        // {
        //     driver?.Quit();
        // }
    }
}