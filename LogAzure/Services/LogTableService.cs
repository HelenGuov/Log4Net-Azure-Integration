
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using LogAzure.Models;

using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace LogAzure.Services
{
    public class LogTableService
    {
        private CloudTable _table; 

        /// <summary>
        /// Initialise azure storage service, and get/or create table name 
        /// </summary>
        public LogTableService()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            //if the table has been created, it will just get that table for further updates. 
            _table = tableClient.GetTableReference("TableDemo");
            _table.CreateIfNotExists();
        }

        /// <summary>
        /// Add entry into azure table 
        /// </summary>
        /// <param name="aLog"></param>
        public void AddEntry(LogEntity aLog)
        {
            TableOperation insertOperation = TableOperation.Insert(aLog);

            // Execute the insert operation.
            _table.Execute(insertOperation);
        }
    }
}