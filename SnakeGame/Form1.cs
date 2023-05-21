using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

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
            if (e.KeyCode == Keys.Left && Configurações.directions != "direita")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Configurações.directions != "esquerda")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Configurações.directions != "baixo")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Configurações.directions != "cima")
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
            Label caption = new Label();
            caption.Text = "Minha pontuação é: " + score + " e o meu recorde é: " + pontuacao + " no jogo da cobra por Walter Rodrigues";
            caption.Font = new Font("Arial", 12, FontStyle.Bold);
            caption.ForeColor = Color.Purple;
            caption.AutoSize = false;
            caption.Width = picCanvas.Width;
            caption.Height = 30;
            caption.TextAlign = ContentAlignment.MiddleCenter;
            picCanvas.Controls.Add(caption);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Print do jogo da cobra por Walter Rodrigues";
            dialog.DefaultExt = "jpeg";
            dialog.Filter = "JPG Image File | *.jpg";
            dialog.ValidateNames = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int Width = Convert.ToInt32(picCanvas.Width);
                int Height = Convert.ToInt32(picCanvas.Height);
                Bitmap bmp = new Bitmap(Width, Height);
                picCanvas.DrawToBitmap(bmp, new Rectangle(0, 0, Width, Height));
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                picCanvas.Controls.Remove(caption);


            }


        }
        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (goLeft)
            {
                Configurações.directions = "Esquerda";
            }
            if (goRight)
            {
                Configurações.directions = "Direita";
            }
            if (goDown)
            {
                Configurações.directions = "Baixo";
            }
            if (goUp)
            {
                Configurações.directions = "Cima";
            }

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Configurações.directions)
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

                    if (Snake[i].x == food.x && Snake[i].y == food.y)
                    {
                        EatFood();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].x == Snake[j].x && Snake[i].y == Snake[j].y)
                        {
                            GameOver();
                        }

                    }
                }

                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
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
            pontuacao = 0;
            score = 0;
            if (score > pontuacao)
            {
                pontuacao = score;
            }

            StartButton.Enabled = false;
            PrintButton.Enabled = false;

            txtPontuacao.Text = "Pontuação: " + pontuacao;

            Ciclo head = new Ciclo { x = 10, y = 5 };
            Snake.Add(head);

            for (int i = 0; i < 10; i++)
            {
                Ciclo body = new Ciclo();
                Snake.Add(body);
            }

            food = new Ciclo { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };
            GameTimer.Start();
        }
        private void EatFood()
        {
            score += 1;

            txtPontuacao.Text = "Pontuação: " + score;

            Ciclo body = new Ciclo
            {
                x = Snake[Snake.Count - 1].x,
                y = Snake[Snake.Count - 1].y
            };

            Snake.Add(body);

            food = new Ciclo { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };
        }
        private void GameOver()
        {
            GameTimer.Stop();
            StartButton.Enabled = true;
            PrintButton.Enabled = true;

            if (score > pontuacao)
            {
                pontuacao = score;
                txtRecorde.Text = "Recorde: " + Environment.NewLine + pontuacao;
                txtRecorde.ForeColor = Color.Maroon;
                txtRecorde.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
    }
}
