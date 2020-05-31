using FontAwesome.Sharp;
using project.Forms;
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
    public partial class PnlShadow : Form
    {
        private IconButton currentBtn;
        private Form currentChildForm;
        public PnlShadow()
        {
            InitializeComponent();
        }

        bool mnuExpanded = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!bunifuTransition1.IsCompleted) return;
            if (pnlMainMenu.ClientRectangle.Contains(PointToClient(Control.MousePosition)))
            {
                if (!mnuExpanded)
                {
                    mnuExpanded = true;
                    pnlMainMenu.Visible = true;
                    pnlMainMenu.Width = 162;
                    bunifuTransition1.Show(pnlMainMenu);
                }
            }
            else
            {
                if (mnuExpanded)
                {
                    mnuExpanded = false;
                    pnlMainMenu.Visible = false;
                    pnlMainMenu.Width = 37;
                    bunifuTransition1.Show(pnlMainMenu);
                }
            }
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
                iconCurrentChild.IconChar = currentBtn.IconChar;
                iconCurrentChild.IconColor = color;

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

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
        private void OpenChildForm(Form childForm)
        {
        if(currentChildForm != null)
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
            lbHome.Text = childForm.Text;   
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color1);
            OpenChildForm(new Dashboard());

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color2);
            OpenChildForm(new Orders());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color3);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color4);
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color5);
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RBGColors.color6);
            OpenChildForm(new Settings());
        }
        private void Reset()
        {
            DisableButton();
            iconCurrentChild.IconChar = IconChar.Home;
            iconCurrentChild.IconColor = Color.MediumPurple;
            lbHome.Text = "Home";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }
    }
}
