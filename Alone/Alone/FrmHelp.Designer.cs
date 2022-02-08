namespace Alone
{
    partial class FrmHelpGuide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelpGuide));
            this.rTxtHelpGuide = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rTxtHelpGuide
            // 
            this.rTxtHelpGuide.BackColor = System.Drawing.Color.White;
            this.rTxtHelpGuide.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTxtHelpGuide.ForeColor = System.Drawing.Color.Black;
            this.rTxtHelpGuide.Location = new System.Drawing.Point(38, 16);
            this.rTxtHelpGuide.Name = "rTxtHelpGuide";
            this.rTxtHelpGuide.ReadOnly = true;
            this.rTxtHelpGuide.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rTxtHelpGuide.Size = new System.Drawing.Size(620, 517);
            this.rTxtHelpGuide.TabIndex = 0;
            this.rTxtHelpGuide.Text = "";
            // 
            // FrmHelpGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.rTxtHelpGuide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmHelpGuide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help Guide";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rTxtHelpGuide;
    }
}