using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming
{
    internal class SnakesAndLadders
    {
        public static void SnakesAndLaddersMethod()
        {
            Random random = new Random();
            string[,] board =
                {
                    { "*", "*", "*", "*", "L1", "*", "*", "*", "*", "*" },
                    { "*", "*", "*", "S1", "*", "*", "*", "L1", "*", "*" },
                    { "*", "*", "*", "*", "*", "L2", "*", "*", "*", "*" },
                    { "*", "*", "*", "*", "*", "S1", "*", "S2", "*", "*" },
                    { "L1", "*", "*", "*", "*", "*", "L2", "*", "*", "*" },
                    { "*", "S1", "*", "*", "*", "*", "*", "*", "S2", "*" },
                    { "*", "*", "L1", "*", "*", "*", "*", "L2", "*", "*" },
                    { "*", "*", "*", "S1", "*", "*", "*", "*", "*", "*" },
                    { "*", "S2", "*", "*", "*", "*", "*", "*", "*", "*" },
                    { "L2", "*", "*", "*", "*", "*", "S2", "*", "*", "*" }
                };

            bool firstWin = false;
            bool secondWin = false;

            //int[,] firstPos = new int[1, 1];
            //int[,]seconPos = new int[1,1];
            //int globalPos1 = 0, v

            int firstPos = 0, secondPos = 0;
            while (true)
            {
                int turn1 = random.Next(1, 7);
                firstPos += turn1;
                if (firstPos > 99) firstPos -= turn1;
                if (board[firstPos/10,firstPos%10] =="L1")
                {
                    while (firstPos<=99 && board[firstPos / 10, firstPos % 10] != "L2") firstPos++;
                }
                if (board[firstPos/10, firstPos % 10] == "S2")
                {
                    while (firstPos >= 0 && board[firstPos / 10, firstPos % 10] != "S1") firstPos--;
                }

                int turn2 = random.Next(1, 7);
                secondPos += turn2;
                if (secondPos > 99) secondPos -= turn2;
                if (board[secondPos / 10, secondPos % 10] == "L1")
                {
                    while (secondPos <= 99 && board[secondPos / 10, secondPos % 10] != "L2") secondPos++;
                }
                if (board[secondPos / 10, secondPos % 10] == "S2")
                {
                    while (secondPos >= 0 && board[secondPos / 10, secondPos % 10] != "S1") secondPos--;
                }

                if (firstPos == 99) firstWin = true;
                if (secondPos == 99) secondWin = true;
                if (firstWin || secondWin) break;
            }
            if (firstWin) Console.Write($"Player1 wins\n");
            else Console.WriteLine($"Player2 wins\n");
            Console.Write($"Player1 : {firstPos}\n");
            Console.Write($"Player2 : {secondPos}");
        }

    }
}
