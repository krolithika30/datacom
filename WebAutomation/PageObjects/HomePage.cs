using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAutomation.PageObjects
{
    public class HomePage : BasePage
    {
        public IWebElement MainMenu => WebDriver.FindElement(By.XPath("//button[contains(@class,'MenuButton')]"));
        public IWebElement PayeesMenu => WebDriver.FindElement(By.XPath("//li[contains(@class,'main-menu-payees')]/a"));
        public IWebElement PayOrTransferMenu => WebDriver.FindElement(By.XPath("//li[contains(@class,'main-menu-paytransfer')]/button"));
        

        public HomePage(IWebDriver driver) : base(driver)
        {

        }

        public bool IsLoaded()
        {
            return Wait.Until(condition =>
            {
                try
                {
                    return MainMenu.Displayed;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public void NavigateToPayeesPage()
        {
            MainMenu.Click();
            PayeesMenu.Click();
            new PayeesPage(WebDriver).IsLoaded();
        }

        public void NavigateToPayOrTransferDialog()
        {
            MainMenu.Click();
            PayOrTransferMenu.Click();
            new PayOrTransferDialog(WebDriver).IsLoaded();
        }

    }
}
