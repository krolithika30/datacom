using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using WebAutomation.Models;

namespace WebAutomation.PageObjects
{
    public class BasePage
    {
        public IWebDriver WebDriver { get; set; }
        public WebDriverWait Wait { get; set; }
        public BasePage(IWebDriver driver)
        {
            WebDriver = driver;
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME_IN_SECONDS));
        }
    }
}
