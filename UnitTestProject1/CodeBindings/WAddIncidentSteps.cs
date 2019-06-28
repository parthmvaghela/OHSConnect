using NUnit.Framework;
using OHSConnect;
using OHSConnect.Common;
using OHSConnect.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Threading;
using TechTalk.SpecFlow;


namespace UnitTestProject1
{
    [Binding]
    public class AddIncidentFeatureSteps
    {
        AddIncidentObject AddIncidentObjectObj;

        
        [Given(@"I add new incident")]
        public void GivenIAddNewIncident()
        {
            try
            {
                AddIncidentObjectObj = new AddIncidentObject(TestSuit.webdriver);

                TestSuit.WaitForElementToBeClickable(AddIncidentObjectObj.AddMenu, 10);

                AddIncidentObjectObj.AddMenu.Click();
                ExtentReport.test = ExtentReport.extent.StartTest("Add an Incident");
                ExtentReport.PrintExtentReport(LogStatus.Pass, "\"Add\" menu clicked", "Pass");

                TestSuit.TakeScreenShot("Pass");

                AddIncidentObjectObj.AddanIncidentMenu.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "\"Add an Incident\" menu clicked", "Pass");

                Thread.Sleep(8000);
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

        [When(@"I Fill the incident report and save")]
        public void WhenIFillTheIncidentReportAndSave()
        {
            try
            {
                Thread.Sleep(2000);

                AddIncidentObjectObj.DateTimeIncident.Click();
                AddIncidentObjectObj.DateTimeIncident.Clear();
                AddIncidentObjectObj.DateTimeIncident.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.AddIncidentCollection, "Date Time of Incident"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Incident date and time selected", "Pass");


                AddIncidentObjectObj.DateTimeReported.Click();
                AddIncidentObjectObj.DateTimeReported.Clear();
                AddIncidentObjectObj.DateTimeReported.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.AddIncidentCollection, "Date Time of Reported"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Reported date and time selected", "Pass");

                AddIncidentObjectObj.BusinessUnitDrpDwn.Click();
                AddIncidentObjectObj.IshanTestOption.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Reported date and time selected", "Pass");

                AddIncidentObjectObj.SpecificLocationtxt.Clear();
                AddIncidentObjectObj.SpecificLocationtxt.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.AddIncidentCollection, "Specific Location"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Specific Location added", "Pass");

                var selectElement = new SelectElement(AddIncidentObjectObj.IncidentTypeDrpDwn);
                selectElement.SelectByText(TestSuit.Readdata.ProcessOnCollection(TestSuit.AddIncidentCollection, "Incident Type"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Incident Type selected successfully", "Pass");

                AddIncidentObjectObj.BriefDescriptiontxt.Clear();
                AddIncidentObjectObj.BriefDescriptiontxt.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.AddIncidentCollection, "Brief Description"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Brief Description added", "Pass");

                AddIncidentObjectObj.NotifiableIncidentNo.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Is this a Notifiable Incident? No selected", "Pass");

                AddIncidentObjectObj.GivenNametxt.Clear();
                AddIncidentObjectObj.GivenNametxt.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.AddIncidentCollection, "Given Name"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Given Name added successfully", "Pass");

                AddIncidentObjectObj.Surnametxt.Clear();
                AddIncidentObjectObj.Surnametxt.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.AddIncidentCollection, "Surname"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Surname added successfully", "Pass");

                AddIncidentObjectObj.DescriptionofInjurytxt.Clear();
                AddIncidentObjectObj.DescriptionofInjurytxt.SendKeys(TestSuit.Readdata.ProcessOnCollection(TestSuit.AddIncidentCollection, "Description of Injury"));
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Description of Injury added successfully", "Pass");

                AddIncidentObjectObj.AddAnotherRecordbtn.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Add Another Record button clicked successfully", "Pass");

                Thread.Sleep(2000);

                AddIncidentObjectObj.ReportedByDrpDwn.Click();
                AddIncidentObjectObj.ReportedByDrpDwnOption.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Reported By selected successfully", "Pass");

                AddIncidentObjectObj.CompletedByDrpDwn.Click();
                AddIncidentObjectObj.CompletedByDrpDwnoption.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Completed By selected successfully", "Pass");

                //AddIncidentObjectObj.OwnerDrpDwn.Click();
                //AddIncidentObjectObj.OwnerDrpDwnOption.Click();
                //TestSuit.PrintExtentReport(LogStatus.Pass, "Next Form Owner selected successfully", "Pass");

                TestSuit.TakeScreenShot("Pass");
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

        [Then(@"Incident report should be saved and finish")]
        public void ThenIncidentReportShouldBeSavedAndFinish()
        {
            try
            {
                AddIncidentObjectObj.SaveandFinishbtn.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Clicked Save and Finish button successfully", "Pass");

                Thread.Sleep(5000);

                TestSuit.TakeScreenShot("Pass");

                AddIncidentObjectObj.HomeMenu.Click();
                ExtentReport.PrintExtentReport(LogStatus.Pass, "Home menu clicked successfully", "Pass");

                TestSuit.pass++;

                Thread.Sleep(5000);

                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, TestSuit.SystemMachineName, TestSuit.MailCollection, TestSuit.ProjectName);
                TestSuit.webdriver.Quit();
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
