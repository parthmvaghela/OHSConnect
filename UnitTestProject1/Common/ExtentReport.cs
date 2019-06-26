using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;

namespace OHSConnect.Common
{
    class ExtentReport
    {
        public static string reportPath;

        public ExtentReport()
        {
            String currentdatetime = datetime();

            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            reportPath = System.Configuration.ConfigurationManager.AppSettings["ExtentReportPath"] + "_" + currentdatetime + ".html";

            extent = new ExtentReports(reportPath, true, DisplayOrder.OldestFirst);
            extent
            .AddSystemInfo("Host Name", "SpecFlow")
            .AddSystemInfo("Environment", EnvironmentName)
            .AddSystemInfo("User Name", UserName);
        }

        public static ExtentReports extent;
        public static ExtentTest test;

        public static string EnvironmentName = System.Configuration.ConfigurationManager.AppSettings["Environment"];
        public static string UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];

        public static String datetime()
        {
            var time = DateTime.Now;
            string formattedTime = time.ToString("MM, dd, yyyy, hh, mm, ss");
            formattedTime = formattedTime.Replace(",", "_");
            Console.WriteLine(formattedTime);
            return formattedTime;
        }

        public static void PrintExtentReport(LogStatus PassorFail, string LogMessage, string StatusLog)
        {
            test.Log(PassorFail, LogMessage, StatusLog);
        }

        [TearDown]
        public static void GetResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                test.Log(LogStatus.Fail, stackTrace + errorMessage);
            }
            extent.EndTest(test);
        }

        [OneTimeTearDown]
        public static void EndReport()
        {
            extent.Flush();
            //extent.Close();
        }
    }
}
