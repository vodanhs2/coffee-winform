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
        DateTime date = DateTime.Now;
        public double totalprice;
        public string nameTable;
        public int table_id;
        public int order_id;
        List<order> lsorder;

        public Payment(double totalprice, List<order> LsOrder)
        {
            InitializeComponent();
            this.lsorder = LsOrder;
            this.totalprice = totalprice;
            foreach (var item in LsOrder)
            {
                coffee_table table = db.coffee_tables.Single(x => x.id == Convert.ToInt32(item.table_id));
                lblName.Text += Convert.ToString(table.name) + ", ";
            }
            Totalprice.Text = "" + totalprice.ToString("#,###,###") + " VNĐ";

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
            order_many_table many = new order_many_table();
            many.create_at = date;
            many.discount = LsOrder.sell;
            many.total_price = totalprice;
            if (Program.em != null)
            {
                many.emp_id = Program.em.id;
            }
            db.order_many_tables.InsertOnSubmit(many);
            db.SubmitChanges();
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.order_many_tables);
            var order_many = from d in db.order_many_tables
                             orderby d.id descending
                             select d;
            foreach (var item in lsorder)
            {
                order order = db.orders.Single(x => x.id == item.id);
                order.many_id = order_many.First().id;
                order.status = 1;
                var detail = from d in db.order_details
                             where d.order_id == item.id
                             select d;
                double price_order = 0;
                foreach (var i in detail)
                {
                    price_order += Convert.ToDouble(i.price * i.quantity);
                }
                if (order.discount != null)
                {
                    if (order.discount > 100)
                    {
                        if (price_order - order.discount <= 0)
                        {
                            order.total_price = 0;
                        }
                        else
                        {
                            order.total_price = price_order - order.discount;
                        }
                    }
                    else
                    {
                        order.total_price = (double)(price_order - (price_order * LsOrder.sell / 100));
                    }
                }
                else
                {
                    order.total_price = price_order;
                }

                db.SubmitChanges();
                var table = db.coffee_tables.Single(x => x.id == item.table_id);
                table.status = 1;
                db.SubmitChanges();
            }
            Orders.order.LoadTable();
            Orders.order.Update();
            Market.market.Close();
            ReportOrder report = new ReportOrder();
            report.id = order_many.First().id;
            report.ShowDialog();
            this.Dispose();
            CheckPayment.check.Dispose();


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
                    if (sendPrice - totalprice == 0)
                    {
                        pricenew.Text = Convert.ToString((sendPrice - totalprice)) + " VNĐ";
                    }
                }
                else
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
