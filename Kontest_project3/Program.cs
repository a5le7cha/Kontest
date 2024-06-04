using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontest_project3
{
    class Program
    {
        private static int GetMinDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; v++)
            {
                if (!shortestPathTreeSet[v] && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        public static int Dijkstra(int[,] graph, int source, int verticesCount)
        {
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];

            for (int i = 0; i < verticesCount; i++)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int count = 0; count < verticesCount - 1; count++)
            {
                int u = GetMinDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < verticesCount; v++)
                {
                    if (!shortestPathTreeSet[v] && graph[u, v] != 0 && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + graph[u, v];
                    }
                }
            }

            int max = int.MinValue;
            for(int i = 0; i<distance.Length; i++)
            {
                if (distance[i] > max) max = distance[i];
            }

            return max;
        }

        static void Main(string[] args)
        {
            //нужно списать и там где ребра не указаны сделать их равными 1, достроить граф до клики
        }
    }
}