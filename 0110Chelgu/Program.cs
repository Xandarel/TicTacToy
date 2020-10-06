using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0110Chelgu
{
    enum Chose
    {
        X=1,
        O=4,
        no_win=50
    }

    class TicTacToy
    {
        private readonly Chose[,] field;
        private readonly int size;
        public TicTacToy(int size=3)
        {
            this.size = size;
            field = new Chose[size, size];
        }
        public string DrawField()
        {
            var drawField = "—————————\n";
            //Console.Write("—————————\n");
            for (var x = 0; x < size; x++)
                for (var y = 0; y < size; y++)
                {
                    if (field[x, y] != 0)
                        drawField += $"|{field[x, y]}|";
                    else
                        drawField += "| |";
                    if (y == size - 1)
                        drawField += "\n—————————\n";
                }
            return drawField;
        }
        public bool GetTurn(Chose chose, int xPos, int yPos)
        {
            if (xPos > field.GetLength(0) || xPos < 0 || yPos > field.GetLength(1) || yPos < 0)
            {
                Console.WriteLine("Ход вне поля");
                return false;
            }

            if (field[xPos, yPos] == 0)
            {
                field[xPos, yPos] = chose;
                CheckWinner();
                return true;
            }
            else
                Console.WriteLine("Эта клетка занята");
            return false;
        }

        private Chose GetAnsverForSumm(params int[] elements)
        {
            foreach (var el in elements)
                switch (el)
                {
                    case 3:
                        return Chose.X;
                    case 12:
                        return Chose.O;
                }
            return Chose.no_win;

        }
        private Chose CheckSumm(int rowCol)
        {
            var rowSumm = 0;
            var columnSumm = 0;
            var mainDiagonal = 0;
            var noMainDiagonal = 0;
            for (var pos = 0; pos<size; pos++)
            {
                rowSumm += (int)field[rowCol, pos];
                columnSumm += (int)field[pos, rowCol];
                mainDiagonal += (int)field[pos, pos];
                noMainDiagonal += (int)field[size - pos - 1, pos];
            }
            return GetAnsverForSumm(rowSumm, columnSumm, mainDiagonal, noMainDiagonal);
        }
        public Chose CheckWinner()
        {
            for (var pos = 0;pos<size;pos++)
            {
                var winner = CheckSumm(pos);
                if (winner == Chose.O || winner == Chose.X)
                    return winner;

            }
            return Chose.no_win;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var a = new TicTacToy();
            for (var turn = 0; ; turn++)
            {
                Console.WriteLine(a.DrawField());
                if (turn % 2 == 0)
                {
                    bool ans;
                    do
                    {
                        Console.WriteLine($"Ходит {Chose.X}");
                        var pos = Console.ReadLine().Split(' ');
                        ans = a.GetTurn(Chose.X, Convert.ToInt32(pos[0]), Convert.ToInt32(pos[1]));
                    } while (!ans);
                }
                else
                {
                    bool ans;
                    do
                    {
                        Console.WriteLine($"Ходит {Chose.O}");
                        var pos = Console.ReadLine().Split(' ');
                        ans = a.GetTurn(Chose.O, Convert.ToInt32(pos[0]), Convert.ToInt32(pos[1]));
                    } while (!ans);
                }
                var winner = a.CheckWinner();
                if (winner == Chose.O || winner == Chose.X)
                {
                    Console.WriteLine(a.DrawField());
                    Console.WriteLine($"Победил {winner}");
                    break;
                }
            }
            Console.ReadKey();
        }
    }
}
