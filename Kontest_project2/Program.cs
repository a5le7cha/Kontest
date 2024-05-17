using System;
using System.Collections.Generic;
using System.IO;

namespace Kontest_project2
{
    class Program
    {
        //задача С. Встреча выпускников
        static void Main(string[] args)
        {
            int n, m;

            //List<Edge> listEdge = new List<Edge>();
            int[,] listEdge;
            long[] listResiding; //список количества проживающих в i-ом городе

            using (StreamReader rd = new StreamReader("input.txt"))
            {
                string[] line = rd.ReadLine().Split(' ');

                n = int.Parse(line[0]);
                m = int.Parse(line[1]);

                listResiding = new long[n];
                listEdge = new int[3,m];

                line = rd.ReadLine().Split(' ');

                for(int i = 0;i<n; i++)
                {
                    listResiding[i] = int.Parse(line[i]);
                }

                for (int i = 0; i < m; i++)
                {
                    line = rd.ReadLine().Split(' ');
                    listEdge[0, i] = int.Parse(line[0]);
                    listEdge[1, i] = int.Parse(line[1]);
                    listEdge[2, i] = int.Parse(line[2]);
                   
                    //listEdge.Add(new Edge() { StartEdge = int.Parse(line[0]), EndEdge = int.Parse(line[1]), Weight = int.Parse(line[2]) });
                }
            }


            n = m;/*listEdge.GetLength(1);*/

            long[,] matrixDistance = new long[n + 1, n + 1];

            for (int i = 1; i <= n; i++)
            {
                matrixDistance[i, i] = 0;
            }

            for (int i = 1; i <= n; i++)
            {
                matrixDistance[listEdge[0, i - 1], listEdge[1, i - 1]] = listEdge[2, i - 1];
                matrixDistance[listEdge[1, i - 1], listEdge[0, i - 1]] = listEdge[2, i - 1];
            }


            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    //if (i == j) continue;
                    if (matrixDistance[i, j] == 0 && i != j) matrixDistance[i, j] = 10000000;
                }
            }

            for (int k = 1; k < n; k++)
            {
                for (int i = 1; i < n; i++)
                {
                    for (int j = 1; j < n; j++)
                    {
                        if (matrixDistance[i, j] > matrixDistance[i, k] + matrixDistance[k, j])
                            matrixDistance[i, j] = matrixDistance[i, k] + matrixDistance[k, j];
                    }
                }
            }

            //long[,] matDis = SearchFloydWarshall(listEdge);
            //long[] sum = new long[listResiding.Length];

            long[] result = new long[listResiding.Length];
            

            for (int j = 1; j <= listResiding.Length; j++)
            {
                for (int i = 0; i < listResiding.Length; i++)
                {
                    result[j - 1] += matrixDistance[i + 1, j] * listResiding[i];
                }
            }

            //sum = MultiMatrix(listResiding, matrixDistance);

            long min = result[0];
            for(int i = 1; i< result.Length; i++)
            {
                if (min > result[i]) min = result[i];
            }

            Console.WriteLine(min);

            Console.ReadKey();
        }
    }
}