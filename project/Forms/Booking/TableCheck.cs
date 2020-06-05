using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Forms.Booking
{
    public partial class TableCheck : Form
    {
        private string labelName = "";
        DataClasses1DataContext db = new DataClasses1DataContext();
        List<coffee_table> lstable;
        public TableCheck()
        {
            lstable = new List<coffee_table>();
            InitializeComponent();
            loadTable();
        }
        CheckBox cb;
        public void loadTable()
        {

            //var tables = from t in db.coffee_tables
            //             where (from b in db.bookings
            //                    where !((b.start_date <=start_date && b.end_date>=start_date) || 
            //                    (b.start_date<=end_date && b.end_date>=end_date)||
            //                    b.start_date>=start_date && b.start_date<=end_date)
            //                    select b.table_id).Equals(t.id)
            //            select t;

            var tables = from t in db.coffee_tables
                                select t;

            flowLayoutPanel1.Controls.Clear();
            if (tables != null)
            {
                foreach (var item in tables)
                {
                    cb = new CheckBox();
                    cb.AutoSize = true;
                    cb.Location = new System.Drawing.Point(13, 13);
                    cb.Name = Convert.ToString(item.id);
                    cb.Size = new System.Drawing.Size(54, 17);
                    cb.TabIndex = 0;
                    cb.Text = "" + item.name;
                    cb.ForeColor = Color.White;
                    cb.UseVisualStyleBackColor = true;
                    flowLayoutPanel1.Controls.Add(cb);
                    cb.CheckedChanged += Cb_CheckedChanged;
                    cb.Tag = item;
                }
            }
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            coffee_table table = (sender as CheckBox).Tag as coffee_table;
            if (cb.Checked == true)
            {
                lstable.Add(table);
                labelName += Convert.ToString(table.name) + ", ";
            }
            else
            {
                lstable.Remove(table);
            }
          
          
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {
         
            BookingInfo.tables = lstable;
            BookingInfo.booking.loadTable();
            this.Dispose();
        }

        private void TableCheck_Load(object sender, EventArgs e)
        {

        }
    }
}
