using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alone
{
    class Player
    {
        private int speed, health, clip, ammoPouch, max_clipSize;
        //The direction the player is facing. This will help with firing the weapon.
        private char direction;

        //Display
        public PictureBox picBxPlayer;
        private int gameLevel;

        //Movement
        public bool up, left, down, right;

        //Reloading
        public ProgressBar prgsBrReloadBar;
        public Timer tmrReloadTime;
        public bool reload;

        //Constructors
        public Player() {}

        public Player(int s, int c, int aP, int m_cS, Point start, int gL, Form frm)
        {
            speed = s;
            health = 120;
            clip = c;
            ammoPouch = aP;
            max_clipSize = m_cS;
            direction = 'r';

            //Create player picture box.
            picBxPlayer = new PictureBox();
            picBxPlayer.Visible = false;
            picBxPlayer.SizeMode = PictureBoxSizeMode.AutoSize;
            gameLevel = gL;
            if (gameLevel == 1)
                picBxPlayer.Image = Properties.Resources.Character1_R;
            else
                picBxPlayer.Image = Properties.Resources.Character2_R;
            picBxPlayer.Location = start;
            frm.Controls.Add(picBxPlayer);
        }

        //Properties
        public int Speed { get { return speed; } }

        public int Health
        {
            get { return health; }
            set
            {
                //Ensure that health does not exceed max limit of 120.
                if (value > 120)
                    health = 120;
                else
                    health = value;
            }
        }

        public int Clip
        {
            get { return clip; }
            set
            {
                //The weapon can only hold a max_clipSize of bullets.
                if (value > max_clipSize)
                {
                    //Add extra ammo to the ammo pouch.
                    ammoPouch += value - max_clipSize;
                    clip = max_clipSize;
                }
                else
                    clip = value;
            }
        }

        public int AmmoPouch
        {
            get { return ammoPouch; }
            set { ammoPouch = value; }
        }

        public int Max_ClipSize { get { return max_clipSize; } }

        public char Direction { get { return direction; } }

        //Methods to move player
        public void Up()
        {
            up = true;
            direction = 'u';
            if (gameLevel == 1)
                picBxPlayer.Image = Properties.Resources.Character1_U;
            else
                picBxPlayer.Image = Properties.Resources.Character2_U;
        }

        public void Left()
        {
            left = true;
            direction = 'l';
            if (gameLevel == 1)
                picBxPlayer.Image = Properties.Resources.Character1_L;
            else
                picBxPlayer.Image = Properties.Resources.Character2_L;
        }

        public void Down()
        {
            down = true;
            direction = 'd';
            if (gameLevel == 1)
                picBxPlayer.Image = Properties.Resources.Character1_D;
            else
                picBxPlayer.Image = Properties.Resources.Character2_D;
        }

        public void Right()
        {
            right = true;
            direction = 'r';
            if (gameLevel == 1)
                picBxPlayer.Image = Properties.Resources.Character1_R;
            else
                picBxPlayer.Image = Properties.Resources.Character2_R;
        }

        //Methods for reloading
        public void Reload()
        {
            prgsBrReloadBar = new ProgressBar();
            prgsBrReloadBar.ForeColor = Color.DarkGoldenrod;
            prgsBrReloadBar.BackColor = Color.LightGray;
            prgsBrReloadBar.Value = 0;
            prgsBrReloadBar.Maximum = 100;
            prgsBrReloadBar.Size = new Size(60, 10);
            prgsBrReloadBar.Style = ProgressBarStyle.Continuous;

            tmrReloadTime = new Timer();
            tmrReloadTime.Interval = 300;
            tmrReloadTime.Tick += new EventHandler(TmrReloadTime_Tick);

            reload = true;
            tmrReloadTime.Start();
        }

        private void TmrReloadTime_Tick(object sender, EventArgs e)
        {
            if (prgsBrReloadBar.Value == prgsBrReloadBar.Maximum)
            {
                prgsBrReloadBar.Dispose();
                reload = false;

                //Amount of bullets needed to fill the gun clip.
                int reloadAmount = max_clipSize - clip;
                //If the ammo in the ammo pouch is not enough to fill the gun clip then transfer what is there.
                if (reloadAmount > ammoPouch)
                {
                    clip += ammoPouch;
                    ammoPouch = 0;
                }
                else
                {
                    clip = max_clipSize;
                    ammoPouch -= reloadAmount;
                }

                tmrReloadTime.Dispose();
            }
            else
                prgsBrReloadBar.Value += 20;
        }
    }
}
