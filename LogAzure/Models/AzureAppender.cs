using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using log4net.Core;
using log4net.Appender;
using LogAzure.Services;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System.Configuration; 

namespace LogAzure.Models
{
    public class AzureAppender : AppenderSkeleton
    {
        private LogTableService _storageService; 

        /// <summary>
        /// This method will be called after log4net has been configured. 
        /// </summary>
        public override void ActivateOptions()
        {
            base.ActivateOptions();
            _storageService = new LogTableService(); 
        }

        /// <summary>
        /// Custom appender for Azure Table
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            DateTime time_stamp = loggingEvent.TimeStamp.ToLocalTime(); 
            //keys
            string partitionKey = string.Format("{0:yyyy-MM-dd HH:mm}", time_stamp);
            string rowKey = string.Format("{0:ss.fff}-{1}", time_stamp, Guid.NewGuid()); 
            string thread = loggingEvent.ThreadName;
            LogEntity log = new LogEntity(partitionKey, rowKey); 
            
            //attributes
            //log.DateLogged = time_stamp; 
            log.Thread = loggingEvent.ThreadName;
            log.Level = loggingEvent.Level.Name; 
            log.Logger = loggingEvent.LoggerName;
            log.Message = loggingEvent.RenderedMessage;
            log.Exception = loggingEvent.GetExceptionString(); 

            _storageService.AddEntry(log); 
        }
      

    }
}