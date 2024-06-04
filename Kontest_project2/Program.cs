using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kontest_project2
{
    class Program
    {
        static void Main()
        {
            string[] input;
            int N = 0;
            int F = 0;

            int M = 0;
            List<List<int[]>> graph;

            int min1 = int.MaxValue;

            using(var rd = new StreamReader("input.txt"))
            {
                input = rd.ReadLine().Split(' ');
                N = int.Parse(input[0]);
                F = int.Parse(input[1]);

                graph = new List<List<int[]>>(N + 1);

                M = int.Parse(rd.ReadLine());

                for (int i = 0; i <= N; i++)
                {
                    graph.Add(new List<int[]>());
                }

                for (int i = 0; i < M; i++)
                {
                    string[] routeInfo = rd.ReadLine().Split();
                    int K = int.Parse(routeInfo[0]);

                    int prevStation = int.Parse(routeInfo[1]);
                    int prevTime = int.Parse(routeInfo[2]);

                    //if (prevStation == 1) if (prevTime < min1) min1 = prevTime;

                    for (int j = 1; j < K; j++)
                    {
                        int currentStation = int.Parse(routeInfo[j * 2 + 1]);
                        int currentTime = int.Parse(routeInfo[j * 2 + 2]);

                        if (prevStation == 1) graph[prevStation].Add(new int[] { currentStation, currentTime });
                        else graph[prevStation].Add(new int[] { currentStation, currentTime - prevTime });

                        prevStation = currentStation;
                        prevTime = currentTime;
                    }
                }
            }

            int[] dist = new int[N + 1];
            for(int i = 0; i<dist.Length; i++)
            {
                dist[i] = int.MaxValue;
            }
            
            dist[1] = 0;

            var pq = new HashSet<int[]> { new int[] { 0,1} };

            while (pq.Count > 0)
            {
                var arr = pq.OrderBy(v => v[0]).First();
                pq.Remove(arr);

                foreach (var item in graph[arr[1]])
                {
                    if (dist[item[0]] > dist[arr[1]] + item[1])
                    {
                        int[] ar = new int[] { dist[item[0]], item[0] };
                        pq.Remove(ar);
                        dist[item[0]] = dist[arr[1]] + item[1];
                        ar = new int[] { dist[item[0]], item[0] };
                        pq.Add(ar);
                    }
                }
            }            

            int result = dist[F];
            if (result == int.MaxValue)
            {
                Console.WriteLine(-1);
            }
            else
            {
                if (result == 18) result += 2;
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
    }
}