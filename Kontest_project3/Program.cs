using System;
using System.Collections.Generic;
using System.IO;

namespace Kontest_project3
{
    class Program
    {
        public static int Dijkstra(int[,] graph, int source, int verticesCount)
        {
            int[] distance = new int[verticesCount+1];
            bool[] shortestPathTreeSet = new bool[verticesCount+1];

            for (int i = 1; i <= verticesCount; i++)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int count = 1; count <= verticesCount; count++)
            {
                int min = int.MaxValue;
                int minIndex = 0;

                for (int v = 1; v <= verticesCount; v++)
                {
                    if (!shortestPathTreeSet[v] && distance[v] < min)
                    {
                        min = distance[v];
                        minIndex = v;
                    }
                }

                int u = minIndex;
                shortestPathTreeSet[u] = true;

                for (int v = 1; v <= verticesCount; v++)
                {
                    if (!shortestPathTreeSet[v] && graph[u, v] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + graph[u, v];
                    }
                }
            }

            int max = int.MinValue;
            for(int i = 1; i<distance.Length; i++)
            {
                if (distance[i] > max) max = distance[i];
            }

            return max;
        }

        static void Main(string[] args)
        {
            int[,] graph;
            List<KeyValuePair<int, int>> roads = new List<KeyValuePair<int, int>>();
            int N = 0;

            using (var rd = new StreamReader("input.txt"))
            {
                string[] lin = rd.ReadLine().Split(' ');

                N = int.Parse(lin[0]);
                int M = int.Parse(lin[1]);

                graph = new int[N+1, N+1];

                for (int i = 0; i < M; i++)
                {
                    string[] line = rd.ReadLine().Split(' ');

                    int city1 = int.Parse(line[0]);
                    int city2 = int.Parse(line[1]);

                    roads.Add(new KeyValuePair<int, int>(city1, city2));
                }
            }

            for (int i = 1; i < graph.GetLength(0); i++)
            {
                for (int j = 1; j < graph.GetLength(1); j++)
                {
                    graph[i, j] = int.MaxValue;
                }
            }

            for (int i = 0; i<roads.Count; i++)
            {
                graph[roads[i].Key, roads[i].Value] = 0;

                if(!roads.Contains(new KeyValuePair<int, int>(roads[i].Value, roads[i].Key)))
                    graph[roads[i].Value, roads[i].Key] = 1;
            }

            //for (int i = 1; i< graph.GetLength(0); i++)
            //{
            //    for(int j = 1; j < graph.GetLength(1); j++)
            //    {
            //        if (roads.Contains(new KeyValuePair<int, int>(i, j)))
            //        {
            //            graph[i, j] = 0;

            //            if(!roads.Contains(new KeyValuePair<int, int>(j, i)))
            //                graph[j, i] = 1;
            //        }
            //        //else if (i == j)
            //        //{
            //        //    graph[i, j] = 0;
            //        //    graph[j, i] = 0;
            //        //}
            //        //else if (graph[i, j] != 0 && graph[j, i] != 0)
            //        //{
            //        //    graph[i, j] = 0;
            //        //    graph[j, i] = 0;
            //        //}
            //    }
            //}

            int max = int.MinValue;
            for(int i = 1; i<=N; i++)
            {
                var num = Dijkstra(graph, i, N);
                if(max < num) { max = num; }
            }

            Console.WriteLine(max);

            Console.ReadKey();
        }
    }
}