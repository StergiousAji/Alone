using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Alone
{
    class Bullet
    {
        private int speed, damage;
        private char direction;

        //Display
        public PictureBox picBxBullet;
        public Timer tmrRefreshRate_B;

        //Movement
        private Action<Bullet> CollisionsWithSurrounding;
        private Bullet b;

        //Constructors
        public Bullet() {}
        
        public Bullet(int s, int d, char dir)
        {
            speed = s;
            damage = d;
            direction = dir;
        }

        //Properties
        public int Speed { get { return speed; } }
        public char Direction { get { return direction; } }
        public int Damage { get { return damage; } }

        //Methods to create bullet and move it in the specified direction.
        public void GenerateBullet(Form frm, Size bulletSize, Point bulletLocation, Action<Bullet> c)
        {
            picBxBullet = new PictureBox();
            picBxBullet.Tag = "Bullet";
            picBxBullet.BackColor = Color.Goldenrod;
            picBxBullet.Size = bulletSize;
            picBxBullet.Location = bulletLocation;
            frm.Controls.Add(picBxBullet);
            picBxBullet.BringToFront();

            tmrRefreshRate_B = new Timer();
            tmrRefreshRate_B.Tick += new EventHandler(TmrRefreshRate_B_Tick);
            tmrRefreshRate_B.Interval = 30;
            tmrRefreshRate_B.Start();

            b = new Bullet();
            CollisionsWithSurrounding = c;
        }

        private void TmrRefreshRate_B_Tick(object sender, EventArgs e)
        {
            int x = picBxBullet.Location.X, y = picBxBullet.Location.Y;
            switch(direction)
            {
                case 'u':
                    y -= speed;
                    break;
                case 'l':
                    x -= speed;
                    break;
                case 'd':
                    y += speed;
                    break;
                case 'r':
                    x += speed;
                    break;
            }

            picBxBullet.Location = new Point(x, y);

            //Call Method to dispose bullet if it collides with surrounding.
            CollisionsWithSurrounding(b);
        }
    }
}
