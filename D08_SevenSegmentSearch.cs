using System;

namespace Advent_of_Code
{
    class D08_SevenSegmentSearch
    {
        /*
        --- Day 8: Seven Segment Search ---
        You barely reach the safety of the cave when the whale smashes into the cave mouth, collapsing it. Sensors indicate another exit to this cave at a much greater depth, so you have no choice but to press on.

        As your submarine slowly makes its way through the cave system, you notice that the four-digit seven-segment displays in your submarine are malfunctioning; they must have been damaged during the escape. You'll be in a lot of trouble without them, so you'd better figure out what's wrong.

        Each digit of a seven-segment display is rendered by turning on or off any of seven segments named a through g:

          0:      1:      2:      3:      4:
         aaaa    ....    aaaa    aaaa    ....
        b    c  .    c  .    c  .    c  b    c
        b    c  .    c  .    c  .    c  b    c
         ....    ....    dddd    dddd    dddd
        e    f  .    f  e    .  .    f  .    f
        e    f  .    f  e    .  .    f  .    f
         gggg    ....    gggg    gggg    ....

          5:      6:      7:      8:      9:
         aaaa    aaaa    aaaa    aaaa    aaaa
        b    .  b    .  .    c  b    c  b    c
        b    .  b    .  .    c  b    c  b    c
         dddd    dddd    ....    dddd    dddd
        .    f  e    f  .    f  e    f  .    f
        .    f  e    f  .    f  e    f  .    f
         gggg    gggg    ....    gggg    gggg

        So, to render a 1, only segments c and f would be turned on; the rest would be off. To render a 7, only segments a, c, and f would be turned on.

        The problem is that the signals which control the segments have been mixed up on each display. The submarine is still trying to display numbers by producing output on signal wires a through g, but those wires are connected to segments randomly. Worse, the wire/segment connections are mixed up separately for each four-digit display! (All of the digits within a display use the same connections, though.)

        So, you might know that only signal wires b and g are turned on, but that doesn't mean segments b and g are turned on: the only digit that uses two segments is 1, so it must mean segments c and f are meant to be on. With just that information, you still can't tell which wire (b/g) goes to which segment (c/f). For that, you'll need to collect more information.

        For each display, you watch the changing signals for a while, make a note of all ten unique signal patterns you see, and then write down a single four digit output value (your puzzle input). Using the signal patterns, you should be able to work out which pattern corresponds to which digit.

        For example, here is what you might see in a single entry in your notes:

        acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab |
        cdfeb fcadb cdfeb cdbaf

        (The entry is wrapped here to two lines so it fits; in your notes, it will all be on a single line.)

        Each entry consists of ten unique signal patterns, a | delimiter, and finally the four digit output value. Within an entry, the same wire/segment connections are used (but you don't know what the connections actually are). The unique signal patterns correspond to the ten different ways the submarine tries to render a digit using the current wire/segment connections. Because 7 is the only digit that uses three segments, dab in the above example means that to render a 7, signal lines d, a, and b are on. Because 4 is the only digit that uses four segments, eafb means that to render a 4, signal lines e, a, f, and b are on.

        Using this information, you should be able to work out which combination of signal wires corresponds to each of the ten digits. Then, you can decode the four digit output value. Unfortunately, in the above example, all of the digits in the output value (cdfeb fcadb cdfeb cdbaf) use five segments and are more difficult to deduce.

        For now, focus on the easy digits. Consider this larger example:

        be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |
        fdgacbe cefdb cefbgd gcbe
        edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec |
        fcgedb cgb dgebacf gc
        fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |
        cg cg fdcagb cbg
        fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |
        efabcd cedba gadfec cb
        aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |
        gecf egdcabf bgf bfgea
        fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |
        gebdcfa ecba ca fadegcb
        dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |
        cefg dcbef fcge gbcadfe
        bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |
        ed bcgafe cdgba cbgef
        egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |
        gbdfcae bgc cg cgb
        gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |
        fgae cfgab fg bagce

        Because the digits 1, 4, 7, and 8 each use a unique number of segments, you should be able to tell which combinations of signals correspond to those digits. Counting only digits in the output values (the part after | on each line), in the above example, there are 26 instances of digits that use a unique number of segments (highlighted above).

        In the output values, how many times do digits 1, 4, 7, or 8 appear?

        Your puzzle answer was 473.

        The first half of this puzzle is complete! It provides one gold star: *

        --- Part Two ---
        Through a little deduction, you should now be able to determine the remaining digits. Consider again the first example above:

        acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab |
        cdfeb fcadb cdfeb cdbaf

        After some careful analysis, the mapping between signal wires and segments only make sense in the following configuration:

         dddd
        e    a
        e    a
         ffff
        g    b
        g    b
         cccc

        So, the unique signal patterns would correspond to the following digits:

        acedgfb: 8
        cdfbe: 5
        gcdfa: 2
        fbcad: 3
        dab: 7
        cefabd: 9
        cdfgeb: 6
        eafb: 4
        cagedb: 0
        ab: 1

        Then, the four digits of the output value can be decoded:

        cdfeb: 5
        fcadb: 3
        cdfeb: 5
        cdbaf: 3

        Therefore, the output value for this entry is 5353.

        Following this same process for each entry in the second, larger example above, the output value of each entry can be determined:

        fdgacbe cefdb cefbgd gcbe: 8394
        fcgedb cgb dgebacf gc: 9781
        cg cg fdcagb cbg: 1197
        efabcd cedba gadfec cb: 9361
        gecf egdcabf bgf bfgea: 4873
        gebdcfa ecba ca fadegcb: 8418
        cefg dcbef fcge gbcadfe: 4548
        ed bcgafe cdgba cbgef: 1625
        gbdfcae bgc cg cgb: 8717
        fgae cfgab fg bagce: 4315

        Adding all of the output values in this larger example produces 61229.

        For each entry, determine all of the wire/segment connections and decode the four-digit output values. What do you get if you add up all of the output values?

        Your puzzle answer was 1097568.

        Both parts of this puzzle are complete! They provide two gold stars: **
        */

        class BruteForce
        {
            char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

            List<string> Permutations = new List<string>();


            public BruteForce() {}

            private void Clear()
            {
                GetPermutations(chars);                
            }


            public int GetSolution(string[] input)
            {
                Clear();

                string[] numbers = new string[10];
                int[] inputNums = new int[input.Length];
                List<int> results = new List<int>();

                for (int i = 0; i < Permutations.Count; i++)
                {
                    string p = Permutations[i];
                    bool matchFound = false;
                    Array.Fill(inputNums, -10 * input.Length);

                    numbers[0] = new string(new char[] { p[0], p[1], p[2], p[4], p[5], p[6] });
                    numbers[1] = new string(new char[] { p[2], p[5] });
                    numbers[2] = new string(new char[] { p[0], p[2], p[3], p[4], p[6] });
                    numbers[3] = new string(new char[] { p[0], p[2], p[3], p[5], p[6] });
                    numbers[4] = new string(new char[] { p[1], p[2], p[3], p[5] });
                    numbers[5] = new string(new char[] { p[0], p[1], p[3], p[5], p[6] });
                    numbers[6] = new string(new char[] { p[0], p[1], p[3], p[4], p[5], p[6] });
                    numbers[7] = new string(new char[] { p[0], p[2], p[5] });
                    numbers[8] = new string(new char[] { p[0], p[1], p[2], p[3], p[4], p[5], p[6] });
                    numbers[9] = new string(new char[] { p[0], p[1], p[2], p[3], p[5], p[6] });

                    List<string> matchedNums = new List<string>(0);

                    for (int i0 = 0; i0 < input.Length; i0++)
                    {
                        matchFound = false;
                        if (i0 == 10) // Handles the stray '|' character
                        {
                            matchFound = true;
                            inputNums[i0] = 0;
                        }
                        else
                        {
                            for (int i1 = 0; i1 < numbers.Length; i1++)
                            {
                                if (IsMatch(input[i0], numbers[i1]))
                                {
                                    matchedNums.Add(i0.ToString() + ", " + input[i0] + " = " + i1.ToString());
                                    matchFound = true;
                                    inputNums[i0] = i1;
                                    break;
                                }
                            }
                        }
                        
                        if (!matchFound) { break; }
                    }

                    if (matchFound)
                    {
                        string outputValues = "";
                        for (int i2 = inputNums.Length - 4; i2 < inputNums.Length; i2++)
                        {
                            if (inputNums[i2] < 0 ) { System.Diagnostics.Debug.WriteLine("D08: Unassigned value found!"); }
                            outputValues += inputNums[i2];
                        }
                        results.Add(int.Parse(outputValues));
                    }
                    /*
                    else if (matchedNums.Count > 0)
                    {
                        Console.WriteLine("\nPermutation " + i);
                        foreach (string s in matchedNums) { Console.WriteLine(s); }
                    }
                    */
                }

                if (results.Count > 1)
                {
                    System.Diagnostics.Debug.WriteLine("D08: More than one permutations results in a valid match with the input string!");
                }
                else if (results.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("D08: Failed to find a matching permutation for the input!");
                }

                return results.First();
            }






            private bool IsMatch(string s0, string s1)
            {
                if (s0.Length != s1.Length) { return false; }
                for (int i = 0; i < s0.Length; i++)
                {
                    if (!s1.Contains(s0[i])) { return false; }
                }
                return true;
            }


            #region PERMUTATION_RECURSION
            // https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
            private void GetPermutations(char[] list)
            {
                Permutations.Clear();
                GetPer(list, 0, list.Length - 1);
            }
            private void GetPer(char[] list, int k, int m)
            {
                if (k == m)
                {
                    Permutations.Add(new string(list));
                }
                else
                    for (int i = k; i <= m; i++)
                    {
                        Switch(ref list[k], ref list[i]);
                        GetPer(list, k + 1, m);
                        Switch(ref list[k], ref list[i]);
                    }
            }
            private static void Switch(ref char a, ref char b)
            {
                char tmp = a;
                a = b;
                b = tmp;
            }
            #endregion PERMUTATION_RECURSION

        }





        public D08_SevenSegmentSearch(string[] input)
        {
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D08: recieved invalid input"); return; }

            Console.WriteLine("---- Day 8, Seven Segment Search ----" + "\n");

            int count1478 = 0;
            foreach (string s in input)
            {
                count1478 += Count1478(s.Split(' '));
            }
            Console.WriteLine("1. Times the digits 1, 4, 7, and 8 appear = " + count1478);


            BruteForce BF = new BruteForce();
            int endCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                endCount += BF.GetSolution(input[i].Split(' '));
            }
            Console.WriteLine("2. Total value of output values = " + endCount);

            Console.WriteLine("\n\n");
        }




        private int Count1478(string[] numStr)
        {
            int count = 0;
            for (int i = 11; i < numStr.Length; i++)
            {
                if (numStr[i].Length == 2 || numStr[i].Length == 3 || numStr[i].Length == 4 || numStr[i].Length == 7) { count++; }
            }
            return count;
        }
    }
}
