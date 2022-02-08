namespace Alone
{
    partial class FrmGameLevel2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGameLevel2));
            this.prgsBrHealth = new System.Windows.Forms.ProgressBar();
            this.tmrRefreshRate = new System.Windows.Forms.Timer(this.components);
            this.picBxBackground = new System.Windows.Forms.PictureBox();
            this.lblGunName = new System.Windows.Forms.Label();
            this.lblAmmo = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBxBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // prgsBrHealth
            // 
            this.prgsBrHealth.BackColor = System.Drawing.Color.LightGray;
            this.prgsBrHealth.ForeColor = System.Drawing.Color.ForestGreen;
            this.prgsBrHealth.Location = new System.Drawing.Point(72, 672);
            this.prgsBrHealth.Maximum = 120;
            this.prgsBrHealth.Name = "prgsBrHealth";
            this.prgsBrHealth.Size = new System.Drawing.Size(165, 23);
            this.prgsBrHealth.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgsBrHealth.TabIndex = 13;
            this.prgsBrHealth.Value = 120;
            // 
            // tmrRefreshRate
            // 
            this.tmrRefreshRate.Enabled = true;
            this.tmrRefreshRate.Interval = 50;
            this.tmrRefreshRate.Tick += new System.EventHandler(this.TmrRefreshRate_Tick);
            // 
            // picBxBackground
            // 
            this.picBxBackground.Image = global::Alone.Properties.Resources.Alone___Lvl_2_Form__Background_;
            this.picBxBackground.Location = new System.Drawing.Point(0, 0);
            this.picBxBackground.Name = "picBxBackground";
            this.picBxBackground.Size = new System.Drawing.Size(850, 720);
            this.picBxBackground.TabIndex = 14;
            this.picBxBackground.TabStop = false;
            // 
            // lblGunName
            // 
            this.lblGunName.AutoSize = true;
            this.lblGunName.BackColor = System.Drawing.Color.Transparent;
            this.lblGunName.Font = new System.Drawing.Font("True Crimes", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGunName.ForeColor = System.Drawing.Color.Black;
            this.lblGunName.Location = new System.Drawing.Point(758, 659);
            this.lblGunName.Name = "lblGunName";
            this.lblGunName.Size = new System.Drawing.Size(48, 14);
            this.lblGunName.TabIndex = 15;
            this.lblGunName.Text = "Nova";
            // 
            // lblAmmo
            // 
            this.lblAmmo.AutoSize = true;
            this.lblAmmo.BackColor = System.Drawing.Color.Transparent;
            this.lblAmmo.Font = new System.Drawing.Font("True Crimes", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmmo.ForeColor = System.Drawing.Color.White;
            this.lblAmmo.Location = new System.Drawing.Point(734, 678);
            this.lblAmmo.Name = "lblAmmo";
            this.lblAmmo.Size = new System.Drawing.Size(91, 26);
            this.lblAmmo.TabIndex = 16;
            this.lblAmmo.Text = "12 / 8";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("True Crimes", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.Black;
            this.lblScore.Location = new System.Drawing.Point(22, 31);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(141, 28);
            this.lblScore.TabIndex = 17;
            this.lblScore.Text = "Score: 0";
            // 
            // FrmGameLevel2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::Alone.Properties.Resources.Alone___Lvl_2_Form__Background_;
            this.ClientSize = new System.Drawing.Size(849, 721);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblGunName);
            this.Controls.Add(this.lblAmmo);
            this.Controls.Add(this.prgsBrHealth);
            this.Controls.Add(this.picBxBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmGameLevel2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alone | Level Two - The Junction";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGameLevel2_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmGameLevel2_KeyReleased);
            ((System.ComponentModel.ISupportInitialize)(this.picBxBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar prgsBrHealth;
        private System.Windows.Forms.Timer tmrRefreshRate;
        private System.Windows.Forms.PictureBox picBxBackground;
        private System.Windows.Forms.Label lblGunName;
        private System.Windows.Forms.Label lblAmmo;
        private System.Windows.Forms.Label lblScore;
    }
}