using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using WebAutomation.Utilities;

namespace WebAutomation.PageObjects
{
    public class PayOrTransferDialog : BasePage
    {

        public IWebElement FormAccountFromChooser => WebDriver.FindElement(By.XPath("//button[@data-monitoring-label='Transfer Form From Chooser']"));
        public IWebElement FormAccountToChooser => WebDriver.FindElement(By.XPath("//button[@data-monitoring-label='Transfer Form To Chooser']"));
        public IWebElement FormSearch => WebDriver.FindElement(By.XPath("//input[@placeholder='Search']"));
        public IWebElement AmountToTransferTextField => WebDriver.FindElement(By.XPath("//input[@data-monitoring-label='Transfer Form Amount']"));
        public IWebElement TransferButton => WebDriver.FindElement(By.XPath("//button//span[text()='Transfer']"));
        public IWebElement TransferSuccessfulMessage => WebDriver.FindElement(By.XPath("//span[text()='Transfer successful']"));


        public PayOrTransferDialog(IWebDriver driver) : base(driver)
        {

        }

        public bool IsLoaded()
        {
            var isFormAccountFromChooser = Wait
                .Until(condition =>
                {
                    try
                    {
                        return FormAccountFromChooser.Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            var isFormAccountToChooser = Wait
                .Until(condition =>
                {
                    try
                    {
                        return FormAccountToChooser.Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            return isFormAccountFromChooser && isFormAccountToChooser;
        }

        public void TranferMoney(string fromAccount, string toAccount, double amount)
        {
            FormAccountFromChooser.Click();
            FormSearch.SendKeys(fromAccount);

            var fromElement = WebDriver.FindElement(By.XPath($"//button//p[contains(@class,'name') and contains(text(),'{fromAccount}')]/ancestor::li//img"));

            WebDriver.JavaScriptClick(fromElement);

            Wait
                .Until(condition =>
                {
                    try
                    {
                        return FormAccountToChooser.Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            FormAccountToChooser.Click();
            FormSearch.SendKeys(toAccount);

            var toElement = WebDriver.FindElement(By.XPath($"//button//p[contains(@class,'name') and contains(text(),'{toAccount}')]/ancestor::li//img"));

            WebDriver.JavaScriptClick(toElement);

            AmountToTransferTextField.SendKeys(amount.ToString());

            TransferButton.Click();

        }

        public double GetAccountBalance(string account)
        {
            var element = WebDriver.FindElement(By.XPath($"//div[@class='account-info']//h3[normalize-space()='{account}']/ancestor::div[@class='account-info']/span[@class='account-balance']"));
            return Convert.ToDouble( element.Text);
        }

        public bool DidTransferSuccessfulMessageAppear()
        {
            return Wait
            .Until(condition =>
            {
                try
                {
                    return TransferSuccessfulMessage.Displayed;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}
