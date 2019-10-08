using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSTask.Command.Request
{
    public class OrderRequest
    {
        // int id, decimal tax, decimal discount,int quntity
        public int ItemId { get; set; }
        public int Price { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public int Quntity { get; set; }
    }
}