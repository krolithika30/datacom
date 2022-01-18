using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using WebAutomation.PageObjects;

namespace WebAutomation.Steps
{
    [Binding]
    public class PayOrTransferDialogSteps : BaseSteps
    {
        public HomePage HomePage { get; set; }
        public PayOrTransferDialog PayOrTransferDialog { get; set; }
        public double TransferredAmount { get; set; }
        public double EverydayBalance { get; set; }
        public double BillsBalance { get; set; }

        public PayOrTransferDialogSteps(IWebDriver webdriver) :base(webdriver)
        {
            HomePage = new HomePage(webdriver);
            PayOrTransferDialog = new PayOrTransferDialog(webdriver);
        }


        [When(@"I access the pay or transfer dialog")]
        public void WhenIAccessThePayOrTransferDialog()
        {
            EverydayBalance = PayOrTransferDialog.GetAccountBalance("Everyday");
            BillsBalance = PayOrTransferDialog.GetAccountBalance("Bills");
            HomePage.NavigateToPayOrTransferDialog();
        }


        [When(@"I transfer (.*)  from Everyday account to Bills account")]
        public void WhenITransferFromEverydayAccountToBillsAccount(string amount)
        {
            TransferredAmount = Convert.ToInt32(Regex.Match(amount, @"\d+").Value);
            PayOrTransferDialog.TranferMoney("Everyday", "Bills", TransferredAmount);
        }

        [Then(@"I should see that successful message is displayed")]
        public void ThenIShouldSeeThatSuccessfulMessageIsDisplayed()
        {
            Assert.IsTrue(PayOrTransferDialog.DidTransferSuccessfulMessageAppear(), "Transfer Successful Message didn't appear");
        }

        [Then(@"the current balance of Everyday account and Bills account is correct")]
        public void ThenTheCurrentBalanceOfEverydayAccountToBillsAccountIsCorrect()
        {
            var newEverydayBalance = PayOrTransferDialog.GetAccountBalance("Everyday");
            var newBillsBalance = PayOrTransferDialog.GetAccountBalance("Bills");

            Assert.AreEqual(EverydayBalance - TransferredAmount, newEverydayBalance, $"Everyday Balance wasn't deducted by an amount of {TransferredAmount}");
            Assert.AreEqual(BillsBalance + TransferredAmount, newBillsBalance, $"Bills Balance didn't increase by an amount of {TransferredAmount}");
        }

    }
}
