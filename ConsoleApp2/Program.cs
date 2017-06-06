using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static int avg = 0;
        static void Main(string[] args)
        {


            List<UserScore> users = new List<UserScore>();
            for (int i = 0; i < 20; i++)
            {
                Random rnd = new Random(DateTime.Now.Millisecond);

                var u = new UserScore
                {
                    Name = "你" + i,
                    Score = rnd.Next(50, 100),
                    RndNum = rnd.Next(1, 100)
                };
                users.Add(u);
                //Console.WriteLine(u.ToString());
                Thread.Sleep(3);
            }


            var total = users.Sum(p => p.Score);
            Console.WriteLine("总分：" + total);
            avg = total / 4;
            Console.WriteLine("平均：" + avg);


            Dictionary<int, List<UserScore>> dicts
                = new Dictionary<int, List<UserScore>>();

            for (int i = 0; i < 4; i++)
            {
                dicts.Add(i, new List<UserScore>());
            }
            //for (int i = 0; i < 5; i++)
            //{
            //    var list = users.OrderByDescending(p => p.Score).ThenBy(p => p.RndNum).ToList();
            //    var teams = dicts.OrderBy(p => p.Value.Sum(x => x.Score)).ToList();
            //    for (int j = 0; j < 4; j++)
            //    {
            //        var item = list[0];
            //        teams[j].Value.Add(item);
            //        list.Remove(item);
            //        users.Remove(item);
            //    }

            //}

            var cha = ClacCha(dicts, avg);
            users = users.OrderBy(p => p.Score).ToList();
            //var first = users.First();
            Grouping(users, 0,new UserScore[20]);

            foreach (var item in dicts)
            {
                Console.WriteLine($"{item.Key }sum->{item.Value.Sum(p => p.Score)}");
            }

            Console.WriteLine("=======================================================");


           // var id = GetRoom();
            //Console.WriteLine(id);
            Console.ReadLine();

        }
        static List<UserScore[]> teams = new List<UserScore[]>();
        static int mincha = int.MaxValue;
        static int claccount = 0;
        private static void Grouping(List<UserScore> users, int first,UserScore [] result)
        {
            if (users.Count == 0)
            {
                claccount++;
                //teams.Add(result);
                Dictionary<int, List<UserScore>> dicts
               = new Dictionary<int, List<UserScore>>();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 5*i; j < 5 * i+5; j++)
                    {
                        dicts[i] = new List<UserScore>();
                        dicts[i].Add(result[j]);
                    }
                }
                var cja = ClacCha(dicts, avg);
                if (cja<mincha)
                {
                    mincha = cja;
                    foreach (var item in result)
                    {
                        Console.Write(item.Score + ",");
                    }
                    Console.WriteLine("最小差为 " + mincha + "     已计算"+claccount);
                    Console.WriteLine("=========================");
                }

            }
            int flag;
            for (int i = 0; i < users.Count; i++)
            {

                var node = users[i];

                flag = 0; // 同一位置，重复的数字只调用一次  
                for (int n = 0; n < i; n++)
                {
                    if (node.Name == users[n].Name)
                    {
                        flag = 1;
                        break;
                    }
                }

                if (flag == 0)
                {
                    result[first] = node;
                    List<UserScore> newRefer = new List<UserScore>();
                    for (int j = 0; j < users.Count; j++)
                    {
                        newRefer.Add(users[j]);
                    }
                    newRefer.RemoveAt(i); // 深拷贝一个新的input集合，把正在处理的元素，从当前的input集合中拿掉  
                    Grouping(newRefer, first + 1, result);
                }
            }
        }

        

        private static int ClacCha(Dictionary<int, List<UserScore>> dicts, int avg)
        {
            var cha = 0;
            foreach (var item in dicts)
            {
                cha += Math.Abs(item.Value.Sum(p => p.Score) - avg);
            }
            return cha;
        }

        private static int GetRoom()
        {
            var cts = new CancellationTokenSource();

            List<Task<int>> tasks = new List<Task<int>>();
            tasks.Add(

                Task.Factory.StartNew<int>((x) =>
                {
                    Thread.Sleep(200);
                    Console.WriteLine(1);
                    return 1;
                }, cts.Token, cts.Token));
            tasks.Add(Task.Factory.StartNew<int>((x) =>
            {
                var xts = ((CancellationToken)x);
                Thread.Sleep(300);
                Console.WriteLine(2);
                return 2;
            }, cts.Token, cts.Token));
            tasks.Add(Task.Factory.StartNew<int>((x) =>
            {
                var xts = ((CancellationToken)x);
                Thread.Sleep(400);
                Console.WriteLine(3);
                return 3;
            }, cts.Token, cts.Token));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var ret = Task.WhenAny(tasks);
            ret.ContinueWith((x) =>
            {
                cts.Cancel();
            });

            tasks.Clear();
            sw.Stop();
            Console.WriteLine(" cost:" + sw.ElapsedMilliseconds);
            return ret.Result.Result;
        }
    }
}
