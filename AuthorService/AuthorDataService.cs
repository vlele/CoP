using AuthorService.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorService
{
    public class AuthorDataService
    {
        private const string connectionString = "";
        private const string tableName = "author";

        private static CloudTable GetTableReference()
        {
            CloudTable table = default(CloudTable);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();

            return table == default(CloudTable) ? null : table;
        }

        public static void InsertSeedData()
        {
            CloudTable authorTable = GetTableReference();
            if (authorTable == null) return;

            Author[] authors = new Author[]
            {
                new Author(){ PartitionKey = "author", RowKey = "OdeToCode", Firstname = "Scott", Lastname = "Allen", Phone = "8790000801", Email = "OdeToCode@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "troyhunt", Firstname = "Troy", Lastname = "Hunt", Phone = "8790000802", Email = "troyhunt@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "danwahlin", Firstname = "Dan", Lastname = "Wahlin", Phone = "8790000803", Email = "danwahlin@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "shawnwildermuth", Firstname = "Shawn", Lastname = "Wildermuth", Phone = "8790000804", Email = "shawnwildermuth@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "housecor", Firstname = "Cory", Lastname = "House", Phone = "8790000805", Email = "housecor@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "jeremybytes", Firstname = "Jeremy", Lastname = "Clark", Phone = "8790000806", Email = "jeremybytes@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "john_papa", Firstname = "John", Lastname = "Papa", Phone = "8790000807", Email = "john_papa@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "skonnard", Firstname = "Aaron", Lastname = "Skonnard", Phone = "8790000808", Email = "skonnard@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "robertsjason", Firstname = "Jason", Lastname = "Roberts", Phone = "8790000809", Email = "robertsjason@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "mcwoodring", Firstname = "Mike", Lastname = "Woodring", Phone = "8790000810", Email = "mcwoodring@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "julielerman", Firstname = "Julie", Lastname = "Lerman", Phone = "8790000811", Email = "julielerman@psmail.com" },
                new Author(){ PartitionKey = "author", RowKey = "TheLoudSteve", Firstname = "Steve", Lastname = "Evans", Phone = "8790000812", Email = "TheLoudSteve@psmail.com" }
            };

            foreach (Author author in authors)
            {
                authorTable.Execute(TableOperation.Insert(author));
            }
        }

        public static IEnumerable<Author> GetAuthors(string firstname, string lastname)
        {
            CloudTable authorTable = GetTableReference();
            if (authorTable == null) return new List<Author>();

            TableQuery<Author> query = default(TableQuery<Author>);

            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
            {
                string lastnameFilter = TableQuery.GenerateFilterCondition("Lastname", QueryComparisons.Equal, lastname);
                string firstnameFilter = TableQuery.GenerateFilterCondition("Firstname", QueryComparisons.Equal, firstname);
                string finalFilter = TableQuery.CombineFilters(lastnameFilter, TableOperators.And, firstnameFilter);

                query = new TableQuery<Author>().Where(finalFilter);
            }
            else if (!string.IsNullOrEmpty(firstname))
            {
                query = new TableQuery<Author>()
                    .Where(TableQuery.GenerateFilterCondition("Firstname", QueryComparisons.Equal, firstname));
            }
            else if (!string.IsNullOrEmpty(lastname))
            {
                query = new TableQuery<Author>()
                    .Where(TableQuery.GenerateFilterCondition("Lastname", QueryComparisons.Equal, lastname));
            }
            else
            {
                query = new TableQuery<Author>();
            }


            return authorTable.ExecuteQuery<Author>(query);
        }
    }
}
