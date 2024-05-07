/*  Created by: 
 *  Project: Brick Breaker
 *  Date: 
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Xml;
using System.Diagnostics;
using System.Windows.Forms.Automation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

       // public static SoundPlayer brickHit = new SoundPlayer();

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, spaceDown;

        // Game values
        int lives, counter, extraSpeed;

        // Paddle and Ball objects
        Paddle paddle;
        Ball ball;

        List<Ball> balls = new List<Ball>();

        // lists for powerups
        List<Powers> powerList = new List<Powers>();

        Stopwatch breakTimer = new Stopwatch();
        Stopwatch gravityTimer = new Stopwatch();
        Stopwatch extendTimer = new Stopwatch();

        public static string level;

        public static bool breakthroughBool;
        public static bool gravityBool;

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush cyanBrush = new SolidBrush(Color.Cyan);

        //placeholder brushes for testing powerups
        SolidBrush breakThrough = new SolidBrush(Color.White);
        SolidBrush multiBall = new SolidBrush(Color.Blue);
        SolidBrush gravity = new SolidBrush(Color.Purple);
        SolidBrush extendPaddle = new SolidBrush(Color.Yellow);
        SolidBrush health = new SolidBrush(Color.Red);

        //declare random
        public static Random r = new Random();
        #endregion

        Rectangle rc_car = new Rectangle();

        Image rccar = Properties.Resources.RC_top1;
        Image ballig = Properties.Resources.toy_story_ball_down1;

        Pen redbrush = new Pen(Color.Red);

        Stopwatch ballwatch = new Stopwatch();

        public GameScreen()
        {
            InitializeComponent();

            //brickHit = Properties.Resources.brickHit;

            OnStart();

            SoundPlayer paddleSound = new SoundPlayer(Properties.Resources.paddleHit);
            SoundPlayer brickSound = new SoundPlayer(Properties.Resources.brickHit);

        }

        public void OnStart()
        {
            //set life counter
            lives = 3;
            counter = extraSpeed = 0;

            List<Label> labels = new List<Label>();

            //set all button presses to false.
            leftArrowDown = rightArrowDown = spaceDown = false;

            //clear blocks list
            blocks.Clear();

            // setup starting paddle values and create paddle object

            int paddleWidth = 80;
            int paddleHeight = 105;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 7;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            int ballX = this.Width / 2 - 10;
            int ballY = this.Height - paddle.height - 80;

            // Creates a new ball
            int xSpeed = 8;
            int ySpeed = 8;
            int ballSize = 20;
            
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);
            balls.Add(ball);

            // level variable
            XmlReader reader = XmlReader.Create($"Resources/{level}.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    Block b;
                    int x = Convert.ToInt32(reader.ReadString());
                    reader.ReadToFollowing("y");
                    int y = Convert.ToInt32(reader.ReadString());
                    reader.ReadToFollowing("hp");
                    int hp = Convert.ToInt32(reader.ReadString());
                    reader.ReadToFollowing("colour");
                    string color = reader.ReadString();
                    b = new Block(x, y, hp);

                    blocks.Add(b);
                }
                
            }

            reader.Close();

            rc_car.X = paddle.x;
            rc_car.Y = paddle.y;
            rc_car.Width = paddle.width;
            rc_car.Height = paddle.height;


            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                default:
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //test code to slow game down (will be removed for final game)
            if (spaceDown)
            {
                gameTimer.Interval = 100;
            }
            else
            {
                gameTimer.Interval = 1;
            }

            // Move the paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }

            // Move ball
            foreach (Ball b in balls)
            {
                b.Move();
            }


            // Check for collision with top and side walls
            foreach (Ball b in balls)
            {
                b.WallCollision(this);
            }


            // Check for ball hitting bottom of screen
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].BottomCollision(this))
                {
                    balls.RemoveAt(i);

                    if (balls.Count == 0)
                    {
                        lives--;

                        // Moves the ball back to origin
                        balls.Add(ball);
                        balls[i].x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                        balls[i].y = (this.Height - paddle.height) - 85;
                    }

                    if (lives == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }
                }
            }
            // Check for collision of ball with paddle, (incl. paddle movement)
            foreach (Ball b in balls)
            {
                b.PaddleCollision(paddle, extraSpeed);

             }

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                for (int i = 0; i < balls.Count; i++)
                {
                    if (balls[i].BlockCollision(b))
                    {
                        b.hp--;

                        //random chance to spawn a powerup
                        if (r.Next(1, 2) == 1)
                        {
                            Powers power = new Powers(b.x + (b.width / 2), b.y + (b.height / 2), "");
                            powerList.Add(power);
                        }
                        if(b.hp == 0)
                        {
                            blocks.Remove(b);
                        }

                        if (blocks.Count == 0)
                        {
                            gameTimer.Enabled = false;
                            OnEnd();
                        }

                        return;
                    }
                }

            }

            // Powers
            foreach (Powers p in powerList)
            {
                //move each powerBall
                p.Move();

                //check for paddle collision to see if the player deserves powerup
                if (p.Collision(paddle))
                {
                    //determine what kind of powerup it is
                    switch (p.type)
                    {
                        case "Breakthrough":
                            //unstoppable ball for duration of time
                            if (breakTimer.IsRunning == true)
                            {
                                breakTimer.Restart();
                            }
                            else
                            {
                                breakTimer.Start();
                                breakthroughBool = true;
                                ballBrush.Color = Color.LightBlue;
                            }
                            break;
                        case "Gravity":
                            //arc balls back upwards 
                            if (gravityTimer.IsRunning == true)
                            {
                               gravityTimer.Restart();
                            }
                            else
                            {
                                gravityTimer.Start();
                                gravityBool = true;
                                ballBrush.Color = Color.LightPink;
                            }
                            break;
                        case "Health":
                            //grants the player an extra life, capped at 5 lives
                            if (lives < 5)
                            {
                                lives++;
                            }
                            break;
                        case "MultiBall":
                            //creates a new ball 
                            Ball newBall = new Ball(ball.x, ball.y, ball.xSpeed * -1, ball.ySpeed, ball.size);
                            balls.Add(newBall);
                            break;
                        case "ExtendPaddle":
                            //extends paddle
                            if (extendTimer.IsRunning == true)
                            {
                                extendTimer.Restart();
                            }
                            else
                            {
                                extendTimer.Start();
                                paddle.width += 40;
                                paddle.x -= 20;
                            }
                            break;
                    }
                    //delete the powerBall
                    powerList.Remove(p);
                    break;
                }
                // if powerBall goes offscreen, delete the ball
                if (p.y > this.Height + 50)
                {
                    powerList.Remove(p);
                    break;
                }
            }
            //check if duration has run out for each powerup
            
            if (4 < Convert.ToDouble(breakTimer.ElapsedMilliseconds / 1000))
            {
                breakTimer.Reset();
                ballBrush.Color = Color.White;
                breakthroughBool = false;
            }
            //extend powerup
            
            if (10 < Convert.ToDouble(extendTimer.ElapsedMilliseconds / 1000))
            {
                extendTimer.Reset();
                paddle.width -= 40;
                paddle.x += 20;
            }
            //gravity powerup

            if (7 < Convert.ToDouble(gravityTimer.ElapsedMilliseconds / 1000))
            {
                gravityTimer.Reset();
                gravityBool = false;
                ballBrush.Color = Color.White;
            }

            //ball animations
            ballwatch.Start();
            if (ballwatch.ElapsedMilliseconds >= 200)
            {
                ballig = Properties.Resources.toy_story_ball_down1;
            }
            if (ballwatch.ElapsedMilliseconds >= 400)
            {
                ballig = Properties.Resources.toy_story_ball_right1;
            }
            if (ballwatch.ElapsedMilliseconds >= 600)
            {
                ballig = Properties.Resources.toy_story_ball_up1;
            }
            if (ballwatch.ElapsedMilliseconds >= 1000)
            {
                ballig = Properties.Resources.toy_story_ball_left1;
            }

            //speeding up the ball every 5 seconds
            counter++;
            if (counter % 5000 == 0)
            {
                extraSpeed++;
            }
            
            if (counter % 100 == 0)
            {
                foreach (Block b in blocks)
                {
                    b.y += 10;
                    if (b.y >= paddle.y)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                        break;
                    }
                }
            }

            //redraw the screen
            Refresh();
        }

        public void OnEnd()
        {
            breakTimer.Reset();
            gravityTimer.Reset();
            extendTimer.Reset();
            gravityBool = false;
            breakthroughBool = false;

            // Goes to the game over screen
            
            Form form = this.FindForm();
            Screens.EndScreen ps = new Screens.EndScreen();

            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            //paddleBrush.Color = paddle.colour;

            e.Graphics.DrawRectangle(redbrush, paddle.x, paddle.y, paddle.width, paddle.height);
            if (leftArrowDown == true)
            {
                paddle.height = 80;
                paddle.width = 105;
                e.Graphics.DrawImage(Properties.Resources.RC_top_right, paddle.x, paddle.y, paddle.width, paddle.height);
            }
            else if (rightArrowDown == true)
            {
                paddle.height = 80;
                paddle.width = 105;
                e.Graphics.DrawImage(Properties.Resources.RC_top_left, paddle.x, paddle.y, paddle.width, paddle.height);
            }
            else
            {
                paddle.height = 105;
                paddle.width = 80;
                e.Graphics.DrawImage(rccar, paddle.x, paddle.y, paddle.width, paddle.height);

            }

            // Draws blocks
            foreach (Block b in blocks)
            {
                if (b.hp == 1)
                {
                    e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
                }
                else if (b.hp == 2)
                {
                    e.Graphics.FillRectangle(yellowBrush, b.x, b.y, b.width, b.height);
                }
                if (b.hp == 3)
                {
                    e.Graphics.FillRectangle(cyanBrush, b.x, b.y, b.width, b.height);
                }
            }
            // Draws powerups
            foreach (Powers p in powerList)
            {
                switch (p.type)
                {
                    case "Breakthrough":
                        e.Graphics.FillRectangle(breakThrough, p.x, p.y, Powers.powerupSize, Powers.powerupSize);
                        break;
                    case "Gravity":
                        e.Graphics.FillRectangle(gravity, p.x, p.y, Powers.powerupSize, Powers.powerupSize);
                        break;
                    case "Health":
                        e.Graphics.FillRectangle(health, p.x, p.y, Powers.powerupSize, Powers.powerupSize);
                        break;
                    case "MultiBall":
                        e.Graphics.FillRectangle(multiBall, p.x, p.y, Powers.powerupSize, Powers.powerupSize);
                        break;
                    case "ExtendPaddle":
                        e.Graphics.FillRectangle(extendPaddle, p.x, p.y, Powers.powerupSize, Powers.powerupSize);
                        break;
                }
            }

            // Draws balls
            foreach (Ball b in balls)
            {
                e.Graphics.DrawImage(ballig, b.x, b.y, b.size, b.size);
            }
        }
    }
}