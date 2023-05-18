using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private List<Ciclo> Snake = new List<Ciclo>();
        private Ciclo food = new Ciclo();

        int maxWidth;
        int maxHeight;

        int score;
        int pontuacao;

        Random rand = new Random();

        bool goLeft, goRight, goDown, goUp;
        public Form1()
        {
            InitializeComponent();

            new Configurações();
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Configurações.direcoes != "direita")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Configurações.direcoes != "esquerda")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Configurações.direcoes != "baixo")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Configurações.direcoes != "cima")
            {
                goDown = true;
            }
        }
        private void StartGame(object sender, EventArgs e)
        {
            ResartGame();
        }

        private void TakeSnapShot(object sender, EventArgs e)
        {
        }
        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (goLeft)
            {
                Configurações.direcoes = "Esquerda";
            }
            if (goRight)
            {
                Configurações.direcoes = "Direita";
            }
            if (goDown)
            {
                Configurações.direcoes = "Baixo";
            }
            if (goUp)
            {
                Configurações.direcoes = "Cima";
            }

            for (int i = Snake.Count -1; i < 0; i--)
            {
                if (i == 0)
                {
                    switch (Configurações.direcoes)
                    {
                        case "Esquerda":
                            Snake[i].x--;
                            break;
                        case "Direita":
                            Snake[i].x++;
                            break;
                        case "Baixo":
                            Snake[i].y++;
                            break;
                        case "Cima":
                            Snake[i].y--;
                            break;
                    }

                    if (Snake[i].x < 0)
                    {
                        Snake[i].x = maxWidth;
                    }
                    if (Snake[i].x > maxWidth)
                    {
                        Snake[i].x = 0;
                    }
                    if (Snake[i].y < 0)
                    {
                        Snake[i].y = maxHeight;
                    }
                    if (Snake[i].y > maxHeight)
                    {
                        Snake[i].y = 0;
                    }
                }
                else
                {
                    Snake[i].x = Snake[i -1].x;
                    Snake[i].y = Snake[i -1].y;
                }
            }
            picCanvas.Invalidate();
        }
        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].x * Configurações.Width,
                    Snake[i].y * Configurações.Height,
                    Configurações.Width, Configurações.Height
                    ));
            }

            canvas.FillEllipse(Brushes.DarkRed, new Rectangle
            (
            food.x * Configurações.Width,
            food.y * Configurações.Height,
            Configurações.Width, Configurações.Height
            ));
        }

        private void Score_Click(object sender, EventArgs e)
        {

        }
        private void ResartGame()
        {
            maxWidth = picCanvas.Width / Configurações.Width - 1;
            maxHeight = picCanvas.Height / Configurações.Height - 1;


            Snake.Clear();

            StartButton.Enabled = false;
            PrintButton.Enabled = false;
            pontuacao = 0;
            txtPontuacao.Text = "Pontuação: " + pontuacao;

            Ciclo head = new Ciclo { x = 10, y = 5 };
            Snake.Add(head);

            for (int i = 0; i <10; i++)
            {
                Ciclo body = new Ciclo();
                Snake.Add(body);
            }

            food = new Ciclo { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };
            GameTimer.Start();
        }
        private void EatFood()
        {

        }
        private void GameOver()
        {

        }
    }
}
