using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Alone
{
    class Zombie
    {
        private int speed, health, damage;
        private Size zombieDimensions;

        //Display
        public PictureBox picBxZombie;
        public int zImage;

        //Movement
        public int zombie_x, zombie_y;
         
        //Constructor
        public Zombie(int s, int d, Size zD)
        {
            speed = s;
            health = 100;
            damage = d;
            zombieDimensions = zD;
            zImage = 1;
        }

        //Properties
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        //Method to create the specified zombies
        public void GenerateZombie(Form frm, int numOfZombies, Func<int[]> rnd)
        {
            health = 100;

            while (numOfZombies != 0)
            {
                //Ensure that the while loop only loops as many times as the desired number of zombies to be created.
                numOfZombies--;

                int[] rndCoord = rnd();
                zombie_x = rndCoord[0];
                zombie_y = rndCoord[1];

                //Create zombie picture box.
                picBxZombie = new PictureBox();
                picBxZombie.Visible = false;
                picBxZombie.Tag = "Zombie";
                if (zImage == 1)
                    picBxZombie.Image = Properties.Resources.Zombie1_U;
                else
                    picBxZombie.Image = Properties.Resources.Zombie2_U;
                picBxZombie.Location = new Point(zombie_x, zombie_y);
                picBxZombie.Size = zombieDimensions;
                frm.Controls.Add(picBxZombie);
            }
        }

        //Methods to move zombies towards player ensuring that they do not cross invisible walls. Load second zombie when the condition is satisfied. (Indicates increased difficulty).
        public void ZombieMovement(Control z, Player p,
            Func<int, int, bool> iW_A, Func<int, int, bool> iW_L, Func<int, int, bool> iW_B, Func<int, int, bool> iW_R)
        {
            int z_x = z.Location.X, z_y = z.Location.Y;

            //Up
            if (z_y > p.picBxPlayer.Location.Y && iW_A(z_x, z_y) == false)
            {
                z_y -= speed;
                if (zImage == 1)
                    ((PictureBox)z).Image = Properties.Resources.Zombie1_U;
                else
                    ((PictureBox)z).Image = Properties.Resources.Zombie2_U;
            }

            //Left
            if (z_x > p.picBxPlayer.Location.X && iW_L(z_x, z_y) == false)
            {
                z_x -= speed;
                if (zImage == 1)
                    ((PictureBox)z).Image = Properties.Resources.Zombie1_L;
                else
                    ((PictureBox)z).Image = Properties.Resources.Zombie2_L;
            }

            //Down
            if (z_y < p.picBxPlayer.Location.Y && iW_B(z_x, z_y) == false)
            {
                z_y += speed;
                if (zImage == 1)
                    ((PictureBox)z).Image = Properties.Resources.Zombie1_D;
                else
                    ((PictureBox)z).Image = Properties.Resources.Zombie2_D;
            }

            //Right
            if (z_x < p.picBxPlayer.Location.X && iW_R(z_x, z_y) == false)
            {
                z_x += speed;
                if (zImage == 1)
                    ((PictureBox)z).Image = Properties.Resources.Zombie1_R;
                else
                    ((PictureBox)z).Image = Properties.Resources.Zombie2_R;
            }

            z.Location = new Point(z_x, z_y);
        }
    }
}
