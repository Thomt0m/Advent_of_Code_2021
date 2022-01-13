/*
Written by Thomas A.

Following the Advent of Code 2021
https://adventofcode.com/ 
 
 */


using System;


namespace Advent_of_Code
{
    class AdventOfCode
    {

        public static void Main()
        {
            AdventOfCode AoC = new AdventOfCode();
            int day = 1;


            // Day 1, Sonar Sweep
            day = 1;
            //new D01_SonarSweep(AoC.GetInputAsInt(day));


            // Day 2, Dive!
            day = 2;
            //new D02_Dive(AoC.GetInput(day));


            // Day 3, Binary Diagnostic
            day = 3;
            //new D03_BinaryDiagnostic(AoC.GetInput(day));


            // Day 4, Giant Squid
            day = 4;
            //new D04_GiantSquid(AoC.GetInput(day));


            // Day 5, Hydrothermal Venture
            day = 5;
            //new D05_HydrothermalVenture(AoC.GetInput(day));


            // Day 6, Lanternfish
            day = 6;
            //new D06_Lanternfish(AoC.GetInputAsInt2(day));


            // Day 7, The Treachery of Whales
            day = 7;
            //new D07_TheTreacheryOfWhales(AoC.GetInputAsInt3(day));


            // Day 8, Seven Segment Search
            day = 8;
            //new D08_SevenSegmentSearch(AoC.GetInput(day));


            // Day 9, Smoke Basin
            day = 9;
            //new D09_SmokeBasin(AoC.GetInput(day));


            // Day 10, Syntax Scoring
            day = 10;
            //new D10_SyntaxScoring(AoC.GetInput(day));


            // Day 11, Dumbo Octopus
            day = 11;
            //new D11_DumboOctopus(AoC.GetInput(day));


            // Day 12, Passage Pathing
            day = 12;
            //new D12_PassagePathing(AoC.GetInput(day));


            // Day 13, Transparent Origami
            day = 13;
            //new D13_TransparentOrigami(AoC.GetInput(day));


            // Day 14, Extended Polymerization
            day = 14;
            //new D14_ExtendedPolymerization(AoC.GetInput(day));


            // Day 15, Chiton
            day = 15;
            new D15_Chiton(AoC.GetInput(day));

        }












        private string GetPath(int day)
        {
            return "Input\\" + day.ToString("D2") + ".txt";
        }


        private string[] GetInput(int day)
        {
            string path = GetPath(day);
            if (!File.Exists(path)) { return new string[0]; }

            return File.ReadAllLines(path);
        }

        private int[] GetInputAsInt(int day)
        {
            string[] strings = GetInput(day);
            int[] content = new int[strings.Length];

            for (int i = 0; i < strings.Length; i++)
            {
                int.TryParse(strings[i], out content[i]);
            }

            return content;
        }

        private int[] GetInputAsInt2(int day)
        {
            string[] strings = GetInput(day)[0].Split(',');
            int[] content = new int[strings.Length];

            for (int i = 0; i < strings.Length; i++)
            {
                int.TryParse(strings[i], out content[i]);
            }

            return content;
        }

        private int[] GetInputAsInt3(int day)
        {
            string[] strings = GetInput(day);
            List<int> content = new List<int>();
            int value = 0;
            foreach (string line in strings)
            {
                foreach (string s in line.Split(','))
                {
                    if (int.TryParse(s, out value)) { content.Add(value); }
                }
            }
            return content.ToArray();
        }
    }




















    public static class Utility
    {
        // ---------------------------------------------------------
        // ----------------     Converting     ---------------------
        // ---------------------------------------------------------
        #region CONVERTING
        public static int[,] ConvertStringArrayToInt2DArray(string[] input)
        {
            int longestLine = 0;
            foreach (string line in input)
            {
                if (line.Length > longestLine) { longestLine = line.Length; }
            }

            int[,] map = new int[input.Length, longestLine];

            int value;
            for (int m = 0; m < input.Length; m++)
            {
                for (int n = 0; n < input[m].Length; n++)
                {
                    if (int.TryParse(input[m][n].ToString(), out value))
                    {
                        map[m, n] = value;
                    }
                }
            }

            return map;
        }
        #endregion CONVERTING








        // ---------------------------------------------------------
        // -----------------     Printing     ----------------------
        // ---------------------------------------------------------
        #region PRINTING
        public static void Print(string[] input, string msg = "")
        {
            Console.WriteLine("\nPrinting string[]: " + msg);
            foreach (string str in input)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
        }

        public static void Print(List<string> input, string msg = "")
        {
            Console.WriteLine("\nPrinting List<string>: " + msg);
            foreach (string str in input)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
        }

        public static void Print(int[,] input, string msg = "")
        {
            Console.WriteLine("\nPrinting int[,]: " + msg);
            string line;
            for (int m = 0; m < input.GetLength(0); m++)
            {
                line = "";
                for (int n = 0; n < input.GetLength(1); n++)
                {
                    line += input[m, n];
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        public static void Print(bool[,] input, string msg = "")
        {
            Console.WriteLine("\nPrinting bool[,]: " + msg);
            string line;
            for (int m = 0; m < input.GetLength(0); m++)
            {
                line = "";
                for (int n = 0; n < input.GetLength(1); n++)
                {
                    line += (input[m, n] ? "*" : " ");
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
        #endregion PRINTING


    }
}




