using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kontest_project2
{
    class Program
    {
        //задача С. Встреча выпускников
        static void Main(string[] args)
        {
            int n, m;

            List<Edge> listEdge = new List<Edge>();
            List<int> listResiding = new List<int>(); //список количества проживающих в i-ом городе

            using (var rd = new StreamReader("input.txt"))
            {
                string[] line = rd.ReadLine().Split(' ');

                n = int.Parse(line[0]);
                m = int.Parse(line[1]);

                line = rd.ReadLine().Split(' ');

                for(int i = 0;i<n; i++)
                {
                    listResiding.Add(int.Parse(line[i]));
                }

                for (int i = 0; i < m; i++)
                {
                    line = rd.ReadLine().Split(' ');

                    listEdge.Add(new Edge() { StartEdge = int.Parse(line[0]), EndEdge = int.Parse(line[1]), Weight = int.Parse(line[2]) });
                }
            }



            Console.ReadKey();
        }

        static int[] Search()
        {
            return new int[3];
        }
    }
}