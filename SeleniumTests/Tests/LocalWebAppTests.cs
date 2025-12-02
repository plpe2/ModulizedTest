using System;
using System.Data.Common;
using System.Security.AccessControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Helpers;
using SeleniumTests.Pages;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using SeleniumTests.Pages.BPAS.Records;

namespace SeleniumTests
{
    [TestClass]
    public class LocalWebAppTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private HttpClient httpClient = new HttpClient();

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless=new");
            options.AddArguments(["--start-maximized", "--ignore-certificate-errors", "--incognito", "--no-sandbox", "--disable-gpu"]);
            driver = new ChromeDriver(options);
        }


        [TestMethod]
        [Ignore]
        public async Task apiCall()
        {
            driver.goToURL("http://192.168.20.71:1021/BuildingPermit/Application#");

            var response = await httpClient.GetAsync("https://dummyjson.com/users?limit=2&skip=10&select=firstName,age");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            // Parse JSON → In this fake API, "users" is an array of user objects
            using JsonDocument doc = JsonDocument.Parse(json);
            var users = doc.RootElement.GetProperty("users");
            string appName = users[1].GetProperty("firstName").GetString();

            //Initialize variables Selecting elements 
            driver.selectElement("Username", appName);
        }

        [TestMethod]
        [TestCategory("Register")]
        [Ignore]
        public void RegisterTest()
        {
            var Register = new Register(driver, wait);
            Register.RegisterTest("http://192.168.20.71:1024", "gerald", "frances", "Male", "Individual");
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
            UserLog.LoginTest("http://192.168.20.71:1024/Account/Login?statusCode=0", "gfrances", "0000028");
            UserAppInfo.FillUserAppInfo("YTRN", true, "Create"); //Pending for Testing
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
            PermitApp.ReceiveApp("NBP2512-00011");
        }

        [TestMethod]
        [TestCategory("PTRAX")]
        [Ignore]
        public void PTRAXTesting()
        {
            var PTRAXTest = new PTRAXTest(driver, wait);
            PTRAXTest.AppReceiving("NBP2512-00011");
            // PTRAXTest.AppEval("NBP2510-00010");
            // PTRAXTest.BillingEval();
        }

        [TestMethod]
        [TestCategory("BPAS")]
        // [Ignore]
        public void BPASTesting()
        {
            var BPASLogin = new BPASLogin(driver, wait);
            var Records_module = new GenAccount(driver, wait);

            BPASLogin.BPASLoginTest();
            BPASLogin.GeodeticTest("NBP2512-00011");
            BPASLogin.ArchiTest("NBP2512-00011");
            BPASLogin.ElectricalTest("NBP2512-00011");
            BPASLogin.StrucuralTest("NBP2512-00011");
            // BPASLogin.MEchanicalTest("NBP2510-00016");
            // BPASLogin.SanitaryTest("NBP2512-00011");
            // BPASLogin.PlumbingTest("NBP2510-00016");
            // BPASLogin.ElectronicsTest("NBP2512-00011");

            // Records_module.MigrateAccount("NEW PACIFIC - SOFIA");
        }

        [TestMethod]
        [TestCategory("Professional")]
        [Ignore]
        public void ProfessionalTesting()
        {
            var Professionalfunc = new AddProf(driver, wait);
            var UserLog = new Login(driver, wait);
            var UserAppInfo = new AppProjInfo(driver, wait);
            UserLog.LoginTest("http://192.168.20.71:1024/Account/Login?statusCode=0", "ranton", "0000001");
            Professionalfunc.CreateProf("KLMPS");
        }


        // [TestCleanup]
        // public void DriverQuit()
        // {
        //     driver?.Quit();
        // }
    }
}