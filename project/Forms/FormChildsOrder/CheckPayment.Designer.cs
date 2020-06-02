namespace project.Forms.FormChildsOrder
{
    partial class CheckPayment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOne = new FontAwesome.Sharp.IconButton();
            this.BtnMany = new FontAwesome.Sharp.IconButton();
            this.btnBack = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // btnOne
            // 
            this.btnOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOne.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnOne.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOne.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOne.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnOne.IconColor = System.Drawing.Color.Black;
            this.btnOne.IconSize = 16;
            this.btnOne.Location = new System.Drawing.Point(26, 50);
            this.btnOne.Name = "btnOne";
            this.btnOne.Rotation = 0D;
            this.btnOne.Size = new System.Drawing.Size(191, 43);
            this.btnOne.TabIndex = 5;
            this.btnOne.Text = "Thanh toán bàn này";
            this.btnOne.UseVisualStyleBackColor = false;
            this.btnOne.Click += new System.EventHandler(this.btnOne_Click);
            // 
            // BtnMany
            // 
            this.BtnMany.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnMany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMany.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.BtnMany.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMany.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.BtnMany.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtnMany.IconColor = System.Drawing.Color.Black;
            this.BtnMany.IconSize = 16;
            this.BtnMany.Location = new System.Drawing.Point(237, 50);
            this.BtnMany.Name = "BtnMany";
            this.BtnMany.Rotation = 0D;
            this.BtnMany.Size = new System.Drawing.Size(192, 43);
            this.BtnMany.TabIndex = 5;
            this.BtnMany.Text = "Thanh toán nhiều bàn";
            this.BtnMany.UseVisualStyleBackColor = false;
            this.BtnMany.Click += new System.EventHandler(this.BtnMany_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBack.IconChar = FontAwesome.Sharp.IconChar.Backward;
            this.btnBack.IconColor = System.Drawing.Color.White;
            this.btnBack.IconSize = 25;
            this.btnBack.Location = new System.Drawing.Point(173, 117);
            this.btnBack.Name = "btnBack";
            this.btnBack.Rotation = 0D;
            this.btnBack.Size = new System.Drawing.Size(91, 36);
            this.btnBack.TabIndex = 6;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // CheckPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(440, 165);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.BtnMany);
            this.Controls.Add(this.btnOne);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "CheckPayment";
            this.Text = "CheckPayment";
            this.Load += new System.EventHandler(this.CheckPayment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnOne;
        private FontAwesome.Sharp.IconButton BtnMany;
        private FontAwesome.Sharp.IconButton btnBack;
    }
}