using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAutomation.Steps
{
    public class BaseSteps
    {
        public IWebDriver WebDriver { get; set; }

        public BaseSteps(IWebDriver webdriver)
        {
            WebDriver = webdriver;
        }
    }
}
