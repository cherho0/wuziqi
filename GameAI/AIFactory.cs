using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI
{
    public class AIFactory
    {
        private static Dictionary<Point, int> _cells = new Dictionary<Point, int>();
        private static int _user = 1;
        private const int Width = 15;
        private const int Height = 15;

        public static Dictionary<Point, int> Cells { get => _cells; set => _cells = value; }
        public static int User { get => _user; set => _user = value; }

        public static bool ClacWhoWin(int x, int y)
        {
            int ok = 0;
            //横排
            for (int i = 0; i < Width; i++)
            {
                if (Cells[new Point(i, y)] == User)
                {
                    ok++;
                }
                if (Cells[new Point(i, y)] != User)
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

                if (Cells[new Point(x, i)] == User)
                {
                    ok++;
                }
                if (Cells[new Point(x, i)] != User)
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
                    if (Cells[new Point(x + i, y + i)] == User)
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
                    if (Cells[new Point(x - i, y - i)] == User)
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
                    if (Cells[new Point(x + i, y - i)] == User)
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
                    if (Cells[new Point(x - i, y + i)] == User)
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

    }
}
