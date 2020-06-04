using LiveCharts;
using LiveCharts.Wpf;
using project.Forms.FormChildsOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace project.Forms.FormChildsDashboard
{
    public partial class OrderPrice : Form
    {
        private Button currentBtn;
        private Form currentChildForm;
        DateTime Now = DateTime.Now;
        public OrderPrice()
        {
            InitializeComponent();
        }
        DataClasses1DataContext db = new DataClasses1DataContext();

        private void OrderPrice_Load(object sender, EventArgs e)
        {

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "doanh thu",
                LabelFormatter = x => x.ToString("C")
            }) ;
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
            
            loadDay();
            ActiveButton(button1, RBGColors.color1);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color1);
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Tháng",
                Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10" },
            });
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var order = (from o in db.order_many_tables
                         select new { Year = o.create_at.Value.Year }).Distinct();
            foreach (var item in order)
            {
                List<double> values = new List<double>();
                for (int i = 1; i <= 12; i++)
                {
                    double value = 0;
                    var data = from o in db.order_many_tables
                               where o.create_at.Value.Month == i && o.create_at.Value.Year == item.Year
                               orderby o.create_at.Value.Month ascending
                               select new { total = o.total_price, month = o.create_at.Value.Month };

                    foreach (var it in data)
                    {
                        value += Convert.ToDouble(it.total);
                    }

                    values.Add(value);



                }
                series.Add(new LineSeries() { Title = item.Year.ToString(), Values = new ChartValues<double>(values) });

            }
            cartesianChart1.Series = series;
            cartesianChart1.ForeColor = Color.White;
            var total = from o in db.order_many_tables
                        select o;
            double totalPrice = 0;
            foreach (var item in total)
            {
                totalPrice += Convert.ToDouble(item.total_price);
            }
            lblTotal.Text = totalPrice.ToString("#,###,###") + " VND";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color2);
            List<string> Days = new List<string>();
            for (int i = 1; i <= 31; i++)
            {
                Days.Add(Convert.ToString(i));
            }
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Ngày",
                Labels = Days
            });
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var order = (from o in db.order_many_tables
                         where o.create_at.Value.Year == Now.Year
                         select new { month = o.create_at.Value.Month,Year = Now.Year }).Distinct();
            foreach (var item in order)
            {
                List<double> values = new List<double>();
                for (int i = 1; i <= 31; i++)
                {
                    double value = 0;
                    var data = from o in db.order_many_tables
                               where o.create_at.Value.Day == i && o.create_at.Value.Month == item.month
                               && o.create_at.Value.Year == item.Year
                               orderby o.create_at.Value.Day ascending
                               select new { total = o.total_price, month = o.create_at.Value.Month };

                    foreach (var it in data)
                    {
                        value += Convert.ToDouble(it.total);
                    }

                    values.Add(value);



                }
                series.Add(new LineSeries() { Title = "Tháng "+item.month.ToString(), Values = new ChartValues<double>(values) });

            }
            cartesianChart1.Series = series;
            var total = from o in db.order_many_tables
                        where o.create_at.Value.Year == Now.Year
                        select o;
            double totalPrice = 0;
            foreach (var item in total)
            {
                totalPrice += Convert.ToDouble(item.total_price);
            }
            lblTotal.Text = totalPrice.ToString("#,###,###") + " VND";
        }
        private void loadDay()
        {
            List<string> Minutes = new List<string>();
            for (int i = 0; i <= 59; i++)
            {
                Minutes.Add(Convert.ToString(i));
            }
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Phút",
                Labels = Minutes
            });
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var order = (from o in db.order_many_tables
                         where o.create_at.Value.Day == Now.Day && o.create_at.Value.Month == Now.Month
                         && o.create_at.Value.Year == Now.Year
                         select new { Hour = o.create_at.Value.Hour, Date = Now.Date }).Distinct();
            foreach (var item in order)
            {
                List<double> values = new List<double>();
                for (int i = 0; i <= 59; i++)
                {
                    double value = 0;
                    var data = from o in db.order_many_tables
                               where o.create_at.Value.Minute == i && o.create_at.Value.Date == item.Date
                               && o.create_at.Value.Hour == item.Hour
                               orderby o.create_at.Value.Day ascending
                               select new { total = o.total_price, month = o.create_at.Value.Month };

                    foreach (var it in data)
                    {
                        value += Convert.ToDouble(it.total);
                    }

                    values.Add(value);



                }
                series.Add(new LineSeries() { Title = item.Hour.ToString() + " Giờ", Values = new ChartValues<double>(values)});
              
            }
            cartesianChart1.Series = series;
            var total = from o in db.order_many_tables
                        where o.create_at.Value.Day == Now.Day && o.create_at.Value.Month == Now.Month
                         && o.create_at.Value.Year == Now.Year
                        select o;
            double totalPrice = 0;
            foreach (var item in total)
            {
                totalPrice += Convert.ToDouble(item.total_price);
            }
            lblTotal.Text = totalPrice.ToString("#,###,###") + " VND";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color1);
            loadDay();
          
        }
    }
}
