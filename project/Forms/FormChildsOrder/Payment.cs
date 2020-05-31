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
    public partial class Payment : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public double totalprice;
        public string nameTable;
        public int table_id;
        public int order_id;
        public Payment(double totalprice, string nameTable, int table_id, int order_id)
        {
            InitializeComponent();
            this.table_id = table_id;
            this.totalprice = totalprice;
            this.order_id = order_id;
            this.nameTable = nameTable;
            lblName.Text = "" + nameTable + " - Thanh toán";
            Totalprice.Text =""+totalprice.ToString("#,###,###") + " VNĐ";

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            coffee_table table = db.coffee_tables.SingleOrDefault(x=>x.id==table_id);
            table.status = 1;
            db.SubmitChanges();
            order order = db.orders.SingleOrDefault(x=>x.id==order_id);
            order.total_price = totalprice;
            order.status = 1;
            db.SubmitChanges();
            Orders.order.LoadTable();
            Market.market.Dispose();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
            
                try
                {
                    double sendPrice = Convert.ToDouble(textBox1.Text);
                    if (sendPrice - totalprice >= 0)
                    {
                        pricenew.Text = Convert.ToString((sendPrice - totalprice).ToString("#,###,###")) + " VNĐ";
                    }
                }
                catch (Exception)
                {

                    textBox1.Text = "";
                }
            }

        
    }
}
