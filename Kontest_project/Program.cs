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

            List<int[]> listEdge = new List<int[]>();
            List<int> listVer = new List<int>();

            using(var rd = new StreamReader("input.txt"))
            {
                string[] line = rd.ReadLine().Split(' ');

                n = int.Parse(line[0]);
                m = int.Parse(line[1]);

                for(int i = 0; i<m; i++)
                {
                    line = rd.ReadLine().Split(' ');
                    int[] edges = new int[3];
                    edges[0] = int.Parse(line[0]);
                    edges[1] = int.Parse(line[1]);
                    edges[2] = int.Parse(line[2]);
                    listEdge.Add(edges);

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
                dis = SearchBellForda(i, n, listEdge);
                if (dis.Count > 1)
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                Console.WriteLine(1);
                Console.WriteLine(dis.Count);
                Console.Write(dis[dis.Count - 1]);
                
                for (int i = 0; i < dis.Count; i++)
                {
                    Console.Write($" {dis[i]}");
                }
            }
            else Console.WriteLine(-1);

            
            Console.ReadKey();
        }

        static List<int> SearchBellForda(int startVertex, int Vercount, List<int[]> listEdge)
        {
            int countV = Vercount;
            int[] distance = new int[countV + 1];
            int[] parents = new int[countV + 1];

            for (int i = 0; i < countV; i++)
            {
                distance[i] = int.MaxValue;
                parents[i] = -1;
            }
                

            distance[startVertex] = 0;
            
            int reg = -1;
            for(int i = 0; i<= countV; i++)
            {
                for(int j = 0; j<listEdge.Count; j++)
                {
                    if(distance[listEdge[j][0]] != int.MaxValue && distance[listEdge[j][1]] > distance[listEdge[j][0]] + listEdge[j][2])
                    {
                        parents[listEdge[j][1]] = listEdge[j][0];
                        distance[listEdge[j][1]] = distance[listEdge[j][0]] + listEdge[j][2];

                        if (i == countV) 
                        {
                            reg = listEdge[j][1];
                            break;
                        }
                    }
                }
            }

            List<int> res = new List<int>();
            if (reg != -1)
            {
                for(int i = 0; i<countV; i++)
                {
                    reg = parents[reg];
                }

                int kid = reg;
                res.Add(kid);
                kid = parents[kid];

                while (kid != reg)
                {
                    res.Add(kid);
                    kid = parents[kid];
                }

                res.Reverse();
            }
            else res.Add(-1);

            return res;
        }
    }
}