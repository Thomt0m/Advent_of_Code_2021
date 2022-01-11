using System;

namespace Advent_of_Code
{
    class D14_ExtendedPolymerization
    {
        /*
        --- Day 14: Extended Polymerization ---
        The incredible pressures at this depth are starting to put a strain on your submarine. The submarine has polymerization equipment that would produce suitable materials to reinforce the submarine, and the nearby volcanically-active caves should even have the necessary input elements in sufficient quantities.

        The submarine manual contains instructions for finding the optimal polymer formula; specifically, it offers a polymer template and a list of pair insertion rules (your puzzle input). You just need to work out what polymer would result after repeating the pair insertion process a few times.

        For example:

        NNCB

        CH -> B
        HH -> N
        CB -> H
        NH -> C
        HB -> C
        HC -> B
        HN -> C
        NN -> C
        BH -> H
        NC -> B
        NB -> B
        BN -> B
        BB -> N
        BC -> B
        CC -> N
        CN -> C

        The first line is the polymer template - this is the starting point of the process.

        The following section defines the pair insertion rules. A rule like AB -> C means that when elements A and B are immediately adjacent, element C should be inserted between them. These insertions all happen simultaneously.

        So, starting with the polymer template NNCB, the first step simultaneously considers all three pairs:

        The first pair (NN) matches the rule NN -> C, so element C is inserted between the first N and the second N.
        The second pair (NC) matches the rule NC -> B, so element B is inserted between the N and the C.
        The third pair (CB) matches the rule CB -> H, so element H is inserted between the C and the B.
        Note that these pairs overlap: the second element of one pair is the first element of the next pair. Also, because all pairs are considered simultaneously, inserted elements are not considered to be part of a pair until the next step.

        After the first step of this process, the polymer becomes NCNBCHB.

        Here are the results of a few steps using the above rules:

        Template:     NNCB
        After step 1: NCNBCHB
        After step 2: NBCCNBBBCBHCB
        After step 3: NBBBCNCCNBBNBNBBCHBHHBCHB
        After step 4: NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB

        This polymer grows quickly. After step 5, it has length 97; After step 10, it has length 3073. After step 10, B occurs 1749 times, C occurs 298 times, H occurs 161 times, and N occurs 865 times; taking the quantity of the most common element (B, 1749) and subtracting the quantity of the least common element (H, 161) produces 1749 - 161 = 1588.

        Apply 10 steps of pair insertion to the polymer template and find the most and least common elements in the result. What do you get if you take the quantity of the most common element and subtract the quantity of the least common element?

        Your puzzle answer was 3058.

        --- Part Two ---
        The resulting polymer isn't nearly strong enough to reinforce the submarine. You'll need to run more steps of the pair insertion process; a total of 40 steps should do it.

        In the above example, the most common element is B (occurring 2192039569602 times) and the least common element is H (occurring 3849876073 times); subtracting these produces 2188189693529.

        Apply 40 steps of pair insertion to the polymer template and find the most and least common elements in the result. What do you get if you take the quantity of the most common element and subtract the quantity of the least common element?

        Your puzzle answer was 3447389044530.

        Both parts of this puzzle are complete! They provide two gold stars: **
        */

        public D14_ExtendedPolymerization(string[] input)
        {
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D14: recieved invalid input"); return; }

            Console.WriteLine("---- Day 14, Extended Polymerization ----" + "\n");

            // Part 1
            Polymer pol = new Polymer(input);
            long result = pol.GetMostMinusLeast(10);
            Console.WriteLine("1. (Polymer Class) Subtracting least common from most common = " + result);

            // Part 2
            result = pol.GetMostMinusLeast(40);
            Console.WriteLine("2. Subtracting least common from most common = " + result);

            Console.WriteLine("\n\n");
        }

        private string[] ExampleInput
        {
            get
            {
                return new string[]
                {
                    "NNCB",
                    "",
                    "CH -> B",
                    "HH -> N",
                    "CB -> H",
                    "NH -> C",
                    "HB -> C",
                    "HC -> B",
                    "HN -> C",
                    "NN -> C",
                    "BH -> H",
                    "NC -> B",
                    "NB -> B",
                    "BN -> B",
                    "BB -> N",
                    "BC -> B",
                    "CC -> N",
                    "CN -> C"
                };
            }
        }











        private class Polymer
        {
            /// <summary>
            /// Original input given
            /// </summary>
            private string[] Input;
            /// <summary>
            /// Starting Polymer string
            /// </summary>
            private string PolymerTemplate;
            /// <summary>
            /// List of all characters that could possibly be in the polymer
            /// </summary>
            private SortedDictionary<char, long> Characters;
            /// <summary>
            /// Insertion instructions, Key (string) = pair to match, Value (char) = char to insert
            /// </summary>
            private Dictionary<string, char> Insertions;
            /// <summary>
            /// All possible pairs that might occure within the polymer, Key (string) = pair, Value (long) = number of occurances
            /// </summary>
            private Dictionary<string, long> PolymerPairs;


            public Polymer(string[] input)
            {
                Input = new string[input.Length];
                input.CopyTo(Input, 0);

                PolymerTemplate = Input[0];


                // Get all Insertion instructions and put them in the 'Insertions' dict
                Insertions = new Dictionary<string, char>();
                string[] lineSegs;
                for (int i = 2; i < input.Length; i++)
                {
                    lineSegs = input[i].Split(' ');
                    if (lineSegs.Length != 3) { System.Diagnostics.Debug.WriteLine($"D14: Polymer.NewInsertionsDict() incorrect number of segments at line {i}, {input[i]}"); }
                    else if (lineSegs[0].Length != 2) { System.Diagnostics.Debug.WriteLine($"D14: Polymer.NewInsertionsDict() invalid length of pair at line {i}, {input[i]}"); }
                    else if (lineSegs[2].Length != 1) { System.Diagnostics.Debug.WriteLine($"D14: Polymer.NewInsertionsDict() invalid length of char at line {i}, {input[i]}"); }
                    else
                    {
                        Insertions.Add(lineSegs[0], lineSegs[2][0]);
                    }
                }


                // Get all possible pairs that might occure in the polymer, and add them to the 'PolymerPairs' dict
                PolymerPairs = new Dictionary<string, long>();
                Characters = new SortedDictionary<char, long>();
                foreach (char charPT in PolymerTemplate)
                {
                    if (!Characters.ContainsKey(charPT)) { Characters.Add(charPT, 0); }
                }
                for (int j = 0; j < Insertions.Count; j++)
                {
                    if (!Characters.ContainsKey(Insertions.ElementAt(j).Key[0])) { Characters.Add(Insertions.ElementAt(j).Key[0], 0); }
                    if (!Characters.ContainsKey(Insertions.ElementAt(j).Key[1])) { Characters.Add(Insertions.ElementAt(j).Key[1], 0); }
                    if (!Characters.ContainsKey(Insertions.ElementAt(j).Value)) { Characters.Add(Insertions.ElementAt(j).Value, 0); }
                }
                string pair;
                foreach (char c1 in Characters.Keys)
                {
                    foreach (char c2 in Characters.Keys)
                    {
                        pair = "" + c1 + c2;
                        try
                        {
                            PolymerPairs.Add(pair, 0);
                        }
                        catch (ArgumentException e)
                        {
                            System.Diagnostics.Debug.WriteLine($"D14: Polymer() pair {pair} is already present in Dictionary");
                            System.Diagnostics.Debug.WriteLine(e.Message);
                        }
                    }
                }
            }


            /// <summary>
            /// Get the value that results from taking the quantity of the most common element and subracting the quantity of the least common element, after <c>steps</c> amount of steps
            /// </summary>
            /// <param name="steps"></param>
            /// <returns></returns>
            public long GetMostMinusLeast(int steps)
            {
                ResetPolymerPairCount();
                ResetCharacterCount();

                // Do the specified number of steps
                for (int step = 0; step < steps; step++)
                {
                    DoStep();
                }

                // Count the occurances of each character in PolymerPairs
                foreach (KeyValuePair<string, long> pp in PolymerPairs)
                {
                    if (pp.Value > 0)
                    {
                        Characters[pp.Key[0]] += pp.Value;
                    }
                }
                Characters[PolymerTemplate.Last()] += 1;

                // Find the most and least common characters
                long min = long.MaxValue;
                long max = 0;
                foreach(long value in Characters.Values)
                {
                    if (value > 0 && value < min) { min = value; }
                    if (value > max) { max = value; }
                }
                return max - min;
            }


            private void DoStep()
            {
                // Copy of PolymerPairs, preserving the starting values of PolymerPairs through this step
                Dictionary<string, long> ppOld = new Dictionary<string, long>(PolymerPairs);

                string key;
                long value;
                char insertion;
                for (int i = 0; i < ppOld.Count; i++)
                {
                    value = ppOld.ElementAt(i).Value;
                    if (value > 0)
                    {
                        key = ppOld.ElementAt(i).Key;
                        if (Insertions.ContainsKey(key))
                        {
                            // Remove the old pairs (as they no longer exist since a char will be inserted between them)
                            ChangePolymerPairValue(key, -value);
                            // Add the two newly created pairs
                            insertion = Insertions[key];
                            ChangePolymerPairValue(new string(new char[] { key[0], insertion }), value);
                            ChangePolymerPairValue(new string(new char[] { insertion, key[1] }), value);
                        }
                    }
                }
            }


            private void ChangePolymerPairValue(string pair, long value = 1)
            {
                try
                {
                    PolymerPairs[pair] += value;
                }
                catch (KeyNotFoundException e)
                {
                    System.Diagnostics.Debug.WriteLine($"D14: Polymer.ChangePolymerPairValue() pair {pair} not found");
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }



            private void ResetPolymerPairCount()
            {
                foreach (string k in PolymerPairs.Keys.ToList())
                {
                    PolymerPairs[k] = 0;
                }

                for (int i = 0; i < PolymerTemplate.Length - 1; i++)
                {
                    string pair = "" + PolymerTemplate[i] + PolymerTemplate[i + 1];
                    PolymerPairs[pair] += 1;
                }
            }

            private void ResetCharacterCount()
            {
                foreach (char k in Characters.Keys.ToList())
                {
                    Characters[k] = 0;
                }
            }
        }
    }
}
