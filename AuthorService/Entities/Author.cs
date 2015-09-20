using Microsoft.WindowsAzure.Storage.Table;

namespace AuthorService.Entities
{
    public class Author : TableEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

