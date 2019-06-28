using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OHSConnect.PageObjects;
using OHSConnect.Common;
using System.Threading;
using RelevantCodes.ExtentReports;
using NUnit.Framework;

namespace OHSConnect.CodeBindings
{
    [Binding]
    public class UserLoginSteps
    {
        LoginPage LoginObject;
        AccountSystem AccountSystemObject;

        [Given(@"That I am on OHS Connect Website")]
        public void GivenThatIamonOHSConnectWebsite()
        {
            try
            {
                ExtentReport ExRepo = new ExtentReport();

                //Open Browser
                TestSuit.OpenBrowser();

                //Open Website
                TestSuit.OpenURL();

                LoginObject = new LoginPage(TestSuit.webdriver);
                ExtentReport.PrintExtentReport(LogStatus.Pass, "OHS Connect Website Open", "Pass");
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                TestSuit.fail++;
                logger.WriteLog(Ex);
                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, TestSuit.SystemMachineName, TestSuit.MailCollection, TestSuit.ProjectName);
                TestSuit.webdriver.Quit();
            }
        }

        [When(@"I have entered username and password")]
        public void WhenIhaveenteredusernameandpassword()
        {
            try
            {
                TestSuit.Readdata.ProcessOnCollection(TestSuit.LoginCollection, "UserName");

                LoginObject.UserNameTextBox.Clear();
                LoginObject.UserNameTextBox.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.LoginCollection, "UserName"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Entered Valid User Name", "Pass");

                LoginObject.PasswordTextBox.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.LoginCollection, "Password"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Entered Valid Password", "Pass");

                TestSuit.TakeScreenShot("Pass");

                LoginObject.LoginButton.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Login button Clicked", "Pass");

                //ExtentReport.GetResult();
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                TestSuit.fail++;
                logger.WriteLog(Ex);
                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, TestSuit.SystemMachineName, TestSuit.MailCollection, TestSuit.ProjectName);
                TestSuit.webdriver.Quit();
            }
        }

        [Then(@"I should be redirected to select account page")]
        public void ThenIshouldberedirectedtoselectaccountpage()
        {
            try
            {
                AccountSystemObject = new AccountSystem(TestSuit.webdriver);

                Thread.Sleep(3000);
                
                TestSuit.TakeScreenShot("Pass");

                if (AccountSystemObject.SelectAccountLabel.Displayed)
                {
                    TestSuit.pass++;

                    ExtentReport.test = ExtentReport.extent.StartTest("Account and System Selection");
                    ExtentReport.PrintExtentReport(LogStatus.Pass, "\"Select an Account\" page is displaying", "Pass");
                }
                else
                {
                    TestSuit.fail++;
                    ExtentReport.PrintExtentReport(LogStatus.Fail, "\"Select an Account\" page is not displaying", "Fail");
                }
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                TestSuit.fail++;
                logger.WriteLog(Ex);
                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, TestSuit.SystemMachineName, TestSuit.MailCollection, TestSuit.ProjectName);
                TestSuit.webdriver.Quit();
            }
        }

        [Given(@"I have selected an account")]
        public void GivenIHaveSelectedAnAccount()
        {
            try
            {
                AccountSystemObject = new AccountSystem(TestSuit.webdriver);

                Thread.Sleep(5000);
                
                AccountSystemObject.SelectAccount.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "\"Ishan Jun Testing\" account selected", "Pass");
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                logger.WriteLog(Ex);
                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, TestSuit.SystemMachineName, TestSuit.MailCollection, TestSuit.ProjectName);
                TestSuit.webdriver.Quit();
            }
        }

        [When(@"I have selected System")]
        public void WhenIHaveSelectedSystem()
        {
            try
            {
                Thread.Sleep(3000);

                TestSuit.TakeScreenShot("Pass");

                AccountSystemObject.SelectSystem.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "\"Solvsafety\" system selected", "Pass");

                Thread.Sleep(3000);
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                TestSuit.fail++;
                logger.WriteLog(Ex);
                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, TestSuit.SystemMachineName, TestSuit.MailCollection, TestSuit.ProjectName);
                TestSuit.webdriver.Quit();
            }
        }

        [Then(@"I should be redirected to Home Page")]
        public void ThenIShouldBeRedirectedToHomePage()
        {
            try
            {
                TestSuit.WaitForElementToBeClickable(AccountSystemObject.UserIcon, 10);

                AccountSystemObject.UserIcon.Click();
                ExtentReport.test = ExtentReport.extent.StartTest("Home Page");
                ExtentReport.PrintExtentReport(LogStatus.Pass, "User Icon is clicked", "Pass");

                Thread.Sleep(2000);

                TestSuit.TakeScreenShot("Pass");

                if (AccountSystemObject.LoggedInUserName.Displayed)
                {
                    TestSuit.pass++;
                    ExtentReport.PrintExtentReport(LogStatus.Pass, "User asserted successfully", "Pass");
                    
                }
                else
                {
                    TestSuit.fail++;
                    ExtentReport.PrintExtentReport(LogStatus.Fail, "User is not asserted", "Fail");
                }

                ExtentReport.EndReport();
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
                TestSuit.fail++;
                logger.WriteLog(Ex);
                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, TestSuit.SystemMachineName, TestSuit.MailCollection, TestSuit.ProjectName);
                TestSuit.webdriver.Quit();
            }
        }
    }
}
