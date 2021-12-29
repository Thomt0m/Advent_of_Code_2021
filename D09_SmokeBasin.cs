using System;

namespace Advent_of_Code
{
    class D09_SmokeBasin
    {
        /*
        --- Day 9: Smoke Basin ---
        These caves seem to be lava tubes. Parts are even still volcanically active; small hydrothermal vents release smoke into the caves that slowly settles like rain.

        If you can model how the smoke flows through the caves, you might be able to avoid it and be that much safer. The submarine generates a heightmap of the floor of the nearby caves for you (your puzzle input).

        Smoke flows to the lowest point of the area it's in. For example, consider the following heightmap:

        2199943210
        3987894921
        9856789892
        8767896789
        9899965678

        Each number corresponds to the height of a particular location, where 9 is the highest and 0 is the lowest a location can be.

        Your first goal is to find the low points - the locations that are lower than any of its adjacent locations. Most locations have four adjacent locations (up, down, left, and right); locations on the edge or corner of the map have three or two adjacent locations, respectively. (Diagonal locations do not count as adjacent.)

        In the above example, there are four low points, all highlighted: two are in the first row (a 1 and a 0), one is in the third row (a 5), and one is in the bottom row (also a 5). All other locations on the heightmap have some lower adjacent location, and so are not low points.

        The risk level of a low point is 1 plus its height. In the above example, the risk levels of the low points are 2, 1, 6, and 6. The sum of the risk levels of all low points in the heightmap is therefore 15.

        Find all of the low points on your heightmap. What is the sum of the risk levels of all low points on your heightmap?

        Your puzzle answer was 425.

        The first half of this puzzle is complete! It provides one gold star: *

        --- Part Two ---
        Next, you need to find the largest basins so you know what areas are most important to avoid.

        A basin is all locations that eventually flow downward to a single low point. Therefore, every low point has a basin, although some basins are very small. Locations of height 9 do not count as being in any basin, and all other locations will always be part of exactly one basin.

        The size of a basin is the number of locations within the basin, including the low point. The example above has four basins.

        The top-left basin, size 3:

        2199943210
        3987894921
        9856789892
        8767896789
        9899965678

        The top-right basin, size 9:

        2199943210
        3987894921
        9856789892
        8767896789
        9899965678

        The middle basin, size 14:

        2199943210
        3987894921
        9856789892
        8767896789
        9899965678

        The bottom-right basin, size 9:

        2199943210
        3987894921
        9856789892
        8767896789
        9899965678

        Find the three largest basins and multiply their sizes together. In the above example, this is 9 * 14 * 9 = 1134.

        What do you get if you multiply together the sizes of the three largest basins?

        Your puzzle answer was 1135260.

        Both parts of this puzzle are complete! They provide two gold stars: **
        */




        int RiskLevel = 0;
        List<int[]> LowPoints = new List<int[]>();

        public D09_SmokeBasin(string[] input)
        {
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D09: recieved invalid input"); return; }

            Console.WriteLine("---- Day 9, Smoke Basin ----" + "\n");


            // Part 1, calculate the total risk-level of the low-points
            byte[,] map = GetMap(input);
            Console.WriteLine("1. Sum of all risk levels = " + GetRiskLevel(map));




            // Part 2, calculate the product of the sizes of the three largest basins
            List<int> basins = GetBasins(map);

            // Get the three largest basins (can also be done by sorting the basins-list by size and taking the first three values)
            int one = 0;
            int two = 0;
            int three = 0;
            for (int i = 0; i < basins.Count; i++)
            {
                if (basins[i] > one)
                {
                    three = two;
                    two = one;
                    one = basins[i];
                }
                else if (basins[i] > two)
                {
                    three = two;
                    two = basins[i];
                }
                else if (basins[i] > three)
                {
                    three = basins[i];
                }
            }
            Console.WriteLine($"2. Product of the three largest basins = {one * two * three}");

            Console.WriteLine("\n\n");
        }


        private byte[,] GetMap(string[] input)
        {
            byte[,] map = new byte[input.Length, input[0].Length];
            for (int m = 0; m < map.GetLength(0); m++)
            {
                for (int n = 0; n < map.GetLength(1); n++)
                {
                    map[m, n] = byte.Parse(input[m][n].ToString());
                }
            }
            return map;
        }


        private int GetRiskLevel(byte[,] map)
        {
            RiskLevel = 0;
            LowPoints = new List<int[]>();
            int mapM = map.GetLength(0) - 1;
            int mapN = map.GetLength(1) - 1;

            // Corners
            if (map[0, 0] < map[0, 1] && map[0, 0] < map[1, 0]) { IncreaseRiskLevel(map[0, 0], new int[] {0, 0}, "[0, 0]"); }
            if (map[mapM, 0] < map[mapM - 1, 0] && map[mapM, 0] < map[mapM, 1]) { IncreaseRiskLevel(map[mapM, 0], new int[] {mapM, 0}, $"[{mapM}, 0]"); }
            if (map[0, mapN] < map[1, mapN] && map[0, mapN] < map[0, mapN - 1]) { IncreaseRiskLevel(map[0, mapN], new int[] {0, mapN}, $"[0, {mapN}]"); }
            if (map[mapM, mapN] < map[mapM - 1, mapN] && map[mapM, mapN] < map[mapM, mapN - 1]) { IncreaseRiskLevel(map[mapM, mapN], new int[] {mapM, mapN}, $"[{mapM}, {mapN}]"); }

            // Edges
            for (int edgeM = 1; edgeM < map.GetLength(0) - 1; edgeM++)
            {
                if (map[edgeM, 0] < map[edgeM - 1, 0] && map[edgeM, 0] < map[edgeM, 1] && map[edgeM, 0] < map[edgeM + 1, 0]) 
                {
                    IncreaseRiskLevel(map[edgeM, 0], new int[] {edgeM, 0}, $"[{edgeM}, 0]");
                }
                if (map[edgeM, mapN] < map[edgeM - 1, mapN] && map[edgeM, mapN] < map[edgeM + 1, mapN] && map[edgeM, mapN] < map[edgeM, mapN - 1])
                {
                    IncreaseRiskLevel(map[edgeM, mapN], new int[] {edgeM, mapN}, $"[{edgeM}, {mapN}");
                }
            }
            for (int edgeN = 1; edgeN < map.GetLength(1) - 1; edgeN++)
            {
                if (map[0, edgeN] < map[0, edgeN + 1] && map[0, edgeN] < map[1, edgeN] && map[0, edgeN] < map[0, edgeN - 1])
                {
                    IncreaseRiskLevel(map[0, edgeN], new int[] {0, edgeN}, $"[0, {edgeN}]");
                }
                if (map[mapM, edgeN] < map[mapM - 1, edgeN] && map[mapM, edgeN] < map[mapM, edgeN + 1] && map[mapM, edgeN] < map[mapM, edgeN - 1])
                {
                    IncreaseRiskLevel(map[mapM, edgeN], new int[] {mapM, edgeN}, $"[{mapM}, {edgeN}");
                }
            }

            // All other cells
            for (int m0 = 1; m0 < map.GetLength(0) - 1; m0++)
            {
                for (int n0 = 1; n0 < map.GetLength(1) - 1; n0++)
                {
                    if (map[m0, n0] < map[m0 - 1, n0] &&
                        map[m0, n0] < map[m0, n0 + 1] &&
                        map[m0, n0] < map[m0 + 1, n0] &&
                        map[m0, n0] < map[m0, n0 - 1])
                    {
                        IncreaseRiskLevel(map[m0, n0], new int[] {m0, n0}, $"[{m0}, {n0}]");
                    }
                }
            }
            return RiskLevel;
        }

        private void IncreaseRiskLevel(int level, int[] coor, string msg = "")
        {
            //if (msg != "") { Console.WriteLine("IncreaseRiskLevel: level = " + level + ", " + msg); }
            RiskLevel += level + 1;
            LowPoints.Add(coor);
        }




        private List<int> GetBasins(byte[,] mapBase)
        {
            byte[,] map = new byte[mapBase.GetLength(0) + 2, mapBase.GetLength(1) + 2];
            bool[,] pointVisited = new bool[map.GetLength(0), map.GetLength(1)];
            int mapM = map.GetLength(0) - 1;
            int mapN = map.GetLength(1) - 1;

            // Copy the content on mapBase to map, with an offset of [1, 1]
            for (int cM = 1; cM < map.GetLength(0) - 1; cM++)
            {
                for (int cN = 1; cN < map.GetLength(1) - 1; cN++)
                {
                    map[cM, cN] = mapBase[cM - 1, cN - 1];
                }
            }
            // Set the 'buffer' edge of the new map to 9, and the buffer-edge of pointVisited to true
            map[0, 0] = 9;
            map[mapM, mapN] = 9;
            pointVisited[0,0] = true;
            pointVisited[mapM, mapN] = true;
            for (int cEdgeM = 0; cEdgeM < map.GetLength(0); cEdgeM++)
            {
                map[cEdgeM, 0] = 9;
                map[cEdgeM, mapN] = 9;
                pointVisited[cEdgeM, 0] = true;
                pointVisited[cEdgeM, mapN] = true;
            }
            for (int cEdgeN = 0; cEdgeN < map.GetLength(1); cEdgeN++)
            {
                map[0, cEdgeN] = 9;
                map[mapM, cEdgeN] = 9;
                pointVisited[0, cEdgeN] = true;
                pointVisited[mapM, cEdgeN] = true;
            }


            List<int> basins = new List<int>();
            if (LowPoints.Count == 0) { GetRiskLevel(map); }

            foreach (int[] lPoint in LowPoints)
            {
                basins.Add(GetBasinSize(map, pointVisited, lPoint[0] + 1, lPoint[1] + 1));
                //Console.WriteLine($"Basin at [{lPoint[0]}, {lPoint[1]}], size = {basins.Last()}");
            }

            return basins;
        }


        private int GetBasinSize(byte[,] map, bool[,] pointVisited, int m, int n)
        {
            int size = 0;
            if (m < 0 || n < 0) { System.Diagnostics.Debug.WriteLine($"D09: GetBasinSize() recieved invalid value for 'm' and/or 'n' - [{m}, {n}]"); return size; }

            // If the point has not yet been visited, and its map-value is not 9
            if (!pointVisited[m, n] && map[m, n] != 9)
            {
                // This point is part of the basin, increase the size by one
                size++;
                pointVisited[m, n] = true;
                // Move on to all adjacent cells, and if their also part of the basin, increase the size further
                size += GetBasinSize(map, pointVisited, m - 1, n);
                size += GetBasinSize(map, pointVisited, m, n + 1);
                size += GetBasinSize(map, pointVisited, m + 1, n);
                size += GetBasinSize(map, pointVisited, m, n - 1);
            }
            return size;
        }








        private void TestGetRiskLevel()
        {
            string[] inputTest = new string[]
            {
                "2199943210",
                "3987894921",
                "9856789892",
                "8767896789",
                "9899965678"
            };
            byte[,] map1 = GetMap(inputTest);
            Console.WriteLine("Risk map1 = " + GetRiskLevel(map1));
        }

        private void PrintMap(byte[,] map)
        {
            Console.WriteLine("\nPrinting map:");
            for (int m = 0; m < map.GetLength(0); m++)
            {
                string row = "";
                for (int n = 0; n < map.GetLength(1); n++)
                {
                    row += map[m, n];
                }
                Console.WriteLine(row);
            }
        }
    }
}
