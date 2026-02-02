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
using System.IO;
using OpenQA.Selenium.DevTools.V125.Database;
using SeleniumTests.Pages.SYSMAN;

namespace SeleniumTests
{
    [TestClass]
    public class LocalWebAppTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private HttpClient httpClient = new HttpClient();

        public TestContext TestContext { get; set; }


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
        // [Ignore]
        public void RegisterTest()
        {
            var Register = new Register(driver, wait);
            Register.RegisterTest("http://192.168.20.71:1024", "jonas", "mutol", "Male", "Individual");
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
            UserLog.LoginTest("http://192.168.20.71:1024/Account/Login?statusCode=0", "emartines", "0000034");
            UserAppInfo.FillUserAppInfo("QRTN", true, "Create"); //Pending for Testing
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
            PermitApp.ReceiveApp("NBP2601-00022");
        }

        [TestMethod]
        [TestCategory("PTRAX")]
        [Ignore]
        public void PTRAXTesting()
        {
            var PTRAXTest = new PTRAXTest(driver, wait);
            PTRAXTest.Receiving_into_Eval("NBP2601-00022");
            // PTRAXTest.AppEval("NBP2601-00022");
            // PTRAXTest.Eval_into_Billing("NBP2601-00022");
            // PTRAXTest.Billing_into_Treasy("NBP2601-00022");
            // PTRAXTest.Treasury_into_Releasing("NBP2601-00022");
        }

        [TestMethod]
        [TestCategory("BPAS")]
        [Ignore]
        public void BPASTesting()
        {
            var BPASLogin = new BPASLogin(driver, wait);
            var Records_module = new GenAccount(driver, wait);

            BPASLogin.BPASLoginTest();
            BPASLogin.GeodeticTest("NBP2601-00022");
            BPASLogin.ArchiTest("NBP2601-00022");
            BPASLogin.ElectricalTest("NBP2601-00022");
            BPASLogin.StrucuralTest("NBP2601-00022");
            // BPASLogin.MEchanicalTest("NBP2601-00022");
            // BPASLogin.SanitaryTest("NBP2601-00022");
            // BPASLogin.PlumbingTest("NBP2601-00022");
            // BPASLogin.ElectronicsTest("NBP2601-00022");

            // Records_module.MigrateAccount("Lot1, Blk30, Brgy. MAMBOG IV, District 2, Bacoor City, Cavite");
        }

        [TestMethod]
        [TestCategory("Professional")]
        [Ignore]
        public void ProfessionalTesting()
        {
            var Professionalfunc = new AddProf(driver, wait);
            var UserLog = new Login(driver, wait);
            var UserAppInfo = new AppProjInfo(driver, wait);
            UserLog.LoginTest("http://192.168.20.71:1024/Account/Login?statusCode=0", "vbote", "0000029");
            Professionalfunc.CreateProf("BOTE");
        }

        [TestMethod]
        [TestCategory("SYSMAN")]
        [Ignore]
        public void sysmanTesting()
        {
            var SysmanFunc = new accAdd(driver, wait);
            SysmanFunc.CreateACcount();
        }


        [TestCleanup]
        public void DriverQuit()
        {
            driver?.Quit();
        }
    }
}