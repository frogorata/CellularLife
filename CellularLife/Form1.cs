using System;
using System.Drawing;
using System.Windows.Forms;

namespace CellularLife
{
    public partial class Form1 : Form
    {
        private const int Rows = 14;
        private const int Cols = 31;
        private const int CellSize = 25;
        private const int Gap = 30;

        private bool _isRunning = true;

        private static readonly Random _random = new Random();
        private Cell[,] _cells = new Cell[Rows, Cols];

        public class Cell
        {
            public bool IsAlive { get; private set; }

            public Cell(bool isAlive)
            {
                IsAlive = isAlive;
            }

            public bool NextState(int aliveNeighbours)
            {
                if (IsAlive)
                    return aliveNeighbours == 2 || aliveNeighbours == 3;
                else
                    return aliveNeighbours == 3;
            }

            public void ApplyState(bool newState)
            {
                IsAlive = newState;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            InitCells();

            timer1.Interval = 200;
            timer1.Tick += TimerTick;
            timer1.Start();
        }

        
        private void InitCells()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    bool alive = _random.Next(2) == 0;
                    _cells[i, j] = new Cell(alive);
                }
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            GenerateNextGeneration();
            Render();
        }

        private void GenerateNextGeneration()
        {
            bool[,] nextState = new bool[Rows, Cols];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    int neighbours = CountAliveNeighbours(i, j);
                    nextState[i, j] = _cells[i, j].NextState(neighbours);
                }
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    _cells[i, j].ApplyState(nextState[i, j]);
                }
            }
        }

        private int CountAliveNeighbours(int x, int y)
        {
            int count = 0;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && nx < Rows && ny >= 0 && ny < Cols)
                    {
                        if (_cells[nx, ny].IsAlive)
                            count++;
                    }
                }
            }

            return count;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                _isRunning = !_isRunning;

                if (_isRunning)
                    timer1.Start();
                else
                    timer1.Stop();
            }
        }

        
        private void Render()
        {
            Bitmap bmp = new Bitmap(pictureBox1.ClientSize.Width,
                                    pictureBox1.ClientSize.Height);

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bmp;

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        int x = 25 + j * Gap;
                        int y = 25 + i * Gap;

                        Brush brush = _cells[i, j].IsAlive
                            ? Brushes.Black
                            : Brushes.White;

                        g.FillRectangle(brush, x, y, CellSize, CellSize);
                        g.DrawRectangle(Pens.Gray, x, y, CellSize, CellSize);
                    }
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            int col = (me.X - 25) / Gap;
            int row = (me.Y - 25) / Gap;

            if (row >= 0 && row < Rows && col >= 0 && col < Cols)
            {
                _cells[row, col].ApplyState(!_cells[row, col].IsAlive);
                Render();
            }
        }
    }
}
