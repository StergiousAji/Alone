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
    public partial class FrmGameLevel1 : Form
    {
        Player player;
        Zombie zombie;
        Bullet bullet;

        //Declaration of common variables making it easily accessible.
        Point gunPosition;
        int x, y;
        Bitmap bmpBackground;
        Random random = new Random();

        Button btnHome;
        Button btnHelp;
        Label lblGameOver;
        Button btnPlayAgain;

        public FrmGameLevel1()
        {
            InitializeComponent();

            //Create new player. Starts off with a speed of 12px/ms, a total of 10 bullets (7/3) and the max clip size for the gun as 7.
            player = new Player(12, 7, 3, 7, new Point(330, 300), 1, this);

            FrmLogin.currentMember.Score = 0;

            //Create new zombie. Starts off with a speed of 3px/ms and can deal a damage of 1.
            zombie = new Zombie(3, 1, new Size(73, 90));
            //Create 2 Zombies and place them randomly on the form.
            zombie.GenerateZombie(this, 2, Zombie_RndSpawnPoint);

            //Initialise variables.
            x = player.picBxPlayer.Location.X;
            y = player.picBxPlayer.Location.Y;
            //Starting gun position to fire bullets from.
            gunPosition = new Point(x + 86, y + 65);

            //Home button and help button to be displayed when game is paused or when game over.
            btnHome = new Button();
            btnHome.Text = "🏠";
            btnHome.Font = new Font("Microsoft Sans Serif", 26.25F);
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Cursor = Cursors.Hand;
            btnHome.Location = new Point(335, 590);
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
            btnHelp.Location = new Point(397, 590);
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
            lblGameOver.Location = new Point(150, 200);
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
            btnPlayAgain.Location = new Point(315, 315);
            btnPlayAgain.Size = new Size(160, 45);
            btnPlayAgain.Visible = false;
            btnPlayAgain.Click += new EventHandler(BtnPlayAgain_Click);
            this.Controls.Add(btnPlayAgain);
            btnPlayAgain.BringToFront();
        }

        //Method to draw the desired image on to the background picture box (For smoother animation/movement and minimise stuttering).
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
            int rnd_x = random.Next(-10, 710);
            int rnd_y = random.Next(-10, 590);
            int[] r = new int[2];
            while (true)
            {
                //Loop to ensure that the random co-ordinate is outside the form but not behind any of the objects on the background:
                //Score box or Tree #1/Bin/Bench or Health box or Tree #2/Weapon Slot box.
                if (!(((rnd_x >= 190 && rnd_x <= 305) && rnd_y <= 0) || (rnd_x <= 0 && (rnd_y >= 70 && rnd_y <= 245)) ||
                    ((rnd_x >= 250 && rnd_x <= 460) && rnd_y >= 550) || (rnd_x >= 690 && (rnd_y >= 85 && rnd_y <= 325))))
                {
                    rnd_x = random.Next(-10, 710);
                    rnd_y = random.Next(-10, 590);
                }
                else
                    break;
            }
            r[0] = rnd_x;
            r[1] = rnd_y;
            return r;
        }

        private void FrmGameLevel1_KeyDown(object sender, KeyEventArgs e)
        {
            //Change directions to true if certain keys pressed.
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    player.Up();
                    gunPosition = new Point(x + 65, y + 9);
                    break;
                case Keys.A:
                case Keys.Left:
                    player.Left();
                    gunPosition = new Point(x + 4, y + 20);
                    break;
                case Keys.S:
                case Keys.Down:
                    player.Down();
                    gunPosition = new Point(x + 20, y + 87);
                    break;
                case Keys.D:
                case Keys.Right:
                    player.Right();
                    gunPosition = new Point(x + 86, y + 65);
                    break;
            }

            //RELOAD
            //-Only reload if the gun clip is not full and the player's ammo pouch is not empty and the player is not already reloading.
            if (e.KeyCode == Keys.R && (player.Clip < player.Max_ClipSize && player.AmmoPouch != 0) && player.reload == false)
            {
                player.Reload();
                SoundPlayer dE_Reload = new SoundPlayer(Properties.Resources.Desert_Eagle_Reload_Sound_Effect);
                dE_Reload.Play();
            }
        }

        private void FrmGameLevel1_KeyReleased(object sender, KeyEventArgs e)
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
            HelpGuide.Dock = DockStyle.Fill;
            HelpGuide.BringToFront();
        }

        private void BtnPlayAgain_Click(object sender, EventArgs e)
        {
            //Restart the level.
            this.Close();
            Form GameLevel1 = new FrmGameLevel1();
            GameLevel1.Show();
        }

        private void TmrRefreshRate_Tick(object sender, EventArgs e)
        {
            //Every time the timer ticks the background picture box's image is set to the Lvl 1 Background for objects to be drawn onto.
            picBxBackground.Image = Properties.Resources.Alone___Lvl_1_Form__Background_;

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

                this.KeyDown -= FrmGameLevel1_KeyDown;
                this.KeyUp -= FrmGameLevel1_KeyReleased;

                //Save the user's score and high score to the file.
                FrmLogin.currentMember.WriteToFile(FrmLogin.currentMember.CountMembers());
            }
            else
                prgsBrHealth.Value = player.Health;

            //AMMUNITION
            //-Warn player when they are low on ammo.
            if (player.Clip != 0 && player.Clip <= 2)
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
                //Draw/re-draw player, zombies and ammo box, onto the background picture box (Creates a smoother animation than using picture boxes).
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

                    //Kill zombies if bullet makes contact with zombies then make new zombies. (1 hit kill in Level One).
                    if (bullet != null && o.Bounds.IntersectsWith(bullet.picBxBullet.Bounds))
                    {
                        FrmLogin.currentMember.Score++;
                        bullet.picBxBullet.Dispose();
                        o.Dispose();

                        int numOfZ = 1;
                        //If player's score is above 50, then increase the player's health by half and difficulty (Damage +1, Speed +1, new zombie image, 4 zombies).
                        if (FrmLogin.currentMember.Score == 50)
                        {
                            player.Health += 60;
                            zombie.Damage++;
                            zombie.Speed++;
                            zombie.zImage = 2;
                            numOfZ = 3;
                        }

                        //Player will get a health boost when they kill a zombie (Harder zombies give larger health boost).
                        if (FrmLogin.currentMember.Score >= 50)
                            player.Health += 1;

                        zombie.GenerateZombie(this, numOfZ, Zombie_RndSpawnPoint);
                        SoundPlayer zombie_Sounds = new SoundPlayer(Properties.Resources.Zombie_Sound_Effect);
                        zombie_Sounds.Play();
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

            //Make top of form, bottom of Score box, Tree #1 and Bin/Bench, invisible walls.
            if (y <= 0 || (x <= 185 && (y >= 60 && y <= 70)) || ((x >= 335 && x <= 505) && (y >= 95 && y <= 105)) || (x >= 495 && y <= 85))
            {
                invisibleWall = true;
            }

            return invisibleWall;
        }

        private bool InvisibleWall_Left(int x, int y)
        {
            invisibleWall = false;

            //Make left-side of form, right-side of Score box, Health bar and Tree #1, invisible walls.
            if (x <= 0 || ((x >= 185 && x <= 190) && y <= 70) || ((x >= 245 && x <= 250) && y >= 530) || ((x >= 495 && x <= 505) && y <= 105))
            {
                invisibleWall = true;
            }

            return invisibleWall;
        }

        private bool InvisibleWall_Below(int x, int y)
        {
            invisibleWall = false;

            //Make bottom of form, top of Health bar, Weapon Slot and Tree #2, invisible walls.
            if (y >= 560 || (x <= 245 && y >= 495) || ((x >= 490 && x <= 600) && (y >= 490 && y <= 495)) || (x >= 570 && (y >= 320 && y <= 330)))
            {
                invisibleWall = true;
            }

            return invisibleWall;
        }

        private bool InvisibleWall_Right(int x, int y)
        {
            invisibleWall = false;

            //Make right-side of form, left-side of Tree #1, Tree #2 and Weapon Slot box, invisible walls.
            if (x >= 650 || ((x >= 305 && x <= 310) && y <= 105) || ((x >= 570 && x <= 575) && (y >= 340 && y <= 560)) ||
                ((x >= 460 && x <= 465) && (y >= 500)))
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
            //Bullet has speed of 30px/ms and can deal damage of 100pts.
            bullet = new Bullet(30, 100, directionFacing);
            bullet.GenerateBullet(this, new Size(6, 6), gunPosition, (Bullet) => BulletCollisions(bullet));
            SoundPlayer dE_Fire = new SoundPlayer(Properties.Resources.Desert_Eagle_Fire_Sound_Effect);
            dE_Fire.Play();
        }

        //BULLET COLLISIONS
        //-Method to check if bullet is colliding with surrounding.
        private void BulletCollisions(Bullet b)
        {
            int x = b.picBxBullet.Location.X, y = b.picBxBullet.Location.Y;
            //Check to see whether bullet has hit Tree #1 or the Bin/Bench or Tree #2. If so then dispose bullet.
            if (((x >= 390 && x <= 500) && y <= 105) || (x >= 500 && y <= 80) || (x >= 650 && (y >= 410 && y <= 540)))
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
                    //Score box or Tree #1/Bin/Bench or Health box or Tree #2/Weapon Slot box.
                    if ((rnd_x <= 185 && rnd_y <= 70) || (rnd_x >= 325 && rnd_y <= 115) || (rnd_x <= 255 && rnd_y >= 535) || 
                        (rnd_x >= 500 && rnd_y >= 355))
                    {
                        rnd_x = random.Next(0, this.Width - 71);
                        rnd_y = random.Next(0, this.Height - 91);
                    }
                    else
                        break;
                }

                //Create ammo box picture box.
                PictureBox picBxAmmoBox = new PictureBox();
                picBxAmmoBox.Image = Properties.Resources.Ammunition_Box_1;
                picBxAmmoBox.Tag = "Ammo Box";
                picBxAmmoBox.Visible = false;
                picBxAmmoBox.Size = new Size(55, 51);
                picBxAmmoBox.Location = new Point(rnd_x, rnd_y);
                this.Controls.Add(picBxAmmoBox);

                //Player gets a 1 in 10 chance of getting bonus ammo boxes. Loop again if so.
                int bonus_ammoBox = random.Next(1, 11);
                if (bonus_ammoBox != 1)
                    break;
            } 
        }
    }
}
