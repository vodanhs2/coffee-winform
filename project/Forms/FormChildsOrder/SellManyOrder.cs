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
    public partial class SellManyOrder : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public double price = 0;
        List<order> lsOrders;
        public SellManyOrder(List<order> lsOrders, double price)
        {
            this.lsOrders = lsOrders;
            this.price = price;
            InitializeComponent();
            foreach (var item in lsOrders)
            {
                coffee_table table = db.coffee_tables.Single(x => x.id == Convert.ToInt32(item.table_id));
                lblName.Text += Convert.ToString(table.name) + ", ";
            }

        }

        private void SellManyOrder_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double gia = Convert.ToDouble(comboBox1.Text);

                if (gia > this.price)
                {
                    gia = this.price;

                }
                foreach (var item in lsOrders)
                {
                    order order = db.orders.Single(x => x.id == Convert.ToInt32(item.id));
                    order.discount = gia;
                    db.SubmitChanges();
                }
                LsOrder.sell = gia;
                LsOrder.lsorderForm.fillSell();
                LsOrder.lsorderForm.Update();
                this.Dispose();


            }
            catch (Exception)
            {

                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
            else
            {
                double gia = Convert.ToDouble(comboBox1.Text);
                if (gia > 100 || gia == 0)
                {
                    PTram.Text = "VND";

                }
                if (gia <= 100)
                {
                    PTram.Text = "%";

                }
            }
        }
    }
}
