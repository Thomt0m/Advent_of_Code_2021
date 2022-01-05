using System;

namespace Advent_of_Code
{
    class D12_PassagePathing
    {
        /*

        */

        public D12_PassagePathing(string[] input)
        {
            //if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D12: recieved invalid input"); return; }

            Console.WriteLine("---- Day 12, Passage Pathing ----" + "\n");

            PointMap map = new PointMap(ExampleInput1);
            PrintMap(map);


            List<List<int>> paths = map.GetPaths("start", "end");
            Console.WriteLine("1. Number of paths = " + paths.Count);


            Console.WriteLine("\n\n");
        }






        private void TEST(List<string> list)
        {
            list.Add("Two");
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

                public int CompareTo_WithStartEnd(Point p)
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
            }



            /// <summary>
            /// Original input
            /// </summary>
            public string[] Input = new string[0];

            /// <summary>
            /// List of all the points, storing their name and connections
            /// </summary>
            public List<Point> Points = new List<Point>();


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
                if (!TryGetIndex(points[0], out index))
                {
                    Points.Add(new Point(points[0], points[1]));
                }
                else
                {
                    Points[index].AddConnection(points[1]);
                }

                if (!TryGetIndex(points[1], out index))
                {
                    Points.Add(new Point(points[1], points[0]));
                }
                else
                {
                    Points[index].AddConnection(points[0]);
                }
            }

            public bool TryGetIndex(string name, out int index)
            {
                // Could use binary search, but 'start' and 'end' are not in the correct order (but at the start and end respectively)
                index = -1;
                for (int i = 0; i < Points.Count; i++)
                {
                    if (Points[i].Name == name) { index = i; return true; }
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

                int startIndex = 0;
                int endIndex = 0;
                if (TryGetIndex(start, out startIndex) && TryGetIndex(end, out endIndex))
                {
                    WalkPath(new List<int>() { startIndex }, endIndex);
                }

                PrintPaths(Paths);
                return Paths;
            }

            private void WalkPath(List<int> path, int end)
            {
                int index = -1;
                string[] conns = Points[path.Last()].GetConnections();
                for (int i = 0; i < conns.Length; i++)
                {
                    string conn = conns[i];
                    if (TryGetIndex(conn, out index))
                    {
                        // If the connection is the end-connection, add it to the path and add the entire path to the list of paths
                        if (index == end) { path.Add(index); Paths.Add(path); }

                        // If the connection results in a repetition (eg A-C-A-C), do nothing
                        else if (path.Count >= 3 && path[^3] == path[^1] && path[^2] == index) { }

                        // If the connection is a small cave, and has been visited before, do nothing
                        else if (Points[index].IsSmall && path.Contains(index)) { }

                        // Else the connection is another step in the path, keep walking
                        else
                        {
                            List<int> newPath = new List<int>(path);
                            newPath.Add(index);
                            WalkPath(newPath, end);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"D12: PointMap.WalkPath() failed to find index of point {conn}");
                    }
                }
            }



            /*
            private void FollowPaths(ref List<List<int>> paths)
            {
                int index = -1;
                List<int> _path = paths.Last();

                // Debug, TODO remove
                PrintPath(_path);

                string[] conns = Points[_path.Last()].GetConnections();
                int validConns = 0;
                for (int i = 0; i < conns.Length; i++)
                {
                    string s = conns[i];
                    if (TryGetIndex(s, out index))
                    {
                        // If the current connection does not result in a repetition (eg A-C-A-C)
                        if (!(_path.Count >= 3 && _path[^3] == _path[^1] && _path[^2] == index))
                        {
                            // If the connection (cave) is not a small cave that has already been visited
                            if (!(Points[index].IsSmall && _path.Contains(index)))
                            {
                                if (validConns > 0)
                                {
                                    paths.Add(new List<int>(_path));
                                }
                                validConns++;
                                paths.Last().Add(index);
                                // If the current connection is not "end", continue following the path
                                if (s != "end")
                                {
                                    FollowPaths(ref paths);
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"D12: PointMap.FollowPaths() failed to find index of point {s}");
                    }
                }
            }


            private bool GetPath(ref List<int> paths)
            {
                // If end is reached
                if (Points[paths.Last()].Name == "end") { return true; }
                // If the last moves were a repeat (eg A-B-A-B)
                else if (paths.Count >= 4 && paths[paths.Count - 4] == paths[paths.Count - 2] && paths[paths.Count - 3] == paths.Last()) { return false; }
                else
                {
                    int index = -1;
                    foreach (string s in Points[paths.Last()].GetConnections())
                    {
                        if (TryGetIndex(s, out index))
                        {
                            // If the current connection is "end", add it to the path and return succesfull
                            if (Points[index].Name == "end") { paths.Add(index); return true; }

                            // If the current connection does not result in a repetition (eg A-B-A-B)
                            else if (!(paths.Count >= 3 && paths[paths.Count - 4] == paths[paths.Count - 2] && paths[paths.Count - 3] == index))
                            {
                                // If not: the cave to be visited is small and has been visited before
                                if (!(Points[index].IsSmall && paths.Contains(index)))
                                {
                                    return
                                }
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"D12: PointMap.GetPath() failed to find index of point {s}");
                        }
                    }
                }


            }
            */

            private bool isIntInPath(int point, List<int> path)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    if (point == path[i]) { return true; }
                }
                return false;
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
        }

    }
}
