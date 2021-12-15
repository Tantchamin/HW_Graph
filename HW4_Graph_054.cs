using System;
using System.Collections.Generic;

namespace HW_Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            int CityNumber = Cities();
            int[,] graphArray = new int[CityNumber, CityNumber];
            string[] cityArray = new string[CityNumber];
            CityName(cityArray);
            InputCityArray(graphArray);
            string destination = InputDestination(cityArray);
            int destinationCounter = CheckDestination(cityArray, destination);

            FindDistance t = new FindDistance();
            t.dijkstra(graphArray, destinationCounter, CityNumber);
            
        }

        static int Cities()
        {
            Console.Write("Input Cities Number: ");
            int Cities = int.Parse(Console.ReadLine());
            return Cities;
        }

        static void CityName(string[] cityArray)
        {

            for (int i = 0; i < cityArray.Length; i++)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                cityArray[i] = name;
            }

        }

        static void InputCityArray(int[,] graphArray)
        {

            // row
            for (int i = 0; i < graphArray.GetLength(0); i++)
            {
                // column
                for (int j = 0; j < graphArray.GetLength(1); j++)
                {
                    
                    if (j > i)
                    {
                        graphArray[i, j] = 0;
                    }
                    else if (j == i)
                    {
                        graphArray[i, j] = -1;
                    }
                    else
                    {
                        graphArray[i, j] = int.Parse(Console.ReadLine());
                    }

                }

            }

            // upper copy lower
            // row
            for (int i = 0; i < graphArray.GetLength(0); i++)
            {
                // column
                for (int j = 0; j < graphArray.GetLength(1); j++)
                {
                    graphArray[i, j] = graphArray[j, i];

                }
            }

        }

        // input destination
        static string InputDestination(string[] cityArray)
        {
            Console.Write("Input destination: ");
            string cityname = Console.ReadLine();
            string destination = CheckDestinationArray(cityname, cityArray);

            return destination;
        }

        static string CheckDestinationArray(string cityname, string[] cityArray)
        {
            int counter = 0;

            while(counter == 0)
            {
                for(int i = 0; i < cityArray.Length; i++)
                {
                    if(cityname == cityArray[i])
                    {
                        counter++;
                    }
                }

                if (counter == 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please input new city.");
                    cityname = Console.ReadLine();
                }
            }

            return cityname;
        }

        // return destination number
        static int CheckDestination(string[] cityArray, string destination)
        {
            int counter = 0;
            for (int i = 0; i < cityArray.Length; i++)
            {
                if (cityArray[i] == destination)
                {
                    counter = i;
                }
            }

            return counter;
        }

    }

    class FindDistance
    {
        int minDistance(int[] dist,
                        bool[] sptSet, int V)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        void printStartToEnd(int[] dist)
        {
            Console.Write("Distance: {0}", dist[0]);
        }

        public void dijkstra(int[,] graph, int src, int V)
        {
            int[] dist = new int[V];

            // sptSet[i] will true if vertex i is included in shortest path
            bool[] sptSet = new bool[V];

            // Initialize all distances as INFINITE and stpSet[] as false
            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            // Distance of source vertex from itself is always 0
            dist[src] = 0;

            // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++)
            {
                int u = minDistance(dist, sptSet, V);

                // Mark the picked vertex as processed
                sptSet[u] = true;

                // Update dist value of the adjacent vertices of the picked vertex.
                for (int v = 0; v < V; v++)
                    if (!sptSet[v] && graph[u, v] != -1 &&
                         dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }

            printStartToEnd(dist);
        }

    }

}
