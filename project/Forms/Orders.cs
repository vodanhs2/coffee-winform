using FontAwesome.Sharp;
using project.Forms.FormChildsOrder;
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

namespace project.Forms
{
    public partial class Orders : Form
    {
        public static Orders order;

        public Orders()
        {
            InitializeComponent();
            order = this;
            LoadTable();
        }
      
        DataClasses1DataContext db = new DataClasses1DataContext();
        private void Orders_Load(object sender, EventArgs e)
        {
           
        }
        public void LoadTable()
        {
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.coffee_tables);
            var table = from tb in db.coffee_tables
                     select tb;
           if(table != null)
            {
                FlowPnlTable.Controls.Clear();
            foreach (var item in table)
            {
                IconButton btn = new IconButton();
                btn.IconChar = IconChar.Coffee;
                btn.Size = new Size(91, 55);
                btn.FlatStyle = FlatStyle.Flat;
                btn.IconSize = 40;
                btn.ForeColor = Color.White;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                btn.Text = item.name;
                btn.FlatAppearance.BorderSize = 0;
                btn.Name = Convert.ToString(item.id);
                btn.IconColor = Color.White;
                switch (item.status)
                {
                    case 1: // trong
                        btn.IconColor = Color.White;
                        break;
                    case 2: //dang duoc chon
                        btn.IconColor = Color.Green;
                        break;
                    case 3://da duoc dat
                        btn.IconColor = Color.Red;
                        break;
                }
                FlowPnlTable.Controls.Add(btn);
                btn.Click += Btn_Click;
                btn.Tag = item;
            }
            }



        }

        private void Btn_Click(object sender, EventArgs e)
        {
            coffee_table coffee_Table = ((sender as Button).Tag) as coffee_table;
            OpenChildForm(new Market(coffee_Table.id, coffee_Table.name,Convert.ToInt32(coffee_Table.status)));
           
        }
        private void OpenChildForm(Form childForm)
        {
           
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            
        }
    }
}
