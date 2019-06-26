using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHSConnect.Common
{
    class logger
    {
        public static string ErrorLogFilePath = ConfigurationManager.AppSettings["ErrorLogFilePath"] + DateTime.UtcNow.Date.ToString("dd_MM_yyyy") + ".txt";

        public static void WriteLog(Exception Ex)
        {
            try
            {
                using (StreamWriter w = File.AppendText(ErrorLogFilePath))
                {
                    Log(Ex.ToString(), w);
                }
            }
            catch (Exception Exc)
            {
                TestSuit.TakeScreenShot("Fail");
                Assert.Fail("Failed with Exception: " + Exc);
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            try
            {
                w.Write("\r\nError Log : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine($"  :{logMessage}");
                w.WriteLine("-------------------------------");
            }
            catch (Exception Ex)
            {
                TestSuit.TakeScreenShot("Fail");
            }

        }
    }
}
