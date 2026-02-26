using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Tests
{
    [TestClass]
    public class BaseTest
    {

        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected HttpClient httpClient = new HttpClient();

        public TestContext TestContext { get; set; }


        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            // options.AddArgument(["--headless=new", "--incognito"]);
            options.AddArguments(["--start-maximized", "--ignore-certificate-errors", "--no-sandbox", "--disable-gpu", "--incognito"]);
            driver = new ChromeDriver(options);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            string TestName = TestContext.TestName;
            Console.WriteLine(TestName);

            string name = TestContext.TestRunDirectory;
            string projectLocation = TestContext.TestResultsDirectory;
            Console.WriteLine("Test Run Directory: " + name);
            Console.WriteLine("Project Location: " + projectLocation);

            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile($"{name}\\Out\\{TestContext.TestName}.png");
            }

            TestContext.AddResultFile("Result");


            driver?.Quit();
            driver?.Dispose();
            httpClient?.Dispose();
        }
    }
}