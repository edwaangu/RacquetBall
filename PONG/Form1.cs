using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PONG
{
    public partial class Form1 : Form
    {

        /**
         Racquet Ball:

         * Blue is Player 1
         * Red is Player 2
         
        */








        // Global Variables
        Rectangle player1 = new Rectangle(10, 70, 10, 60);
        Rectangle player2 = new Rectangle(10, 270, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        Random randGen = new Random();

        int playerTurn = 1;
        int player1Score = 0;
        int player2Score = 0;
        bool roundStarted = false;

        int playerSpeed = 4;
        double ballXSpeed = 0;
        double ballYSpeed = 0;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        Pen outlinePen = new Pen(Color.White, 3);
        SolidBrush redBrush = new SolidBrush(Color.Coral);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);


        public Form1()
        {
            InitializeComponent();
            roundStarted = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            ball.X += Convert.ToInt32(Math.Round(ballXSpeed));
            ball.Y += Convert.ToInt32(Math.Round(ballYSpeed));

            // player 1
            if(wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }
            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }
            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            // player 2
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }
            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }
            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }
            if (rightArrowDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
            }

            // has the ball hit the top/bottom walls?
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;
            }

            // does the ball hit either player
            if (player1.IntersectsWith(ball) && ballXSpeed < 0 && playerTurn == 1 && roundStarted == true)
            {

                double nextBallSpeedX = (randGen.NextDouble() - 0.5) / 8;
                double nextBallSpeedY = (randGen.NextDouble() - 0.5) / 8;
                ballXSpeed *= -1.05 + nextBallSpeedX;
                ballYSpeed *= 1.05 + nextBallSpeedY;
                ball.X = player1.X + ball.Width;
                playerTurn = 2;
            }
            else if (player2.IntersectsWith(ball) && ballXSpeed < 0 && playerTurn == 2 && roundStarted == true)
            {
                double nextBallSpeedX = (randGen.NextDouble() - 0.5) / 8;
                double nextBallSpeedY = (randGen.NextDouble() - 0.5) / 8;
                ballXSpeed *= -1.05 + nextBallSpeedX;
                ballYSpeed *= 1.05 + nextBallSpeedY;
                ball.X = player2.X + ball.Width;
                playerTurn = 1;
            }

            // checking for round start
            if (player1.IntersectsWith(ball) && playerTurn == 1 && roundStarted == false)
            {
                int startBallSpeedX = randGen.Next(5, 9);
                int startBallSpeedY = 0;
                while (startBallSpeedY == 0) { 
                    startBallSpeedY = randGen.Next(-7, 7);
                }
                ballXSpeed = startBallSpeedX;
                ballYSpeed = startBallSpeedY;
                roundStarted = true;
            }
            else if (player2.IntersectsWith(ball) && playerTurn == 2 && roundStarted == false)
            {
                int startBallSpeedX = randGen.Next(5, 9);
                int startBallSpeedY = 0;
                while (startBallSpeedY == 0)
                {
                    startBallSpeedY = randGen.Next(-7, 7);
                }
                ballXSpeed = startBallSpeedX;
                ballYSpeed = startBallSpeedY;
                roundStarted = true;
            }

            // did a ball miss a player
            if (ball.X < 0)
            {
                if(playerTurn == 1)
                {

                    player2Score++;
                    p2ScoreLabel.Text = $"{player2Score}";
                }
                else
                {
                    player1Score++;
                    p1ScoreLabel.Text = $"{player1Score}";
                }

                ball.X = 295;
                ball.Y = 195;

                ballXSpeed = 0;
                ballYSpeed = 0;
                roundStarted = false;

                player1.Y = 70;
                player2.Y = 270;

                player1.X = 10;
                player2.X = 10;
            }
            else if(ball.X > this.Width - ball.Width)
            {
                ball.X = this.Width - ball.Width;
                ballXSpeed *= -1;
            }

            //check score and stop?
            if(player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!";
            }
            else if(player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!";
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(redBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);

            // depending on whos turn it is, give the player an outline
            if(playerTurn == 1)
            {
                e.Graphics.DrawRectangle(outlinePen, player1);
            }
            else
            {
                e.Graphics.DrawRectangle(outlinePen, player2);
            }
        }
    }
}
