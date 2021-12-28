using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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





        class SevenSegment
        {
            string[] Input;

            char A;
            char B;
            char C;
            char D;
            char E;
            char F;
            char G;

            char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

            List<string> Numbers = new List<string>();
            List<List<char>> Segments = new List<List<char>>();

            public SevenSegment(string input)
            {
                Clear();

                Input = input.Split(' ');
            }

            public void Clear()
            {
                Input = new string[0];

                A = '?';
                B = '?';
                C = '?';
                D = '?';
                E = '?';
                F = '?';
                G = '?';

                Numbers = new List<string>() { "", "", "", "", "", "", "", "", "", "" };
                Segments = new List<List<char>>();
                for (int segCount = 0; segCount < 7; segCount++)
                {
                    Segments.Add(new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
                }
            }


            /// <summary>
            /// Find the sum value of the last 4 elements of the input
            /// Deduces the value of each element
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public int GetOutput(string input)
            {
                Clear();

                Input = input.Split(' ');

                int output = 0;



                // Try to find the 'simple' number 1,4,7,8. All of which have a unique length. Log any that are found
                FindSimpleNumbers();

                // Try to find the more 'complex' numbers 0,2,3,5,6,9. All of which have a length of either 5 or 6. Log any thata are found
                FindComplexNumbers();


                return output;

            }



            /// <summary>
            /// Look for the numbers 1, 4, 7, and 8
            /// These numbers all have a unique number of 'active' segments
            /// Log them if found
            /// </summary>
            private void FindSimpleNumbers()
            {
                int i = Find_1(Input);
                if (i > -1) { LogNumber(1, Input[i]); }

                i = Find_4(Input);
                if (i > -1) { LogNumber(4, Input[i]); }

                i = Find_7(Input);
                if (i > -1) { LogNumber(7, Input[i]); }

                i = Find_8(Input);
                if (i > -1) { LogNumber(8, Input[i]); }
            }


            private void FindComplexNumbers()
            {
                List<string> numbersL5 = FindByLengthAll(Input, 5);
                List<string> numbersL6 = FindByLengthAll(Input, 6);

                // Try and find the number of length 5 that contains the same segments as 1 and/or 7
                // This number can only be the number 3, as both 2 and 5 do not contain all segments of 1 and/or 7
                // Try and find the number of length 6 that does not contain the same segments as 1 and/or 7
                // This number can only be the number 6, as both 0 and 9 contain the same segments as 1 and/or 7
                string s17 = "";
                if (IsNumKnown(1)) { s17 = Numbers[1]; }
                else if (IsNumKnown(7)) { s17 = Numbers[7]; }
                if (s17 != "")
                {
                    for (int i0 = 0; i0 < numbersL5.Count; i0++)
                    {
                        if (IsContainedIn(s17, numbersL5[i0]))
                        {
                            LogNumber(3, numbersL5[i0]);
                            numbersL5.RemoveAll(x => x == Numbers[3]);
                            break;
                        }
                    }

                    for (int i1 = 0; i1 < numbersL6.Count; i1++)
                    {
                        if (!IsContainedIn(s17, numbersL6[i1]))
                        {
                            LogNumber(6, numbersL6[i1]);
                            numbersL6.RemoveAll(x => x == Numbers[6]);
                            break;
                        }
                    }
                }

                // Try and find the number of length 6 that contains the same segments as either 3 or 4
                // This number can only be 9 as both 0 and 6 do not contain all the segments of 3 and/or 4
                string s34 = "";
                if (IsNumKnown(3)) { s34 = Numbers[3]; }
                else if (IsNumKnown(4)) { s34 = Numbers[4]; }
                if (s34 != "")
                {
                    for (int i2 = 0; i2 < numbersL6.Count; i2++)
                    {
                        if (IsContainedIn(s34, numbersL6[i2]))
                        {
                            LogNumber(9, numbersL6[i2]);
                            numbersL6.RemoveAll(x => x == Numbers[9]);
                            break;
                        }
                    }
                }




                

            }




            private int Find_1(string[] numStr) { return FindByLength(numStr, 2); }
            private int Find_4(string[] numStr) { return FindByLength(numStr, 4); }
            private int Find_7(string[] numStr) { return FindByLength(numStr, 3); }
            private int Find_8(string[] numStr) { return FindByLength(numStr, 7); }


            private int FindByLength(string[] numStr, int length)
            {
                for (int i = 0; i < numStr.Length; i++)
                {
                    if (numStr[i].Length == length) { return i; }
                }
                return -1;
            }


            private List<string> FindByLengthAll(string[] numStr, int length)
            {
                List<string> list = new List<string>();
                for (int i = 0; i < numStr.Length; i++)
                {
                    if (numStr[i].Length == length)
                    {
                        if (!list.Contains(numStr[i]))
                        {
                            list.Add(numStr[i]);
                        }
                    }
                }
                return list;
            }

            /// <summary>
            /// Is number 0  completely contained within number 1
            /// Are all elements of number 0 present in number 1
            /// </summary>
            /// <param name="num0"></param>
            /// <param name="num1"></param>
            /// <returns></returns>
            private bool IsContainedIn(string num0, string num1)
            {
                foreach (char c in num0)
                {
                    if (!num1.Contains(c)) { return false; }
                }
                return true;
            }

            /// <summary>
            /// Find the characters that are not present in number 0 but are present in number 1
            /// </summary>
            /// <param name="num0"></param>
            /// <param name="num1"></param>
            /// <returns></returns>
            private List<char> GetDiff(int num0, string num1)
            {
                return GetDiff(Numbers[num0], num1);
            }
            /// <summary>
            /// Find all characters that are not present in number 0 but are present in number 1
            /// </summary>
            /// <param name="num0"></param>
            /// <param name="num1"></param>
            /// <returns></returns>
            private List<char> GetDiff(string num0, string num1)
            {
                List<char> diff = new List<char>();
                foreach(char c1 in num1)
                {
                    if (!num0.Contains(c1)) { diff.Add(c1); }
                }
                return diff;
            }


            private bool IsNumKnown(int number)
            {
                return !string.IsNullOrWhiteSpace(Numbers[number]);
            }




            private void LogNumber(int number, string s)
            {
                if (IsNumKnown(number)) { return; }

                Numbers[number] = s;

                switch (number)
                {
                    case 0:
                        {
                            // For each possible character
                            foreach (char c in chars)
                            {
                                // If the character is present in the characters for this number
                                if (s.Contains(c))
                                {
                                    // Remove it from all segments that are not part of this number
                                    Segments[3].Remove(c);
                                }
                                // Else the character is not present in the characters for this number
                                else
                                {
                                    // Remove it from all segments that are part of this number
                                    Segments[0].Remove(c);
                                    Segments[1].Remove(c);
                                    Segments[2].Remove(c);
                                    Segments[4].Remove(c);
                                    Segments[5].Remove(c);
                                    Segments[6].Remove(c);
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            foreach (char c in chars)
                            {
                                if (s.Contains(c))
                                {
                                    Segments[0].Remove(c);
                                    Segments[1].Remove(c);
                                    Segments[3].Remove(c);
                                    Segments[4].Remove(c);
                                    Segments[6].Remove(c);
                                }
                                else
                                {
                                    Segments[2].Remove(c);
                                    Segments[5].Remove(c);
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            foreach (char c in chars)
                            {
                                if (s.Contains(c))
                                {
                                    Segments[1].Remove(c);
                                    Segments[5].Remove(c);
                                }
                                else
                                {
                                    Segments[0].Remove(c);
                                    Segments[2].Remove(c);
                                    Segments[3].Remove(c);
                                    Segments[4].Remove(c);
                                    Segments[6].Remove(c);
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            foreach (char c in chars)
                            {
                                if (s.Contains(c))
                                {
                                    Segments[1].Remove(c);
                                    Segments[4].Remove(c);
                                }
                                else
                                {
                                    Segments[0].Remove(c);
                                    Segments[2].Remove(c);
                                    Segments[3].Remove(c);
                                    Segments[5].Remove(c);
                                    Segments[6].Remove(c);
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            foreach (char c in chars)
                            {
                                if (s.Contains(c))
                                {
                                    Segments[0].Remove(c);
                                    Segments[4].Remove(c);
                                    Segments[6].Remove(c);
                                }
                                else
                                {
                                    Segments[1].Remove(c);
                                    Segments[2].Remove(c);
                                    Segments[3].Remove(c);
                                    Segments[5].Remove(c);
                                }
                            }
                            break;
                        }
                    case 5:
                        {
                            foreach (char c in chars)
                            {
                                if (s.Contains(c))
                                {
                                    Segments[2].Remove(c);
                                    Segments[4].Remove(c);
                                }
                                else
                                {
                                    Segments[0].Remove(c);
                                    Segments[1].Remove(c);
                                    Segments[3].Remove(c);
                                    Segments[5].Remove(c);
                                    Segments[6].Remove(c);
                                }
                            }
                            break;
                        }
                    case 6:
                        {
                            foreach (char c in chars)
                            {
                                if (s.Contains(c))
                                {
                                    Segments[2].Remove(c);
                                }
                                else
                                {
                                    Segments[0].Remove(c);
                                    Segments[1].Remove(c);
                                    Segments[3].Remove(c);
                                    Segments[4].Remove(c);
                                    Segments[5].Remove(c);
                                    Segments[6].Remove(c);
                                }
                            }
                            break;
                        }
                    case 7:
                        {
                            foreach (char c in chars)
                            {
                                if (s.Contains(c))
                                {
                                    Segments[1].Remove(c);
                                    Segments[3].Remove(c);
                                    Segments[4].Remove(c);
                                    Segments[6].Remove(c);
                                }
                                else
                                {
                                    Segments[0].Remove(c);
                                    Segments[2].Remove(c);
                                    Segments[5].Remove(c);
                                }
                            }
                            break;
                        }
                    case 8:
                        {
                            // All segments active, does not provide any information
                            break;
                        }
                    case 9:
                        {
                            foreach (char c in chars)
                            {
                                if (s.Contains(c))
                                {
                                    Segments[4].Remove(c);
                                }
                                else
                                {
                                    Segments[0].Remove(c);
                                    Segments[1].Remove(c);
                                    Segments[2].Remove(c);
                                    Segments[3].Remove(c);
                                    Segments[5].Remove(c);
                                    Segments[6].Remove(c);
                                }
                            }
                            break;
                        }
                }

                CheckForSegmentSolutions();
            }


            private void CheckForSegmentSolutions()
            {
                for (int i = 0; i < Segments.Count; i++)
                {
                    if (Segments[i].Count == 0) { System.Diagnostics.Debug.WriteLine($"D08: Segment {i} does not contain any possibilities !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"); }
                    else if (Segments[i].Count == 1)
                    {
                        switch (i)
                        {
                            case 0: { A = Segments[i][0]; break; }
                            case 1: { B = Segments[i][0]; break; }
                            case 2: { C = Segments[i][0]; break; }
                            case 3: { D = Segments[i][0]; break; }
                            case 4: { E = Segments[i][0]; break; }
                            case 5: { F = Segments[i][0]; break; }
                            case 6: { G = Segments[i][0]; break; }
                        }
                        Console.WriteLine($"Solution found, segment {i} = {Segments[i][0]}");
                    }
                }
            }


        }






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
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D06: recieved invalid input"); return; }

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




        private void OLDMETHOD(string[] input)
        {

            /* - Look for the numbers with a unique length
             *      - 1, 4, 7 ,8
             * - Try and find any segment that has an undisputed position
             *      - (segments of 7) minus (segments of 1) give segment A (top line)
             * 
             */


            int totalCount = 0;
            int count = 0;
            List<string> numbers = new List<string>() { "", "", "", "", "", "", "", "", "", "" };
            bool[] isNumKnown = new bool[numbers.Count];
            List<List<char>> segments = new List<List<char>>();
            int numsIndex = -1;
            foreach (string s in input)
            {
                for (int numRange = 0; numRange < numbers.Count; numRange++) { numbers[numRange] = ""; }
                Array.Fill(isNumKnown, false);

                segments = new List<List<char>>();
                for (int segCount = 0; segCount < 7; segCount++)
                {
                    segments.Add(new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
                }

                char A = 'h';
                char B = 'h';
                char C = 'h';
                char D = 'h';
                char E = 'h';
                char F = 'h';
                char G = 'h';

                string[] numStr = s.Split(' ');

                // Find 'simple' numbers, numbers who can be recognised by the number of segments
                numsIndex = Find_1(numStr);
                if (numsIndex != -1)
                {
                    numbers[1] = numStr[numsIndex];
                    isNumKnown[1] = true;
                }

                numsIndex = Find_4(numStr);
                if (numsIndex != -1)
                {
                    numbers[4] = numStr[numsIndex];
                    isNumKnown[4] = true;
                }

                numsIndex = Find_7(numStr);
                if (numsIndex != -1)
                {
                    numbers[7] = numStr[numsIndex];
                    isNumKnown[7] = true;
                }

                numsIndex = Find_8(numStr);
                if (numsIndex != -1)
                {
                    numbers[8] = numStr[numsIndex];
                    isNumKnown[8] = true;
                }


                // Check if output is readable
                count = GetOutputValuesTotal(numStr, numbers);
                if (count > -1)
                {
                    Console.WriteLine("2. ouput value found at 0");
                    totalCount += count;
                    continue;
                }




                // Find all numbers of length 5
                List<string> numL5 = FindByLengthAll(numStr, 5);
                // Try and deduce any values
                if (isNumKnown[1])
                {
                    for (int iL5_0 = 0; iL5_0 < numL5.Count; iL5_0++)
                    {
                        if (Contains(numbers[1], numL5[iL5_0]))
                        {
                            numbers[3] = numL5[iL5_0];
                            isNumKnown[3] = true;
                            numL5.RemoveAt(iL5_0);
                            continue;
                        }
                    }
                }
                else if (isNumKnown[7])
                {
                    for (int iL5_0 = 0; iL5_0 < numL5.Count; iL5_0++)
                    {
                        if (Contains(numbers[7], numL5[iL5_0]))
                        {
                            numbers[3] = numL5[iL5_0];
                            isNumKnown[3] = true;
                            numL5.RemoveAt(iL5_0);
                            continue;
                        }
                    }
                }

                // Find all numbers of length 6
                List<string> numL6 = FindByLengthAll(numStr, 6);
                // Try and deduce any values
                for (int iL6 = 0; iL6 < numL6.Count; iL6++)
                {
                    string num = numL6[iL6];
                    if (isNumKnown[1])
                    {
                        if (!Contains(numbers[1], num))
                        {
                            numbers[6] = num;
                            isNumKnown[6] = true;
                            numL6.RemoveAt(iL6);
                            continue;
                        }
                    }
                    else if (isNumKnown[7])
                    {
                        if (!Contains(numbers[1], num))
                        {
                            numbers[6] = num;
                            isNumKnown[6] = true;
                            numL6.RemoveAt(iL6);
                            continue;
                        }
                    }

                    if (isNumKnown[4])
                    {
                        if (Contains(numbers[4], num))
                        {
                            numbers[9] = num;
                            isNumKnown[9] = true;
                            numL6.RemoveAt(iL6);
                            continue;
                        }
                        else if (isNumKnown[1])
                        {
                            if (Contains(numbers[1], num))
                            {
                                numbers[0] = num;
                                isNumKnown[0] = true;
                                numL6.RemoveAt(iL6);
                                continue;
                            }
                        }
                        else if (isNumKnown[7])
                        {
                            if (Contains(numbers[7], num))
                            {
                                numbers[0] = num;
                                isNumKnown[0] = true;
                                numL6.RemoveAt(iL6);
                                continue;
                            }
                        }
                    }
                    else if (isNumKnown[3])
                    {
                        if (Contains(numbers[3], num))
                        {
                            numbers[9] = num;
                            isNumKnown[9] = true;
                            numL6.RemoveAt(iL6);
                            continue;
                        }
                        else if (isNumKnown[1])
                        {
                            if (Contains(numbers[1], num))
                            {
                                numbers[0] = num;
                                isNumKnown[0] = true;
                                numL6.RemoveAt(iL6);
                                continue;
                            }
                        }
                        else if (isNumKnown[7])
                        {
                            if (Contains(numbers[7], num))
                            {
                                numbers[0] = num;
                                isNumKnown[0] = true;
                                numL6.RemoveAt(iL6);
                                continue;
                            }
                        }
                    }
                }


                // Check if output is readable
                count = GetOutputValuesTotal(numStr, numbers);
                if (count > -1)
                {
                    Console.WriteLine("2. ouput value found at 0");
                    totalCount += count;
                    continue;
                }



                // Try and deduce any values
                if (numL5.Count > 0)
                {
                    for (int iL5 = 0; iL5 < numL5.Count; iL5++)
                    {
                        string num = numL5[iL5];
                        if (!isNumKnown[3])
                        {
                            if (isNumKnown[9])
                            {
                                if (Contains(num, numbers[9]))
                                {
                                    numbers[3] = num;
                                    isNumKnown[3] = true;
                                    numL5.RemoveAt(iL5);
                                    continue;
                                }
                            }
                        }

                        if (isNumKnown[6])
                        {
                            if (Contains(num, numbers[6]))
                            {
                                numbers[5] = num;
                                isNumKnown[5] = true;
                                numL5.RemoveAt(iL5);
                                continue;
                            }
                            else if (isNumKnown[1])
                            {
                                if (!Contains(numbers[1], num))
                                {
                                    numbers[2] = num;
                                    isNumKnown[2] = true;
                                    numL5.RemoveAt(iL5);
                                    continue;
                                }
                            }
                            else if (isNumKnown[7])
                            {
                                if (!Contains(numbers[1], num))
                                {
                                    numbers[2] = num;
                                    isNumKnown[2] = true;
                                    numL5.RemoveAt(iL5);
                                    continue;
                                }
                            }
                        }
                    }


                    // Check if output is readable
                    count = GetOutputValuesTotal(numStr, numbers);
                    if (count > -1)
                    {
                        Console.WriteLine("2. ouput value found at 0");
                        totalCount += count;
                        continue;
                    }



                }



                Console.WriteLine("No Count could be deduced, for " + s);




                // Deduce any segments that can be deduced
                if (isNumKnown[1] && isNumKnown[7])
                {
                    A = GetDiffSingle(numbers[1], numbers[7]);
                    segments[0] = new List<char>() { A };
                }
                if (isNumKnown[0])
                {
                    D = GetMissingSingle(numbers[0]);
                    segments[3] = new List<char> { D };
                }
                if (isNumKnown[6])
                {
                    C = GetMissingSingle(numbers[6]);
                    segments[2] = new List<char> { C };
                }
                if (isNumKnown[9])
                {
                    E = GetMissingSingle(numbers[9]);
                    segments[4] = new List<char> { E };
                }


                /*
                foreach(string sl6 in numL6)
                {
                    if (isNumKnown[1])
                    {
                        if (!Contains(numbers[1], sl6))
                        {
                            numbers[6] = sl6;
                            isNumKnown[6] = true;
                            numL6.Remove(sl6);
                            continue;
                        }
                    }
                    if (isNumKnown[4])
                    {
                        if (Contains(numbers[4], sl6))
                        {
                            numbers[9] = sl6;
                            isNumKnown[9] = true;
                            numL6.Remove(sl6);
                            continue;
                        }
                    }
                    if (isNumKnown[1] && isNumKnown[4])
                    {
                        if (Contains(numbers[1], sl6) && !Contains(numbers[4], sl6))
                        {
                            numbers[0] = sl6;
                            isNumKnown[0] = true;
                            numL6.Remove(sl6);
                            continue;
                        }
                    }
                }
                */





            }


            /*
            List<List<char>> segments = new List<List<char>>();
            for (int segCount = 0; segCount < 7; segCount++)
            {
                segments.Add(new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
            }
            */


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



        private int GetOutputValuesTotal(string[] numStr, List<string> numbers)
        {
            int total = 0;
            int value = 0;
            for (int i = numStr.Length - 1; i > numStr.Length - 5; i--)
            {
                value = numbers.IndexOf(numStr[i]);
                if (value == -1) { return -1; }
                else { total += value; }
            }
            return total;
        }




        private int Find_1(string[] numStr) { return FindByLength(numStr, 2); }

        private int Find_4(string[] numStr) { return FindByLength(numStr, 4); }

        private int Find_7(string[] numStr) { return FindByLength(numStr, 3); }

        private int Find_8(string[] numStr) { return FindByLength(numStr, 7); }

        private int FindByLength(string[] numStr, int length)
        {
            for (int i = 0; i < numStr.Length; i++)
            {
                if (numStr[i].Length == length) { return i; }
            }
            return -1;
        }

        private List<string> FindByLengthAll(string[] numStr, int length)
        {
            List<string> list = new List<string>();
            for (int i = 0;i < numStr.Length; i++)
            {
                if (numStr[i].Length == length)
                {
                    if (!list.Contains(numStr[i]))
                    {
                        list.Add(numStr[i]);
                    }
                }
            }
            return list;
        }


        private char GetDiffSingle(string s, string l)
        {
            bool isPresent = false;
            for (int i = 0; i < l.Length; i++)
            {
                isPresent = false;
                for (int j = 0; j < s.Length; j++)
                {
                    if (l[i] == s[j]) { isPresent = true; break; }
                }
                if (!isPresent) { return l[i]; }
            }
            // Only reached if both strings only contain the same characters, should not happen under correct use
            return '?';
        }

        private List<char> GetDiff(string s, string l)
        {
            List<char> diff = new List<char>();
            bool isPresent = false;
            for (int i = 0; i < l.Length; i++)
            {
                isPresent = false;
                for (int j = 0; j < s.Length; j++)
                {
                    if (l[i] == s[j]) { isPresent = true; break; }
                }
                if (!isPresent)
                {
                    if (!diff.Contains(l[i]))
                    {
                        diff.Add(l[i]);
                    }
                }
            }
            return diff;
        }

        private char GetMissingSingle(string s)
        {
            foreach (char c in chars)
            {
                if (!s.Contains(c)) { return c; }
            }
            // Only reached if s contains every character in chars, should not happen under correct use
            return '?';
        }

        private bool Contains(string s, string l)
        {
            bool containsAll = true;
            bool contains = false;
            foreach (char cs in s)
            {
                contains = false;
                foreach (char cl in l)
                {
                    if (cs == cl) { contains = true; break; }
                }
                if (!contains) { containsAll = false; break; }
            }
            return containsAll;
        }

    }
}
