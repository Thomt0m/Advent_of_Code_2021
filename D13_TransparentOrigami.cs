using System;

namespace Advent_of_Code
{
    class D13_TransparentOrigami
    {
        /*
        --- Day 13: Transparent Origami ---
        You reach another volcanically active part of the cave. It would be nice if you could do some kind of thermal imaging so you could tell ahead of time which caves are too hot to safely enter.

        Fortunately, the submarine seems to be equipped with a thermal camera! When you activate it, you are greeted with:

        Congratulations on your purchase! To activate this infrared thermal imaging
        camera system, please enter the code found on page 1 of the manual.
        Apparently, the Elves have never used this feature. To your surprise, you manage to find the manual; as you go to open it, page 1 falls out. It's a large sheet of transparent paper! The transparent paper is marked with random dots and includes instructions on how to fold it up (your puzzle input). For example:

        6,10
        0,14
        9,10
        0,3
        10,4
        4,11
        6,0
        6,12
        4,1
        0,13
        10,12
        3,4
        3,0
        8,4
        1,10
        2,14
        8,10
        9,0

        fold along y=7
        fold along x=5

        The first section is a list of dots on the transparent paper. 0,0 represents the top-left coordinate. The first value, x, increases to the right. The second value, y, increases downward. So, the coordinate 3,0 is to the right of 0,0, and the coordinate 0,7 is below 0,0. The coordinates in this example form the following pattern, where # is a dot on the paper and . is an empty, unmarked position:

        ...#..#..#.
        ....#......
        ...........
        #..........
        ...#....#.#
        ...........
        ...........
        ...........
        ...........
        ...........
        .#....#.##.
        ....#......
        ......#...#
        #..........
        #.#........

        Then, there is a list of fold instructions. Each instruction indicates a line on the transparent paper and wants you to fold the paper up (for horizontal y=... lines) or left (for vertical x=... lines). In this example, the first fold instruction is fold along y=7, which designates the line formed by all of the positions where y is 7 (marked here with -):

        ...#..#..#.
        ....#......
        ...........
        #..........
        ...#....#.#
        ...........
        ...........
        -----------
        ...........
        ...........
        .#....#.##.
        ....#......
        ......#...#
        #..........
        #.#........

        Because this is a horizontal line, fold the bottom half up. Some of the dots might end up overlapping after the fold is complete, but dots will never appear exactly on a fold line. The result of doing this fold looks like this:

        #.##..#..#.
        #...#......
        ......#...#
        #...#......
        .#.#..#.###
        ...........
        ...........

        Now, only 17 dots are visible.

        Notice, for example, the two dots in the bottom left corner before the transparent paper is folded; after the fold is complete, those dots appear in the top left corner (at 0,0 and 0,1). Because the paper is transparent, the dot just below them in the result (at 0,3) remains visible, as it can be seen through the transparent paper.

        Also notice that some dots can end up overlapping; in this case, the dots merge together and become a single dot.

        The second fold instruction is fold along x=5, which indicates this line:

        #.##.|#..#.
        #...#|.....
        .....|#...#
        #...#|.....
        .#.#.|#.###
        .....|.....
        .....|.....

        Because this is a vertical line, fold left:

        #####
        #...#
        #...#
        #...#
        #####
        .....
        .....

        The instructions made a square!

        The transparent paper is pretty big, so for now, focus on just completing the first fold. After the first fold in the example above, 17 dots are visible - dots that end up overlapping after the fold is completed count as a single dot.

        How many dots are visible after completing just the first fold instruction on your transparent paper?

        Your puzzle answer was 775.

        --- Part Two ---
        Finish folding the transparent paper according to the instructions. The manual says the code is always eight capital letters.

        What code do you use to activate the infrared thermal imaging camera system?

        Your puzzle answer was REUPUPKR.

        Both parts of this puzzle are complete! They provide two gold stars: **
        */

        public D13_TransparentOrigami(string[] input)
        {
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D13: recieved invalid input"); return; }

            Console.WriteLine("---- Day 13, Transparent Origami ----" + "\n");

            List<int[]> points = GetPoints(input);
            string[] foldInstr = GetFoldInstructions(input);
            bool[,] sheet = GetSheet(points);
            bool[,] sheetFolded;

            // Part 1
            string[] foldInstrFirst = new string[1] { foldInstr[0] };
            sheetFolded = FoldSheet(sheet, foldInstrFirst);
            Console.WriteLine("1. Visible dots after first fold = " + CountVisiblePoints(sheetFolded));


            // Part 2
            sheetFolded = FoldSheet(sheet, foldInstr);
            Console.WriteLine("2. Resulting sheet =");
            PrintPart2(sheetFolded);


            Console.WriteLine("\n\n");
        }








        private List<int[]> GetPoints(string[] input)
        {
            List<int[]> points = new List<int[]>();

            foreach (string line in input)
            {
                if (string.IsNullOrWhiteSpace(line)) { break; }

                string[] ps = line.Split(',');
                if (ps.Length != 2)
                {
                    System.Diagnostics.Debug.WriteLine("D13: GetPoints() did not split line as expected");
                    PrintDebug(ps);
                }
                else
                {
                    int value;
                    int[] point = new int[2];
                    if (int.TryParse(ps[0], out value)) { point[1] = value; }
                    else { System.Diagnostics.Debug.WriteLine("D13: GetPoints() failed to parse X - " + ps[0]); }
                    if (int.TryParse(ps[1], out value)) { point[0] = value; }
                    else { System.Diagnostics.Debug.WriteLine("D13: GetPoints() failed to parse Y - " + ps[1]); }
                    points.Add(point);
                }
            }

            return points;
        }

        private string[] GetFoldInstructions(string[] input)
        {
            string[] foldInstructions = new string[0];
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(input[i]) || input[i][0] != 'f')
                {
                    foldInstructions = new string[input.Length - (i + 1)];
                    Array.Copy(input, i + 1, foldInstructions, 0, foldInstructions.Length);
                    break;
                }
            }
            return foldInstructions;
        }

        private bool[,] GetSheet(List<int[]> points)
        {
            bool[,] sheet;
            int M = 0;
            int N = 0;
            foreach (int[] point in points)
            {
                if (point[0] > M) { M = point[0]; }
                if (point[1] > N) { N = point[1]; }
            }
            sheet = new bool[M + 1, N + 1];

            foreach (int[] point in points)
            {
                sheet[point[0], point[1]] = true;
            }

            return sheet;
        }








        private bool[,] FoldSheet(bool[,] sheet, string[] foldInstructions)
        {
            bool[,] sheetFolded = (bool[,])sheet.Clone();

            int value = 0;
            foreach (string fInstr in foldInstructions)
            {
                if (fInstr[11] == 'x' && int.TryParse(fInstr[13..], out value))
                {
                    sheetFolded = FoldX(sheetFolded, value);
                }
                else if (fInstr[11] == 'y' && int.TryParse(fInstr[13..], out value))
                {
                    sheetFolded = FoldY(sheetFolded, value);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("D13: FoldSheet() failed to read fold instruction " + fInstr);
                    break;
                }
            }

            return sheetFolded;
        }

        private bool[,] FoldX(bool[,] sheet, int x)
        {
            if (x < 0 || x >= sheet.GetLength(1))
            {
                System.Diagnostics.Debug.WriteLine("D13: FoldX() recieved invalid value for x " + x);
                return sheet;
            }

            bool[,] sheetFolded = new bool[sheet.GetLength(0), x];

            for (int m = 0; m < sheet.GetLength(0); m++)
            {
                for (int n0 = 0; n0 < x; n0++)
                {
                    if (sheet[m, n0]) { sheetFolded[m, n0] = true; }
                }
                int n1Max = Math.Min(sheet.GetLength(1) - x, x + 1);
                for (int n1 = 1; n1 < n1Max; n1++)
                {
                    if (sheet[m, x + n1]) { sheetFolded[m, x - n1] = true; }
                }
            }

            return sheetFolded;
        }

        private bool[,] FoldY(bool[,] sheet, int y)
        {
            if (y < 0 || y >= sheet.GetLength(0))
            {
                System.Diagnostics.Debug.WriteLine("D13: FoldX() recieved invalid value for y " + y);
                return sheet;
            }

            bool[,] sheetFolded = new bool[y, sheet.GetLength(1)];

            for (int n = 0; n < sheet.GetLength(1); n++)
            {
                for (int m0 = 0; m0 < y; m0++)
                {
                    if (sheet[m0, n]) { sheetFolded[m0, n] = true; }
                }
                int m1Max = Math.Min(sheet.GetLength(0) - y, y + 1);
                for (int m1 = 1; m1 < m1Max; m1++)
                {
                    if (sheet[y + m1, n]) { sheetFolded[y - m1, n] = true; }
                }
            }

            return sheetFolded;
        }






        private int CountVisiblePoints(bool[,] sheet)
        {
            int points = 0;
            for (int m = 0; m < sheet.GetLength(0); m++)
            {
                for (int n = 0; n < sheet.GetLength(1); n++)
                {
                    if (sheet[m, n]) { points++; }
                }
            }
            return points;
        }









        private void PrintDebug(string[] strArray)
        {
            foreach (string str in strArray)
            {
                System.Diagnostics.Debug.WriteLine(str);
            }
        }

        private void Print(string[] strArray, string msg = "")
        {
            Console.WriteLine("\nPrinting string[]: " + msg);
            foreach (string str in strArray)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
        }

        private void Print(List<int[]> points, string msg = "")
        {
            Console.WriteLine("\nPrinting List<int[]>: " + msg);
            foreach (int[] point in points)
            {
                Console.WriteLine($"[{point[0]}, {point[1]}]");
            }
            Console.WriteLine();
        }

        private void Print(bool[,] sheet, string msg = "")
        {
            Console.WriteLine("\nPrinting bool[,]: " + msg);
            string line;
            for (int m = 0; m < sheet.GetLength(0); m++)
            {
                line = "";
                for (int n = 0; n < sheet.GetLength(1); n++)
                {
                    line += (sheet[m,n] ? "#" : "-");
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        private void PrintPart2(bool[,] sheetFolded)
        {
            string line;
            for (int m = 0; m < sheetFolded.GetLength(0); m++)
            {
                line = "";
                for (int n = 0; n < sheetFolded.GetLength(1); n++)
                {
                    line += (sheetFolded[m, n] ? "#" : " ");
                }
                Console.WriteLine(line);
            }
        }








        private string[] ExampleInput
        {
            get
            {
                return new string[]
                {
                    "6,10",
                    "0,14",
                    "9,10",
                    "0,3",
                    "10,4",
                    "4,11",
                    "6,0",
                    "6,12",
                    "4,1",
                    "0,13",
                    "10,12",
                    "3,4",
                    "3,0",
                    "8,4",
                    "1,10",
                    "2,14",
                    "8,10",
                    "9,0",
                    "",
                    "fold along y=7",
                    "fold along x=5"
                };
            }

        }
    }
}
