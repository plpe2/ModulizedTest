using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTests.Helpers
{
    public static class WebDriverExtension
    {
        public static void selectElement(this IWebDriver driver, string elementId, string tvalue)
        {
            var element = driver.FindElement(By.Name(elementId));
            element.Clear();
            element.SendKeys(tvalue);
            return;
        }

        public static void goToURL(this IWebDriver driver, string UrlString)
        {
            driver.Navigate().GoToUrl(UrlString);
        }

        public static void selectDropdown(this IWebDriver driver, string titleName, string valueSelect)
        {
            var dropdown = new SelectElement(driver.FindElement(By.Name(titleName)));
            dropdown.SelectByText(valueSelect);
            return;
        }

        public static void UntilLoadingDisappears(this WebDriverWait wait, IWebDriver driver)
        {
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='loading']/img"));
                return elements.Count == 0 || !elements[0].Displayed;
            });
        }

        public static void addressGens(this IWebDriver driver, string elementName)
        {
            Random rand = new Random();
            var addressRand = rand.Next(10);
            var element = driver.FindElement(By.Name(elementName));
            element.Clear();
            element.SendKeys("" + addressRand);
            return;
        }
    }
}