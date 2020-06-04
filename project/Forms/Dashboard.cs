using FontAwesome.Sharp;
using project.Forms.FormChildsDashboard;
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
    public partial class Dashboard : Form
    {
        private IconButton currentBtn;
        private Form currentChildForm;
        public Dashboard()
        {
            InitializeComponent();
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
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.IconColor = color;
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
                currentBtn.IconColor = Color.White;
            }
        }
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //open only form
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            PnlShadow.nameTitle = "Dashboarch / "+ childForm.Text;
            PnlShadow.pnlshadow.LoadTitle();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color1);
            OpenChildForm(new OrderPrice());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color2);
            OpenChildForm(new RevEmployee());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color3);
            OpenChildForm(new Dashboard());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color4);
            OpenChildForm(new Dashboard());
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
