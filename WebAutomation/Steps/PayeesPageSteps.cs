using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WebAutomation.Models;
using WebAutomation.PageObjects;

namespace WebAutomation.Steps
{
    [Binding]
    public class PayeesPageSteps : BaseSteps
    {
        public HomePage HomePage { get; set; }
        public PayeesPage PayeesPage { get; set; }
        public string PayeeName { get; set; }
        public PayeesPageSteps(IWebDriver webdriver) : base(webdriver)
        {
            HomePage = new HomePage(WebDriver);
            PayeesPage = new PayeesPage(WebDriver);
        }

        [When(@"I navigate to payee page via the navigation menu")]
        public void WhenINavigateToPayeePageViaTheNavigationMenu()
        {
            HomePage.NavigateToPayeesPage();
        }

        [Then(@"I should be able to see the payee page")]
        public void ThenIShouldBeAbleToSeeThePayeePage()
        {
            Assert.IsTrue(PayeesPage.IsLoaded(), "Payees Page didn't load.");
        }

        [Given(@"that I am in the payee page")]
        public void GivenThatIAmInThePayeePage()
        {
            HomePage.NavigateToPayeesPage();
            Assert.IsTrue(PayeesPage.IsLoaded(), "Payees Page didn't load.");
        }

        [When(@"I add a new payee")]
        public void WhenIAddANewPayee()
        {

            var payeeDetails = new PayeeDetails
            {
                Account = "0000001",
                PayeeName = "Sony Playstation 5",
                Bank = "01",
                Branch = "0001",
                Suffix = "001"
            };

            PayeesPage.AddPayee(payeeDetails);
        }

        [Then(@"I should be able to see the new payee in the payees list")]
        public void ThenIShouldBeAbleToSeeTheNewPayeeInThePayeesList()
        {
            Assert.IsTrue(PayeesPage.DoesPayeeAddedMessageAppear(), "\"Payee Added\" Message didn't appear");
            Assert.IsTrue(PayeesPage.IsTheNewPayeeAddedInTheList(PayeeName), "\"Payee Added\" Message didn't appear");
        }

        [When(@"I try to add a payee with blank information")]
        public void WhenITryToAddAPayeeWithBlankInformation()
        {

            PayeesPage.AddBlankPayee();
        }

        [Then(@"I should see that payee name is a required field")]
        public void ThenIShouldSeeThatPayeeNameIsARequiredField()
        {
            Assert.IsTrue(PayeesPage.DoesPayeeNameARequiredFieldTooltipAppear(), "\"Payee is a required field\" tooltip didn't appear");
            Assert.IsTrue(PayeesPage.DoesErrorMessageAppear(), "Error message didn't appear");
        }

        [Then(@"If I populate the payee name")]
        public void ThenIfIPopulateThePayeeName()
        {
            PayeesPage.AddPayeeWhenModalIsOpen("Sample");
        }


        [Then(@"the validation errors should disappear")]
        public void ThenTheValidationErrorsShouldDisappear()
        {
            Assert.IsFalse(PayeesPage.DoesPayeeNameARequiredFieldTooltipAppear(), "\"Payee is a required field\" tooltip didn't appear");
            Assert.IsFalse(PayeesPage.DoesErrorMessageAppear(), "Error message didn't appear");
        }

        [Then(@"I should see that the payee list is sorted in ascending order")]
        public void ThenIShouldSeeThatThePayeeListIsSortedInAscendingOrder()
        {
            var payeeDetails = new PayeeDetails
            {
                Account = "0000001",
                PayeeName = "Sony Playstation 5",
                Bank = "01",
                Branch = "0001",
                Suffix = "001"
            };

            PayeesPage.AddPayee(payeeDetails);

            var sortedAscendingPayees = PayeesPage.GetPayeeNames().OrderBy(x => x);
            var actualPayeeNames = PayeesPage.GetPayeeNames();

            CollectionAssert.AreEqual(sortedAscendingPayees, actualPayeeNames, "Payees are not sorted in ascending way.");

        }

        [Then(@"If I click the header")]
        public void ThenIfIClickTheHeader()
        {
            PayeesPage.ReverseSortOrderOfPayeeList();
        }

        [Then(@"should see that the payee list is sorted in descending order")]
        public void ThenShouldSeeThatThePayeeListIsSortedInDescendingOrder()
        {
            var sortedDescendingPayees = PayeesPage.GetPayeeNames().OrderByDescending(x => x);
            var actualPayeeNames = PayeesPage.GetPayeeNames();

            CollectionAssert.AreEqual(sortedDescendingPayees, actualPayeeNames, "Payees are not sorted in descending way.");

        }

    }
}
