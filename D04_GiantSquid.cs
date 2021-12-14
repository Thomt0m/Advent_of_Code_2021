using System;

namespace Advent_of_Code
{
    class D04_GiantSquid
    {
        /*
        --- Day 4: Giant Squid ---
        You're already almost 1.5km (almost a mile) below the surface of the ocean, already so deep that you can't see any sunlight. What you can see, however, is a giant squid that has attached itself to the outside of your submarine.

        Maybe it wants to play bingo?

        Bingo is played on a set of boards each consisting of a 5x5 grid of numbers. Numbers are chosen at random, and the chosen number is marked on all boards on which it appears. (Numbers may not appear on all boards.) If all numbers in any row or any column of a board are marked, that board wins. (Diagonals don't count.)

        The submarine has a bingo subsystem to help passengers (currently, you and the giant squid) pass the time. It automatically generates a random order in which to draw numbers and a random set of boards (your puzzle input). For example:

        7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

        22 13 17 11  0
         8  2 23  4 24
        21  9 14 16  7
         6 10  3 18  5
         1 12 20 15 19

         3 15  0  2 22
         9 18 13 17  5
        19  8  7 25 23
        20 11 10 24  4
        14 21 16 12  6

        14 21 17 24  4
        10 16 15  9 19
        18  8 23 26 20
        22 11 13  6  5
         2  0 12  3  7

        After the first five numbers are drawn (7, 4, 9, 5, and 11), there are no winners, but the boards are marked as follows (shown here adjacent to each other to save space):

        22 13 17 11  0         3 15  0  2 22        14 21 17 24  4
         8  2 23  4 24         9 18 13 17  5        10 16 15  9 19
        21  9 14 16  7        19  8  7 25 23        18  8 23 26 20
         6 10  3 18  5        20 11 10 24  4        22 11 13  6  5
         1 12 20 15 19        14 21 16 12  6         2  0 12  3  7

        After the next six numbers are drawn (17, 23, 2, 0, 14, and 21), there are still no winners:

        22 13 17 11  0         3 15  0  2 22        14 21 17 24  4
         8  2 23  4 24         9 18 13 17  5        10 16 15  9 19
        21  9 14 16  7        19  8  7 25 23        18  8 23 26 20
         6 10  3 18  5        20 11 10 24  4        22 11 13  6  5
         1 12 20 15 19        14 21 16 12  6         2  0 12  3  7

        Finally, 24 is drawn:

        22 13 17 11  0         3 15  0  2 22        14 21 17 24  4
         8  2 23  4 24         9 18 13 17  5        10 16 15  9 19
        21  9 14 16  7        19  8  7 25 23        18  8 23 26 20
         6 10  3 18  5        20 11 10 24  4        22 11 13  6  5
         1 12 20 15 19        14 21 16 12  6         2  0 12  3  7

        At this point, the third board wins because it has at least one complete row or column of marked numbers (in this case, the entire top row is marked: 14 21 17 24 4).

        The score of the winning board can now be calculated. Start by finding the sum of all unmarked numbers on that board; in this case, the sum is 188. Then, multiply that sum by the number that was just called when the board won, 24, to get the final score, 188 * 24 = 4512.

        To guarantee victory against the giant squid, figure out which board will win first. What will your final score be if you choose that board?

        Your puzzle answer was 10680.

        The first half of this puzzle is complete! It provides one gold star: *

        --- Part Two ---
        On the other hand, it might be wise to try a different strategy: let the giant squid win.

        You aren't sure how many bingo boards a giant squid could play at once, so rather than waste time counting its arms, the safe thing to do is to figure out which board will win last and choose that one. That way, no matter which boards it picks, it will win for sure.

        In the above example, the second board is the last to win, which happens after 13 is eventually called and its middle column is completely marked. If you were to keep playing until this point, the second board would have a sum of unmarked numbers equal to 148 for a final score of 148 * 13 = 1924.

        Figure out which board will win last. Once it wins, what would its final score be?
        */




        public D04_GiantSquid(string[] input)
        {
            if (input == null || input.Length == 0) { System.Diagnostics.Debug.WriteLine("D04_GiantSquid: recieved invalid input"); return; }

            Console.WriteLine("---- Day 4, Giant Squid ----" + "\n");

            int card_dim = 5;

            List<int> numbers = GetNumbers(input);
            List<int[,]> cards = GetCards(input, card_dim);

            // TODO remove
            /*
            Console.WriteLine("numbers.Count = " + numbers.Count);
            foreach (int num in numbers) { Console.WriteLine(num); }

            Console.WriteLine("cards.Count = " + cards.Count);
            foreach (int[,] card in cards)
            {
                Console.WriteLine(card[0,0]);
            }
            */


            bool[,,] cardsValuesTicked = new bool[cards.Count, card_dim, card_dim];
            int[,,] cardsLineCompletion = new int[cards.Count, cards[0].Rank, card_dim];
            // winDetails details: 0 = card index, 1 = orientation of winning line -> 0-column 1-row, 2 = winning line index, 3 = index of last number called
            int[] winDetails = new int[4];
            for (int num = 0; num < numbers.Count; num++)
            {
                for (int card = 0; card < cards.Count; card++)
                {
                    for (int m = 0; m < card_dim; m++)
                    {
                        for (int n = 0; n < card_dim; n++)
                        {
                            if (cards[card][m,n] == numbers[num])
                            {
                                cardsValuesTicked[card, m, n] = true;
                                cardsLineCompletion[card, 0, m]++;
                                cardsLineCompletion[card, 1, n]++;
                                if (cardsLineCompletion[card, 0, m] >= card_dim)
                                {
                                    winDetails[0] = card;
                                    winDetails[1] = 0;
                                    winDetails[2] = m;
                                    winDetails[3] = num;
                                    goto WinnerFound;
                                }
                                else if(cardsLineCompletion[card, 1, m] >= card_dim)
                                {
                                    winDetails[0] = card;
                                    winDetails[1] = 1;
                                    winDetails[2] = m;
                                    winDetails[3] = num;
                                    goto WinnerFound;
                                }
                            }
                        }
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("D04: No winning card was found");

        WinnerFound:

            // TODO remove
            /*
            Console.WriteLine(isWinnerFound ? "Winner found" : "Winner not found");
            string winnerMessage = "Winning card = " + winDetails[0] + ", " + (winDetails[1] == 0 ? "column" : "row") + " = " + winDetails[2] + ", final num = " + numbers[winDetails[3]];
            Console.WriteLine(winnerMessage);
            */

            int remainingValue = 0;
            for (int m1 = 0; m1 < card_dim; m1++)
            {
                for (int n1 = 0; n1 < card_dim; n1++)
                {
                    if (!cardsValuesTicked[winDetails[0], m1, n1]) { remainingValue += cards[winDetails[0]][m1, n1]; }
                }
            }
            Console.WriteLine("1. Multiplying remaining numbers by last called number = " + remainingValue * numbers[winDetails[3]]);





            Console.WriteLine("\n\n");
        }




        private List<int> GetNumbers(string[] input)
        {
            List<int> numbers = new List<int>();

            string[] numStrs = input[0].Split(',');
            int value = -1;
            foreach (string numStr in numStrs)
            {
                if (int.TryParse(numStr, out value))
                {
                    numbers.Add(value);
                }
            }
            return numbers;
        }

        private List<int[,]> GetCards(string[] input, int card_dim = 5)
        {
            List<int[,]> cards = new List<int[,]>();

            int[,] card;
            string[] values;
            int value;
            int n;
            for (int i = 2; i < input.Length; i += card_dim + 1)
            {
                card = new int[card_dim, card_dim];
                for (int m = 0; m < card_dim; m++)
                {
                    values = input[i + m].Split(' ');
                    n = 0;
                    foreach (string valstr in values)
                    {
                        if (int.TryParse(valstr, out value))
                        {
                            card[m, n] = value;
                            n++;
                        }
                    }
                }
                cards.Add(card);
            }

            return cards;
        }

        private void FillArray3DBool(bool[,,] array, bool value)
        {
            for (int m = 0; m < array.GetLength(0); m++)
            {
                for (int n = 0; n < array.GetLength(1); n++)
                {
                    for (int o = 0; o < array.GetLength(2); o++)
                    {
                        array[m, n, o] = value;
                    }
                }
            }
        }

        private void FillArray3DInt(int[,,] array, int value)
        {
            for (int m = 0; m < array.GetLength(0); m++)
            {
                for (int n = 0; n < array.GetLength(1); n++)
                {
                    for (int o = 0; o < array.GetLength(2); o++)
                    {
                        array[m, n, o] = value;
                    }
                }
            }
        }


    }
}
