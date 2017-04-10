using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LogAzure.Controllers
{
    public class Home1Controller : Controller
    {
        //
        // GET: /Home1/
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            //System.Diagnostics.Debug.WriteLine(DateTime.Now + " Calling Index");

            Random rand = new Random();
            logger.Info(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTimeOffset.Now));
            Thread.Sleep(rand.Next(1000));

            logger.Info("ok");
            Thread.Sleep(rand.Next(1000));
            logger.Debug("No input");
            Thread.Sleep(rand.Next(1000));

            logger.Debug("endLogger");
            return View();         
        }


    }
}
