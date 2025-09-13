using System;
using System.Drawing;
using System.Windows.Forms;

namespace kg_lab1
{
    public partial class MainForm : Form
    {
        private float[,] L;
        private float[,] proection;
        private int cenX;
        private int cenY;
        private Graphics _graphics;

        public MainForm() 
            => InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cenX = Size.Width / 2;
            cenY = Size.Height / 2;
            SetDefaultPosition();
            //Косоугольная проекция
            float[,] p =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { -(float)(Math.Cos(Math.PI/4)), -(float)(Math.Sin(Math.PI/4)), 0, 0},
                { cenX, cenY, 0, 1}
            };
            proection = p;
            DrawL();
        }

        //умножение матриц
        private float[,] Mult(float[,] X, float[,] Y)
        {
            float[,] result = new float[X.GetLength(0), Y.GetLength(1)];
            for (int i = 0; i < X.GetLength(0); i++)
                for (int j = 0; j < Y.GetLength(1); j++)
                    for (int k = 0; k < Y.GetLength(0); k++)
                        result[i, j] += X[i, k] * Y[k, j];
            return result;
        }

        //отрисовка осей
        private void DrawAxis()
        {
            _graphics = CreateGraphics();
            _graphics.Clear(Color.White);
            float[,] Axis =
            {
        { 0, 0, 0, 1},
        { 500, 0, 0, 1},
        { 0, 400, 0, 1},
        { 0, 0, 500, 1},
        { 490, 5, 0, 1},
        { 490, -5, 0, 1},
        { 5, 390, 0, 1},
        { -5, 390, 0, 1},
        { 12, 0, 495, 1},
        { -10, 0, 480, 1}
    };
            Axis = Mult(Axis, proection);

            // Преобразуем Y-координаты для корректного отображения в Windows Forms
            for (int i = 0; i < Axis.GetLength(0); i++)
            {
                Axis[i, 1] = Size.Height - Axis[i, 1];
            }

            #region X
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[1, 0], Axis[1, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[1, 0], Axis[1, 1], Axis[4, 0], Axis[4, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[1, 0], Axis[1, 1], Axis[5, 0], Axis[5, 1]);
            #endregion
            #region Y
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[2, 0], Axis[2, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[2, 0], Axis[2, 1], Axis[6, 0], Axis[6, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[2, 0], Axis[2, 1], Axis[7, 0], Axis[7, 1]);
            #endregion
            #region Z
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[3, 0], Axis[3, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[3, 0], Axis[3, 1], Axis[8, 0], Axis[8, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[3, 0], Axis[3, 1], Axis[9, 0], Axis[9, 1]);
            #endregion
        }
        //начальные значения L
        private void SetDefaultPosition()
        {
            float[,] DefL =
            {
        // Передняя грань буквы L (7 точек)
        { 0, 0, 0, 1 },      // A - 0
        { 0, 60, 0, 1 },     // B - 1
        { 20, 60, 0, 1 },    // C - 2
        { 20, 20, 0, 1 },    // D - 3
        { 60, 20, 0, 1 },    // E - 4
        { 60, 0, 0, 1 },     // F - 5
        { 20, 0, 0, 1 },     // G - 6
        
        // Задняя грань буквы L (7 точек)
        { 0, 0, 10, 1 },     // A' - 7
        { 0, 60, 10, 1 },    // B' - 8
        { 20, 60, 10, 1 },   // C' - 9
        { 20, 20, 10, 1 },   // D' - 10
        { 60, 20, 10, 1 },   // E' - 11
        { 60, 0, 10, 1 },    // F' - 12
        { 20, 0, 10, 1 }     // G' - 13
    };
            L = DefL;
        }

        //отрисовка проекции буквы
        private void DrawL()
        {
            _graphics = CreateGraphics();
            DrawAxis();
            float[,] matrixDraw = Mult(L, proection);

            // Преобразуем Y-координаты для корректного отображения в Windows Forms
            for (int i = 0; i < matrixDraw.GetLength(0); i++)
            {
                matrixDraw[i, 1] = Size.Height - matrixDraw[i, 1];
            }

            // Отрисовка передней грани
            _graphics.DrawLine(Pens.Red, matrixDraw[0, 0], matrixDraw[0, 1], matrixDraw[1, 0], matrixDraw[1, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[1, 0], matrixDraw[1, 1], matrixDraw[2, 0], matrixDraw[2, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[2, 0], matrixDraw[2, 1], matrixDraw[3, 0], matrixDraw[3, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[3, 0], matrixDraw[3, 1], matrixDraw[4, 0], matrixDraw[4, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[4, 0], matrixDraw[4, 1], matrixDraw[5, 0], matrixDraw[5, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[5, 0], matrixDraw[5, 1], matrixDraw[6, 0], matrixDraw[6, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[6, 0], matrixDraw[6, 1], matrixDraw[0, 0], matrixDraw[0, 1]);

            // Отрисовка задней грани
            _graphics.DrawLine(Pens.Red, matrixDraw[7, 0], matrixDraw[7, 1], matrixDraw[8, 0], matrixDraw[8, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[8, 0], matrixDraw[8, 1], matrixDraw[9, 0], matrixDraw[9, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[9, 0], matrixDraw[9, 1], matrixDraw[10, 0], matrixDraw[10, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[10, 0], matrixDraw[10, 1], matrixDraw[11, 0], matrixDraw[11, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[11, 0], matrixDraw[11, 1], matrixDraw[12, 0], matrixDraw[12, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[12, 0], matrixDraw[12, 1], matrixDraw[13, 0], matrixDraw[13, 1]);
            _graphics.DrawLine(Pens.Red, matrixDraw[13, 0], matrixDraw[13, 1], matrixDraw[7, 0], matrixDraw[7, 1]);

            // Соединение передней и задней граней
            for (int i = 0; i < 7; i++)
            {
                _graphics.DrawLine(Pens.Red, matrixDraw[i, 0], matrixDraw[i, 1], matrixDraw[i + 7, 0], matrixDraw[i + 7, 1]);
            }
        }

        //поместить буквы начального размера в центр системы координат
        private void buttonDeffaultPosition_Click(object sender, EventArgs e)
        {
            SetDefaultPosition();
            DrawL();
        }

        //движение вдоль OX в положительном направлении
        private void MovePlusX_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { toMove, 0, 0, 1}
            };
            L = Mult(L, Move);
            DrawL();
        }

        //движение вдоль OX в отрицательном направлении
        private void MoveMinusX_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { -toMove, 0, 0, 1}
            };
            L = Mult(L, Move);
            DrawL();
        }

        //движение вдоль OY в положительном направлении
        private void MovePlusY_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, toMove, 0, 1}
            };
            L = Mult(L, Move);
            DrawL();
        }

        //движение вдоль OY в отрицательном направлении
        private void MoveMinusY_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, -toMove, 0, 1}
            };
            L = Mult(L, Move);
            DrawL();
        }

        //движение вдоль OZ в положительном направлении
        private void MovePlusZ_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, toMove, 1}
            };
            L = Mult(L, Move);
            DrawL();
        }

        //движение вдоль OZ в отрицательном направлении
        private void MoveMinusZ_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, -toMove, 1}
            };
            L = Mult(L, Move);
            DrawL();
        }

        //вращение вокруг OX вправо
        private void RotateRightX_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI /180);
            float[,] Rotate =
            {
                { 1, 0, 0, 0},
                { 0, (float)(Math.Cos(angle)), (float)(Math.Sin(angle)), 0},
                { 0, -(float)(Math.Sin(angle)), (float)(Math.Cos(angle)), 0},
                { 0, 0, 0, 1}
            };
            L = Mult(L, Rotate);
            DrawL();
        }

        //вращение вокруг OX влево
        private void RotateLeftX_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { 1, 0, 0, 0},
                { 0, (float)Math.Cos(angle), -((float)(Math.Sin(angle))), 0},
                { 0, ((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            L = Mult(L, Rotate);
            DrawL();
        }

        //вращение вокруг OY вправо
        private void RotateRightY_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), 0, ((float)(Math.Sin(angle))), 0},
                { 0, 1, 0, 0},
                { -((float)(Math.Sin(angle))), 0, ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            L = Mult(L, Rotate);
            DrawL();
        }

        //вращение вокруг OY влево
        private void RotateLeftY_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), 0, -((float)(Math.Sin(angle))), 0},
                { 0, 1, 0, 0},
                { ((float)(Math.Sin(angle))), 0, ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            L = Mult(L, Rotate);
            DrawL();
        }

        //вращение вокруг OZ вправо
        private void RotateRightZ_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), -((float)(Math.Sin(angle))), 0, 0},
                { ((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };
            L = Mult(L, Rotate);
            DrawL();
        }

        //вращение вокруг OZ влево
        private void RotateLeftZ_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), ((float)(Math.Sin(angle))), 0, 0},
                { -((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };
            L = Mult(L, Rotate);
            DrawL();
        }

        //растяжение
        private void Stretch_Click(object sender, EventArgs e)
        {
            float[,] Stretch =
            {
                { 2, 0, 0, 0},
                { 0, 2, 0, 0},
                { 0, 0, 2, 0},
                { 0, 0, 0, 1}
            };
            L = Mult(L, Stretch);
            DrawL();
        }

        //сжатие
        private void Clench_Click(object sender, EventArgs e)
        {
            float[,] Clench =
            {
                { (float)(0.5), 0, 0, 0},
                { 0, (float)(0.5), 0, 0},
                { 0, 0, (float)(0.5), 0},
                { 0, 0, 0, 1}
            };
            L = Mult(L, Clench);
            DrawL();
        }

        //анимация движения по спирали вдоль OX
        private void taskOX_Click(object sender, EventArgs e)
        {
            int way = 180;
            int count = 0;
            int coef = 3;
            float angle = (float)(10 * Math.PI / 180);
            float[,] Spiral =
            {
                { 1, 0, 0, 0},
                { 0, (float)(Math.Cos(angle)), (float)(Math.Sin(angle)), 0},
                { 0, -(float)(Math.Sin(angle)), (float)(Math.Cos(angle)), 0},
                { 1, 0, 0, 1}
            };
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler((o, ev) =>
            {
                count++;
                L = Mult(L, Spiral);
                DrawL();
                if (count % 3 == 0)
                    timer.Interval += coef;
                if (count == (way / 2)) 
                {
                    Spiral[1, 2] *= -1;
                    Spiral[2, 1] *= -1;
                    Spiral[3, 0] *= -1;
                    coef *= -1;
                }
                if (count == way)
                {
                    Timer t = o as Timer;
                    t.Stop();
                }
            });
            timer.Start();
        }

        //анимация движения по спирали вдоль OY
        private void taskOY_Click(object sender, EventArgs e)
        {
            int way = 180;
            int count = 0;
            int coef = 3;
            float angle = (float)(10 * Math.PI / 180);
            float[,] Spiral =
            {
                { ((float)(Math.Cos(angle))), 0, -((float)(Math.Sin(angle))), 0},
                { 0, 1, 0, 0},
                { ((float)(Math.Sin(angle))), 0, ((float)(Math.Cos(angle))), 0},
                { 0, 1, 0, 1}
            };
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler((o, ev) =>
            {
                count++;
                L = Mult(L, Spiral);
                DrawL();
                if (count % 3 == 0)
                    timer.Interval += coef;
                if (count == (way / 2))
                {
                    Spiral[0, 2] *= -1;
                    Spiral[2, 0] *= -1;
                    Spiral[3, 1] *= -1;
                    coef *= -1;
                }
                if (count == way)
                {
                    Timer t = o as Timer;
                    t.Stop();
                }
            });
            timer.Start();
        }

        //анимация движения по спирали вдоль OZ
        private void taskOZ_Click(object sender, EventArgs e)
        {
            int way = 180;
            int count = 0;
            int coef = 3;
            float angle = (float)(10 * Math.PI / 180);
            float[,] Spiral =
            {
                { ((float)(Math.Cos(angle))), ((float)(Math.Sin(angle))), 0, 0},
                { -((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 1, 1}
            };
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler((o, ev) =>
            {
                count++;
                L = Mult(L, Spiral);
                DrawL();
                if (count % 3 == 0)
                    timer.Interval += coef;
                if (count == (way / 2))
                {
                    Spiral[0, 1] *= -1;
                    Spiral[1, 0] *= -1;
                    Spiral[3, 2] *= -1;
                    coef *= -1;
                }
                if (count == way)
                {
                    Timer t = o as Timer;
                    t.Stop();
                }
            });
            timer.Start();
        }
    }

}
