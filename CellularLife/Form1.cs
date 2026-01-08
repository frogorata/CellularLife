using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CellularLife
{
    public partial class Form1 : Form
    {
        private readonly Timer _timer = new Timer();
        private const int gap = 30;
        private const int row = 14;
        private const int col = 31;
        public bool[,] matrixCellIsLive = new bool[row, col];

        private class Cell
        {
            Random random = new Random();
            public bool isLive = false;
            Rectangle rectangle = new Rectangle();
            private int cellSize = 25;

            public bool ovarianCell(int x, int y)
            {
                return true;
            }
            public Rectangle GetBounds(int StepX, int StepY, int pictureBox1Width, int pictureBox1Height)
            {
                rectangle.X += StepX;
                rectangle.Y += StepY;
                rectangle.Width = cellSize;
                rectangle.Height = cellSize;
                return rectangle;
            }

            public bool GetRandomBool()
            {
                bool value = random.Next(2) == 0;
                return value;
            }
        }

        public Form1()
        {
            InitializeComponent();
            _timer.Interval = 500;
            _timer.Tick += TimerTick;
            _timer.Start();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            GenerationCells();
            RenderingCell();
        }
        private void GenerationCells()
        {

        }
        private void RenderingCell()
        {
            Random random = new Random();
            Cell cell = new Cell();
            Rectangle rect = cell.GetBounds(25, 25, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);

            Bitmap bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.Image = bmp;
            Graphics g = Graphics.FromImage(bmp);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    matrixCellIsLive[i, j] = cell.GetRandomBool();
                    if (matrixCellIsLive[i, j] == cell.isLive)
                        g.FillRectangle(Brushes.Black, rect.X, rect.Y, rect.Width, rect.Height);
                    else
                        g.FillRectangle(Brushes.White, rect.X, rect.Y, rect.Width, rect.Height);
                    rect = cell.GetBounds(gap, 0, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                }
                rect = cell.GetBounds(-gap * col, gap, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}