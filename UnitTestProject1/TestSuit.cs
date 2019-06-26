using OHSConnect.Common;
using OpenQA.Selenium;
using System.Configuration;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium.Support.UI;
using OHSConnect.PageObjects;
using System;
using System.Threading;
using NUnit.Framework;

namespace OHSConnect
{
    class TestSuit
    {
        public static IWebDriver webdriver;

        public static int pass = 0, fail = 0, skip = 0, error = 0;

        public static ExcelReport.ReadData Readdata = new ExcelReport.ReadData();

        public static string[] LoginCollection = Readdata.ReadAllRowDataBetweenColumns("FieldInformation", "Value", "Login", ConfigurationManager.AppSettings["InputDataPath"].ToString());
        public static string[] MailCollection = Readdata.ReadAllRowDataBetweenColumns("FieldInformation", "Value", "Email", ConfigurationManager.AppSettings["InputDataPath"].ToString());
        public static string[] AddIncidentCollection = Readdata.ReadAllRowDataBetweenColumns("FieldInformation", "Value", "AddIncident", ConfigurationManager.AppSettings["InputDataPath"].ToString());


        //To Read project name from App.config file if available,otherwise it will take it from InputDataFile
        public static string ProjectName = string.Concat((ConfigurationManager.AppSettings["ProjectName"].ToString() == "") ? Readdata.ProcessOnCollection(LoginCollection, "Project Name") : ConfigurationManager.AppSettings["ProjectName"].ToString());

        //Variable to set browser and system information
        public static string BrowserName, BrowserVersion, BrowserPlatForm, SystemOsversion, SystemMachineName;

        public static string Driverpath = ConfigurationManager.AppSettings["Driverpath"];

        public static void WaitForElementToBeClickable(IWebElement WebElement, double span)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(TestSuit.webdriver, System.TimeSpan.FromSeconds(span));
                wait.PollingInterval.Add(System.TimeSpan.FromMilliseconds(5));
                wait.Until(ExpectedConditions.ElementToBeClickable(WebElement));
            }
            catch (Exception Ex)
            {
                TakeScreenShot("Fail");
                logger.WriteLog(Ex);
                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, SystemMachineName, MailCollection, ProjectName);
                webdriver.Quit();
            }
            
        }

        public static void TakeScreenShot(string ScrShtSts)
        {
            try
            {
                Thread.Sleep(2000);
                //Take the screen-shot
                Screenshot ScrShtImg = ((ITakesScreenshot)webdriver).GetScreenshot();

                if (ScrShtSts == "Pass")
                {                    
                    //Save the screen-shot
                    ScrShtImg.SaveAsFile(ConfigurationManager.AppSettings["ScreenShotPathPass"] + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".png", ScreenshotImageFormat.Png);
                }
                else if (ScrShtSts == "Fail")
                {
                    //Save the screen-shot
                    ScrShtImg.SaveAsFile(ConfigurationManager.AppSettings["ScreenShotPathFail"] + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".png", ScreenshotImageFormat.Png);
                }
                else
                {
                }
            }
            catch (Exception Ex)
            {
                logger.WriteLog(Ex);
                ExtentReport.EndReport();
                SendEmail.email_send(ExtentReport.reportPath, logger.ErrorLogFilePath, SystemMachineName, MailCollection, ProjectName);
                webdriver.Quit();
            }
            
        }
    }
}
