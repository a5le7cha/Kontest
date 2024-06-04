using System;
using System.Collections.Generic;
using System.IO;

namespace Kontest_project3
{
    class myclass
    {
        static void ain()
        {
            int reverseRoad = int.MaxValue;
            string[] input;
            int N = 0;
            int M = 0;
            int[][] roads;
            List<int>[] listAdjacency;
            using (var rd = new StreamReader("input.txt"))
            {
                input = rd.ReadLine().Split();
                N = int.Parse(input[0]);
                M = int.Parse(input[1]);

                listAdjacency = new List<int>[N + 1];

                for (int i = 1; i <= N; i++)
                {
                    listAdjacency[i] = new List<int>();
                }

                roads = new int[M][];

                for (int i = 0; i < M; i++)
                {
                    string[] roadInput = rd.ReadLine().Split();
                    roads[i] = new int[] { int.Parse(roadInput[0]), int.Parse(roadInput[1]) };

                    listAdjacency[int.Parse(roadInput[0])].Add(int.Parse(roadInput[1]));
                    listAdjacency[int.Parse(roadInput[1])].Add(int.Parse(roadInput[0]));
                }

                for (int i = 1; i < listAdjacency.Length; i++)
                {
                    listAdjacency[i].Sort();
                }
            }

            for (int i = 1; i < listAdjacency.Length; i++)
            {
                int num = DFS(i, roads, listAdjacency);
                if (num < reverseRoad) reverseRoad = num;
            }

            Console.WriteLine(reverseRoad);
            Console.ReadKey();
        }

        static int DFS(int start, int[][] roads, List<int>[] listAdjacency)
        {
            int countRevRoad = 0, min = int.MinValue;
            Stack<int> stack = new Stack<int>();
            int[] markerVer = new int[listAdjacency.Length + 1];

            for (int i = 1; i < listAdjacency.Length; i++)
            {
                markerVer[i] = 0;//маркируем 0
            }

            stack.Push(start);

            while (stack.Count > 0)
            {
                int topVer = stack.Pop();
                markerVer[topVer] = 2;

                for (int i = 1; i < listAdjacency[topVer].Count; i++)
                {
                    if (markerVer[listAdjacency[topVer][i]] == 0)
                    {
                        stack.Push(listAdjacency[topVer][i]);
                        markerVer[listAdjacency[topVer][i]] = 1;
                    }
                }

                if (stack.Count == 0) break;
                int topPeek = stack.Peek();

                for (int i = 0; i < roads.Length; i++)
                {
                    if (roads[i][1] == topVer && roads[i][0] == topPeek)
                    {
                        countRevRoad++;
                        break;
                    }
                }
            }

            return countRevRoad;
        }
    }
    class Floyda_Warshall
    {
        static int INF = int.MaxValue;

        static int FloydWarshall(int N, int[][] roads)
        {
            int[,] dist = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    dist[i, j] = i == j ? 0 : INF;
                }
            }

            foreach (var road in roads)
            {
                int a = road[0] - 1;
                int b = road[1] - 1;
                dist[a, b] = 1;
            }

            for (int k = 1; k < N; k++)
            {
                for (int i = 1; i < N; i++)
                {
                    for (int j = 1; j < N; j++)
                    {
                        if (dist[i, j] > dist[i, k] + dist[k, j]) dist[i, j] = dist[i, k] + dist[k, j];
                    }
                }
            }

            int minReverses = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i != j)
                    {
                        if (dist[i, j] == INF)
                        {
                            return INF;
                        }
                        minReverses = Math.Max(minReverses, dist[i, j]);
                    }
                }
            }

            return minReverses;
        }

        static void point()
        {
            string[] input;
            int N = 0;
            int M = 0;
            int[][] roads;
            using (var rd = new StreamReader("input.txt"))
            {
                input = rd.ReadLine().Split();
                N = int.Parse(input[0]);
                M = int.Parse(input[1]);

                roads = new int[M][];

                for (int i = 0; i < M; i++)
                {
                    string[] roadInput = rd.ReadLine().Split();
                    roads[i] = new int[] { int.Parse(roadInput[0]), int.Parse(roadInput[1]) };
                }
            }

            int minReverses = FloydWarshall(N, roads);
            Console.WriteLine(minReverses);
            Console.ReadKey();
        }
    }
}
