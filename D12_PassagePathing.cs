using System;

namespace Advent_of_Code
{
    class D12_PassagePathing
    {
        /*
        --- Day 12: Passage Pathing ---
        With your submarine's subterranean subsystems subsisting suboptimally, the only way you're getting out of this cave anytime soon is by finding a path yourself. Not just a path - the only way to know if you've found the best path is to find all of them.

        Fortunately, the sensors are still mostly working, and so you build a rough map of the remaining caves (your puzzle input). For example:

        start-A
        start-b
        A-c
        A-b
        b-d
        A-end
        b-end

        This is a list of how all of the caves are connected. You start in the cave named start, and your destination is the cave named end. An entry like b-d means that cave b is connected to cave d - that is, you can move between them.

        So, the above cave system looks roughly like this:

            start
            /   \
        c--A-----b--d
            \   /
             end

        Your goal is to find the number of distinct paths that start at start, end at end, and don't visit small caves more than once. There are two types of caves: big caves (written in uppercase, like A) and small caves (written in lowercase, like b). It would be a waste of time to visit any small cave more than once, but big caves are large enough that it might be worth visiting them multiple times. So, all paths you find should visit small caves at most once, and can visit big caves any number of times.

        Given these rules, there are 10 paths through this example cave system:

        start,A,b,A,c,A,end
        start,A,b,A,end
        start,A,b,end
        start,A,c,A,b,A,end
        start,A,c,A,b,end
        start,A,c,A,end
        start,A,end
        start,b,A,c,A,end
        start,b,A,end
        start,b,end

        (Each line in the above list corresponds to a single path; the caves visited by that path are listed in the order they are visited and separated by commas.)

        Note that in this cave system, cave d is never visited by any path: to do so, cave b would need to be visited twice (once on the way to cave d and a second time when returning from cave d), and since cave b is small, this is not allowed.

        Here is a slightly larger example:

        dc-end
        HN-start
        start-kj
        dc-start
        dc-HN
        LN-dc
        HN-end
        kj-sa
        kj-HN
        kj-dc

        The 19 paths through it are as follows:

        start,HN,dc,HN,end
        start,HN,dc,HN,kj,HN,end
        start,HN,dc,end
        start,HN,dc,kj,HN,end
        start,HN,end
        start,HN,kj,HN,dc,HN,end
        start,HN,kj,HN,dc,end
        start,HN,kj,HN,end
        start,HN,kj,dc,HN,end
        start,HN,kj,dc,end
        start,dc,HN,end
        start,dc,HN,kj,HN,end
        start,dc,end
        start,dc,kj,HN,end
        start,kj,HN,dc,HN,end
        start,kj,HN,dc,end
        start,kj,HN,end
        start,kj,dc,HN,end
        start,kj,dc,end

        Finally, this even larger example has 226 paths through it:

        fs-end
        he-DX
        fs-he
        start-DX
        pj-DX
        end-zg
        zg-sl
        zg-pj
        pj-he
        RW-he
        fs-DX
        pj-RW
        zg-RW
        start-pj
        he-WI
        zg-he
        pj-fs
        start-RW

        How many paths through this cave system are there that visit small caves at most once?

        Your puzzle answer was 5958.

        --- Part Two ---
        After reviewing the available paths, you realize you might have time to visit a single small cave twice. Specifically, big caves can be visited any number of times, a single small cave can be visited at most twice, and the remaining small caves can be visited at most once. However, the caves named start and end can only be visited exactly once each: once you leave the start cave, you may not return to it, and once you reach the end cave, the path must end immediately.

        Now, the 36 possible paths through the first example above are:

        start,A,b,A,b,A,c,A,end
        start,A,b,A,b,A,end
        start,A,b,A,b,end
        start,A,b,A,c,A,b,A,end
        start,A,b,A,c,A,b,end
        start,A,b,A,c,A,c,A,end
        start,A,b,A,c,A,end
        start,A,b,A,end
        start,A,b,d,b,A,c,A,end
        start,A,b,d,b,A,end
        start,A,b,d,b,end
        start,A,b,end
        start,A,c,A,b,A,b,A,end
        start,A,c,A,b,A,b,end
        start,A,c,A,b,A,c,A,end
        start,A,c,A,b,A,end
        start,A,c,A,b,d,b,A,end
        start,A,c,A,b,d,b,end
        start,A,c,A,b,end
        start,A,c,A,c,A,b,A,end
        start,A,c,A,c,A,b,end
        start,A,c,A,c,A,end
        start,A,c,A,end
        start,A,end
        start,b,A,b,A,c,A,end
        start,b,A,b,A,end
        start,b,A,b,end
        start,b,A,c,A,b,A,end
        start,b,A,c,A,b,end
        start,b,A,c,A,c,A,end
        start,b,A,c,A,end
        start,b,A,end
        start,b,d,b,A,c,A,end
        start,b,d,b,A,end
        start,b,d,b,end
        start,b,end

        The slightly larger example above now has 103 paths through it, and the even larger example now has 3509 paths through it.

        Given these new rules, how many paths through this cave system are there?

        Your puzzle answer was 150426.

        Both parts of this puzzle are complete! They provide two gold stars: **
        */

        public D12_PassagePathing(string[] input)
        {
            //if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D12: recieved invalid input"); return; }

            Console.WriteLine("---- Day 12, Passage Pathing ----" + "\n");

            PointMap map = new PointMap(input);

            // Part 1
            List<List<int>> paths = map.GetPaths("start", "end");
            Console.WriteLine("1. Number of paths through cave = " + paths.Count);

            // Part 2
            paths = map.GetPaths2("start", "end");
            Console.WriteLine("2. Number of paths through cave = " + paths.Count);


            Console.WriteLine("\n\n");
        }


        









        private void PrintMap(PointMap map)
        {
            Console.WriteLine("\nPrinting Map:");
            string line;
            foreach (PointMap.Point point in map.Points)
            {
                line = point.Name + " - ";
                foreach (string s in point.GetConnections())
                {
                    line += s + " ";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }









        private string[] ExampleInput1
        {
            get
            {
                return new string[]
                {
                    "start-A",
                    "start-b",
                    "A-c",
                    "A-b",
                    "b-d",
                    "A-end",
                    "b-end"
                };

                // Total number of paths,
                // that begin at 'start' and end at 'end', and visit any small caves (lower case) no more than once,
                // is 10
                // that begin at 'start' and end at 'end', and visit one small cave twice (excluding 'start' and 'end) and any other small caves no more than once
                // is 36
                /*
                start,A,b,A,c,A,end
                start,A,b,A,end
                start,A,b,end
                start,A,c,A,b,A,end
                start,A,c,A,b,end
                start,A,c,A,end
                start,A,end
                start,b,A,c,A,end
                start,b,A,end
                start,b,end
                */
            }
        }

        private string[] ExampleInput2
        {
            get
            {
                return new string[]
                {
                "dc-end",
                "HN-start",
                "start-kj",
                "dc-start",
                "dc-HN",
                "LN-dc",
                "HN-end",
                "kj-sa",
                "kj-HN",
                "kj-dc"
                };
                // Total number of paths,
                // that begin at 'start' and end at 'end', and visit any small caves (lower case) no more than once,
                // is 19
                // that begin at 'start' and end at 'end', and visit one small cave twice (excluding 'start' and 'end) and any other small caves no more than once
                // is 103
                /*
                start,HN,dc,HN,end
                start,HN,dc,HN,kj,HN,end
                start,HN,dc,end
                start,HN,dc,kj,HN,end
                start,HN,end
                start,HN,kj,HN,dc,HN,end
                start,HN,kj,HN,dc,end
                start,HN,kj,HN,end
                start,HN,kj,dc,HN,end
                start,HN,kj,dc,end
                start,dc,HN,end
                start,dc,HN,kj,HN,end
                start,dc,end
                start,dc,kj,HN,end
                start,kj,HN,dc,HN,end
                start,kj,HN,dc,end
                start,kj,HN,end
                start,kj,dc,HN,end
                start,kj,dc,end
                */
            }
        }

        private string[] ExampleInput3
        {
            get
            {
                return new string[]
                {
                "fs-end",
                "he-DX",
                "fs-he",
                "start-DX",
                "pj-DX",
                "end-zg",
                "zg-sl",
                "zg-pj",
                "pj-he",
                "RW-he",
                "fs-DX",
                "pj-RW",
                "zg-RW",
                "start-pj",
                "he-WI",
                "zg-he",
                "pj-fs",
                "start-RW"
                };
                // Total number of paths,
                // that begin at 'start' and end at 'end', and visit any small caves (lower case) no more than once,
                // is 226
                // that begin at 'start' and end at 'end', and visit one small cave twice (excluding 'start' and 'end) and any other small caves no more than once
                // is 3509
            }
        }












        private class PointMap
        {
            public class Point
            {
                private string _Name;
                private bool _IsSmall;
                private List<string> Connections;

                public Point(string name, string connection)
                {
                    _Name = name;
                    _IsSmall = char.IsLower(_Name[0]);

                    Connections = new List<string>();
                    Connections.Add(connection);
                }

                public void AddConnection(string connection)
                {
                    if (!Connections.Contains(connection))
                    {
                        Connections.Add(connection);
                    }
                }

                public void SortConnections()
                {
                    Connections.Sort();
                }

                // Compares (sorts) alphabetically, but places "start" at 0, and "end" at the end
                public int CompareTo_StartEnd(Point p)
                {
                    if (_Name == "start") return -1;
                    else if (_Name == "end") return 1;
                    else
                    {
                        if (p.Name == "start") { return 1; }
                        else if (p.Name == "end") { return -1; }
                        else
                        {
                            return _Name.CompareTo(p.Name);
                        }                        
                    }
                }

                public int CompareTo(Point p)
                {
                    return _Name.CompareTo(p.Name);
                }

                public string Name { get { return _Name; } }
                public bool IsSmall { get { return _IsSmall;} }
                public string[] GetConnections() { return Connections.ToArray(); }
                public string GetConnectionsAsString()
                {
                    string conns = "";
                    for (int i = 0; i < Connections.Count - 1; i++)
                    {
                        conns += Connections[i] + " - ";
                    }
                    conns += Connections.Last();
                    return conns;
                }
            }



            /// <summary>
            /// Original input
            /// </summary>
            public string[] Input = new string[0];

            /// <summary>
            /// List of all the points, storing their name and connections
            /// </summary>
            public List<Point> Points = new List<Point>();

            /// <summary>
            /// List of all paths walked, succesfully from starting-point to end-point, by WalkPath()
            /// </summary>
            private List<List<int>> Paths = new List<List<int>>();


            public PointMap(string[] input)
            {
                NewMap(input);
            }




            public void NewMap(string[] input)
            {
                Input = (string[])input.Clone();
                Points = new List<Point>();

                foreach (string s in Input)
                {
                    NewPoint(s);
                }

                SortPoints();
                for (int i = 0; i < Points.Count; i++)
                {
                    Points.ElementAt(i).SortConnections();
                }
            }

            public void NewPoint(string input)
            {
                string[] points = input.Split('-');

                int index = -1;
                if (!TryGetIndexLin(points[0], out index))
                {
                    Points.Add(new Point(points[0], points[1]));
                }
                else
                {
                    Points[index].AddConnection(points[1]);
                }

                if (!TryGetIndexLin(points[1], out index))
                {
                    Points.Add(new Point(points[1], points[0]));
                }
                else
                {
                    Points[index].AddConnection(points[0]);
                }
            }




            public bool TryGetIndexLin(string name, out int index)
            {
                index = -1;
                for (int i = 0; i < Points.Count; i++)
                {
                    if (Points[i].Name == name) { index = i; return true; }
                }
                return false;
            }

            public bool TryGetIndexBin(string name, out int index)
            {
                index = -1;
                int min = 0;
                int max = Points.Count - 1;
                int mid;
                while (min <= max)
                {
                    mid = (min + max) / 2;
                    if (Points[mid].Name == name)
                    {
                        index = mid;
                        return true;
                    }
                    else if (Points[mid].Name.CompareTo(name) > 0)
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        min = mid + 1;
                    }
                }
                return false;
            }


            public void SortPoints()
            {
                Points.Sort((x, y) => x.CompareTo(y));
            }







            public List<List<int>> GetPaths(string start, string end)
            {
                Paths = new List<List<int>>();

                int startIndex;
                int endIndex;
                if (TryGetIndexBin(start, out startIndex) && TryGetIndexBin(end, out endIndex))
                {
                    WalkPath(new List<int>() { startIndex }, endIndex);
                }
                return new List<List<int>>(Paths);
            }

            private void WalkPath(List<int> path, int end)
            {
                int index = -1;
                string[] conns = Points[path.Last()].GetConnections();
                for (int i = 0; i < conns.Length; i++)
                {
                    string conn = conns[i];
                    if (TryGetIndexBin(conn, out index))
                    {
                        // If the connection results in a repetition (eg A-C-A-C), do nothing
                        if (path.Count >= 3 && path[^3] == path[^1] && path[^2] == index) { }

                        // If the connection is a small cave, and has been visited before, do nothing
                        else if (Points[index].IsSmall && path.Contains(index)) { }

                        // Else the connection is another step in the path
                        else
                        {
                            List<int> newPath = new List<int>(path);
                            newPath.Add(index);
                            // If the path ends at the following connection
                            if (index == end)
                            {
                                Paths.Add(newPath);
                            }
                            // Else keep walking
                            else
                            {
                                WalkPath(newPath, end);
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"D12: PointMap.WalkPath() failed to find index of point {conn}");
                    }
                }
            }




            public List<List<int>> GetPaths2(string start, string end)
            {
                Paths = new List<List<int>>();

                int startIndex;
                int endIndex;
                if (TryGetIndexBin(start, out startIndex) && TryGetIndexBin(end, out endIndex))
                {
                    WalkPath2(new List<int>() { startIndex }, endIndex);
                }
                return new List<List<int>>(Paths);
            }

            private void WalkPath2(List<int> path, int end)
            {
                int index = -1;
                string[] conns = Points[path.Last()].GetConnections();
                bool smallCaveAllowed;
                for (int i = 0; i < conns.Length; i++)
                {
                    string conn = conns[i];
                    if (TryGetIndexBin(conn, out index))
                    {
                        // If the connection is the starting connection, do nothing
                        if (index == path[0]) { }

                        else
                        {
                            smallCaveAllowed = true;
                            // If the cave is small and has been visited before
                            if (Points[index].IsSmall && path.Contains(index))
                            {
                                // If any small cave was visited more than once, don't allow this small cave on the path
                                if (IsAnySmallCaveVisitedTwice(path)) { smallCaveAllowed = false; }
                            }
                            // If a small cave is allowed
                            if (smallCaveAllowed)
                            {
                                List<int> newPath = new List<int>(path);
                                newPath.Add(index);
                                // If the path ends at the following connection
                                if (index == end)
                                {
                                    Paths.Add(newPath);
                                }
                                // Else keep walking
                                else
                                {
                                    WalkPath2(newPath, end);
                                }                                
                            }
                        }

                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"D12: PointMap.WalkPath() failed to find index of point {conn}");
                    }
                }
            }

            private bool IsAnySmallCaveVisitedTwice(List<int> path)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    if (Points[path[i]].IsSmall)
                    {
                        int count = GetCount(i, path);
                        if (count > 1)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            private int GetCount(int index, List<int> path)
            {
                int count = 1;
                for (int i = index + 1; i < path.Count; i++)
                {
                    if (path[index] == path[i]) { count++; }
                }
                return count;
            }




            private void PrintPath(List<int> path)
            {
                string line = "";
                for (int i = 0; i < path.Count - 1; i++)
                {
                    line += Points[path[i]].Name + " - ";
                }
                line += Points[path.Last()].Name;
                Console.WriteLine(line);
            }

            private void PrintPaths(List<List<int>> paths)
            {
                Console.WriteLine("\nPrinting Paths:");
                foreach (List<int> path in paths)
                {
                    PrintPath(path);
                }
            }

            private void PrintConnections(string point, string[] conns)
            {
                Console.WriteLine("\nPrinting Connections:");
                string line = point + " = ";
                for (int i = 0; i < conns.Length - 1; i++)
                {
                    line += conns[i] + " - ";
                }
                line += conns.Last();
                Console.WriteLine(line);
            }
        }

    }
}
