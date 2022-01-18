using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebAutomation.Models;

namespace WebAutomation.PageObjects
{
    public class PayeesPage : BasePage
    {
        public IWebElement PageHeading => WebDriver.FindElement(By.XPath("//h1//span[text()='Payees']"));
        public IWebElement LastElementOfTheList => WebDriver.FindElement(By.XPath("(//ul[contains(@class,'List')]//li)[last()]"));
        public IWebElement AddPayeeButton => WebDriver.FindElement(By.XPath("//button/span[text()='Add']"));
        public IWebElement PayeeNameTextField => WebDriver.FindElement(By.XPath("//form[@id='apm-form']//input[@name='apm-name']"));
        public IWebElement BankTextField => WebDriver.FindElement(By.XPath("//div[contains(@class,'account-number')]//input[@name='apm-bank']"));
        public IWebElement BranchTextField => WebDriver.FindElement(By.XPath("//div[contains(@class,'account-number')]//input[@name='apm-branch']"));
        public IWebElement AccountTextField => WebDriver.FindElement(By.XPath("//div[contains(@class,'account-number')]//input[@name='apm-account']"));
        public IWebElement SuffixTextField => WebDriver.FindElement(By.XPath("//div[contains(@class,'account-number')]//input[@name='apm-suffix']"));
        public IWebElement SubmitPayeeButton => WebDriver.FindElement(By.XPath("//form[@id='apm-form']//button[text()='Add']"));
        public IWebElement PayeeAddedMessage => WebDriver.FindElement(By.XPath("//span[text()='Payee added']"));
        public IWebElement PayeeNameIsARequiredFieldTooltip => WebDriver.FindElement(By.XPath("//p[contains(text(),'Payee Name is a required field')]"));
        public IWebElement ErrorMessage => WebDriver.FindElement(By.XPath("//div[@class = 'error-header']"));
        public List<IWebElement> PayeeNameList => WebDriver.FindElements(By.XPath("//ul[contains(@class,'List')]//li//span[contains(@class,'payee-name')]")).ToList();
        public IWebElement PayeeNameHeaderColumns => WebDriver.FindElement(By.XPath("//h3[contains(@class,'payee-name')]/span[text()='Name']"));
        

        public PayeesPage(IWebDriver driver) : base(driver)
        {

        }

        public void ReverseSortOrderOfPayeeList()
        {
            PayeeNameHeaderColumns.Click();
        }

        public bool IsLoaded()
        {
            var isPageHeadingLoaded = Wait
                .Until(condition =>
                {
                    try
                    {
                        return PageHeading.Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            var isLastElementOfTheListLoaded = Wait
                .Until(condition =>
                {
                    try
                    {
                        return LastElementOfTheList.Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            return isPageHeadingLoaded && isLastElementOfTheListLoaded;
        }

        public void AddPayee(PayeeDetails payeeDetails)
        {
            AddPayeeButton.Click();
            Wait
                .Until(condition =>
                {
                    try
                    {
                        return SuffixTextField.Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            PayeeNameTextField.SendKeys(payeeDetails.PayeeName);
            PayeeNameTextField.SendKeys(Keys.Return);
            PayeeNameTextField.SendKeys(Keys.Return);
            BankTextField.SendKeys(payeeDetails.Bank);
            BranchTextField.SendKeys(payeeDetails.Branch);
            AccountTextField.SendKeys(payeeDetails.Account);
            SuffixTextField.SendKeys(payeeDetails.Suffix);
            SubmitPayeeButton.Click();
        }

        public void AddPayeeWhenModalIsOpen(string name)
        {
            PayeeNameTextField.SendKeys(name);
            PayeeNameTextField.SendKeys(Keys.Return);
        }

        public void AddBlankPayee()
        {
            AddPayeeButton.Click();
            SubmitPayeeButton.Click();
        }

        public bool IsTheNewPayeeAddedInTheList(string name)
        {

            try
            {
                return WebDriver.FindElement(By.XPath($"//ul[contains(@class,'List')]//li//span[contains(text(),'{name}')]")).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DoesPayeeAddedMessageAppear()
        {
            return Wait
                .Until(condition =>
                {
                    try
                    {
                        return PayeeAddedMessage.Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
        }

        public bool DoesPayeeNameARequiredFieldTooltipAppear()
        {
            try
            {
                return PayeeNameIsARequiredFieldTooltip.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DoesErrorMessageAppear()
        {
            try
            {
                return ErrorMessage.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<string> GetPayeeNames()
        {
            return PayeeNameList.Select(x => x.Text).ToList();
        }

    }
}
