using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AcumenTest.Core
{
    public static class WebDriverExtensions
    {
        public static void WaitUntilElementIsVisible(this IWebDriver driver, By locator, int maxseconds)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(maxseconds))
                .Until(ExpectedConditions.ElementIsVisible(locator));
        }

    }
}