using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

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
            if (titleName != "cmbApplicationKind")
            {
                var dropdown = new SelectElement(driver.FindElement(By.Name(titleName)));
                dropdown.SelectByText(valueSelect);
                return;
            }
            else
            {
                var dropdown = new SelectElement(driver.FindElement(By.Id(titleName)));
                dropdown.SelectByText(valueSelect);
                return;
            }
        }

        public static void UntilLoadingDisappears(this WebDriverWait wait, IWebDriver driver)
        {
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//*[@id='loading']/img"));
                return elements.Count == 0 || !elements[0].Displayed;
            });
        }

        public static void waitElementDisappear(this WebDriverWait wait, IWebDriver driver)
        {
            var xPath = "//div[contains(text(), 'Loading')]";
            wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath(xPath));
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

        public static void EvalGens(this IWebDriver driver, string elementName)
        {
            Random rand = new Random();
            var addressRand = rand.Next(150, 250);
            var element = driver.FindElement(By.Name(elementName));
            element.Clear();
            element.SendKeys("" + addressRand);
            return;
        }

        public static void ProjInfoGens(this IWebDriver driver, string elementName, string field)
        {
            Random rand = new Random();
            string[] codes = new string[3];
            for (int x = 0; x < 3; x++)
            {
                var addressRand = rand.Next(500, 999);
                codes[x] = String.Concat("", addressRand);
            }
            var element = driver.FindElement(By.Name(elementName));
            element.Clear();
            switch (field)
            {
                case "LPIN":
                    element.SendKeys(codes[0] + codes[1] + codes[2] + codes[0]);
                    return;
                case "TDN":
                    element.SendKeys(codes[1] + codes[2] + codes[0] + codes[1]);
                    return;
                case "TCT":
                    element.SendKeys("T-" + codes[1] + codes[0] + codes[2]);
                    return;
                default:
                    break;
            }
        }

        public static void ClickElement(this IWebDriver driver, WebDriverWait wait, string elementName)
        {
            bool clicked = false;
            int attempts = 0;

            while (!clicked && attempts < 3)
            {
                try
                {
                    var btn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(elementName)));
                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "arguments[0].scrollIntoView({ behavior: 'auto', block: 'center'});",
                        btn
                    );
                    btn.Click();
                    clicked = true;
                }
                catch (ElementClickInterceptedException)
                {
                    attempts++;
                    Thread.Sleep(200); // small delay for animation to finish
                }
            }
        }

        public static void DocReqClick(this IWebDriver driver, WebDriverWait wait, string elementLoc)
        {
            driver.ClickElement(wait, elementLoc);
            wait.UntilLoadingDisappears(driver);
        }
    }
}