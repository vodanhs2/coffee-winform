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
    public partial class CheckPayment : Form
    {
        public static CheckPayment check;
        public double totalprice;
        List<order> orders = new List<order>();
        public CheckPayment(double totalprice, List<order> orders)
        {
            InitializeComponent();
            check = this;
            this.totalprice = totalprice;
            this.orders = orders;
        }
        public CheckPayment()
        {
            InitializeComponent();
        }
        private void btnOne_Click(object sender, EventArgs e)
        {
            Payment pay = new Payment(Convert.ToDouble(totalprice), orders);
            pay.ShowDialog();

        }

        private void BtnMany_Click(object sender, EventArgs e)
        {
            LsOrder ls = new LsOrder();
            ls.ShowDialog();

        }

        private void CheckPayment_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
