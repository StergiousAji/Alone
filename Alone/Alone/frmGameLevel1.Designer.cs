namespace Alone
{
    partial class FrmGameLevel1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGameLevel1));
            this.lblScore = new System.Windows.Forms.Label();
            this.tmrRefreshRate = new System.Windows.Forms.Timer(this.components);
            this.lblGunName = new System.Windows.Forms.Label();
            this.prgsBrHealth = new System.Windows.Forms.ProgressBar();
            this.lblAmmo = new System.Windows.Forms.Label();
            this.picBxBackground = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBxBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("True Crimes", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.Black;
            this.lblScore.Location = new System.Drawing.Point(20, 31);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(141, 28);
            this.lblScore.TabIndex = 3;
            this.lblScore.Text = "Score: 0";
            // 
            // tmrRefreshRate
            // 
            this.tmrRefreshRate.Enabled = true;
            this.tmrRefreshRate.Interval = 1;
            this.tmrRefreshRate.Tick += new System.EventHandler(this.TmrRefreshRate_Tick);
            // 
            // lblGunName
            // 
            this.lblGunName.AutoSize = true;
            this.lblGunName.BackColor = System.Drawing.Color.Transparent;
            this.lblGunName.Font = new System.Drawing.Font("True Crimes", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGunName.Location = new System.Drawing.Point(618, 597);
            this.lblGunName.Name = "lblGunName";
            this.lblGunName.Size = new System.Drawing.Size(109, 14);
            this.lblGunName.TabIndex = 8;
            this.lblGunName.Text = "Desert Eagle";
            // 
            // prgsBrHealth
            // 
            this.prgsBrHealth.BackColor = System.Drawing.Color.LightGray;
            this.prgsBrHealth.ForeColor = System.Drawing.Color.ForestGreen;
            this.prgsBrHealth.Location = new System.Drawing.Point(72, 609);
            this.prgsBrHealth.Maximum = 120;
            this.prgsBrHealth.Name = "prgsBrHealth";
            this.prgsBrHealth.Size = new System.Drawing.Size(165, 23);
            this.prgsBrHealth.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgsBrHealth.TabIndex = 9;
            this.prgsBrHealth.Value = 120;
            // 
            // lblAmmo
            // 
            this.lblAmmo.AutoSize = true;
            this.lblAmmo.BackColor = System.Drawing.Color.Transparent;
            this.lblAmmo.Font = new System.Drawing.Font("True Crimes", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmmo.ForeColor = System.Drawing.Color.White;
            this.lblAmmo.Location = new System.Drawing.Point(633, 615);
            this.lblAmmo.Name = "lblAmmo";
            this.lblAmmo.Size = new System.Drawing.Size(77, 26);
            this.lblAmmo.TabIndex = 10;
            this.lblAmmo.Text = "7 / 3";
            // 
            // picBxBackground
            // 
            this.picBxBackground.BackColor = System.Drawing.Color.Transparent;
            this.picBxBackground.Image = global::Alone.Properties.Resources.Alone___Lvl_1_Form__Background_;
            this.picBxBackground.Location = new System.Drawing.Point(0, 0);
            this.picBxBackground.Name = "picBxBackground";
            this.picBxBackground.Size = new System.Drawing.Size(740, 660);
            this.picBxBackground.TabIndex = 7;
            this.picBxBackground.TabStop = false;
            // 
            // FrmGameLevel1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = global::Alone.Properties.Resources.Alone___Lvl_1_Form__Background_;
            this.ClientSize = new System.Drawing.Size(739, 656);
            this.Controls.Add(this.lblAmmo);
            this.Controls.Add(this.prgsBrHealth);
            this.Controls.Add(this.lblGunName);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.picBxBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmGameLevel1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alone | Level One - Central Park";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGameLevel1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmGameLevel1_KeyReleased);
            ((System.ComponentModel.ISupportInitialize)(this.picBxBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Timer tmrRefreshRate;
        private System.Windows.Forms.Label lblGunName;
        private System.Windows.Forms.ProgressBar prgsBrHealth;
        private System.Windows.Forms.Label lblAmmo;
        private System.Windows.Forms.PictureBox picBxBackground;
    }
}