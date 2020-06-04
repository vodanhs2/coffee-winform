using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models
{
    class ReportModelOrder
    {
        public int order_id { get; set; }
        public string table_name { get; set; }
        public int product_id { get; set; }
        public string product_Name { get; set; }
        public int quantity { get; set; }
        public double discount { get; set; }
        public double price { get; set; }

    }
}
