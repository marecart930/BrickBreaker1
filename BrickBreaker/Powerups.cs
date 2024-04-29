using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    /*
     * Create Powerups that can benefit the player while playing the game
     * -+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+--
     * Breakthrough: ball that can break all in its path without being stopped for a duration of 7 seconds
     * Gravity: balls can no longer fall, and will arc back upwards to strike more blocks for a duration of 10 seconds
     * Health: grants the player extra health, caps at 4
     * MultiBall: creates a new ball at the base of the paddle
     * ExtendPaddle: extends the paddle for a duration of 10 seconds
     * 
     */
    internal class Powers
    {
        //declare basic variables
        public int x;
        public int y;
        public static int xSpeed = 0;
        public static int ySpeed = 3;
        public const int powerupSize = 15;
        public string type;

        // create powerups
        public Powers(int x, int y, string type)
        {
            this.x = x;
            this.y = y;
            //determine type of powerup by random chance, then saving the value
            switch (GameScreen.r.Next(4, 5))
            {
                case 1:
                    type = "Breakthrough";
                    break;
                case 2:
                    type = "Gravity";
                    break;
                case 3:
                    type = "Health";
                    break;
                case 4:
                    type = "MultiBall";
                    break;
                case 5:
                    type = "ExtendPaddle";
                    break;
            }
            this.type = type;
        }
      
        public void Move()
        {
            //move the powerBall
            y += ySpeed;
        }
        public bool Collision(Paddle paddle)
        {
            //check for simple collision between powerBall and paddle

            Rectangle player = new Rectangle(paddle.x, paddle.y, paddle.width, paddle.height);
            Rectangle powerBall = new Rectangle(x, y, powerupSize, powerupSize);

            if (player.IntersectsWith(powerBall))
            {
                return true;
            }
            return false;
        }
    }
}
