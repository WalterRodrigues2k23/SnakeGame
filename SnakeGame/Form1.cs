using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {

        }

        private void ResartGame()
        {
            maxWidth = picCanvas.Width / Configurações.Width - 1;
            maxHeight = picCanvas.Height / Configurações.Height - 1;


            Snake.Clear();
            StartButton.Enabled = false;
            PrintButton.Enabled = false;



        }

        private void EatFood()
        {

        }

        private void GameOver()
        {

        }

    }
}
