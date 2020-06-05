using CrystalDecisions.CrystalReports.Engine;
using project.Forms.FormChildsOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Forms.Booking
{
    public partial class BookingInfo : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public static BookingInfo booking;
        public static List<coffee_table> tables;
        private string nameTable;
        List<coffee_table> lstable_empty ;
        public BookingInfo()
        {
            booking = this;
            InitializeComponent();
            lstable_empty = new List<coffee_table>();
            loadTable();
        }
        public void loadTable()
        {
            nameTable = null;
            if (tables != null)
            {
                foreach (var item in tables)
                {
                    nameTable += item.name + ", ";
                }
              
                txtBan.Text = nameTable;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TableCheck tableCheck = new TableCheck();
         
            tableCheck.ShowDialog();
        }
       

       
        private void button2_Click(object sender, EventArgs e)
        {
            //yyyy-M-dd HH:mm
            DateTime start_date = (DateTime) dateTimePicker1.Value;
            DateTime end_date = (DateTime) dateTimePicker2.Value;
          
            List<string> error = new List<string>();
            if (tables != null)
            {
                foreach (var item in tables)
                {
                    var table_empty = from t in db.coffee_tables
                                 where (from b in db.bookings
                                        where ((b.start_date <= start_date && b.end_date >= start_date) ||
                                        (b.start_date <= end_date && b.end_date >= end_date) ||
                                         b.start_date >= start_date && b.start_date <= end_date)
                                        select b.table_id).Contains(t.id)
                                        where t.id == item.id
                                        where t.status != 1
                                      select t;

                   
                        if (table_empty.Count()>0)
                        {
                            error.Add(item.name);
                        }
                        else
                        {
                        if (error.Count()>0)
                        {
                            error.Remove(item.name);
                        }
                            lstable_empty.Add(item);

                        }
                    

                }
            }
            if (error.Count > 0)
            {
                string n = null;
                foreach (var item in error)
                {
                    n += item + ", ";
                }
                MessageBox.Show(""+n+" đã có người đặt hoặc đang sử dụng !");
            }
            if (ValidateChildren(ValidationConstraints.Enabled) && error.Count==0)
            {
                MessageBox.Show("Đăng ký đặt bàn thành công!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                customer c = new customer();
                c.email = txtEmail.Text;
                c.name = txtName.Text;
                c.phone = txtPhone.Text;
                c.create_at = DateTime.Now;
                c.quantity = Convert.ToInt32(numericUpDown1.Value);
                c.status = 0;
                db.customers.InsertOnSubmit(c);
                db.SubmitChanges();
                db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.customers);
                var cus = from ca in db.customers
                          orderby ca.id descending
                          select ca;
                foreach (var item in lstable_empty)
                {
                    booking b = new booking();
                    b.start_date = start_date;
                    b.end_date = end_date;
                    b.table_id = item.id;
                    b.cus_id = cus.First().id;
                    db.bookings.InsertOnSubmit(b);
                    db.SubmitChanges();

                }
                showView();
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                e.Cancel = true;
                txtName.Focus();
                errorProvider1.SetError(txtName, "vui lòng nhập tên khách hàng !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtName,null);
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
            if (r.IsMatch(txtPhone.Text)==false)
            {
                e.Cancel = true;
                txtName.Focus();
                errorProvider1.SetError(txtPhone, "Số điện thoại không hợp lệ !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPhone, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (r.IsMatch(txtEmail.Text) == false)
            {
                e.Cancel = true;
                txtName.Focus();
                errorProvider1.SetError(txtEmail, "email không hợp lệ !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void numericUpDown1_Validating(object sender, CancelEventArgs e)
        {
            if (numericUpDown1.Value <=0)
            {
                e.Cancel = true;
                txtName.Focus();
                errorProvider1.SetError(numericUpDown1, "số người phải lớn hơn 0 !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(numericUpDown1, null);
            }
        }

        private void txtBan_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtBan.Text))
            {
                e.Cancel = true;
                txtName.Focus();
                errorProvider1.SetError(txtBan, "vui lòng chọn bàn !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtBan, null);
            }
        }

        private void BookingInfo_Load(object sender, EventArgs e)
        {
            showView();
        }
        private void showView()
        {
            var show = from p in db.bookings
                       select new
                       {
                           Id = p.customer.id,
                           Name = p.customer.name,
                           Phone = p.customer.phone,
                           Email = p.customer.email,
                           Quantity = p.customer.quantity,
                           Table = p.coffee_table.name,
                           Start_Date = p.start_date,
                           EndDate = p.end_date
                       };
            dataGridView1.DataSource = show;
        }
    }
}
