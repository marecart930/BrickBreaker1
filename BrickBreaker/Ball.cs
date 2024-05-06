using System;
using System.CodeDom;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.XPath;

namespace BrickBreaker
{
    public class Ball
    {
        public int x, y, xSpeed, ySpeed, size;
        public Color colour;

        public static Random rand = new Random();

        public Ball(int _x, int _y, int _xSpeed, int _ySpeed, int _ballSize)
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

            if (y > 350 && ySpeed > 0 && GameScreen.gravityBool)
            {
                y -= (ySpeed / 2);
                ySpeed *= -1;
                y--;
            }
        }
        public bool BlockCollision(Block b)
        {
            //creating temporary rectangles
            Rectangle blockRecLeft = new Rectangle(b.x - 1, b.y, 2, b.height);
            Rectangle blockRecRight = new Rectangle(b.x + b.width - 1, b.y, 2, b.height);
            Rectangle blockRecMiddleBottom = new Rectangle(b.x + 1, b.y + b.height - 1, b.width - 2, 1);
            Rectangle blockRecMiddleTop = new Rectangle(b.x + 1, b.y, b.width - 2, 1);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            //checking for intersection with each part of the blocks
            if ((ballRec.IntersectsWith(blockRecMiddleBottom) || ballRec.IntersectsWith(blockRecMiddleTop)) && x > b.x && x < b.x + b.width)
            {
                if (GameScreen.breakthroughBool == false)
                {
                    ySpeed *= -1;
                }

                return true;
            }
            if ((ballRec.IntersectsWith(blockRecLeft) || ballRec.IntersectsWith(blockRecRight)) && y > b.y && y < b.y + b.height)
            {
                if (GameScreen.breakthroughBool == false)
                {
                    xSpeed *= -1;
                }

                return true;
            }

            //returning false if no intersection
            return false;
        }

        public void PaddleCollision(Paddle p, int extraSpeed)
        {
            //creating temporary rectangles
            Rectangle blockRecLeft = new Rectangle(p.x - 1, p.y, 2, p.height);
            Rectangle blockRecRight = new Rectangle(p.x + p.width - 1, p.y, 2, p.height);
            Rectangle blockRecMiddleBottom = new Rectangle(p.x + 1, p.y + p.height - 1, p.width - 2, 1);
            Rectangle blockRecMiddleTop = new Rectangle(p.x + 1, p.y, p.width - 2, 1);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            int collisonPoint = 0;

            //checking for intersection between the ball and top of the block
            if ((ballRec.IntersectsWith(blockRecMiddleBottom) || ballRec.IntersectsWith(blockRecMiddleTop)) && x > p.x && x < p.x + p.width)
            {
                //resetting the position of the ball to the top of the paddle and checking where it hit the paddle
                collisonPoint = x - p.x;

                //changing the x and y relative to where it hits the paddle (closer to the edge means shallower angle)
                if (collisonPoint < 5 || collisonPoint > 55)
                {
                    if (xSpeed < 0)
                    {
                        xSpeed = -10 - extraSpeed;
                        ySpeed = -4 - extraSpeed;
                    }
                    else
                    {
                        xSpeed = 10 + extraSpeed;
                        ySpeed = -4 - extraSpeed;
                    }
                }
                else if (collisonPoint > 5 && collisonPoint < 55)
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
                else if (collisonPoint == 50)
                {
                    //xSpeed = 0;
                }
            }

            //checking if the ball intersects with the sides of the paddle
            if ((ballRec.IntersectsWith(blockRecLeft) || ballRec.IntersectsWith(blockRecRight)) && y > p.y && y < p.y + p.height)
            {
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