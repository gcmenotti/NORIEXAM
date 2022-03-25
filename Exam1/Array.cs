using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    public class ArrayTest
    {
        public static void First(int n)
        {
            int[] arr = new int[n];

            try
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (arr[j] > arr[j + 1])
                        {
                            var temp = arr[j];
                            arr[j] = arr[j + 1];
                            arr[j + 1] = temp;
                        }
                    }
                }
               
                Console.WriteLine("no tiene mucha complejidad me parece super basico, tiene un bug porque no se utiliza la i en el for y adicional tiene redundancia al asignar valores con el temp y el arr ");

            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }



        }
    }
}
