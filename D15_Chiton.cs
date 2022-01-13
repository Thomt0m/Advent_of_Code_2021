using System;
using static Advent_of_Code.Utility;

namespace Advent_of_Code
{
    class D15_Chiton
    {
        /*
        --- Day 15: Chiton ---
        You've almost reached the exit of the cave, but the walls are getting closer together. Your submarine can barely still fit, though; the main problem is that the walls of the cave are covered in chitons, and it would be best not to bump any of them.

        The cavern is large, but has a very low ceiling, restricting your motion to two dimensions. The shape of the cavern resembles a square; a quick scan of chiton density produces a map of risk level throughout the cave (your puzzle input). For example:

        1163751742
        1381373672
        2136511328
        3694931569
        7463417111
        1319128137
        1359912421
        3125421639
        1293138521
        2311944581

        You start in the top left position, your destination is the bottom right position, and you cannot move diagonally. The number at each position is its risk level; to determine the total risk of an entire path, add up the risk levels of each position you enter (that is, don't count the risk level of your starting position unless you enter it; leaving it adds no risk to your total).

        Your goal is to find a path with the lowest total risk. In this example, a path with the lowest total risk is highlighted here:

        1163751742
        1381373672
        2136511328
        3694931569
        7463417111
        1319128137
        1359912421
        3125421639
        1293138521
        2311944581

        The total risk of this path is 40 (the starting position is never entered, so its risk is not counted).

        What is the lowest total risk of any path from the top left to the bottom right?

        Your puzzle answer was 503.

        The first half of this puzzle is complete! It provides one gold star: *

        --- Part Two ---
        Now that you know how to find low-risk paths in the cave, you can try to find your way out.

        The entire cave is actually five times larger in both dimensions than you thought; the area you originally scanned is just one tile in a 5x5 tile area that forms the full map. Your original map tile repeats to the right and downward; each time the tile repeats to the right or downward, all of its risk levels are 1 higher than the tile immediately up or left of it. However, risk levels above 9 wrap back around to 1. So, if your original map had some position with a risk level of 8, then that same position on each of the 25 total tiles would be as follows:

        8 9 1 2 3
        9 1 2 3 4
        1 2 3 4 5
        2 3 4 5 6
        3 4 5 6 7

        Each single digit above corresponds to the example position with a value of 8 on the top-left tile. Because the full map is actually five times larger in both dimensions, that position appears a total of 25 times, once in each duplicated tile, with the values shown above.

        Here is the full five-times-as-large version of the first example above, with the original map in the top left corner highlighted:

        11637517422274862853338597396444961841755517295286
        13813736722492484783351359589446246169155735727126
        21365113283247622439435873354154698446526571955763
        36949315694715142671582625378269373648937148475914
        74634171118574528222968563933317967414442817852555
        13191281372421239248353234135946434524615754563572
        13599124212461123532357223464346833457545794456865
        31254216394236532741534764385264587549637569865174
        12931385212314249632342535174345364628545647573965
        23119445813422155692453326671356443778246755488935
        22748628533385973964449618417555172952866628316397
        24924847833513595894462461691557357271266846838237
        32476224394358733541546984465265719557637682166874
        47151426715826253782693736489371484759148259586125
        85745282229685639333179674144428178525553928963666
        24212392483532341359464345246157545635726865674683
        24611235323572234643468334575457944568656815567976
        42365327415347643852645875496375698651748671976285
        23142496323425351743453646285456475739656758684176
        34221556924533266713564437782467554889357866599146
        33859739644496184175551729528666283163977739427418
        35135958944624616915573572712668468382377957949348
        43587335415469844652657195576376821668748793277985
        58262537826937364893714847591482595861259361697236
        96856393331796741444281785255539289636664139174777
        35323413594643452461575456357268656746837976785794
        35722346434683345754579445686568155679767926678187
        53476438526458754963756986517486719762859782187396
        34253517434536462854564757396567586841767869795287
        45332667135644377824675548893578665991468977611257
        44961841755517295286662831639777394274188841538529
        46246169155735727126684683823779579493488168151459
        54698446526571955763768216687487932779859814388196
        69373648937148475914825958612593616972361472718347
        17967414442817852555392896366641391747775241285888
        46434524615754563572686567468379767857948187896815
        46833457545794456865681556797679266781878137789298
        64587549637569865174867197628597821873961893298417
        45364628545647573965675868417678697952878971816398
        56443778246755488935786659914689776112579188722368
        55172952866628316397773942741888415385299952649631
        57357271266846838237795794934881681514599279262561
        65719557637682166874879327798598143881961925499217
        71484759148259586125936169723614727183472583829458
        28178525553928963666413917477752412858886352396999
        57545635726865674683797678579481878968159298917926
        57944568656815567976792667818781377892989248891319
        75698651748671976285978218739618932984172914319528
        56475739656758684176786979528789718163989182927419
        67554889357866599146897761125791887223681299833479

        Equipped with the full map, you can now find a path from the top left corner to the bottom right corner with the lowest total risk:

        11637517422274862853338597396444961841755517295286
        13813736722492484783351359589446246169155735727126
        21365113283247622439435873354154698446526571955763
        36949315694715142671582625378269373648937148475914
        74634171118574528222968563933317967414442817852555
        13191281372421239248353234135946434524615754563572
        13599124212461123532357223464346833457545794456865
        31254216394236532741534764385264587549637569865174
        12931385212314249632342535174345364628545647573965
        23119445813422155692453326671356443778246755488935
        22748628533385973964449618417555172952866628316397
        24924847833513595894462461691557357271266846838237
        32476224394358733541546984465265719557637682166874
        47151426715826253782693736489371484759148259586125
        85745282229685639333179674144428178525553928963666
        24212392483532341359464345246157545635726865674683
        24611235323572234643468334575457944568656815567976
        42365327415347643852645875496375698651748671976285
        23142496323425351743453646285456475739656758684176
        34221556924533266713564437782467554889357866599146
        33859739644496184175551729528666283163977739427418
        35135958944624616915573572712668468382377957949348
        43587335415469844652657195576376821668748793277985
        58262537826937364893714847591482595861259361697236
        96856393331796741444281785255539289636664139174777
        35323413594643452461575456357268656746837976785794
        35722346434683345754579445686568155679767926678187
        53476438526458754963756986517486719762859782187396
        34253517434536462854564757396567586841767869795287
        45332667135644377824675548893578665991468977611257
        44961841755517295286662831639777394274188841538529
        46246169155735727126684683823779579493488168151459
        54698446526571955763768216687487932779859814388196
        69373648937148475914825958612593616972361472718347
        17967414442817852555392896366641391747775241285888
        46434524615754563572686567468379767857948187896815
        46833457545794456865681556797679266781878137789298
        64587549637569865174867197628597821873961893298417
        45364628545647573965675868417678697952878971816398
        56443778246755488935786659914689776112579188722368
        55172952866628316397773942741888415385299952649631
        57357271266846838237795794934881681514599279262561
        65719557637682166874879327798598143881961925499217
        71484759148259586125936169723614727183472583829458
        28178525553928963666413917477752412858886352396999
        57545635726865674683797678579481878968159298917926
        57944568656815567976792667818781377892989248891319
        75698651748671976285978218739618932984172914319528
        56475739656758684176786979528789718163989182927419
        67554889357866599146897761125791887223681299833479

        The total risk of this path is 315 (the starting position is still never entered, so its risk is not counted).

        Using the full map, what is the lowest total risk of any path from the top left to the bottom right?

        Your puzzle answer was 2853.

        Both parts of this puzzle are complete! They provide two gold stars: **
        */

        public D15_Chiton(string[] input)
        {
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D15: recieved invalid input"); return; }

            Console.WriteLine("---- Day 15, Chiton ----" + "\n");


            // Part 1
            int[,] map = ConvertStringArrayToInt2DArray(input);
            Point start = new Point(0, 0);
            Point end = new Point(map.GetLength(0) - 1, map.GetLength(1) - 1);
            DijkstraSolver dSolver = new DijkstraSolver(map);
            long cost = dSolver.GetCost(start, end);
            Console.WriteLine("1. Lowest risk to reach bottom right = " + cost);


            // Part 2
            int[,] mapXL = GetPart2Extension(map);
            end = new Point(mapXL.GetLength(0) - 1, mapXL.GetLength(1) - 1);
            dSolver.NewMap(mapXL);
            cost = dSolver.GetCost(start, end);
            Console.WriteLine("2. Lowest risk to reach bottom right = " + cost);


            Console.WriteLine("\n\n");
        }








        private int[,] GetPart2Extension(int[,] map, int sizeX = 5)
        {
            sizeX = Math.Clamp(sizeX, 1, 10);
            int[,] mapXL = new int[map.GetLength(0) * sizeX, map.GetLength(1) * sizeX];

            int[] numbers = new int[9] { 9, 1, 2, 3, 4, 5, 6, 7, 8 };

            int M = map.GetLength(0);
            int N = map.GetLength(1);

            for (int m = 0; m < M; m++)
            {
                for (int n = 0; n < N; n++)
                {
                    for (int xM = 0; xM < sizeX; xM++)
                    {
                        for (int xN = 0; xN < sizeX; xN++)
                        {
                            mapXL[m + xM * M, n + xN * N] = numbers[(map[m, n] + xM + xN) % 9];
                        }
                    }
                }
            }

            return mapXL;
        }







        private bool[,] ConvertRouteToBool2DArray(List<Point> route)
        {
            int maxM = 0;
            int maxN = 0;
            foreach (Point p0 in route)
            {
                if (p0.M > maxM) { maxM = p0.M; }
                if (p0.N > maxN) { maxN = p0.N; }
            }

            bool[,] map = new bool[maxM + 1, maxN + 1];

            foreach (Point p1 in route)
            {
                map[p1.M, p1.N] = true;
            }
            return map;
        }

        private int[,] GetRouteCostIncreaseMap(List<Point> route, int[,] map)
        {
            int cost = 0;
            int[,] costMap = new int[map.GetLength(0), map.GetLength(1)];
            foreach (Point p in route)
            {
                cost += map[p.M, p.N];
                costMap[p.M, p.N] = cost;
            }
            return costMap;
        }



        private void PrintMap(long[,] map)
        {
            Console.WriteLine("Printing Map");

            int longestValue = 0;
            string s;
            for (int m = 0; m < map.GetLength(0); m++)
            {
                for (int n = 0; n < map.GetLength(1); n++)
                {
                    s = map[m, n].ToString();
                    if (s.Length > longestValue) { longestValue = s.Length; }
                }
            }
            longestValue += 1;

            string line;
            for (int m = 0; m < map.GetLength(0); m++)
            {
                line = "";
                for (int n = 0; n < map.GetLength(1); n++)
                {
                    line += (map[m, n].ToString()).PadRight(longestValue, ' ');
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }















        

        private string[] ExampleInput
        {
            get
            {
                return new string[]
                {
                    "1163751742",
                    "1381373672",
                    "2136511328",
                    "3694931569",
                    "7463417111",
                    "1319128137",
                    "1359912421",
                    "3125421639",
                    "1293138521",
                    "2311944581"
                };
            }
        }


        private struct Point
        {
            public int M = 0;
            public int N = 0;
            public Point() { }
            public Point(int m, int n)
            {
                M = m;
                N = n;
            }
            public Point(Point p)
            {
                M = p.M;
                N = p.N;
            }

            public bool Equals(Point p)
            {
                return (M == p.M && N == p.N);
            }
        }


        private class DijkstraSolver
        {
            /// <summary>
            /// Map to traverse
            /// </summary>
            private int[,] Map;
            /// <summary>
            /// Map containing the risk/cost incurred when visiting a cell
            /// </summary>
            private int[,] RiskMap;
            /// <summary>
            /// Map logging the cost needed to reach each point
            /// </summary>
            private long[,] CostMap;
            /// <summary>
            /// Map logging which points have been visited and which haven't
            /// </summary>
            private bool[,] Visited;
            /// <summary>
            /// Map containing the coordinates of the point(cell) that provided the cheapest route to a cell
            /// </summary>
            private Point[,] BacktrackMap;
            /// <summary>
            /// Contains all points that are to be visited
            /// </summary>
            private List<Point> Queue;

            /// <summary>
            /// Offset for all neighbouring Points
            /// </summary>
            private Point[] Neighbours = new Point[4] { new Point(0, -1), new Point(-1, 0), new Point(0, 1), new Point(1, 0) };

            /* Neigbours with diagonal movement
            private Point[] Neighbours = new Point[8] { new Point(-1, -1), new Point(-1, 0), new Point(-1, 1), new Point(0, -1), new Point(0, 1), new Point(1, -1), new Point(1, 0), new Point(1, 1)};
            */



            private Point Start;
            private Point End;
            private bool isCalculated;



            public DijkstraSolver(int[,] map)
            {
                Map = new int[map.GetLength(0), map.GetLength(1)];
                Map = (int[,])map.Clone();
                RiskMap = GetCopyInt2DArraysWithBuffer(map);
                CostMap = new long[RiskMap.GetLength(0), RiskMap.GetLength(1)];
                Visited = new bool[RiskMap.GetLength(0), RiskMap.GetLength(1)];
                BacktrackMap = new Point[RiskMap.GetLength(0), RiskMap.GetLength(1)];
                Queue = new List<Point>();

                Start = new Point(-1, -1);
                End = new Point(-1, -1);
                isCalculated = false;
            }

            public void NewMap(int[,] map)
            {
                Map = new int[map.GetLength(0), map.GetLength(1)];
                Map = (int[,])map.Clone();
                RiskMap = GetCopyInt2DArraysWithBuffer(map);
                CostMap = new long[RiskMap.GetLength(0), RiskMap.GetLength(1)];
                Visited = new bool[RiskMap.GetLength(0), RiskMap.GetLength(1)];
                BacktrackMap = new Point[RiskMap.GetLength(0), RiskMap.GetLength(1)];

                Start = new Point(-1, -1);
                End = new Point(-1, -1);
                isCalculated = false;
            }




            public void Calculate(Point start, Point end)
            {
                Start = new Point(start);
                End = new Point(end);

                // Set all points on the riskMap to the largest value possible
                ResetCostMap();

                // Set points on the buffer edge to true, so they will not be visited, and the rest to false
                ResetVisited();

                // Clear all backtrack points
                ResetBacktrackMap();

                // Offset 'start' and 'end' to adjust for the buffer edge
                start.M += 1;
                start.N += 1;
                end.M += 1;
                end.N += 1;

                // Set the cost of the starting-point to 0
                CostMap[start.M, start.N] = 0;

                // Clear the Queue of points to visited
                Queue = new List<Point>();
                Queue.Add(start);


                long lowestCost;
                int indexLowest;
                Point point;
                Point nbrPoint;
                long newCost;
                int iterations = 0;
                int maxIters = 400000;
                bool isEndVisited = false;
                while (!isEndVisited && Queue.Count > 0 && iterations < maxIters)
                {
                    // Find the cheapest point in the queue
                    lowestCost = int.MaxValue;
                    indexLowest = -1;
                    for (int p0 = 0; p0 < Queue.Count; p0++)
                    {
                        if (GetCost(Queue[p0]) < lowestCost)
                        {
                            lowestCost = GetCost(Queue[p0]);
                            indexLowest = p0;
                        }
                    }

                    // If no cheapest point was found, break. (should not happen)
                    if (indexLowest == -1)
                    {
                        System.Diagnostics.Debug.WriteLine("D15: DijkstraSolver.GetCostMap() failed to find cheapest point in Queue");
                        break;
                    }

                    // Set the cheapest point as the current point, register that it has been visited, and remove it from the Queue
                    point = new Point(Queue[indexLowest]);
                    Visited[point.M, point.N] = true;
                    Queue.RemoveAt(indexLowest);

                    // Foreach of the neighbouring points
                    foreach (Point n0 in Neighbours)
                    {
                        nbrPoint = new Point(point.M + n0.M, point.N + n0.N);

                        // Compare the currently known cheapest cost with the cost of reaching said neighbour via the current point, if it is lower then update the cost of the neighbour 
                        newCost = GetCost(point) + GetRisk(nbrPoint);
                        if (newCost < GetCost(nbrPoint))
                        {
                            CostMap[nbrPoint.M, nbrPoint.N] = newCost;
                            BacktrackMap[nbrPoint.M, nbrPoint.N] = point;
                        }

                        // If the neighbour has not been visited yet, add it to the Queue
                        if (!IsVisited(nbrPoint) && !Queue.Contains(nbrPoint))
                        {
                            Queue.Add(nbrPoint);
                        }
                    }



                    iterations++;
                    /* Debug, write every 100th iteration to Console
                    if (iterations % 100 == 0)
                    {
                        Console.WriteLine("Dijkstra iterations " + iterations);
                    }
                    */
                }

                isCalculated = iterations < maxIters;
            }





            public long GetCost(Point start, Point end)
            {
                CheckIsCalculated(start, end);
                return CostMap[end.M + 1, end.N + 1];
            }


            public List<Point> GetCheapestRoute(Point start, Point end)
            {
                CheckIsCalculated(start, end);

                start.M += 1;
                start.N += 1;
                end.M += 1;
                end.N += 1;

                List<Point> backtrack = new List<Point>();
                backtrack.Add(end);
                Point nextP;
                int iterations = 0;
                bool isStartReached = false;
                while (!isStartReached && iterations < 2000)
                {
                    nextP = GetBacktrack(backtrack.Last());
                    backtrack.Add(nextP);
                    isStartReached = start.Equals(nextP);
                    iterations++;
                }

                List<Point> route = new List<Point>();
                for (int i = backtrack.Count - 1; i >= 0; i--)
                {
                    route.Add(new Point(backtrack[i].M - 1, backtrack[i].N - 1));
                }
                return route;
            }




            public long[,] GetCostMapClean(Point start, Point end)
            {
                CheckIsCalculated(start, end);

                // Copy the CostMap to a new map that does not have the buffer edge
                long[,] costMapClean = new long[Map.GetLength(0), Map.GetLength(1)];
                for (int copyM = 0; copyM < costMapClean.GetLength(0); copyM++)
                {
                    for (int copyN = 0; copyN < costMapClean.GetLength(1); copyN++)
                    {
                        costMapClean[copyM, copyN] = CostMap[copyM + 1, copyN + 1];
                    }
                }
                return costMapClean;
            }







            private int GetRisk(Point p)
            {
                return RiskMap[p.M, p.N];
            }

            private long GetCost(Point p)
            {
                return CostMap[p.M, p.N];
            }

            private bool IsVisited(Point p)
            {
                return Visited[p.M, p.N];
            }

            private Point GetBacktrack(Point p)
            {
                return BacktrackMap[p.M, p.N];
            }


            private Point Add(Point p0, Point p1)
            {
                return new Point(p0.M + p1.M, p0.N + p1.N);
            }




            private void CheckIsCalculated(Point start, Point end)
            {
                if (!isCalculated || !Start.Equals(start) || !End.Equals(end))
                {
                    Calculate(start, end);
                }
            }



            private int[,] GetCopyInt2DArraysWithBuffer(int[,] orig)
            {
                int[,] copy = new int[orig.GetLength(0) + 2, orig.GetLength(1) + 2];
                for (int m = 0; m < orig.GetLength(0); m++)
                {
                    for (int n = 0; n < orig.GetLength(1); n++)
                    {
                        copy[m + 1, n + 1] = orig[m, n];
                    }
                }

                int M = copy.GetLength(0) - 1;
                int N = copy.GetLength(1) - 1;
                copy[0,0] = int.MaxValue;
                copy[0, N] = int.MaxValue;
                copy[M, 0] = int.MaxValue;
                copy[M, N] = int.MaxValue;
                for (int m0 = 1; m0 < copy.GetLength(0); m0++)
                {
                    copy[m0, 0] = int.MaxValue;
                    copy[m0, N] = int.MaxValue;
                }
                for (int n0 = 1; n0 < copy.GetLength(1); n0++)
                {
                    copy[0, n0] = int.MaxValue;
                    copy[M, n0] = int.MaxValue;
                }
                return copy;
            }

            /// <summary>
            /// Set all points in the 2D-array to int.MaxValue
            /// </summary>
            /// <param name="input"></param>
            private void ResetCostMap()
            {
                for (int m = 0; m < CostMap.GetLength(0); m++)
                {
                    for (int n = 0; n < CostMap.GetLength(1); n++)
                    {
                        CostMap[m, n] = int.MaxValue;
                    }
                }
            }

            /// <summary>
            /// Set all points along the edge of the 2D-array to true, and all others to false
            /// </summary>
            /// <param name="input"></param>
            private void ResetVisited()
            {
                int M = Visited.GetLength(0) - 1;
                int N = Visited.GetLength(1) - 1;

                Visited = new bool[M + 1, N + 1];

                Visited[0, 0] = true;
                Visited[0, N] = true;
                Visited[M, 0] = true;
                Visited[M, N] = true;

                for (int m = 1; m < Visited.GetLength(0) - 1; m++)
                {
                    Visited[m, 0] = true;
                    Visited[m, N] = true;
                }
                for (int n = 1; n < Visited.GetLength(1) - 1; n++)
                {
                    Visited[0, n] = true;
                    Visited[M, n] = true;
                }
            }

            private void ResetBacktrackMap()
            {
                for (int m = 0; m < BacktrackMap.GetLength(0); m++)
                {
                    for (int n = 0; n < BacktrackMap.GetLength(1); n++)
                    {
                        BacktrackMap[m, n] = new Point();
                    }
                }
            }


            public int[,] GetMap()
            {
                return (int[,])Map.Clone();
            }

            public int[,] GetRiskMap()
            {
                return (int[,])RiskMap.Clone();
            }

            public long[,] GetCostMap()
            {
                return (long[,])CostMap.Clone();
            }

            public bool[,] GetVisited()
            {
                return (bool[,])Visited.Clone();
            }

            public Point[,] GetBacktrackMap()
            {
                return (Point[,])BacktrackMap.Clone();
            }

        }


    }
}
