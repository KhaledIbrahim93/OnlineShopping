using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSTask.Command.Response
{
    public class OrderResponse
    {
        public DateTime? OrderDate { get; set; }
        public DateTime ? RequestDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Satuts { get; set; }
        public string CustomerName { get; set; }
        public decimal? TotualPrice { get; set; }
    }
}