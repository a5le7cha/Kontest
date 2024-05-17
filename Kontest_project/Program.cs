using System;
using System.Collections.Generic;
using System.IO;

namespace Kontest_project
{
    class Program
    {
        //задача B. Цикл отрицательного веса
        static void Main(string[] args)
        {
            int n, m;

            List<Edge> listEdge = new List<Edge>();
            List<int> listVer = new List<int>();

            using(var rd = new StreamReader("input.txt"))
            {
                string[] line = rd.ReadLine().Split(' ');

                n = int.Parse(line[0]);
                m = int.Parse(line[1]);

                for(int i = 0; i<m; i++)
                {
                    line = rd.ReadLine().Split(' ');

                    listEdge.Add(new Edge() { StartEdge = int.Parse(line[0]), EndEdge = int.Parse(line[1]), Weight = int.Parse(line[2]) });

                    if (!listVer.Contains(int.Parse(line[0])))
                        listVer.Add(int.Parse(line[0]));

                    if (!listVer.Contains(int.Parse(line[1])))
                        listVer.Add(int.Parse(line[1]));
                }
            }

            List<int> dis = new List<int>();
            bool flag = false;

            for (int i = 0; i< listVer.Count; i++)
            {
                dis = SearchBellForda(i, listVer, listEdge);
                if (dis.Count > 1)
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                Console.WriteLine(1);
                Console.WriteLine(dis.Count - 1);
                var item = dis[0];
                dis[0] = dis[1];
                dis[1] = item;
                for (int i = 0; i < dis.Count; i++)
                {
                    Console.Write($"{dis[i]} ");
                }
            }
            else Console.WriteLine(-1);

            

            Console.ReadKey();
        }

        static List<int> SearchBellForda(int startVertex, List<int> listVer, List<Edge> listEdge)
        {
            int[] distance = new int[listVer.Count+1];
            int[] parents = new int[listVer.Count + 1];

            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = 100000;
                parents[i] = i;
            }
                

            distance[startVertex] = 0;
            bool flag = false;
            int reg = startVertex;
            for(int i = 0; i<= listVer.Count; i++)
            {
                for(int j = 0; j<listEdge.Count; j++)
                {
                    if(distance[listEdge[j].EndEdge] > distance[listEdge[j].StartEdge] + listEdge[j].Weight)
                    {
                        if (i == listVer.Count) 
                        { 
                            flag = true;
                            reg = listEdge[j].EndEdge;
                        }

                        distance[listEdge[j].EndEdge] = distance[listEdge[j].StartEdge] + listEdge[j].Weight;
                        parents[listEdge[j].EndEdge] = listEdge[j].StartEdge;
                    }
                }
            }

            List<int> res = new List<int>();
            if (flag)
            {
                int kid = reg, parent = parents[reg];
                while (parent != reg)
                {
                    res.Add(parent);
                    kid = parents[kid];
                    parent = parents[kid];
                }
                if (parent == reg)
                {
                    res.Add(parent);
                    res.Add(kid);
                }
            }
            else res.Add(-1);

            return res;
        }
    }
}