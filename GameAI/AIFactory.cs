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

        /// <summary>
        /// 判断输赢
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int ClacWhoWin(int x, int y)
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
                    return ok;
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
                    return ok;
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
                    if (x + i < Width && y + i < Height)
                    {
                        if (Cells[new Point(x + i, y + i)] == User)
                        {
                            ok++;
                        }
                        else
                        {
                            add = false;
                        }
                    }
                    
                }
                if (sub)
                {
                    if (x - i >= 0 && y - i >= 0)
                    {
                        if (Cells[new Point(x - i, y - i)] == User)
                        {
                            ok++;

                        }
                        else
                        {
                            sub = false;
                        }
                    }
                    
                }
                if (ok == 5)
                {
                    return ok;
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
                    if (x + i < Width && y - i >= 0)
                    {
                        if (Cells[new Point(x + i, y - i)] == User)
                        {
                            ok++;
                        }
                        else
                        {
                            add = false;
                        }
                    }
                   
                }
                if (sub)
                {
                    if (x - i >= 0&& y + i < Width)
                    {
                        if (Cells[new Point(x - i, y + i)] == User)
                        {
                            ok++;

                        }
                        else
                        {
                            sub = false;
                        }
                    }
                    
                }
                if (ok == 5)
                {
                    return ok;
                }
            }
            return 0;
        }

        /// <summary>
        /// 计算这个点的分数
        /// </summary>
        /// <param name="u"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int ClacScore(int u, int x, int y)
        {
            int ok = 0;
            var add = true;
            var sub = true;
            //横排
            for (int i = 0; i < Width; i++)
            {
                if (i == 0)
                {
                    ok++;
                    continue;
                }
                if (sub)
                {
                    if (x - i >= 0)
                    {
                        if (Cells[new Point(x - i, y)] == u)
                        {
                            ok++;
                        }
                        else
                        {
                            sub = false;
                        }
                    }

                }
                if (add)
                {
                    if (x + i < Width)
                    {
                        if (Cells[new Point(x + i, y)] == u)
                        {
                            ok++;
                        }
                        else
                        {
                            add = false;
                        }
                    }

                }

            }
            var ok1 = 0;
            add = true;
            sub = true;
            //竖排
            for (int i = 0; i < Height; i++)
            {
                if (i == 0)
                {
                    ok1++;
                    continue;
                }
                if (sub)
                {
                    if (y - i >= 0)
                    {
                        if (Cells[new Point(x, y - i)] == u)
                        {
                            ok1++;
                        }
                        else
                        {
                            sub = false;
                        }
                    }

                }
                if (add)
                {
                    if (y + i < Height)
                    {
                        if (Cells[new Point(x, y + i)] == u)
                        {
                            ok1++;
                        }
                        else
                        {
                            add = false;
                        }
                    }

                }
            }
            var ok2 = 0;
            add = true;
            sub = true;
            //左斜             
            for (int i = 0; i < Width; i++)
            {

                if (i == 0)
                {
                    ok2++; continue;
                }
                if (add)
                {
                    if (x + i < Width && y + i < Height)
                    {
                        if (Cells[new Point(x + i, y + i)] == u)
                        {
                            ok2++;
                        }
                        else
                        {
                            add = false;
                        }
                    }

                }
                if (sub)
                {
                    if (x - i >= 0 && y - i >= 0)
                    {
                        if (Cells[new Point(x - i, y - i)] == u)
                        {
                            ok2++;

                        }
                        else
                        {
                            sub = false;
                        }
                    }

                }

            }
            add = true;
            sub = true;

            //右斜
            var ok3 = 0;
            for (int i = 0; i < Width; i++)
            {

                if (i == 0)
                {
                    ok3++; continue;
                }
                if (add)
                {
                    if (x + i < Width && y - i >= 0)
                    {
                        if (Cells[new Point(x + i, y - i)] == u)
                        {
                            ok3++;
                        }
                        else
                        {
                            add = false;
                        }
                    }

                }
                if (sub)
                {
                    if (x - i >= 0 && y + i < Width)
                    {
                        if (Cells[new Point(x - i, y + i)] == u)
                        {
                            ok3++;

                        }
                        else
                        {
                            sub = false;
                        }
                    }

                }
            }
            List<int> list = new List<int>() { ok, ok1, ok2, ok3 };
            return list.OrderByDescending(p => p).First();
        }
        /// <summary>
        /// 获取AI点
        /// </summary>
        /// <returns></returns>
        public static Point GetBestPoint()
        {
            Dictionary<Point, int> _scorePoint = new Dictionary<Point, int>();
            //先把已经占掉的点黑掉
            foreach (var cell in Cells)
            {
                _scorePoint.Add(cell.Key, 0);
                if (cell.Value != 0)
                {
                    _scorePoint[cell.Key] = -1;
                    continue;
                }
                //计算分数            
                //优先走我的籽，一个籽10分
                var score = ClacScore(User, cell.Key.X, cell.Key.Y);
                //其次堵别人的籽,一个籽15分
                var otherscore = ClacScore(User == 1 ? 2 : 1, cell.Key.X, cell.Key.Y);

                
                _scorePoint[cell.Key] = score > otherscore? score:otherscore;
            }

            return _scorePoint.OrderByDescending(p => p.Value).First().Key;
        }
    }
}
