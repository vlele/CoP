using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailActorService.Entities
{
    public class Email : TableEntity
    {
        public string To { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
