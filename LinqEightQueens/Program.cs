using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqEightQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Chessboard();
            Explore(board);
            Console.ReadLine();
        }

        static int resIdx = 1;
        static void Explore(Chessboard board)
        {
            //放皇后的順序一律由左上到右下，排除重複組合
            var minPos = board.QueenPositions.Any() ? board.QueenPositions.Max() : 0;
            foreach (var pos in board.Slots.Where(o => o > minPos))
            {
                var newBoard = new Chessboard(board, pos);
                if (newBoard.TouchDown)
                {
                    Console.WriteLine($"=== Result {resIdx++:00} ===");
                    newBoard.Print();
                }
                else if (!newBoard.NoSolution)
                {
                    Explore(newBoard);
                }
            }
        }
    }
}
