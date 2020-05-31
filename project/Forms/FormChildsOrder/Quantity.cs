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
    public partial class Quantity : Form
    {
         DateTime date = DateTime.Now;
        public int pro_id;
        public int table_id;
        public double price;
        public string name;
      
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Quantity(int pro_id,string name,double price, int table_id)
        {
           
            InitializeComponent();
            this.pro_id = pro_id;
            this.name = name;
            this.price = price;
            this.table_id = table_id;
            Fill();
            var order = from d in db.order_details
                        join o in db.orders on d.order_id equals o.id
                        where d.pro_id == pro_id
                        where o.table_id == table_id
                        where o.status == 0
                        select new { id = d.id, quantity = d.quantity, price = d.price };
            if (order.Count()>0)
            {
                txtGia.Text = Convert.ToString(order.First().price);
                txtQuantity.Value = Convert.ToInt32(order.First().quantity);
            }

        }
        private void Fill()
        {
            coffee_table table = db.coffee_tables.SingleOrDefault(x=>x.id==table_id);
            ProName.Text = name;
            txtGia.Text = Convert.ToString(price);
            TableName.Text = table.name;

            

        }
        private void Quantity_Load(object sender, EventArgs e)
        {

        }
       
       
       
        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            var order = from o in db.orders
                        where o.table_id == table_id
                        where o.status == 0
                        select o;
            if (order.Count() <= 0)
            {
                order ord = new order();
                ord.table_id = table_id;
                ord.create_at = date;
                ord.status = 0;
                db.orders.InsertOnSubmit(ord);
                db.SubmitChanges();
                
            }
          
            
            var details = from d in db.order_details
                        join o in db.orders on d.order_id equals o.id
                        where d.pro_id == pro_id
                        where o.table_id == table_id
                        where o.status == 0
                        select new { id = d.id, quantity = d.quantity, price = d.price };
           
            if (details.Count() > 0)
            {
                order_detail detail = db.order_details.SingleOrDefault(x => x.id == details.First().id);
                detail.price = Convert.ToDouble(txtGia.Text);
                detail.quantity = Convert.ToInt32(txtQuantity.Value);
                db.SubmitChanges();
            }
            else
            {

                var getOrder = from o in db.orders
                            where o.table_id == table_id
                            where o.status == 0
                            select o;
                order_detail detail = new order_detail();
                detail.order_id = getOrder.First().id;
                detail.pro_id = pro_id;
                detail.price = Convert.ToDouble(txtGia.Text);
                detail.quantity = Convert.ToInt32(txtQuantity.Value);
                db.order_details.InsertOnSubmit(detail);
                db.SubmitChanges();
                db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.order_details);
            }
           
          
            coffee_table tables = db.coffee_tables.Single(x => x.id == table_id);
            Cart cart = new Cart();
            tables.status = 2;
            db.SubmitChanges();
            Orders.order.LoadTable();
            Orders.order.Update();
            Market.market.fillLsProduct(cart.getOrderId(table_id));
            Market.market.Update();

            this.Close();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NbQuantity_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
