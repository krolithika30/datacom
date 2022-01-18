using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using WebAutomation.PageObjects;

namespace WebAutomation.Steps
{
    [Binding]
    public class SharedSteps : BaseSteps
    {
        public HomePage HomePage { get; set; }

        public SharedSteps(IWebDriver webdriver) : base(webdriver)
        {
            
        }

        [Given(@"that I am in the home page")]
        public void GivenThatIAmInTheHomePage()
        {
            HomePage = new HomePage(WebDriver);
            Assert.IsTrue(HomePage.IsLoaded(), "Home Page didn't load.");
        }
    }
}
