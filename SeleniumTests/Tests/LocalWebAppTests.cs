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
using Microsoft.VisualBasic;

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
            // options.AddArgument(["--headless=new", "--incognito"]);
            options.AddArguments(["--start-maximized", "--ignore-certificate-errors", "--no-sandbox", "--disable-gpu", "--incognito"]);
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
            driver.selectElement(wait, "Username", appName);
        }

        [TestMethod]
        [TestCategory("Register")]
        // [Ignore]
        public void RegisterTest()
        {
            var Register = new Register(driver, wait);
            Register.RegisterTest(Config.LiveRegistration, "andrei", "charles", "Male", "Individual");
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
            UserLog.LoginTest(Config.LiveOnlineApp, "palero", "0000037");
            UserAppInfo.FillUserAppInfo("PALERO", true, "Create"); //Pending for Testing
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
            WebPLogin.WebPLoginTesting(Config.LiveWebPortal);
            PermitApp.ReceiveApp("NBP2602-00023");
        }

        [TestMethod]
        [TestCategory("PTRAX")]
        [Ignore]
        public void PTRAXTesting()
        {
            // PTRAXTest is where the environment being configured
            var PTRAXTest = new PTRAXTest(driver, wait, Config.LivePTRAX);

            // PTRAXTest.Receiving_into_Eval("NBP2602-00023");
            // PTRAXTest.AppEval("NBP2602-00023");
            // PTRAXTest.Eval_into_Billing("NBP2602-00023");
            PTRAXTest.Billing_into_Treasy("NBP2602-00023");
            // PTRAXTest.Treasury_into_Releasing("NBP2602-00023");
        }

        [TestMethod]
        [TestCategory("BPAS")]
        [Ignore]
        public void BPASTesting()
        {
            var BPASLogin = new BPASLogin(driver, wait, Config.LiveBPAS);
            var Records_module = new GenAccount(driver, wait);

            BPASLogin.BPASLoginTest();
            BPASLogin.GeodeticTest("NBP2602-00023");
            BPASLogin.ArchiTest("NBP2602-00023");
            BPASLogin.ElectricalTest("NBP2602-00023");
            BPASLogin.StrucuralTest("NBP2602-00023");
            // BPASLogin.MEchanicalTest("NBP2602-00023");
            // BPASLogin.SanitaryTest("NBP2602-00023");
            // BPASLogin.PlumbingTest("NBP2602-00023");
            // BPASLogin.ElectronicsTest("NBP2602-00023");

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
            UserLog.LoginTest("http://192.168.20.71:1024/Account/Login?statusCode=0", "vbote", "0000179");
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
            // string name = TestContext.TestRunDirectory;
            // string projectLocation = TestContext.TestResultsDirectory;
            // Console.WriteLine("Test Run Directory: " + name);
            // Console.WriteLine("Project Location: " + projectLocation);

            // if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            // {
            //     Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            //     ss.SaveAsFile($"{name}\\Out\\{TestContext.TestName}.png");
            // }

            // TestContext.AddResultFile("Result");


            driver?.Quit();
        }
    }
}