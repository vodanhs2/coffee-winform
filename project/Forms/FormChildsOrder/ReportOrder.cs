using project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Forms.FormChildsOrder
{
    public partial class ReportOrder : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public int id { get; set; }
        public ReportOrder()
        {
            InitializeComponent();
        }

    

        private void ReportOrder_Load_1(object sender, EventArgs e)
        {
            Reportorder reportorder = new Reportorder();
            //var _order = db.order_details
            //    .Select(x => new ReportModelOrder
            //    {
            //        order_id = id,
            //        table_name = x.order.coffee_table.name,
            //        product_id = Convert.ToInt32(x.pro_id),
            //        product_Name = x.product.name,
            //        quantity = Convert.ToInt32(x.quantity),
            //        discount = Convert.ToDouble(x.order.order_many_table.discount),
            //        price = Convert.ToDouble(x.price)
            //    });

            var _orderdetails = from or in db.orders
                                join d in db.order_details on or.id equals d.order_id
                                select new { many_id = or.many_id, order_id = d.order_id, price = d.price, productId = d.pro_id, productName = d.product.name, quantity = d.quantity, discount = or.discount, tableName = or.coffee_table.name };
            var results = _orderdetails.Where(x => x.many_id == id).Distinct();

            var import = results.Select(x => new ReportModelOrder
            {
                order_id = Convert.ToInt32(x.order_id),
                table_name = x.tableName,
                product_id = Convert.ToInt32(x.productId),
                product_Name = x.productName,
                quantity = Convert.ToInt32(x.quantity),
                discount = x.discount != null ? Convert.ToDouble(x.discount) : 0,
                price = Convert.ToDouble(x.price)
            });
            reportorder.SetDataSource(import);
            crystalReportViewer1.ReportSource = reportorder;
            crystalReportViewer1.Show();
        }
    }
}
