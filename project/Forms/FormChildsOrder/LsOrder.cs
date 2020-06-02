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
    public partial class LsOrder : Form
    {
        public static LsOrder lsorderForm;
        public static double sell;
        DataClasses1DataContext db = new DataClasses1DataContext();
        List<order> lsorders;
        public double totalPrice = 0;
        public string name;
        public double sumPrice = 0;
        public LsOrder()
        {
            InitializeComponent();
            lsorderForm = this;
            lsorders = new List<order>();
            order_load();
            fillSell();
        }
        private void LsOrder_Load(object sender, EventArgs e)
        {

        }
        CheckBox cb;
        public void order_load()
        {
            var order = from o in db.orders
                        where o.status == 0
                        select o;
            flowLayoutPanel1.Controls.Clear();
            if (order != null)
            {
                foreach (var item in order)
                {
                    coffee_table table = db.coffee_tables.Single(x => x.id == item.table_id);
                    if (table != null)
                    {
                        cb = new CheckBox();
                        cb.AutoSize = true;
                        cb.Location = new System.Drawing.Point(13, 13);
                        cb.Name = Convert.ToString(item.id);
                        cb.Size = new System.Drawing.Size(54, 17);
                        cb.TabIndex = 0;
                        cb.Text = "" + table.name;
                        cb.ForeColor = Color.White;
                        cb.UseVisualStyleBackColor = true;
                        flowLayoutPanel1.Controls.Add(cb);
                        cb.CheckedChanged += Cb_CheckedChanged; ;
                        cb.Tag = item;

                    }
                }
            }
        }
        public void fillSell()
        {


            if (LsOrder.sell != 0)
            {
                if (LsOrder.sell > 100)
                {
                    lblSell.Text = Convert.ToString(LsOrder.sell) + " VND";
                    if (sumPrice - LsOrder.sell <= 0)
                    {
                        totalPrice = 0;
                        lblTotal.Text = "0 VNĐ";
                    }
                    else
                    {
                        totalPrice = (double)(sumPrice - LsOrder.sell);
                        lblTotal.Text = "" + totalPrice.ToString("#,###,###") + " VND";

                    }
                }
                else
                {
                    lblSell.Text = "" + LsOrder.sell + " %";
                    totalPrice = (double)(sumPrice - (sumPrice * LsOrder.sell / 100));
                    lblTotal.Text = "" + totalPrice.ToString("#,###,###") + " VND";
                }
            }
            else
            {
                lblSell.Text = "" + LsOrder.sell;
                totalPrice = sumPrice;
                lblTotal.Text = "" + totalPrice.ToString("#,###,###") + " VND";
            }

        }
        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            order or = ((sender as CheckBox).Tag) as order;
            var detail = from d in db.order_details
                         where d.order_id == or.id
                         select d;
            foreach (var item in detail)
            {
                if (cb.Checked == true)
                {

                    sumPrice += (Convert.ToDouble(item.price * item.quantity));
                }
                else
                {
                    sumPrice -= (Convert.ToDouble(item.price * item.quantity));
                }

            }
            SumPrice.Text = Convert.ToString(sumPrice);
            totalPrice = sumPrice;
            lblTotal.Text = "" + totalPrice.ToString("#,###,###") + " VND";

            if (cb.Checked == true)
            {
                lsorders.Add(or);
            }
            else
            {
                lsorders.Remove(or);
            }
            fillSell();
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {
            Payment payment = new Payment(totalPrice, lsorders);
            payment.ShowDialog();
        }

        private void pnlSell_MouseClick(object sender, MouseEventArgs e)
        {
            SellManyOrder sell = new SellManyOrder(lsorders, sumPrice);
            sell.ShowDialog();
        }

        private void pnlSell_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
