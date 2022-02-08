using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alone
{
    public partial class FrmLogin : Form
    {
        //Instantiate member
        public static Member currentMember = new Member();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void TxtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnLogin_Click(new object(), new EventArgs());
        }

        private void TxtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnLogin_Click(new object(), new EventArgs());
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            //If textboxes are not empty then check text within.
            if ((!(String.IsNullOrEmpty(txtUsername.Text))) || (!(String.IsNullOrEmpty(txtPassword.Text))))
            {
                currentMember.ReadFromFile(txtUsername.Text);
                foreach (Member m in FrmRegistration.members)
                {
                    //If a null space is reached in the members array then break as the rest of the array is also empty spaces.
                    if (m == null)
                        break;

                    if (m.Username == (txtUsername.Text).Trim())
                    {
                        //Member Found
                        currentMember = m;
                        //Nullify (Remove) error, in case error has been set before.
                        ep.SetError(txtUsername, null);
                        break;
                    }
                    else
                        ep.SetError(txtUsername, "This username does not exist.");
                }

                //Check if the password entered matches the member's corresponding username in the database. If so then user has successfully logged in.
                if (currentMember.Password == (txtPassword.Text).Trim())
                {
                    ep.Icon = Properties.Resources.Tick;
                    foreach (Control control in this.Controls)
                    {
                        if (control is TextBox)
                            ep.SetError(control, "Match Found");
                    }
                    MessageBox.Show("You have successfully logged in!" + Environment.NewLine + "You can now play the game.",
                        $"Welcome {currentMember.FirstName} {currentMember.LastName}!");

                    //Go to Main Menu and change global loggedIn boolean to true so play button will be enabled.
                    this.Close();
                    FrmMainMenu.loggedIn = true;
                    BtnHome_Click(sender, e);
                }
                else
                    ep.SetError(txtPassword, "This password is incorrect.");
            }

            //If text boxes are empty then ERROR.
            if (String.IsNullOrEmpty(txtUsername.Text))
                ep.SetError(txtUsername, "Please enter your username to login.");

            if (String.IsNullOrEmpty(txtPassword.Text))
                ep.SetError(txtPassword, "Please enter your password to login.");
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            //Go to Main Menu.
            this.Close();
            Form MainMenu = new FrmMainMenu();
            MainMenu.Show();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //Go to Registration Page.
            this.Close();
            Form Registration = new FrmRegistration();
            Registration.Show();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            //Go to Help Page.
            UserControl HelpGuide = new UcHelpGuide();
            HelpGuide.Location = new Point(0, 0);
            this.Controls.Add(HelpGuide);
            HelpGuide.Dock = DockStyle.Fill;
            HelpGuide.BringToFront();
        }
    }
}
