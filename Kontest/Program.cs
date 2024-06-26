﻿//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace Kontest
//{
//    class Program
//    {
//        //задача A. Max Stream
//        static void Main(string[] args)
//        {
//            int n, m, MaxStream = 0;
//            List<Edge> listEdge = new List<Edge>();

//            using (var rd = new StreamReader("input.txt"))
//            {
//                string[] line = rd.ReadLine().Split(' ');
//                n = int.Parse(line[0]);
//                m = int.Parse(line[1]);

//                for (int i = 0; i < m; i++)
//                {
//                    line = rd.ReadLine().Split(' ');
//                    listEdge.Add(new Edge() { StartEdge = int.Parse(line[0]), EndEdge = int.Parse(line[1]), Throughput = int.Parse(line[2]) });
//                }
//            }

//            int MinThroughput = -1;

//            //путь по ребрам
//            List<Edge> Path = PathSearchByUsingBFS(listEdge, 1, n);

//            while (Path != null)
//            {
//                MinThroughput = Path[0].Throughput;

//                foreach (var item in Path)
//                    if (item.Throughput < MinThroughput)
//                        MinThroughput = item.Throughput;

//                MaxStream += MinThroughput;

//                foreach (var item in Path)
//                    item.Throughput -= MinThroughput;

//                for (int i = 0; i < listEdge.Count; i++)
//                {
//                    if (Path.Contains(listEdge[i]))
//                    {
//                        listEdge[i].Throughput = Path[Path.IndexOf(listEdge[i])].Throughput;
//                        if (listEdge[i].Throughput == 0)
//                        {
//                            listEdge.Remove(listEdge[i]);
//                            i--;
//                        }
//                    }
//                }

//                Path = PathSearchByUsingBFS(listEdge, 1, n);
//            }

//            Console.WriteLine($"{MaxStream}");

//            Console.ReadKey();
//        }


//        //в метод будет передаваться всегда список актуальных ребер, без удаленной 
//        static List<Edge> PathSearchByUsingBFS(List<Edge> listEdge, int StartVertex, int EndVertex)
//        {
//            //массив, в котором будет лежать номера верншин пути в порядке
//            //от стартовой до конечной
//            List<int> Path = new List<int>();

//            // список: 0 - номер вершины, 1 - метка, 2 - пердок (у стартовой предок -1)
//            List<Vertex> listVertex = new List<Vertex>();
//            List<int> ListedVertex = new List<int>();

//            Queue<Vertex> VertexQueue = new Queue<Vertex>();

//            Vertex start_vertex = new Vertex() { Number = StartVertex, Marker = 1, Parent = null };

//            VertexQueue.Enqueue(start_vertex);
//            listVertex.Add(start_vertex);
//            ListedVertex.Add(StartVertex);

//            Vertex ExtractableVertex = null;

//            while (VertexQueue.Count != 0)
//            {
//                ExtractableVertex = VertexQueue.Dequeue();
//                ExtractableVertex.Marker = 2;

//                for (int i = 0; i < listEdge.Count; i++)
//                {
//                    if (listEdge[i].StartEdge == ExtractableVertex.Number)
//                    {
//                        //в список ListedVertex заносятся те, что уже добавлялись в очередь,
//                        //так как могут быть разные предки, то такое использование
//                        if (!ListedVertex.Contains(listEdge[i].EndEdge))
//                        {
//                            Vertex AddVertex = new Vertex() { Number = listEdge[i].EndEdge, Marker = 1, Parent = ExtractableVertex };

//                            ListedVertex.Add(AddVertex.Number);
//                            VertexQueue.Enqueue(AddVertex);
//                            listVertex.Add(AddVertex);
//                        }
//                    }
//                }
//            }

//            int indexEndVertex = ListedVertex.IndexOf(EndVertex);

//            if (indexEndVertex == -1) return null;

//            Vertex vertexPath = listVertex[indexEndVertex];

//            while (vertexPath.Parent != null)
//            {
//                Path.Add(vertexPath.Number);
//                vertexPath = vertexPath.Parent;
//            }

//            Path.Add(vertexPath.Number);

//            Path.Reverse();

//            List<Edge> pathEdge = new List<Edge>();

//            for (int i = 0; i < Path.Count - 1; i++)
//            {
//                foreach (var item in listEdge)
//                {
//                    if (item.StartEdge == Path[i] && item.EndEdge == Path[i + 1])
//                    {
//                        pathEdge.Add(item);
//                        break;
//                    }
//                }
//            }

//            return pathEdge;
//        }
//    }
//}


using System;
using System.Collections.Generic;

namespace project
{
    class RoadRepair
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int minReversibleRoads = int.MaxValue;

        static void Main()
        {
            string[] input = Console.ReadLine().Split();
            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);

            graph = new List<int>[n+1];
            visited = new bool[n+1];

            for (int i = 0; i <= n; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < m; i++)
            {
                string[] road = Console.ReadLine().Split();
                int city1 = int.Parse(road[0]);
                int city2 = int.Parse(road[1]);

                graph[city1].Add(city2);
            }

            for (int i = 1; i < n; i++)
            {
                for(int j = 1; j< visited.Length; j++)
                {
                    visited[j] = false;
                }

                DFS(i, i, 0);
            }

            Console.WriteLine(minReversibleRoads);

            Console.ReadKey();
        }

        private static void DFS(int start, int current, int reversibleRoads)
        {
            if (current != start && graph[current].Contains(start))
            {
                minReversibleRoads = Math.Min(minReversibleRoads, reversibleRoads);
                return;
            }

            visited[current] = true;

            foreach (int neighbor in graph[current])
            {
                if (!visited[neighbor])
                {
                    if (current != start) // Check if reversing direction
                    {
                        DFS(start, neighbor, reversibleRoads + 1);
                    }
                    else
                    {
                        DFS(start, neighbor, reversibleRoads);
                    }
                }
            }

            visited[current] = false;
        }
    }

}