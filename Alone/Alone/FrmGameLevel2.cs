using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Media;

namespace Alone
{
    public partial class FrmGameLevel2 : Form
    {
        Player player;
        Zombie zombie;
        Bullet bullet;

        //Declaration of common variables making it easily accessible.
        Point gunPosition;
        int x, y, bottom;
        Bitmap bmpBackground;
        Random random = new Random();

        Button btnHome;
        Button btnHelp;
        Label lblGameOver;
        Button btnPlayAgain;

        public FrmGameLevel2()
        {
            InitializeComponent();

            //Create new player. Starts off with a speed of 13px/ms, a total of 12 bullets (12/8) and the max clip size for the shotgun as 12.
            player = new Player(13, 12, 8, 12, new Point(425, 360), 2, this);

            FrmLogin.currentMember.Score = 0;

            //Create new zombie. Starts off with a speed of 2px/ms and can deal a damage of 1.
            zombie = new Zombie(4, 2, new Size(57, 70));
            //Create 2 Zombies and place them randomly on the form.
            zombie.GenerateZombie(this, 4, Zombie_RndSpawnPoint);

            //Initialise variables.
            x = player.picBxPlayer.Location.X;
            y = player.picBxPlayer.Location.Y;
            //Starting gun position to fire bullets from.
            gunPosition = new Point(x + 99, y + 48);

            //Home button and help button to be displayed when game is paused.
            btnHome = new Button();
            btnHome.Text = "🏠";
            btnHome.Font = new Font("Microsoft Sans Serif", 26.25F);
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Cursor = Cursors.Hand;
            btnHome.Location = new Point(364, 655);
            btnHome.Size = new Size(52, 54);
            btnHome.BackColor = Color.Firebrick;
            btnHome.Visible = false;
            btnHome.Click += new EventHandler(BtnHome_Click);
            this.Controls.Add(btnHome);
            btnHome.BringToFront();

            btnHelp = new Button();
            btnHelp.Text = "?";
            btnHelp.Font = new Font("Lucida Calligraphy", 26.25F, FontStyle.Bold);
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.Location = new Point(427, 655);
            btnHelp.Size = new Size(52, 54);
            btnHelp.BackColor = Color.SteelBlue;
            btnHelp.Visible = false;
            btnHelp.Click += new EventHandler(BtnHelp_Click);
            this.Controls.Add(btnHelp);
            btnHelp.BringToFront();

            //Game Over label and retry button will appear when the player gets eliminated.
            lblGameOver = new Label();
            lblGameOver.Text = "GAME OVER";
            lblGameOver.Font = new Font("OCR A Extended", 60, FontStyle.Bold);
            lblGameOver.BackColor = Color.Maroon;
            lblGameOver.Location = new Point(190, 200);
            lblGameOver.Size = new Size(476, 83);
            lblGameOver.Visible = false;
            this.Controls.Add(lblGameOver);
            lblGameOver.BringToFront();

            btnPlayAgain = new Button();
            btnPlayAgain.Text = "Play Again?";
            btnPlayAgain.Font = new Font("Rockwell", 16);
            btnPlayAgain.FlatStyle = FlatStyle.Flat;
            btnPlayAgain.Cursor = Cursors.Hand;
            btnPlayAgain.BackColor = Color.ForestGreen;
            btnPlayAgain.Location = new Point(350, 315);
            btnPlayAgain.Size = new Size(160, 45);
            btnPlayAgain.Visible = false;
            btnPlayAgain.Click += new EventHandler(BtnPlayAgain_Click);
            this.Controls.Add(btnPlayAgain);
            btnPlayAgain.BringToFront();
        }

        //Method to draw the desired image on to the background picture box(For smoother animation/movement and minimise stuttering).
        private void Draw(PictureBox pB)
        {
            bmpBackground = (Bitmap)picBxBackground.Image;
            Graphics g = Graphics.FromImage(bmpBackground);
            g.DrawImage(pB.Image, pB.Location.X, pB.Location.Y, pB.Width, pB.Height);
            picBxBackground.Image = bmpBackground;
        }

        //Method to return an array of a random co-ordinate for the spawn point for the zombies.
        private int[] Zombie_RndSpawnPoint()
        {
            int rnd_x = random.Next(-10, 840); 
            int rnd_y = random.Next(-10, 690);
            int[] r = new int[2];
            while (true)
            {
                //Loop to ensure that the random co-ordinate is outside the form but not behind any of the objects on the background:
                //T-L Building/Tree #1 or T-R Building or B-L Building/Health box or Tree #2/Weapon Slot.
                if (!(((rnd_x >= 200 && rnd_x <= 470) && rnd_y <= 0) || (rnd_x <= 0 && (rnd_y >= 215 && rnd_y <= 540)) || 
                    ((rnd_x >= 255 && rnd_x <= 535) && rnd_y >= 670) || (rnd_x >= 820 && (rnd_y >= 100 && rnd_y <= 430))))
                {
                    rnd_x = random.Next(-10, 840);
                    rnd_y = random.Next(-10, 690);
                }
                else
                    break;
            }
            r[0] = rnd_x;
            r[1] = rnd_y;
            return r;
        }

        private void FrmGameLevel2_KeyDown(object sender, KeyEventArgs e)
        {
            //Change directions to true if certain keys pressed.
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    player.Up();
                    gunPosition = new Point(x + 47, y);
                    break;
                case Keys.A:
                case Keys.Left:
                    player.Left();
                    gunPosition = new Point(x, y + 15);
                    break;
                case Keys.S:
                case Keys.Down:
                    player.Down();
                    gunPosition = new Point(x + 15, y + 99);
                    break;
                case Keys.D:
                case Keys.Right:
                    player.Right();
                    gunPosition = new Point(x + 99, y + 48);
                    break;
            }

            //RELOAD
            //-Only reload if the gun clip is not full and the player's ammo pouch is not empty and the player is not already reloading.
            if (e.KeyCode == Keys.R && (player.Clip < player.Max_ClipSize && player.AmmoPouch != 0) && player.reload == false)
            {
                player.Reload();
                SoundPlayer nova_Reload = new SoundPlayer(Properties.Resources.Nova_Shotgun_Reload_Sound_Effect);
                nova_Reload.Play();
            }
        }

        private void FrmGameLevel2_KeyReleased(object sender, KeyEventArgs e)
        {
            //Change directions to false if certain keys released. If spacebar released then fire weapon.
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    player.up = false;
                    break;
                case Keys.A:
                case Keys.Left:
                    player.left = false;
                    break;
                case Keys.S:
                case Keys.Down:
                    player.down = false;
                    break;
                case Keys.D:
                case Keys.Right:
                    player.right = false;
                    break;
            }

            //PAUSE GAME
            //-If 'P' or 'Esc' is pressed then pause game and wait for any other key to be pressed.
            if (e.KeyCode == Keys.P || e.KeyCode == Keys.Escape)
            {
                //Show buttons and stop timer.
                tmrRefreshRate.Stop();
                btnHome.Visible = true;
                btnHelp.Visible = true;
            }
            else
            {
                //Hide buttons and restart timer.
                tmrRefreshRate.Start();
                btnHome.Visible = false;
                btnHelp.Visible = false;

                //FIRE WEAPON
                //-User only able to fire weapon when they are not reloading.
                if (e.KeyCode == Keys.Space && player.reload == false)
                {
                    if (player.Clip != 0)
                    {
                        Fire(player.Direction);

                        //If player is low on ammo after firing, then drop ammo box to refill ammo.
                        if (player.Clip == 0 && player.AmmoPouch == 0)
                            AmmoDrop();
                    }
                    else
                    {
                        //Play empty gun shot sound effect if clip is empty.
                        SoundPlayer emptyClipFire = new SoundPlayer(Properties.Resources.Empty_Gun_Shot_Sound_Effect);
                        emptyClipFire.Play();
                    }

                }
            }
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            //Save the user's score and high score to the file.
            FrmLogin.currentMember.WriteToFile(FrmLogin.currentMember.CountMembers());

            //Go to Main Menu.
            this.Close();
            Form MainMenu = new FrmMainMenu();
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

        private void BtnPlayAgain_Click(object sender, EventArgs e)
        {
            //Restart the level.
            this.Close();
            Form GameLevel2 = new FrmGameLevel2();
            GameLevel2.Show();
        }

        private void TmrRefreshRate_Tick(object sender, EventArgs e)
        {
            //Every time the timer ticks the background picture box's image is set to the Lvl 1 Background for objects to be drawn onto.
            picBxBackground.Image = Properties.Resources.Alone___Lvl_2_Form__Background_;

            bottom = player.picBxPlayer.Bottom;

            //SCORE
            //-Display player's score.
            this.lblScore.Text = $"Score: {FrmLogin.currentMember.Score.ToString()}";

            //HEALTH
            //-Warn player when their health is low.
            if (player.Health <= 20)
                prgsBrHealth.ForeColor = Color.DarkRed;
            else
                prgsBrHealth.ForeColor = Color.ForestGreen;

            if (player.Health <= 0)
            {
                //DEAD - GAME OVER
                //-Stop timer and remove key events then change to game over screen.
                tmrRefreshRate.Stop();
                lblGameOver.Visible = true;
                btnPlayAgain.Visible = true;
                btnHome.Visible = true;
                btnHelp.Visible = true;

                this.KeyDown -= FrmGameLevel2_KeyDown;
                this.KeyUp -= FrmGameLevel2_KeyReleased;

                //Save the user's score and high score to the file.
                FrmLogin.currentMember.WriteToFile(FrmLogin.currentMember.CountMembers());
            }
            else
                prgsBrHealth.Value = player.Health;
                
            //AMMUNITION
            //-Warn player when they are low on ammo.
            if (player.Clip != 0 && player.Clip <= 5)
                lblAmmo.ForeColor = Color.DarkOrange;
            else if (player.Clip == 0)
                lblAmmo.ForeColor = Color.DarkRed;
            else
                lblAmmo.ForeColor = Color.White;

            //-Display amount of ammo the player has.
            this.lblAmmo.Text = $"{player.Clip.ToString()} / {player.AmmoPouch.ToString()}";

            //MOVEMENT
            if (player.up && InvisibleWall_Above(x, y) == false)
                y -= player.Speed;
            if (player.left && InvisibleWall_Left(x, y) == false)
                x -= player.Speed;
            if (player.down && InvisibleWall_Below(x, y) == false)
                y += player.Speed;
            if (player.right && InvisibleWall_Right(x, y) == false)
                x += player.Speed;

            player.picBxPlayer.Location = new Point(x, y);

            //RELOADING
            //-Check if player is reloading. If so, then put reload bar on form and make it follow player.
            if (player.reload)
            {
                player.prgsBrReloadBar.Location = new Point(x + 18, y - 7);
                this.Controls.Add(player.prgsBrReloadBar);
                player.prgsBrReloadBar.BringToFront();
            }

            //COLLISIONS & ZOMBIE MOVEMENT
            foreach (Control o in this.Controls)
            {
                //Draw/re-draw player, zombies and ammo box images, onto the background image of the background picture box (Creates a smoother animation than using picture boxes).
                if (o is PictureBox && ((String)o.Tag != "Background" && (String)o.Tag != "Bullet"))
                    Draw((PictureBox)o);

                //AMMUNITION REFILL BOX COLLISIONS
                //-If player collides with ammo boxes then player will collect ammo.
                if (o is PictureBox && (String)o.Tag == "Ammo Box" && o.Bounds.IntersectsWith(player.picBxPlayer.Bounds))
                {
                    o.Dispose();
                    player.AmmoPouch += player.Max_ClipSize;
                }

                //ZOMBIE COLLISIONS AND MOVEMENT
                if (o is PictureBox && (String)o.Tag == "Zombie")
                {
                    //Damage player if contact is made with player.
                    if (o.Bounds.IntersectsWith(player.picBxPlayer.Bounds))
                        player.Health -= zombie.Damage;

                    //Damage zombies if bullet makes contact with zombies then make new zombies. (2 hit kill in Level 2).
                    if (bullet != null && o.Bounds.IntersectsWith(bullet.picBxBullet.Bounds))
                    {
                        zombie.Health -= bullet.Damage;
                        bullet.picBxBullet.Dispose();

                        int numOfZ = 1;

                        //If player's score is reaches 20, then increase the player's health by 80hp and difficulty (Speed +1, new zombie image, 5 zombies).
                        if (FrmLogin.currentMember.Score == 19)
                        {
                            player.Health += 80;
                            zombie.Speed++;
                            zombie.zImage = 2;
                            numOfZ = 2;
                        }

                        if (zombie.Health == 0)
                        {
                            FrmLogin.currentMember.Score++;
                            o.Dispose();

                            //Player will get a health boost when they kill a zombie (Harder zombies give larger health boost).
                            if (FrmLogin.currentMember.Score >= 20)
                                player.Health += 4;
                            else
                                player.Health += 2;

                            zombie.GenerateZombie(this, numOfZ, Zombie_RndSpawnPoint);
                            SoundPlayer zombie_Sounds = new SoundPlayer(Properties.Resources.Zombie_Sound_Effect);
                            zombie_Sounds.Play();
                        }
                    }

                    //Move zombies towards player, ensuring it does not cross over surroundings (Invisible Walls).
                    zombie.ZombieMovement(o, player,
                        InvisibleWall_Above, InvisibleWall_Left, InvisibleWall_Below, InvisibleWall_Right);
                }

            }
        }

        //INVISIBLE WALLS
        //-Methods to create invisible walls on parts of the form.
        //-Sometimes the invisible walls have to have a thickness of 5px to compensate for the speed of objects.
        bool invisibleWall;
        private bool InvisibleWall_Above(int x, int y)
        {
            invisibleWall = false;

            //Make top of form, bottom of Tree #1, T-L Building, Abandoned Car and T-R Building invisible walls.
            if (y <= 0 || (x <= 95 && y <= 215) || (x <= 200 && y <= 95) || ((x >= 300 && x <= 400) && (y >= 200 && y <= 210)) || 
                (x >= 455 && y <= 100))
            {
                invisibleWall = true;
            }

            return invisibleWall;
        }

        private bool InvisibleWall_Left(int x, int y)
        {
            invisibleWall = false;

            //Make left-side of form, right-side of Tree #1, T-L Building, Abandoned Car, B-L Building and Health Box invisible walls.
            if (x <= 0 || (x <= 100 && y <= 210) || (x <= 205 && y <= 95) || ((x >= 300 && x <= 400) && (y >= 50 && y <= 210)) ||
                ((x >= 200 && x <= 205) && y >= 545) || (x <= 255 && y >= 570))
            {
                invisibleWall = true;
            }

            return invisibleWall;
        }

        private bool InvisibleWall_Below(int x, int y)
        {
            invisibleWall = false;

            //Make bottom of form, top of B-L Building, Health box, Weapon Slot box, Tree #2 and Abandoned Car invisible walls.
            if (y >= 650 || (x <= 200 && y >= 505) || (x <= 250 && y >= 550) || (x >= 530 && y >= 550) || (x >= 645 && y >= 400) || 
                ((x >= 295 && x <= 395) && (y >= 20 && y <= 50)))
            {
                invisibleWall = true;
            }

            return invisibleWall;
        }

        private bool InvisibleWall_Right(int x, int y)
        {
            invisibleWall = false;

            //Make right-side of form, left-side of T-R Building, Abandoned Car, Tree #2 and Weapon Slot box invisible walls.
            if (x >= 780 || (x >= 430 && y <= 90) || ((x >= 265 && x <= 300) && (y >= 45 && y <= 210)) || (x >= 615 && y >= 420) ||
                (x >= 490 && y >= 590))
            {
                invisibleWall = true;
            }
            return invisibleWall;
        }

        //FIRE WEAPON
        //-Method to decrement player's ammo and then create a new bullet and fire it in the direction the player is facing.
        private void Fire(char directionFacing)
        {
            player.Clip--;
            //Bullet has speed of 50px/ms and can deal a damage of 50pts.
            bullet = new Bullet(50, 50, directionFacing);
            bullet.GenerateBullet(this, new Size(7, 7), gunPosition, (Bullet) => BulletCollisions(bullet));
            SoundPlayer nova_Fire = new SoundPlayer(Properties.Resources.Nova_Shotgun_Fire_Sound_Effect);
            nova_Fire.Play();
        }

        //BULLET COLLISIONS
        //-Method to check if bullet is colliding with surrounding.
        private void BulletCollisions(Bullet b)
        {
            int x = b.picBxBullet.Location.X, y = b.picBxBullet.Location.Y;
            //Check to see whether bullet has hit T-L Building, Tree #1, T-R Building, Abandoned Car, Tree #2 or B-L Building. If so then dispose bullet.
            if ((x <= 200 && y <= 90) || (x <= 100 && y <= 200) || (x >= 530 && y <= 80) || ((x >= 355 && x <= 400) && (y >= 110 && y <= 215)) || 
                (x >= 710 && (y >= 500 && y <= 595)) || (x <= 200 && y >= 605))
            {
                b.picBxBullet.Dispose();
                b.tmrRefreshRate_B.Dispose();
            }
        }

        //AMMO DROP
        //-Method to generate and place ammo box at random co-ordinate on the form if player is low on ammo.
        private void AmmoDrop()
        {
            while (true)
            {
                int rnd_x = random.Next(0, this.Width - 71);
                int rnd_y = random.Next(0, this.Height - 91);
                while (true)
                {
                    //Loop to ensure that the random co-ordinate is not on top of the following objects in the background:
                    //T-L Building/Tree #1 or T-R Building or Abandoned Car or B-L Building/Health box or Tree #2/Weapon Slot.
                    if ((rnd_x <= 200 && rnd_y <= 200) || (rnd_x >= 475 && rnd_y <= 95) || ((x >= 300 && x <= 400) && (y >= 65 && y <= 215)) || 
                        (rnd_x <= 255 && rnd_y >= 560) || (rnd_x >= 540 && rnd_y >= 455))
                    {
                        rnd_x = random.Next(0, this.Width - 71);
                        rnd_y = random.Next(0, this.Height - 91);
                    }
                    else
                        break;
                }

                //Create ammo box picture box.
                PictureBox picBxAmmoBox = new PictureBox();
                picBxAmmoBox.Image = Properties.Resources.Ammunition_Box_2;
                picBxAmmoBox.Tag = "Ammo Box";
                picBxAmmoBox.Visible = false;
                picBxAmmoBox.Size = new Size(53, 45);
                picBxAmmoBox.Location = new Point(rnd_x, rnd_y);
                this.Controls.Add(picBxAmmoBox);

                //Player gets a 1 in 30 chance of getting bonus ammo boxes.
                int bonus_ammoBox = random.Next(1, 31);
                if (bonus_ammoBox != 1)
                    break;
            }
        }
    }
}
