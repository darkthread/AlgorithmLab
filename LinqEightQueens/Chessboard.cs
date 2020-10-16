using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqEightQueens
{
    public class Chessboard
    {
        const int DIG_SHIFT = 10;
        const int GRID_SIZE = 6;
        const int QUOTA = 6;
        /// <summary>
        /// 棋盤空格
        /// </summary>
        public HashSet<int> Slots { get; set; }
        /// <summary>
        /// 皇后位置
        /// </summary>
        public List<int> QueenPositions { get; set; }
        /// <summary>
        /// 空白棋盤
        /// </summary>
        public Chessboard()
        {
            //產生 11,12,...,18,21,22...28,...88 格子座標陣列
            Slots = new HashSet<int>(
                    Enumerable.Range(1, GRID_SIZE)
                        .SelectMany(x => Enumerable.Range(1, GRID_SIZE)
                        .Select(y => x * DIG_SHIFT + y))
                    );
            QueenPositions = new List<int>();
        }
        /// <summary>
        /// 現有棋盤在特定位置放上皇后生出新棋盤
        /// </summary>
        /// <param name="board">現有棋盤</param>
        /// <param name="pos">皇后位置</param>
        public Chessboard(Chessboard board, int pos)
        {
            Slots = new HashSet<int>(board.Slots.Except(new int[] { pos }));
            QueenPositions = new List<int>(board.QueenPositions.Concat(new int[] { pos }));
            var qX = pos / DIG_SHIFT;
            var qY = pos % DIG_SHIFT;
            Action<int, int> removeSlot = (px, py) =>
            {
                //超出範圍時忽略
                if (px < 1 || px > GRID_SIZE || py < 1 || py > GRID_SIZE) return;
                var v = px * DIG_SHIFT + py;
                if (Slots.Contains(v)) Slots.Remove(v);
            };
            for (var x = 1; x <= GRID_SIZE; x++)
            {
                for (var y = 1; y <= GRID_SIZE; y++)
                {
                    removeSlot(x, qY); //水平線
                    removeSlot(qX, y); //垂直線
                }
                var tx = x + (qX - qY);
                removeSlot(tx, x); //左上右下斜線
                tx = qX + qY - x;
                removeSlot(tx, x); //右上左下斜線
            }
        }
        /// <summary>
        /// 空格數小於未放皇后數量，無解
        /// </summary>
        public bool NoSolution => Slots.Count < QUOTA - QueenPositions.Count;
        /// <summary>
        /// 皇后數量達標
        /// </summary>
        public bool TouchDown => QUOTA == QueenPositions.Count;
        //印出棋盤
        public void Print()
        {
            for (var y = 1; y <= GRID_SIZE; y++)
            {
                for (var x = 1; x <= GRID_SIZE; x++)
                {
                    var pos = x * DIG_SHIFT + y;
                    if (QueenPositions.Contains(pos))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = 
                            Slots.Contains(pos) ? ConsoleColor.White : ConsoleColor.Yellow;
                    }
                    Console.Write("█ ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
