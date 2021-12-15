using System;


namespace Advent_of_Code
{
    class D05_HydrothermalVenture
    {
        /*
        --- Day 5: Hydrothermal Venture ---
        You come across a field of hydrothermal vents on the ocean floor! These vents constantly produce large, opaque clouds, so it would be best to avoid them if possible.

        They tend to form in lines; the submarine helpfully produces a list of nearby lines of vents (your puzzle input) for you to review. For example:

        0,9 -> 5,9
        8,0 -> 0,8
        9,4 -> 3,4
        2,2 -> 2,1
        7,0 -> 7,4
        6,4 -> 2,0
        0,9 -> 2,9
        3,4 -> 1,4
        0,0 -> 8,8
        5,5 -> 8,2
        Each line of vents is given as a line segment in the format x1,y1 -> x2,y2 where x1,y1 are the coordinates of one end the line segment and x2,y2 are the coordinates of the other end. These line segments include the points at both ends. In other words:

        An entry like 1,1 -> 1,3 covers points 1,1, 1,2, and 1,3.
        An entry like 9,7 -> 7,7 covers points 9,7, 8,7, and 7,7.
        For now, only consider horizontal and vertical lines: lines where either x1 = x2 or y1 = y2.

        So, the horizontal and vertical lines from the above list would produce the following diagram:

        .......1..
        ..1....1..
        ..1....1..
        .......1..
        .112111211
        ..........
        ..........
        ..........
        ..........
        222111....
        In this diagram, the top left corner is 0,0 and the bottom right corner is 9,9. Each position is shown as the number of lines which cover that point or . if no line covers that point. The top-left pair of 1s, for example, comes from 2,2 -> 2,1; the very bottom row is formed by the overlapping lines 0,9 -> 5,9 and 0,9 -> 2,9.

        To avoid the most dangerous areas, you need to determine the number of points where at least two lines overlap. In the above example, this is anywhere in the diagram with a 2 or larger - a total of 5 points.

        Consider only horizontal and vertical lines. At how many points do at least two lines overlap?

        Your puzzle answer was 6687.

        --- Part Two ---
        Unfortunately, considering only horizontal and vertical lines doesn't give you the full picture; you need to also consider diagonal lines.

        Because of the limits of the hydrothermal vent mapping system, the lines in your list will only ever be horizontal, vertical, or a diagonal line at exactly 45 degrees. In other words:

        An entry like 1,1 -> 3,3 covers points 1,1, 2,2, and 3,3.
        An entry like 9,7 -> 7,9 covers points 9,7, 8,8, and 7,9.
        Considering all lines from the above example would now produce the following diagram:

        1.1....11.
        .111...2..
        ..2.1.111.
        ...1.2.2..
        .112313211
        ...1.2....
        ..1...1...
        .1.....1..
        1.......1.
        222111....
        You still need to determine the number of points where at least two lines overlap. In the above example, this is still anywhere in the diagram with a 2 or larger - now a total of 12 points.

        Consider all of the lines. At how many points do at least two lines overlap?

        Your puzzle answer was 19851.

        Both parts of this puzzle are complete! They provide two gold stars: **
        */





        public D05_HydrothermalVenture(string[] input)
        {
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D04_GiantSquid: recieved invalid input"); return; }

            Console.WriteLine("---- Day 5, Hyrdothermal Venture ----" + "\n");

            int[,] lines = GetLines(input);

            ushort[,] grid = new ushort[1000, 1000];

            // Find all 1D lines, lines that are either horizontal or vertical, and log their index values
            List<int> sLn = new List<int>();
            List<int> dLn = new List<int>();
            for (int l0 = 0; l0 < lines.GetLength(0); l0++)
            {
                if (lines[l0, 0] == lines[l0, 2] || lines[l0, 1] == lines[l0, 3])
                {
                    int sx1 = lines[l0, 0];
                    int sy1 = lines[l0, 1];
                    int sx2 = lines[l0, 2];
                    int sy2 = lines[l0, 3];

                    // If line is horizontal
                    if (sx1 == sx2)
                    {
                        if (sy1 < sy2) { for (int ly0 = sy1; ly0 <= sy2; ly0++) { grid[sx1, ly0]++; } }
                        else { for (int lsy1 = sy2; lsy1 <= sy1; lsy1++) { grid[sx1, lsy1]++; } }
                    }
                    // Else line is vertical
                    else
                    {
                        if (sx1 < sx2) { for (int lx0 = sx1; lx0 <= sx2; lx0++) { grid[lx0, sy1]++; } }
                        else { for (int lsx1 = sx2; lsx1 <= sx1; lsx1++) { grid[lsx1, sy1]++; } }
                    }
                }
            }

            int pointsStraight = 0;
            for (int m = 0; m < grid.GetLength(0); m++)
            {
                for (int n = 0; n < grid.GetLength(1); n++)
                {
                    if (grid[m,n] >= 2) { pointsStraight++; }
                }
            }
            Console.WriteLine("1. Number of points where 2 or more lines cross = " + pointsStraight);



            int startX;
            int range;
            int startY;
            for (int l1 = 0; l1 < lines.GetLength(0); l1++)
            {
                if (Math.Abs(lines[l1, 0] - lines[l1, 2]) == Math.Abs(lines[l1, 1] - lines[l1, 3]))
                {
                    startX = lines[l1, 0];
                    startY = lines[l1, 1];
                    range = Math.Abs(lines[l1, 0] - lines[l1, 2]);
                    if (lines[l1, 0] < lines[l1, 2])
                    {
                        if (lines[l1, 1] < lines[l1, 3])
                        {
                            for (int l20 = 0; l20 <= range; l20++) { grid[startX + l20, startY + l20]++; }
                        }
                        else
                        {
                            for (int l21 = 0; l21 <= range; l21++) { grid[startX + l21, startY - l21]++; }
                        }
                    }
                    else
                    {
                        if (lines[l1, 1] < lines[l1, 3])
                        {
                            for (int l20 = 0; l20 <= range; l20++) { grid[startX - l20, startY + l20]++; }
                        }
                        else
                        {
                            for (int l21 = 0; l21 <= range; l21++) { grid[startX - l21, startY - l21]++; }
                        }
                    }
                }
            }

            int pointsDiag = 0;
            for (int m = 0; m < grid.GetLength(0); m++)
            {
                for (int n = 0; n < grid.GetLength(1); n++)
                {
                    if (grid[m, n] >= 2) { pointsDiag++; }
                }
            }
            Console.WriteLine("2. Number of points where 2 or more lines cross = " + pointsDiag);

            Console.WriteLine("\n\n");
        }








        private int[,] GetLines(string[] input)
        {
            int[,] lines = new int[input.Length, 4];

            List<string> parts = new List<string>();
            int value = 0;
            char[] separators = new char[] { ',', ' ', '-', '>' };
            for (int i = 0; i < input.Length; i++)
            {
                parts = input[i].Split(separators).ToList();
                for (int j0 = 0; j0 < parts.Count; j0++)
                {
                    if (string.IsNullOrWhiteSpace(parts[j0]))
                    {
                        parts.RemoveAt(j0);
                        j0--;
                    }
                }
                for (int j1 = 0; j1 < lines.GetLength(1); j1++)
                {
                    if (int.TryParse(parts[j1], out value)) { lines[i, j1] = value; }
                    else { System.Diagnostics.Debug.WriteLine("D05: Failed to parse value, line = " + i + ", column = " + j1); }
                }
            }

            return lines;
        }



        

        private bool IsBetween(int i0, int i1, int i2)
        {
            return ((i0 <= i1 && i1 <= i2) || (i0 >= i1 && i1 >= i2));
        }

        private bool Intersect(int[] line1, int[] line2, out float[] point)
        {
            point = new float[2] { 0, 0 };

            int line1X1 = line1[0];
            int line1Y1 = line1[1];
            int line1X2 = line1[2];
            int line1Y2 = line1[3];

            int line2X1 = line2[0];
            int line2Y1 = line2[1];
            int line2X2 = line2[2];
            int line2Y2 = line2[3];

            float a1 = line1Y2 - line1Y1;
            float b1 = line1X1 - line1X2;
            float c1 = a1*line1X1 + b1*line1Y1;

            float a2 = line2Y2 - line2Y1;
            float b2 = line2X1 - line2X2;
            float c2 = a2 * line2X1 + b2 * line2Y1;

            float d = a1 * b2 - a2 * b1;
            if (d == 0) { return false; }
            else
            {
                point = new float[2];
                point[0] = (b2*c1 - b1*c2) / d;
                point[1] = (a1*c2 - a2*c1) / d;

                return (IsPointOnLine(line1, point) && IsPointOnLine(line2, point));
            }
        }

        private bool Intersect(int l1X1, int l1Y1, int l1X2, int l1Y2, int l2X1, int l2Y1, int l2X2, int l2Y2, out float[] point)
        {
            point = new float[2] { 0, 0 };

            float a1 = l1Y2 - l1Y1;
            float b1 = l1X1 - l1X2;
            float c1 = a1 * l1X1 + b1 * l1Y1;

            float a2 = l2Y2 - l2Y1;
            float b2 = l2X1 - l2X2;
            float c2 = a2 * l2X1 + b2 * l2Y1;

            float d = a1 * b2 - a2 * b1;
            if (d == 0) { return false; }
            else
            {
                point = new float[2];
                point[0] = (b2 * c1 - b1 * c2) / d;
                point[1] = (a1 * c2 - a2 * c1) / d;

                return (IsPointOnLine(l1X1, l1Y1, l1X2, l1Y2, point) && IsPointOnLine(l2X1, l2Y1, l2X2, l2Y2, point));
            }
        }

        private bool IsPointOnLine(int[] line, float[] point)
        {
            int x1 = line[0];
            int y1 = line[1];
            int x2 = line[2];
            int y2 = line[3];
            float pX = point[0];
            float pY = point[1];
            return ((x1 <= pX && x2 >= pX || x1 >= pX && x2 <= pX) && (y1 <= pY && y2 >= pY || y1 >= pY && y2 <= pY));
        }

        private bool IsPointOnLine(int x1, int y1, int x2, int y2, float[] point)
        {
            float pX = point[0];
            float pY = point[1];
            return ((x1 <= pX && x2 >= pX || x1 >= pX && x2 <= pX) && (y1 <= pY && y2 >= pY || y1 >= pY && y2 <= pY));
        }
    }
}
