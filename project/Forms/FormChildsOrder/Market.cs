using FontAwesome.Sharp;
using project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Forms.FormChildsOrder
{
   
    public partial class Market : Form
    {
        public static Market market;
        DataClasses1DataContext db = new DataClasses1DataContext();
        private int id;
        private string name;
        private int status;
        private int order_id;
        private double price= 0;
        private decimal totalprice=0;
        Cart cart = new Cart();
        List<order_detail> lsorder_Details;
      
        public Market(int id,string name,int status)
        {
            
            InitializeComponent();
            market = this;
            this.id = id;
            this.name = name;
            this.status = status;
            pnlsProduct.Visible = false;
            pnlPayment.Visible = false;
            NameTable.Text = name;
           
            order orders = cart.getTableId(id);
          
            if(orders != null) {
                lsorder_Details = cart.getDsOrder(orders.id);
                fillLsProduct(0);
                order_id = orders.id;
                lblTime.Text = Convert.ToString(orders.create_at);
            }
            switch (status)
            {
                case 1:
                    lblStatus.Text = "Trống";
                    break;
                case 2:
                    lblStatus.Text = "Đang phục vụ";
                    btnBook.Visible=false;
                    btnBack.Visible = false;
                    OpenChildForm(new Product(id));

                    break;
                case 3:
                    lblStatus.Text = "Đã được đặt";
                    break;
                default:
                    lblStatus.Text = "Trống";
                    break;
            }
        }
        public void fillHome()
        {

            order or = cart.getTableId(id);
            order_id = or.id;
            int ck = cart.CheckLsProduct(order_id,id);
            
            btnOrder.Visible = true;
            if (ck==0)
            {
                
                btnOrder.Visible = false;
                CancelOrder();
            }
            if (or.discount != null)
            {
                if (or.discount > 100)
                {
                    lblSell.Text = "" + or.discount + " VND";
                    if (price - or.discount <= 0)
                    {
                        totalprice = 0;
                        lblTotal.Text = "0 VND";
                    }
                    else
                    {
                        totalprice = (decimal)(price - or.discount);
                        lblTotal.Text = "" + totalprice.ToString("#,###,###") + " VND";

                    }
                }
                else
                {
                    lblSell.Text = "" + or.discount + " %";
                    totalprice = (decimal)(price - (price * or.discount / 100));
                    lblTotal.Text = "" + totalprice.ToString("#,###,###") + " VND";
                }
            }
            else
            {
                totalprice = (decimal) price;
                    lblTotal.Text = "" + totalprice.ToString("#,###,###") + " VND";
            }


        }
        public void fillLsProduct(int id_order)
        {
          
          
            if (id_order != 0)
            {
                lsorder_Details = cart.getDsOrder(id_order);
                order or = cart.getTableId(id);
                price = 0;
            }
            this.order_id = id_order;
            if (lsorder_Details != null) { 
               
                pnlsProduct.Visible = true;
                pnlPayment.Visible = true;
                btnOrder.Text = "Thanh toán";
                if (btnOrder.Text == "Thanh toán")
                {
                    btnOrder.Location = new System.Drawing.Point(97, 0);
                }
                else
                {
                    btnOrder.Location = new System.Drawing.Point(163, 5);
                }
                pnlsProduct.Controls.Clear();
                foreach (var item in lsorder_Details)
                {
                    var product = db.products.SingleOrDefault(x=>x.id==item.pro_id);
                    price += Convert.ToDouble(item.price * item.quantity); 

                    // 
                    // IconClose
                    // 
                    IconPictureBox icon = new IconPictureBox();
                  icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
                   icon.ForeColor = System.Drawing.Color.Red;
                   icon.IconChar = FontAwesome.Sharp.IconChar.Times;
                   icon.IconColor = System.Drawing.Color.Red;
                   icon.IconSize = 17;
                   icon.Location = new System.Drawing.Point(258, 9);
                   icon.Name = "IconClose";
                   icon.Size = new System.Drawing.Size(29, 17);
                   icon.TabIndex = 1;
                   icon.TabStop = false;
                    // 
                    // lblquantityPro
                    // 
                    Label lblQuantity = new Label();
                    lblQuantity.AutoSize = true;
                    lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblQuantity.ForeColor = System.Drawing.Color.White;
                    lblQuantity.Location = new System.Drawing.Point(197, 9);
                    lblQuantity.Name = Convert.ToString(item.pro_id);
                    lblQuantity.Size = new System.Drawing.Size(26, 17);
                    lblQuantity.TabIndex = 0;
                    lblQuantity.Text = "x "+item.quantity;
                    // 
                    // lblpricePro
                    // 
                    Label lblPrice = new Label();
                   lblPrice.AutoSize = true;
                   lblPrice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                   lblPrice.ForeColor = System.Drawing.Color.Red;
                   lblPrice.Location = new System.Drawing.Point(89, 9);
                   lblPrice.Name = Convert.ToString(item.pro_id);
                   lblPrice.Size = new System.Drawing.Size(76, 17);
                   lblPrice.TabIndex = 0;
                   lblPrice.Text = ""+item.price+" VND";
                    // 
                    // lblNamePro
                    // 
                    Label lblNamePro = new Label();
                    lblNamePro.AutoSize = true;
                    lblNamePro.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblNamePro.ForeColor = System.Drawing.Color.White;
                    lblNamePro.Location = new System.Drawing.Point(16, 9);
                    lblNamePro.Name = Convert.ToString(item.pro_id);
                    lblNamePro.Size = new System.Drawing.Size(39, 17);
                    lblNamePro.TabIndex = 0;
                    lblNamePro.Text = Convert.ToString(product.name);


                    // 
                    // panel
                    // 
                    Panel pn = new Panel();
                    pn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    pn.Controls.Add(icon);
                    pn.Controls.Add(lblQuantity);
                    pn.Controls.Add(lblPrice);
                    pn.Controls.Add(lblNamePro);
                    pn.Location = new System.Drawing.Point(3, 3);
                    pn.Name = Convert.ToString(product.name);
                    pn.Size = new System.Drawing.Size(300, 42);
                    pn.TabIndex = 2;
                    pn.Click += Pn_Click;
                    icon.Click += Icon_Click;
                    icon.Tag = item;
                    pn.Tag = item;
                    pnlsProduct.Controls.Add(pn);
            }
                fillHome();
            }
            SumPrice.Text = Convert.ToString(price);


        }

        private void Icon_Click(object sender, EventArgs e)
        {
            order_detail or = ((sender as IconPictureBox).Tag) as order_detail;
            var product = db.products.SingleOrDefault(x => x.id == or.pro_id);
            DialogResult dialog = MessageBox.Show("Hủy sản phẩm: " + product.name + " ?", "Hủy sản phẩm: "+product.name+" ?", MessageBoxButtons.YesNoCancel,
        MessageBoxIcon.Information);
        if(dialog == DialogResult.Yes)
            {
                int delete = cart.deletePro(Convert.ToInt32(or.pro_id), Convert.ToInt32(or.order_id), id);
             
                if (delete == 1)
                {
                    fillLsProduct(this.order_id);
                }
                if (delete == 2)
                {
                    fillLsProduct(this.order_id);
                    CancelOrder();
                }
            }
        }

        private void Pn_Click(object sender, EventArgs e)
        {
            order_detail or = ((sender as Panel).Tag) as order_detail;
            var product = db.products.SingleOrDefault(x => x.id == or.pro_id);
            Quantity qty = new Quantity(Convert.ToInt32(or.pro_id), product.name,Convert.ToDouble(or.price),id);
            qty.ShowDialog();
        }

        public void CancelOrder()
        {
            Button btnHuy = new Button();
            btnHuy.Text = "Hủy bàn";
            btnHuy.Size = new System.Drawing.Size(100, 40);
            btnOrder.Visible = false;
            pnlPayment.Visible = false;
            btnHuy.Click += BtnHuy_Click;
            pnlsProduct.Controls.Clear();
            pnlsProduct.Controls.Add(btnHuy);
            pnlsProduct.Update();

        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
           coffee_table tables = db.coffee_tables.Single(x => x.id == id);
            tables.status = 1;
            db.SubmitChanges();
            Orders.order.LoadTable();
            this.Dispose();
            cart.CancelOrder(this.order_id);
            
        }

        DateTime today = DateTime.Now;
       
        private void Market_Load(object sender, EventArgs e)
        {
           
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (btnOrder.Text == "Hủy bàn")
            {
                lblTime.Text = "_ _ _";
                lblStatus.Text = "Trống";
                btnOrder.Text = "Gọi món";
                btnBook.Visible=true;
                PnlProduct.Controls.Clear();
                coffee_table tables = db.coffee_tables.Single(x => x.id == id);
                tables.status = 1;
                db.SubmitChanges();
                Orders.order.LoadTable();
                return;
            }
            if (btnOrder.Text== "Gọi món") {
                lblTime.Text = Convert.ToString(today);
                lblStatus.Text = "Đang phục vụ";
                btnOrder.Text = "Hủy bàn";
                btnBook.Visible =false;
          
            OpenChildForm(new Product(id));
            }
          
            if (btnOrder.Text == "Thanh toán")
            {
                Payment pay = new Payment(Convert.ToDouble(totalprice),name,id,order_id);
                pay.ShowDialog();
            }
        }
        private void OpenChildForm(Form childForm)
        {

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PnlProduct.Controls.Clear();
            PnlProduct.Controls.Add(childForm);
            PnlProduct.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            coffee_table tables = db.coffee_tables.Single(x => x.id == id);
            tables.status = 1;
            db.SubmitChanges();
            this.Close();
        }

        private void pnlSell_MouseClick(object sender, MouseEventArgs e)
        {
            Sell sell = new Sell(order_id,name,price);
            sell.ShowDialog();
        }
    }
}
