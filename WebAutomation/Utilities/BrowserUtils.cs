using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAutomation.Utilities
{
    public static class BrowserUtils
    {
        public static void JavaScriptClick(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor) driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
    }
}
