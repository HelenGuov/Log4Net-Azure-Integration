using LogAzure.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LogAzure.Controllers
{
    public class HomeController : AsyncController
    {
        // GET: /Home/
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            //WriteLargeRecords();
            //WriteVarietyLevels(); 
            WriteShortRecords(); 
            return View();
        }
        
        private void WriteShortRecords()
        {
            logger.Error("mock error", new NullReferenceException()); 
        }

        private void WriteVarietyLevels()
        {
            Random rand = new Random();
            int range = 5000;

            int iteration = 2;
            int numRecords = 50; 
            int i = 0; 
            while (i < 2) {

                for (int j=0; j<numRecords; j++) {
                    logger.Error( "Mock error");
                    Thread.Sleep(rand.Next(range));
                    logger.Debug("Mock debug");
                    Thread.Sleep(rand.Next(range));
                    logger.Fatal("Mock fatal"); 
                    Thread.Sleep(rand.Next(range)); 
                    logger.Warn("Mock warning");
                }
                i++; 
            }
        }

        private void WriteLargeRecords()
        {
            //System.Diagnostics.Debug.WriteLine(DateTime.Now + " Calling Index");
            Random rand = new Random();

            int j = 1;
            while (j <= 2)
            {
                for (int i = 0; i < 20; i++)
                {
                    logger.Info(string.Format("Application starts at {0:yyyy-MM-dd HH:mm:ss.fff}", DateTimeOffset.Now));
                    Thread.Sleep(rand.Next(5000));
                }
                logger.Info(String.Format("end of {0} x 1000 records", j));
                j++;
            }
        }

        private void TestAsync()
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now + " Calling Index");

            Random rand = new Random();
            logger.Info(string.Format("Application starts at {0:yyyy-MM-dd HH:mm:ss.fff}", DateTimeOffset.Now));
            Thread.Sleep(rand.Next(5000));

            logger.Info("Logging.. ");
            Thread.Sleep(rand.Next(5000));
            logger.Debug("log as debug");
            Thread.Sleep(rand.Next(5000));


            List<String> strList = new List<String>();
            try
            {
                string mystr = strList.First();
            }
            catch (Exception e)
            {
                logger.Error("Error when receiving first item from strlist " + Environment.NewLine + "Exception: " + e.Message);
            }
        }
    }
}
