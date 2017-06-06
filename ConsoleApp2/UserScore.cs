using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class UserScore
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public int RndNum { get; set; }

        public override string ToString()
        {
            return Name + ":" + Score;
        }
    }
}
