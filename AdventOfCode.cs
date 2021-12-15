/*
Written by Thomas A.

Following the Advent of Code 2021
https://adventofcode.com/ 
 
 */


using System;
using System.Diagnostics;


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
            new D07_TheTreacheryOfWhales(AoC.GetInputAsInt3(day));

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
}




