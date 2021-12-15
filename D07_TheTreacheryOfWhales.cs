using System;

namespace Advent_of_Code
{
    class D07_TheTreacheryOfWhales
    {
        /*
        --- Day 7: The Treachery of Whales ---
        A giant whale has decided your submarine is its next meal, and it's much faster than you are. There's nowhere to run!

        Suddenly, a swarm of crabs (each in its own tiny submarine - it's too deep for them otherwise) zooms in to rescue you! They seem to be preparing to blast a hole in the ocean floor; sensors indicate a massive underground cave system just beyond where they're aiming!

        The crab submarines all need to be aligned before they'll have enough power to blast a large enough hole for your submarine to get through. However, it doesn't look like they'll be aligned before the whale catches you! Maybe you can help?

        There's one major catch - crab submarines can only move horizontally.

        You quickly make a list of the horizontal position of each crab (your puzzle input). Crab submarines have limited fuel, so you need to find a way to make all of their horizontal positions match while requiring them to spend as little fuel as possible.

        For example, consider the following horizontal positions:

        16,1,2,0,4,2,7,1,2,14
        This means there's a crab with horizontal position 16, a crab with horizontal position 1, and so on.

        Each change of 1 step in horizontal position of a single crab costs 1 fuel. You could choose any horizontal position to align them all on, but the one that costs the least fuel is horizontal position 2:

        Move from 16 to 2: 14 fuel
        Move from 1 to 2: 1 fuel
        Move from 2 to 2: 0 fuel
        Move from 0 to 2: 2 fuel
        Move from 4 to 2: 2 fuel
        Move from 2 to 2: 0 fuel
        Move from 7 to 2: 5 fuel
        Move from 1 to 2: 1 fuel
        Move from 2 to 2: 0 fuel
        Move from 14 to 2: 12 fuel
        This costs a total of 37 fuel. This is the cheapest possible outcome; more expensive outcomes include aligning at position 1 (41 fuel), position 3 (39 fuel), or position 10 (71 fuel).

        Determine the horizontal position that the crabs can align to using the least fuel possible. How much fuel must they spend to align to that position?

        Your puzzle answer was 354129.

        The first half of this puzzle is complete! It provides one gold star: *

        --- Part Two ---
        The crabs don't seem interested in your proposed solution. Perhaps you misunderstand crab engineering?

        As it turns out, crab submarine engines don't burn fuel at a constant rate. Instead, each change of 1 step in horizontal position costs 1 more unit of fuel than the last: the first step costs 1, the second step costs 2, the third step costs 3, and so on.

        As each crab moves, moving further becomes more expensive. This changes the best horizontal position to align them all on; in the example above, this becomes 5:

        Move from 16 to 5: 66 fuel
        Move from 1 to 5: 10 fuel
        Move from 2 to 5: 6 fuel
        Move from 0 to 5: 15 fuel
        Move from 4 to 5: 1 fuel
        Move from 2 to 5: 6 fuel
        Move from 7 to 5: 3 fuel
        Move from 1 to 5: 10 fuel
        Move from 2 to 5: 6 fuel
        Move from 14 to 5: 45 fuel
        This costs a total of 168 fuel. This is the new cheapest possible outcome; the old alignment position (2) now costs 206 fuel instead.

        Determine the horizontal position that the crabs can align to using the least fuel possible so they can make you an escape route! How much fuel must they spend to align to that position?
        */


        public D07_TheTreacheryOfWhales(int[] input)
        {
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D06: recieved invalid input"); return; }

            Console.WriteLine("---- Day 7, The Treachery Of Whales ----" + "\n");

            int highestValue = 0;
            for (int i0 = 0; i0 < input.Length; i0++)
            {
                if (input[i0] > highestValue) { highestValue = input[i0]; }
            }
            highestValue += 1;
            
            int[] crabPos = new int[highestValue];
            for (int i1 = 0; i1 < input.Length; i1++)
            {
                crabPos[input[i1]]++;
            }

            int[] fuelToPos = new int[highestValue];
            for (int i2 = 0; i2 < fuelToPos.Length; i2++)
            {
                for (int j2 = 0; j2 < crabPos.Length; j2++)
                {
                    fuelToPos[i2] += Math.Abs(i2 - j2) * crabPos[j2];
                }
            }

            int cheapestPos = -1;
            long cheapestPosFuelCost = long.MaxValue;
            for (int i3 = 0; i3 < fuelToPos.Length; i3++)
            {
                if (fuelToPos[i3] < cheapestPosFuelCost)
                {
                    cheapestPos = i3;
                    cheapestPosFuelCost = fuelToPos[i3];
                }
            }
            Console.WriteLine("1. Fuel cost to reach cheapest position = " + cheapestPosFuelCost);

            /* Method calculating fuel cost for each position
            long[] fuelToPosLong = new long[highestValue];
            int delta = 0;
            for (int i4 = 0; i4 < fuelToPosLong.Length; i4++)
            {
                for (int j4 = 0; j4 < crabPos.Length; j4++)
                {
                    delta = Math.Abs(i4 - j4);
                    fuelToPosLong[i4] += (delta * ((delta + 1) / 2)) * crabPos[j4];
                }
            }

            cheapestPos = -1;
            cheapestPosFuelCost = long.MaxValue;
            for (int i5 = 0; i5 < fuelToPosLong.Length; i5++)
            {
                if (fuelToPosLong[i5] < cheapestPosFuelCost)
                {
                    cheapestPos = i5;
                    cheapestPosFuelCost = fuelToPosLong[i5];
                }
            }
            Console.WriteLine("2. Fuel cost to reach cheapest position = " + cheapestPosFuelCost);
            */

            long totalPos = 0;
            foreach (int pos in input)
            {
                totalPos += pos;
            }
            int centerPos = (int)Math.Round(totalPos / (double)input.Length);
            long fuelToCenter = 0;
            int delta = 0;
            for (int i4 = 0; i4 < crabPos.Length; i4++)
            {
                delta = Math.Abs(centerPos - i4);
                fuelToCenter += (delta * ((delta + 1) / 2)) * crabPos[i4];
            }
            Console.WriteLine("2. Fuel cost the reach cheapest position = " + fuelToCenter);
            
        }

    }
}
