using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Helpers;
using System;

namespace SeleniumTests
{
    public class Register
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public Register(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void RegisterTest(string url, string fName, string lName)
        {
            driver.goToURL(url);

            driver.FindElement(By.Id("rbOwner")).Click();

            driver.selectDropdown("OwnershipTypeID", "Individual");

            driver.selectDropdown("Owner.Title", "Mr.");

            //Owner Information
            driver.FindElement(By.Name("Application.IsOwner")).Click();
            driver.selectElement("Owner.FirstName", fName);
            driver.selectElement("Owner.LastName", lName);
            driver.selectElement("Owner.MobileNo", "9391873976");
            driver.selectElement("Owner.Email", "villanuevapv1@gmail.com");
            driver.selectElement("OwnerAddress.FullAddress", "Blk 5 Lot 2 MOLINO HOMES MOLINO IV BACOOR, CAVITE");
            driver.selectElement("OwnerAddress.Zipcode", "4102");

            //Login Credentials
            driver.selectElement("AccountInfo.Username", String.Concat(fName,lName));
            driver.selectElement("AccountInfo.Password", "P@ssw0rd");
            driver.selectElement("AccountInfo.ConfirmPassword", "P@ssw0rd");

            driver.selectDropdown("AccountInfo.SecurityQuestionID", "What is your Mothers' mother maiden name?");

            driver.selectElement("AccountInfo.SecurityAnswer", "Google");
            driver.selectElement("AccountInfo.SecurityCode", "1234");

            driver.FindElement(By.XPath("//*[@id='formRegister']/div[2]/div/div[2]/button")).Click();

            wait.Until(d => d.FindElement(By.Id("ModalConfirmMessage")).Displayed);

            driver.FindElement(By.XPath("//*[@id='ModalConfirmMessage']/div/div/div[3]/button[2]")).Click();
        }
    }
}