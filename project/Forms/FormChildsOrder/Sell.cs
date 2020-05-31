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
    public partial class Sell : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public double price;
        public int order_id;
        public string table_name;
        public Sell(int order_id,string table_name,double price )
        {
            InitializeComponent();
            this.order_id = order_id;
            this.table_name = table_name;
            this.price = price;
            lblName.Text = "" + table_name + " - Giảm Giá";
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
                order order = db.orders.SingleOrDefault(x => x.id == order_id);
                order.discount=gia;
                db.SubmitChanges();
                Market.market.fillHome();
                Market.market.Update();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
