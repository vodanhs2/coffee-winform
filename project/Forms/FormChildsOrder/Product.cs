using FontAwesome.Sharp;
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
    public partial class Product : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        private int table_id;
        public Product(int table_id)
        {
            InitializeComponent();
            this.table_id = table_id;
            fill();
        }
        private void fill()
        {
            var cats = from c in db.categories
                       select c;
            if(cats.Count()>0)
            {
                PnlCategory.Controls.Clear();
                IconButton btn ;
                FontFamily fontFamily = new FontFamily("Segoe UI Semibold");
                foreach (var item in cats)
                {
                    btn = new IconButton();
                    btn.Size = new Size(138, 40);
                    btn.FlatStyle = FlatStyle.Flat;
                    string hex = item.mausac;
                    btn.BackColor = System.Drawing.ColorTranslator.FromHtml(hex);
                    btn.ForeColor = Color.White;
                    btn.Text = item.name;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.White;
                    btn.Font = new Font(fontFamily, 8,FontStyle.Bold, GraphicsUnit.Point);

                   
                    btn.Name = Convert.ToString(item.id);
                    PnlCategory.Controls.Add(btn);
                    btn.Click += Btn_Click;
                    btn.Tag = item;
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            category cats = ((sender as Button).Tag) as category;
            var pro = from p in db.products
                      join c in db.categories
                      on p.cat_id equals c.id
                      where c.id == cats.id
                      select p;
            flowLayoutPanel1.Controls.Clear();
            foreach (var item in pro)
            {
                Panel pnl = new Panel();
                Label lbName = new Label();
                Label lbPrice = new Label();

                pnl.Controls.Clear();
               
                pnl.ForeColor = Color.White;
                pnl.Location = new System.Drawing.Point(23, 13);
                pnl.Name = Convert.ToString(item.id);
                pnl.Size = new System.Drawing.Size(135, 69);
                pnl.BorderStyle = BorderStyle.Fixed3D;
                pnl.TabIndex = 2;

                // 
                // labelPrice
                // 
                lbPrice.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbPrice.ForeColor = System.Drawing.Color.Red;
                lbPrice.Location = new System.Drawing.Point(0, 36);
                lbPrice.Size = new System.Drawing.Size(139, 23);
                lbPrice.TabIndex = 1;
                lbPrice.Text = Convert.ToString(item.price.ToString("#,##0"))+"VND/x1";
                lbPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // labelName
                // 
                lbName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbName.Location = new System.Drawing.Point(2, 8);
                lbName.Size = new System.Drawing.Size(136, 23);
                lbName.TabIndex = 0;
                lbName.Text = item.name;
                lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                pnl.Controls.Add(lbPrice);
                pnl.Controls.Add(lbName);
                flowLayoutPanel1.Controls.Add(pnl);
                pnl.Click += Pnl_Click;
                pnl.Tag = item;
            }
        }

        private void Pnl_Click(object sender, EventArgs e)
        {
            product pros = ((sender as Panel).Tag) as product;
            Quantity qty = new Quantity(pros.id,pros.name,pros.price,table_id);
            qty.ShowDialog();
           
        }

        private void Product_Load(object sender, EventArgs e)
        {

        }
    }
}
