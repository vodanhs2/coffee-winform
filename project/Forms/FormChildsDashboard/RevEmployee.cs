using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Forms.FormChildsDashboard
{
    public partial class RevEmployee : Form
    {
        private Button currentBtn;
        DateTime Now = DateTime.Now;
        DataClasses1DataContext db = new DataClasses1DataContext();
        public RevEmployee()
        {
            InitializeComponent();
        }
        private void RevEmployee_Load(object sender, EventArgs e)
        {
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "doanh thu",
                LabelFormatter = x => x.ToString("C")
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;

            loadDay();
            ActiveButton(button1, RBGColors.color1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color2);
            loadDay();
        }
        private struct RBGColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        private void ActiveButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (Button)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                //icon Current child
                //iconCurrentChild.IconChar = currentBtn.IconChar;
                //iconCurrentChild.IconColor = color;

            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {

                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.White;

            }
        }
        private void loadDay()
        {
            List<string> Hours = new List<string>();
            for (int i = 0; i <= 23; i++)
            {
                Hours.Add(Convert.ToString(i + " - " + (i + 1) +" Giờ"));
            }
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Giờ",
                Labels = Hours
            });
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var order = (from o in db.order_many_tables
                         join em in db.employees on o.emp_id equals em.id
                         where o.create_at.Value.Date == Now.Date
                         where o.emp_id != null
                         select new { Date = Now.Date,emp_id=o.emp_id, Name = em.name}).Distinct();
          
            foreach (var item in order)
            {   
                List<double> values = new List<double>();
                for (int i = 0; i <= 23; i++)
                {
                    double value = 0;
                    var data = from o in db.order_many_tables
                               join em in db.employees on o.emp_id equals em.id
                               where o.create_at.Value.Hour ==i 
                               && o.create_at.Value.Date== item.Date
                               && o.emp_id == item.emp_id
                               orderby o.emp_id ascending
                               select new { total = o.total_price, name = em.name };
                  
                    foreach (var it in data)
                    {
                        value += Convert.ToDouble(it.total);
                    }
                    values.Add(value);
                }
                
               
                series.Add(new LineSeries() { Title = item.Name.ToString() + "", Values = new ChartValues<double>(values) });
                
            }
            cartesianChart1.Series = series;

            var totalPrice = from o in db.order_many_tables
                              where o.create_at.Value.Date == Now.Date
                              select o;
            double total = 0;
            foreach (var item in totalPrice)
            {
                total += Convert.ToDouble(item.total_price);
            }
            lblTotal.Text = Convert.ToString(total.ToString("#,###,###")+ " VND");
            var min = db.GetTotalByEmpDAY().Min(x=>x.total);
            LblMinPrice.Text = Convert.ToString(min);
           
            var max = db.GetTotalByEmpDAY().Max(x => x.total);
            LblMaxPrice.Text = Convert.ToString(max);
            string Namemax = "";
            string NameMin = "";
            foreach (var item in db.GetTotalByEmpDAY())
            {
                if (item.total==max)
                {
                    Namemax += item.username+", ";
                }
                if (item.total == min)
                {
                    NameMin += item.username + ", ";
                }
                
            }
            lblEmNameMax.Text = Namemax;
            lblEmNameMin.Text = NameMin;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color2);
            List<string> Months = new List<string>();
            for (int i = 1; i <= 31; i++)
            {
                Months.Add(Convert.ToString("Ngày " + i));
            }
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Ngày",
                Labels = Months
            });
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var order = (from o in db.order_many_tables
                         join em in db.employees on o.emp_id equals em.id
                         where o.create_at.Value.Month == Now.Month
                         where o.create_at.Value.Year == Now.Year
                         where o.emp_id != null
                         select new { Month = o.create_at.Value.Month, emp_id = o.emp_id, Name = em.name }).Distinct();

            foreach (var item in order)
            {
                List<double> values = new List<double>();
                for (int i = 1; i <= 31; i++)
                {
                    double value = 0;
                    var data = from o in db.order_many_tables
                               join em in db.employees on o.emp_id equals em.id
                               where o.create_at.Value.Day == i
                               && o.create_at.Value.Month == item.Month
                               && o.emp_id == item.emp_id
                               orderby o.emp_id ascending
                               select new { total = o.total_price, name = em.name };

                    foreach (var it in data)
                    {
                        value += Convert.ToDouble(it.total);
                    }
                    values.Add(value);
                }


                series.Add(new LineSeries() { Title = item.Name.ToString() + "", Values = new ChartValues<double>(values) });

            }
            cartesianChart1.Series = series;

            var totalPrice = from o in db.order_many_tables
                             where o.create_at.Value.Month == Now.Month
                             select o;
            double total = 0;
            foreach (var item in totalPrice)
            {
                total += Convert.ToDouble(item.total_price);
            }
            lblTotal.Text = Convert.ToString(total.ToString("#,###,###") + " VND");
            var min = db.GetTotalByEmpMonth().Min(x => x.total);
            LblMinPrice.Text = Convert.ToString(min);

            var max = db.GetTotalByEmpMonth().Max(x => x.total);
            LblMaxPrice.Text = Convert.ToString(max);
            string Namemax = "";
            string NameMin = "";
            foreach (var item in db.GetTotalByEmpMonth())
            {
                if (item.total == max)
                {
                    Namemax += item.username + ", ";
                }
                if (item.total == min)
                {
                    NameMin += item.username + ", ";
                }

            }
            lblEmNameMax.Text = Namemax;
            lblEmNameMin.Text = NameMin;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color3);
            List<string> Months = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                Months.Add(Convert.ToString("Tháng " + i));
            }
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Tháng",
                Labels = Months
            });
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var order = (from o in db.order_many_tables
                         join em in db.employees on o.emp_id equals em.id
                         where o.create_at.Value.Year == Now.Year
                         where o.emp_id != null
                         select new { emp_id = o.emp_id, Name = em.name }).Distinct();

            foreach (var item in order)
            {
                List<double> values = new List<double>();
                for (int i = 1; i <= 12; i++)
                {
                    double value = 0;
                    var data = from o in db.order_many_tables
                               join em in db.employees on o.emp_id equals em.id
                               where o.create_at.Value.Month == i
                               && o.emp_id == item.emp_id
                               orderby o.emp_id ascending
                               select new { total = o.total_price, name = em.name };

                    foreach (var it in data)
                    {
                        value += Convert.ToDouble(it.total);
                    }
                    values.Add(value);
                }


                series.Add(new LineSeries() { Title = item.Name.ToString() + "", Values = new ChartValues<double>(values) });

            }
            cartesianChart1.Series = series;

            var totalPrice = from o in db.order_many_tables
                             where o.create_at.Value.Year == Now.Year
                             select o;
            double total = 0;
            foreach (var item in totalPrice)
            {
                total += Convert.ToDouble(item.total_price);
            }
            lblTotal.Text = Convert.ToString(total.ToString("#,###,###") + " VND");
            var min = db.GetTotalByEmpYear().Min(x => x.total);
            LblMinPrice.Text = Convert.ToString(min);

            var max = db.GetTotalByEmpYear().Max(x => x.total);
            LblMaxPrice.Text = Convert.ToString(max);
            string Namemax = "";
            string NameMin = "";
            foreach (var item in db.GetTotalByEmpYear())
            {
                if (item.total == max)
                {
                    Namemax += item.username + ", ";
                }
                if (item.total == min)
                {
                    NameMin += item.username + ", ";
                }

            }
            lblEmNameMax.Text = Namemax;
            lblEmNameMin.Text = NameMin;
        }
    }
}
