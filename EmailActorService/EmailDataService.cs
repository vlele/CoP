using EmailActorService.Entities;
using EmailActorService.Interfaces.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailActorService
{
    public class EmailDataService
    {
        private const string connectionString = "";
        private const string tableName = "email";

        private static CloudTable GetTableReference()
        {
            CloudTable table = default(CloudTable);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();

            return table == default(CloudTable) ? null : table;
        }

        public static void Insert(string to, string message, string thandle, string status)
        {
            CloudTable emailTable = GetTableReference();
            if (emailTable == null) return;

            TableOperation insertOperation = TableOperation.Insert(new Email()
            {
                PartitionKey = "author",
                RowKey = thandle,
                To = to,
                Message = message,
                Status = status
            });

            emailTable.Execute(insertOperation);
        }

        public static IEnumerable<Email> GetEmails(string fromDate, string toDate)
        {
            CloudTable emailTable = GetTableReference();
            if (emailTable == null) return new List<Email>();

            TableQuery<Email> query = default(TableQuery<Email>);

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                var fromDateTime = DateTime.SpecifyKind(Convert.ToDateTime(fromDate), DateTimeKind.Utc);
                var toDateTime = DateTime.SpecifyKind(Convert.ToDateTime(toDate), DateTimeKind.Utc);

                string fromDateFilter = TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, fromDateTime);
                string toDateFilter = TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThanOrEqual, toDateTime);
                string finalFilter = TableQuery.CombineFilters(fromDateFilter, TableOperators.And, toDateFilter);

                query = new TableQuery<Email>().Where(finalFilter);
            }
            else
            {
                query = new TableQuery<Email>();
            }

            return emailTable.ExecuteQuery<Email>(query);
        }
    }
}
