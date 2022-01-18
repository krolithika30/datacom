using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace WebAutomation.Miscellaneous
{
    [Binding]
    public class Hooks
    {
        public IObjectContainer ObjectContainer { get; set; }
        public IWebDriver WebDriver { get; set; }

        public Hooks(IObjectContainer objectContainer)
        {
            ObjectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            WebDriver = new ChromeDriver();
            var waitTime = Convert.ToInt32(ConfigurationManager.AppSettings["WaitTime"]);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitTime);
            WebDriver.Manage().Window.Maximize();
            ObjectContainer.RegisterInstanceAs(WebDriver);

            var url = ConfigurationManager.AppSettings["AppUrl"];
            try
            {
                WebDriver.Url = url;
            }
            catch (Exception)
            {
                var errorMessage = $"Automation can't access site \"{url}\"";
                Console.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            WebDriver.Quit();
        }
    }
}
