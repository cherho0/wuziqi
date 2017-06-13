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
        public static Dictionary<Point, int> cells = new Dictionary<Point, int>();
        public static int user = 1;
        private const int Width = 15;
        private const int Height = 15;
        int big = 30;
        int top = 50;
        int left = 50;
        public static bool ClacWhoWin(int x, int y)
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

    }
}
