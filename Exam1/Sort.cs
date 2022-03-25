using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    public class Sort
    {
        public static void Second()
        {
            List<int> list = new List<int>() { 2, 99, 0, 56, 8, 1 };

            list.Sort();
            Console.WriteLine(string.Join(",", list));

            list.Reverse();
            Console.WriteLine(string.Join(",", list));

        }
    }
}
