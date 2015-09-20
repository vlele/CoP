using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailActorService.Interfaces.Models
{
    public class EmailModel
    {
        public DateTimeOffset RequestDateTime { get; set; }
        public string Status { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
    }
}
