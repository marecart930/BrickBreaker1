using System;
using System.CodeDom;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.XPath;

namespace BrickBreaker
{
    public class Ball
    {
        public int x, xSpeed, size;
        public double ySpeed, y;
        public Color colour;

        public static Random rand = new Random();

        public Ball(int _x, double _y, int _xSpeed, double _ySpeed, int _ballSize)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _ballSize;

        }

        public void Move()
        {

            x = x + xSpeed;
            y = y + ySpeed;
            /*
                        if (y > (GameScreen.height/2) && ySpeed > 0 && GameScreen.gravityBool)
                        {
                            y -= (ySpeed / 2);
                            ySpeed *= -1;

                        }
            */
            if (GameScreen.gravityBool == true)
            {
                ySpeed -= .2;
            }

        }
        public bool BlockCollision(Block b)
        {
            //creating temporary rectangles
            Rectangle blockRecLeft = new Rectangle(b.x + 1, b.y, 2, b.height); //amoguss //blow up more
            Rectangle blockRecRight = new Rectangle(b.x + b.width - 1, b.y, 2, b.height);
            Rectangle blockRecMiddle = new Rectangle(b.x - 1, b.y - 1, b.width - 2, b.height + 2);
            Rectangle ballRec = new Rectangle(x, Convert.ToInt32(y), size, size);

            //checking for intersection with each part of the blocks
            if (ballRec.IntersectsWith(blockRecMiddle))
            {
                ySpeed *= -1;

                if (GameScreen.breakthroughBool == false)
                {
                    xSpeed *= -1;
                }
                return true;
            }
            else if (ballRec.IntersectsWith(blockRecLeft) && xSpeed > 0)
            {
                if (GameScreen.breakthroughBool == false)
                {
                    xSpeed *= -1;
                }

                return true;
            }
            else if (ballRec.IntersectsWith(blockRecRight) && xSpeed < 0)
            {
                xSpeed *= -1;
                if (GameScreen.breakthroughBool == false)
                {
                    ySpeed *= -1;
                }

                return true;
            }

            //returning false if no intersection
            return false;
        }

        public void PaddleCollision(Paddle p, int extraSpeed)
        {
            //creating temporary rectangles
            Rectangle blockRecLeft = new Rectangle(p.x, p.y + 1, 2, p.height);
            Rectangle blockRecRight = new Rectangle(p.x + p.width - 1, p.y + 1, 2, p.height);
            Rectangle blockRecMiddle = new Rectangle(p.x + 1, p.y, p.width - 1, p.height + 2);
            Rectangle ballRec = new Rectangle(x, Convert.ToInt32(y), size, size);

            int collisonPoint = 0;

            //checking for intersection between the ball and top of the block
            if (ballRec.IntersectsWith(blockRecMiddle))
            {
                //resetting the position of the ball to the top of the paddle and checking where it hit the paddle
                y = p.y - size;
                collisonPoint = x - p.x;
                //changing the x and y speed relative to where it hits the paddle (closer to the edge means shallower angle)
                if (collisonPoint < 5 || collisonPoint > 55)
                {
                    if (xSpeed < 0)
                    {
                        xSpeed = -9 - extraSpeed;
                        ySpeed = -5 - extraSpeed;
                    }
                    else
                    {
                        xSpeed = 9 + extraSpeed;
                        ySpeed = -5 - extraSpeed;
                    }
                }
                else if (collisonPoint > 5 && collisonPoint < 55)
                {
                    if (xSpeed < 0)
                    {
                        xSpeed = -8 - extraSpeed;
                        ySpeed = -6 - extraSpeed;
                    }
                    else
                    {
                        xSpeed = 8 + extraSpeed;
                        ySpeed = -6 - extraSpeed;
                    }
                }
            }

            //checking if the ball intersects with the sides of the paddle
            if (ballRec.IntersectsWith(blockRecLeft) && xSpeed > 0)
            {
                y = p.y - size;
                xSpeed *= -1;
            }
            else if (ballRec.IntersectsWith(blockRecRight) && xSpeed < 0)
            {
                y = p.y - size;
                xSpeed *= -1;
            }
        }

        public void WallCollision(UserControl UC)
        {
            // Collision with left wall
            if (x <= 0)
            {
                x = 0;
                xSpeed *= -1;
            }
            // Collision with right wall
            if (x >= (UC.Width - size))
            {
                x = UC.Width - size;
                xSpeed *= -1;
            }
            // Collision with top wall
            if (y <= 2)
            {
                y = 0;
                ySpeed *= -1;
            }
        }

        public bool BottomCollision(UserControl UC)
        {
            Boolean didCollide = false;

            if (y >= UC.Height)
            {
                didCollide = true;
            }

            return didCollide;
        }

    }
}