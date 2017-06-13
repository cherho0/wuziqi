using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class GameForm : Form
    {
        private const int Width = 15;
        private const int Height = 15;
        int big = 30;
        int top = 50;
        int left = 50;
        Dictionary<Point, int> cells = new Dictionary<Point, int>();
        int user = 1;
        public GameForm()
        {
            InitializeComponent();
            gamePnl.BackColor = Color.Orange;
            gamePnl.Paint += GamePnl_Paint;
            gamePnl.MouseClick += GamePnl_MouseClick;
            initCells();

        }

        void SetText()
        {
            label1.Text = "AlphaNy   \r\n By Ny" ;
            Graphics g = gamePnl.CreateGraphics();

            Pen pen = GetPen(user);
            Font myFont = new Font("微软雅黑", 12);

            //创建线渐变画刷：   
            LinearGradientBrush myBrush = new 
                LinearGradientBrush(ClientRectangle, Color.Green, 
                Color.Black, LinearGradientMode.Vertical);
              g.DrawString("当前方： ", myFont, myBrush, new RectangleF(0, gamePnl.Height-55, 80, big));  
                       
            g.DrawEllipse(pen, 80, gamePnl.Height-55, big, big);
            g.FillEllipse(pen.Brush, 80, gamePnl.Height - 55, big, big);
        }

        private void initCells()
        {
            cells.Clear();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var p = new Point(x, y);
                    cells.Add(p, 0);
                }
            }
        }

        Pen GetPen(int u)
        {
            if (u == 1)
            {
                Pen pen = new Pen(Color.White);
                pen.Width = 1;
                pen.Brush = new SolidBrush(Color.White);
                return pen;
            }
            else
            {
                Pen pen = new Pen(Color.Black);
                pen.Width = 1;
                pen.Brush = new SolidBrush(Color.Black);
                return pen;
            }
        }

        private void GamePnl_MouseClick(object sender, MouseEventArgs e)
        {
            var x = (e.X - left + 15) / big;
            var y = (e.Y - top + 15) / big;
            var newp = new Point(x, y);
            if (!cells.ContainsKey(newp) || cells[newp] != 0)
            {
                return;
            }
            Pen pen = GetPen(user);
            Graphics g = gamePnl.CreateGraphics();
            cells[newp] = user;
            g.DrawEllipse(pen, x * big + left - big / 2, y * big + top - big / 2, big, big);
            g.FillEllipse(pen.Brush, x * big + left - big / 2, y * big + top - big / 2, big, big);
            var win = ClacWhoWin(x, y);
            if (win)
            {
                MessageBox.Show(user == 1 ? "白方胜" : "黑方胜");
                initCells();
                gamePnl.Refresh();
            }
            user = user == 1 ? 2 : 1;
            SetText();
        }

        private bool ClacWhoWin(int x, int y)
        {
            int ok = 0;
            //横排
            for (int i = 0; i < Width; i++)
            {
                if (cells[new Point(i, y)] == user)
                {
                    ok++;
                }
                if (cells[new Point(i, y)] != user)
                {
                    ok = 0;
                }
                if (ok == 5)
                {
                    return true;
                }
            }
            ok = 0;
            //竖排
            for (int i = 0; i < Height; i++)
            {

                if (cells[new Point(x, i)] == user)
                {
                    ok++;
                }
                if (cells[new Point(x, i)] != user)
                {
                    ok = 0;
                }
                if (ok == 5)
                {
                    return true;
                }
            }
            var add = true;
            var sub = true;
            //左斜             
            for (int i = 0; i < Width; i++)
            {

                if (i == 0)
                {
                    ok++; continue;
                }
                if (add)
                {
                    if (x + i >= Width || y + i >= Height)
                    {
                        continue;
                    }
                    if (cells[new Point(x + i, y + i)] == user)
                    {
                        ok++;
                    }
                    else
                    {
                        add = false;
                    }
                }
                if (sub)
                {
                    if (x - i < 0 || y - i < 0)
                    {
                        continue;
                    }
                    if (cells[new Point(x - i, y - i)] == user)
                    {
                        ok++;

                    }
                    else
                    {
                        sub = false;
                    }
                }
                if (ok == 5)
                {
                    return true;
                }
            }
            add = true;
            sub = true;

            //右斜
            ok = 0;
            for (int i = 0; i < Width; i++)
            {

                if (i == 0)
                {
                    ok++; continue;
                }
                if (add)
                {
                    if (x + i >= Width || y - i < 0)
                    {
                        continue;
                    }
                    if (cells[new Point(x + i, y - i)] == user)
                    {
                        ok++;
                    }
                    else
                    {
                        add = false;
                    }
                }
                if (sub)
                {
                    if (x - i < 0 || y + i >= Width)
                    {
                        continue;
                    }
                    if (cells[new Point(x - i, y + i)] == user)
                    {
                        ok++;

                    }
                    else
                    {
                        sub = false;
                    }
                }
                if (ok == 5)
                {
                    return true;
                }
            }
            return false;
        }

        private void GamePnl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            pen.Width = 1;
            var rows = Width;
            var cols = Height;


            for (int i = 0; i < rows + 1; i++)
            {
                g.DrawLine(pen, left, big * i + top, big * Width + left, big * i + top);
            }

            for (int i = 0; i < cols + 1; i++)
            {
                g.DrawLine(pen, big * i + left, top, big * i + left, big * Width + top);
            }

            foreach (var item in cells)
            {
                if (item.Value == 1)
                {
                    pen = GetPen(1);
                    g.DrawEllipse(pen, item.Key.X * big + left - big / 2, item.Key.Y * big + top - big / 2, big, big);
                    g.FillEllipse(pen.Brush, item.Key.X * big + left - big / 2, item.Key.Y * big + top - big / 2, big, big);
                }
                else if (item.Value == 2)
                {
                    pen = GetPen(2);
                    g.DrawEllipse(pen, item.Key.X * big + left - big / 2, item.Key.Y * big + top - big / 2, big, big);
                    g.FillEllipse(pen.Brush, item.Key.X * big + left - big / 2, item.Key.Y * big + top - big / 2, big, big);

                }
            }
            SetText();

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            initCells();
            gamePnl.Refresh();

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 b = new AboutBox1();
            b.ShowDialog();
        }

        protected override void OnClosed(EventArgs e)
        {
            Application.Exit();
        }
    }
}
