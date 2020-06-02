using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class login : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public login()
        {
            InitializeComponent();

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPass.Text;
            admin ad = db.admins.SingleOrDefault(x => x.username == username && x.password == password);
            employee em = db.employees.SingleOrDefault(x => x.username == username && x.password == password);
            if (ad != null || em != null)
            {
                if (ad != null)
                {
                    this.Hide();
                    Program.ad = ad;
                    PnlShadow shadow = new PnlShadow();
                    shadow.Show();
                }
                if (em != null)
                {
                    Program.em = em;
                    this.Hide();
                    PnlShadow shadow = new PnlShadow();
                    shadow.Show();
                }

            }
            else
            {
                MessageBox.Show("tài khoản hoặc mật khẩu không chính xác!");
            }

        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
