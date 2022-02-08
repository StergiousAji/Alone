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
    public partial class FrmHighScores : Form
    {
        int[] highScores;
        int[] topTenHighScores;
        string[] usernames;
        Label[,] lblInfo;

        public FrmHighScores()
        {
            InitializeComponent();

            //Instantiate highScores array to hold every member's highest score.
            highScores = new int[FrmRegistration.members.Length];
            for (int i = 0; i < highScores.Length; i++)
            {
                //If a null space is reached in the members array then break as the rest of the array is also empty spaces.
                if (FrmRegistration.members[i] == null)
                    break;

                highScores[i] = FrmRegistration.members[i].HighScore;
            }

            //Sort the high scores in ascending order and then reverse to sort in descending order.
            Array.Sort(highScores);
            Array.Reverse(highScores);

            //Only the top ten high scores are wanted.
            topTenHighScores = new int[10];
            Array.Copy(highScores, topTenHighScores, 10);
            
            //Instantiate 2-Dimensional label array to hold 10 usernames and their corresponding high score.
            lblInfo = new Label[10, 10];
            
            usernames = new string[10];
            //First for loop to loop through the elements of the usernames array.
            for (int i = 0; i < usernames.Length; i++)
            {
                //Second for loop to loop through the elements of the members array.
                for (int j = 0; j < FrmRegistration.members.Length; j++)
                {
                    //If a null space is reached in the members array then break as the rest of the array is also empty spaces.
                    if (FrmRegistration.members[j] == null)
                        break;

                    //If a member's high score matches one of the top ten high score, then store it in the usernames array.
                    //The usernames will be in order with the top 10 high scores.
                    if (FrmRegistration.members[j].HighScore == topTenHighScores[i])
                    {
                        //To prevent usernames being repeated, the loop will continue if the usernames array already contains the current username found.
                        if (usernames.Contains(FrmRegistration.members[j].Username))
                            continue;

                        usernames[i] = FrmRegistration.members[j].Username;
                    }
                }
            }

            PopulateLblInfo();

            //For loop to change the text of the current logged in member's username (if present) and score to bold on the table. [Highlight current user]
            for (int i = 0; i < lblInfo.GetLength(0); i++)
            {
                if (FrmLogin.currentMember.Username == lblInfo[0, i].Text)
                {
                    lblInfo[0, i].Font = new Font("Palatino Linotype", 18, FontStyle.Bold);
                    lblInfo[1, i].Font = new Font("Palatino Linotype", 18, FontStyle.Bold);
                    lblInfo[0, i].ForeColor = Color.White;
                    lblInfo[1, i].ForeColor = Color.White;
                }
            }

            DisplayInfo();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            //Return to Game Level Select Screen.
            this.Close();
            FrmMainMenu MainMenu = new FrmMainMenu();
            MainMenu.Show();
            //Call Play Button Click Event to open Game Level Select Screen.
            MainMenu.BtnPlay_Click(sender, e);
        }

        private void PopulateLblInfo()
        {
            for (int i = 0; i < lblInfo.GetLength(0); i++)
            {
                //Instantiate the 10 labels in the first element of the array and set its text to the corresponding username.
                lblInfo[0, i] = new Label();
                lblInfo[0, i].Text = usernames[i];
                lblInfo[0, i].Font = new Font("Palatino Linotype", 16);
                lblInfo[0, i].AutoSize = true;
                lblInfo[0, i].Anchor = AnchorStyles.Left;

                //Instantiate the 10 labels in the second element of the array and set its text to the corresponding high score, keeping it in descending order.
                lblInfo[1, i] = new Label();
                lblInfo[1, i].Text = topTenHighScores[i].ToString();
                lblInfo[1, i].Font = new Font("Palatino Linotype", 16);
                lblInfo[1, i].AutoSize = true;
                lblInfo[1, i].Anchor = AnchorStyles.None;
            }
        }

        private void DisplayInfo()
        {
            /*For loop to position the username labels row by row onto the second column of the table and 
            the top 10 high score labels row by row onto the last column of the table.*/
            int n = 0;
            for (int i = 1; i <= 10; i++)
            {
                //If an index of the usernames array is found to be null, then break as there are no more usernames to output (Rest of array is empty).
                if (usernames[n] == null)
                    break;

                tblHighScores.Controls.Add(lblInfo[0, n], 1, i);
                tblHighScores.Controls.Add(lblInfo[1, n], 2, i);
                n++;
            }
        }
    }
}
