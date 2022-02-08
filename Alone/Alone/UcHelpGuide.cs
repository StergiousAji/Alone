using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Alone
{
    public partial class UcHelpGuide : UserControl
    {
        public UcHelpGuide()
        {
            InitializeComponent();

            string[] helpGuide = File.ReadAllLines("HelpGuide.txt");

            for (int i = 0; i < helpGuide.Length; i++)
            {
                rTxtHelpGuide.Text += helpGuide[i];
                rTxtHelpGuide.Text += "\n";
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
