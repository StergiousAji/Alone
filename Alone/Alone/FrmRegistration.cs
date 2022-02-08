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
    
    public partial class FrmRegistration : Form
    {
        //Instatiate new member.
        public static Member[] members = new Member[50]; 
        Member member = new Member();

        public FrmRegistration()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //Check again if all user input valid once Register button clicked.
            if (member.FirstName == txtFirstName.Text && member.LastName == txtLastName.Text && member.Username == txtUsername.Text && 
                member.DateOfBirth == dtpDateOfBirth.Value && member.Email == txtEmail.Text && member.Password == txtPassword.Text)
            {
                //Place any previously registered members into the array of members.
                member.ReadFromFile(member.Username);

                int i = 0;
                //Add current member to first null element in the array then break from loop.
                while (i < members.Length)
                {
                    if (members[i] == null)
                    {
                        members[i] = member;
                        break;
                    }
                    else
                        i++;
                }

                int numOfMembers = i + 1;
                member.WriteToFile(numOfMembers);

                //If the current user input equals the latest valid user input then place ticks beside each of the textboxes
                ep.Icon = Properties.Resources.Tick;
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox || control is DateTimePicker)
                        ep.SetError(control, "Valid");
                }
                MessageBox.Show("You have successfully created your account!" + Environment.NewLine + "You can now login to play the game.", 
                    $"Welcome {member.FirstName} {member.LastName}!");

                //Go to Login Page (Call click event).
                BtnLogin_Click(sender, e);
            }
            else
            {
                //Place error providers where necessary.
                CheckFirstName();
                CheckLastName();
                CheckUsername();
                CheckEmail();
                CheckDateOfBirth();
                CheckPassword();
            }
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            //Go to Main Menu.
            this.Close();
            Form MainMenu = new FrmMainMenu();
            MainMenu.Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            //Go to Login Page.
            this.Close();
            Form Login = new FrmLogin();
            Login.Show();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            //Go to Help Page
            UserControl HelpGuide = new UcHelpGuide();
            HelpGuide.Location = new Point(0, 0);
            this.Controls.Add(HelpGuide);
            HelpGuide.Dock = DockStyle.Fill;
            HelpGuide.BringToFront();
        }

        //Validate user input as they enter characters into each text box.
        private void TxtFirstName_TextChanged(object sender, EventArgs e)
        {
            CheckFirstName();
        }

        private void TxtLastName_TextChanged(object sender, EventArgs e)
        {
            CheckLastName();
        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            CheckUsername();
        }

        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {
            CheckEmail();
        }

        private void DtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            CheckDateOfBirth();
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            CheckPassword();
        }

        //Check and Validate User input (Methods).
        private void CheckFirstName()
        {
            try
            {
                member.FirstName = txtFirstName.Text;
            }
            catch (CustomException ex)
            {
                ep.Icon = Properties.Resources.Error;
                ep.SetError(txtFirstName, ex.Message);
            }
            finally
            {
                if (member.FirstName == txtFirstName.Text)
                    ep.SetError(txtFirstName, null);
            }
        }

        private void CheckLastName()
        {
            try
            {
                member.LastName = txtLastName.Text;
            }
            catch (CustomException ex)
            {
                ep.Icon = Properties.Resources.Error;
                ep.SetError(txtLastName, ex.Message);
            }
            finally
            {
                if (member.LastName == txtLastName.Text)
                    ep.SetError(txtLastName, null);
            }
        }

        public void CheckUsername()
        {
            try
            {
                member.Username = txtUsername.Text;
            }
            catch (CustomException ex)
            {
                ep.Icon = Properties.Resources.Error;
                ep.SetError(txtUsername, ex.Message);
            }
            finally
            {
                if (member.Username == txtUsername.Text)
                    ep.SetError(txtUsername, null);
            }
        }

        private void CheckEmail()
        {
            try
            {
                member.Email = txtEmail.Text;
            }
            catch (CustomException ex)
            {
                ep.Icon = Properties.Resources.Error;
                ep.SetError(txtEmail, ex.Message);
            }
            finally
            {
                if (member.Email == txtEmail.Text)
                    ep.SetError(txtEmail, null);
            }
        }

        private void CheckDateOfBirth()
        {
            try
            {
                member.DateOfBirth = dtpDateOfBirth.Value;
            }
            catch (CustomException ex)
            {
                ep.Icon = Properties.Resources.Error;
                ep.SetError(dtpDateOfBirth, ex.Message);
            }
            finally
            {
                if (member.DateOfBirth == dtpDateOfBirth.Value)
                    ep.SetError(dtpDateOfBirth, null);
            }
        }

        public void CheckPassword()
        {
            try
            {
                member.Password = txtPassword.Text;
            }
            catch (CustomException ex)
            {
                ep.Icon = Properties.Resources.Error;
                ep.SetError(txtPassword, ex.Message);
            }
            finally
            {
                if (member.Password == txtPassword.Text)
                    ep.SetError(txtPassword, null);
            }
        }
    }
}
