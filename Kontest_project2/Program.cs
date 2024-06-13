using System;
using System.Collections.Generic;
using System.IO;

namespace Kontest_project2
{
    struct Flight
    {
        public int TimeBegin;   
        public int TimeEnd;     
        public int DesStstion;  
    }

    class Program
    {
        static void Main()
        {
            List<List<Flight>> rasp = new List<List<Flight>>();

            int N, F, m;
            using (var rd = new StreamReader("input.txt"))
            {
                string[] line = rd.ReadLine().Split(' ');

                N = int.Parse(line[0]);
                F = int.Parse(line[1]);

                line = rd.ReadLine().Split(' ');
                m = int.Parse(line[0]);


                for (int i = 0; i <= N; i++)
                {
                    rasp.Add(new List<Flight>());
                }

                for (int i = 0; i < m; i++)
                {
                    string[] inputs = rd.ReadLine().Split(' ');
                    int len = int.Parse(inputs[0]);
                    int prevStation = int.Parse(inputs[1]);
                    int prevTime = int.Parse(inputs[2]);

                    for (int j = 3; j < 2*len; j+=2)
                    {
                        //inputs = rd.ReadLine().Split(' ');
                        int curStation = int.Parse(inputs[j]);
                        int curTime = int.Parse(inputs[j + 1]);

                        rasp[prevStation].Add(new Flight { TimeBegin = prevTime, TimeEnd = curTime, DesStstion = curStation });

                        prevStation = curStation;
                        prevTime = curTime;
                    }
                }
            }

            const int INF = int.MaxValue;
            int[] minTime = new int[N + 1];
            bool[] isFinal = new bool[N + 1];

            for (int i = 0; i < minTime.Length; i++)
            {
                minTime[i] = INF;
            }

            minTime[1] = 0;

            while (true)
            {
                int minSt = -1;
                int minT = INF;

                for (int i = 1; i <= N; i++)
                {
                    if (!isFinal[i] && minTime[i] < minT)
                    {
                        minT = minTime[i];
                        minSt = i;
                    }
                }

                if (minT == INF)
                    break;

                isFinal[minSt] = true;

                foreach (var edge in rasp[minSt])
                {
                    if (edge.TimeBegin >= minTime[minSt])
                    {
                        if (edge.TimeEnd < minTime[edge.DesStstion])
                        {
                            minTime[edge.DesStstion] = edge.TimeEnd;
                        }
                    }
                }
            }

            using(var wr = new StreamWriter("output.txt"))
            {
                if (minTime[F] == INF)
                    wr.WriteLine("-1");
                else
                    wr.WriteLine(minTime[F]);
            }
            

            //Console.ReadKey();
        }
    }
}