using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Alone
{
    public partial class FrmMainMenu : Form
    {
        public static bool loggedIn = false;
        Button btnLevel1;
        Button btnLevel2;
        Button btnHighScores;
        Button btnBack;

        public FrmMainMenu()
        {
            InitializeComponent();

            //Play Main Menu Music
            SoundPlayer backgroundMusic = new SoundPlayer(Properties.Resources.Malo_Garcia___March_of_Progress);
            backgroundMusic.PlayLooping();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //Go to Registration Page.
            this.Hide();
            Form Registration = new FrmRegistration();
            Registration.Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            //Go to Login Page.
            this.Hide();
            Form Login = new FrmLogin();
            Login.Show();
        }

        public void BtnPlay_Click(object sender, EventArgs e)
        {
            //Only execute game level select screen if the user is logged in.
            DialogResult answer = DialogResult.No;
            if (!loggedIn)
                answer = MessageBox.Show("You are currently not logged in, would you like to continue as a guest?", "Play as Guest", MessageBoxButtons.YesNo);

            if (loggedIn || answer == DialogResult.Yes)
            {
                this.Controls.Clear();
                this.Text = "Game Level Select";

                btnLevel1 = new Button();
                btnLevel1.Text = "Level One - Central Park";
                btnLevel1.Font = new Font("Rockwell", 18);
                btnLevel1.FlatStyle = FlatStyle.Flat;
                btnLevel1.Cursor = Cursors.Hand;
                btnLevel1.Location = new Point(35, 245);
                btnLevel1.Size = new Size(270, 70);
                btnLevel1.BackColor = Color.SeaGreen;
                btnLevel1.Click += new EventHandler(BtnLevel1_Click);
                btnLevel1.MouseHover += new EventHandler(BtnLevel1_MouseHover);
                this.Controls.Add(btnLevel1);

                btnLevel2 = new Button();
                btnLevel2.Text = "Level Two - The Junction";
                btnLevel2.Font = new Font("Rockwell", 18);
                btnLevel2.FlatStyle = FlatStyle.Flat;
                btnLevel2.Cursor = Cursors.Hand;
                btnLevel2.Location = new Point(35, 380);
                btnLevel2.Size = new Size(270, 70);
                btnLevel2.BackColor = Color.Peru;
                btnLevel2.Click += new EventHandler(BtnLevel2_Click);
                btnLevel2.MouseHover += new EventHandler(BtnLevel2_MouseHover);
                this.Controls.Add(btnLevel2);

                btnHighScores = new Button();
                btnHighScores.Text = "High Scores";
                btnHighScores.Font = new Font("Rockwell", 15);
                btnHighScores.FlatStyle = FlatStyle.Flat;
                btnHighScores.Cursor = Cursors.Hand;
                btnHighScores.Location = new Point(250, 530);
                btnHighScores.Size = new Size(90, 60);
                btnHighScores.BackColor = Color.Teal;
                btnHighScores.Click += new EventHandler(BtnHighScores_Click);
                this.Controls.Add(btnHighScores);

                btnBack = new Button();
                btnBack.Text = "Back";
                btnBack.Font = new Font("Rockwell", 15);
                btnBack.FlatStyle = FlatStyle.Flat;
                btnBack.Cursor = Cursors.Hand;
                btnBack.Location = new Point(10, 555);
                btnBack.Size = new Size(80, 35);
                btnBack.BackColor = Color.Firebrick;
                btnBack.Click += new EventHandler(BtnBack_Click);
                this.Controls.Add(btnBack);
            }
        }

        private void BtnPlay_MouseHover(object sender, EventArgs e)
        {
            //Check if user logged in and show appropriate tool tip.
            if (!loggedIn)
            {
                ToolTip btnPlayInfo = new ToolTip();
                btnPlayInfo.SetToolTip(btnPlay, "You are not logged in but you can play as a guest.");
            }
        }

        private void BtnLevel1_Click(object sender, EventArgs e)
        {
            //Go to Level 1
            this.Hide();
            Form GameLevel1 = new FrmGameLevel1();
            GameLevel1.Show();
        }

        private void BtnLevel1_MouseHover(object sender, EventArgs e)
        {
            //Description of Game Level One.
            ToolTip btnLevel1Info = new ToolTip();
            btnLevel1Info.SetToolTip(btnLevel1, "Beginner Level: Enemies are slow, low in quantity and deal less damage.");
        }

        private void BtnLevel2_Click(object sender, EventArgs e)
        {
            //Go to Level 2
            this.Hide();
            Form GameLevel2 = new FrmGameLevel2();
            GameLevel2.Show();
        }

        private void BtnLevel2_MouseHover(object sender, EventArgs e)
        {
            //Description of Game Level Two.
            ToolTip btnLevel2Info = new ToolTip();
            btnLevel2Info.SetToolTip(btnLevel2, "Expert Level: Enemies are fast, hordes appear at a time and deal more damage.");
        }

        private void BtnHighScores_Click(object sender, EventArgs e)
        {
            //Go to High Scores Table.
            this.Hide();
            Form HighScores = new FrmHighScores();
            HighScores.Show();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            //Go Back to Main Menu with enabled play button.
            this.Hide();
            FrmMainMenu MainMenu = new FrmMainMenu();
            MainMenu.btnPlay.BackColor = Color.ForestGreen;
            MainMenu.btnPlay.ForeColor = Color.Black;
            MainMenu.Show();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            //Open Help Page.
            UserControl HelpGuide = new UcHelpGuide();
            HelpGuide.Location = new Point(0, 0);
            this.Controls.Add(HelpGuide);
            HelpGuide.BringToFront();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } 
    }
}
