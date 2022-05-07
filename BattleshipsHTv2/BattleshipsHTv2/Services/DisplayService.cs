using BattleshipsHTv2.Entities.BoardElements;
using BattleshipsHTv2.Entities.Ships;
using BattleshipsHTv2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Services
{
    public class DisplayService
    {
        private ConsoleColor Background { get; set; }
        private ConsoleColor Foreground { get; set; }

        public void WaitForTime(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void PrintBoard(Board board)
        {
            Clear();
            Square[,] boardToPrint = board.Ocean;
            Square s = new Square(11, 11);
            string water = s.GetCharacter().ToString();
            ConsoleColor waterColor = s.GetColore();
            Console.WriteLine("    A B C D E F G H I J ");


            for (int i = 0; i < boardToPrint.GetLength(0); i++)
            {
                if (i >= board.Size - 1)
                {
                    Console.Write($" {i + 1} ");
                }
                else { Console.Write($"  {i + 1} "); }

                for (int j = 0; j < boardToPrint.GetLength(1); j++)
                {
                    if (boardToPrint[j, i].SquareStatus == SquareStatusEnum.ship)
                    {
                        Print($"{water} ", waterColor);
                    }
                    else
                    {
                        Print($"{boardToPrint[j, i].GetCharacter()} ", boardToPrint[j, i].GetColore());
                    }
                }
                Console.WriteLine();
            }
        }

        public void PrintBoard(Board board, Ship ship)
        {
            (int xmin, int xmax, int ymin, int ymax) shipCords = GenerateTuple(ship.OriginPoint.x, ship.OriginPoint.y, ship.Length, ship.Direction);
            Clear();
            Square[,] boardToPrint = board.Ocean;
            Console.WriteLine("    A B C D E F G H I J ");

            for (int i = 0; i < boardToPrint.GetLength(0); i++)
            {
                if (i >= board.Size - 1)
                {
                    Console.Write($" {i + 1} ");
                }
                else { Console.Write($"  {i + 1} "); }

                for (int j = 0; j < boardToPrint.GetLength(1); j++)
                {
                    if ((j >= shipCords.xmin && j <= shipCords.xmax) && (i >= shipCords.ymin && i <= shipCords.ymax))
                    {
                        if (board.possibleShip(ship))
                        {
                            Print("X ", ConsoleColor.Green);
                        }
                        else
                        {
                            Print("X ", ConsoleColor.DarkRed);
                        }
                    }
                    else { Print($"{boardToPrint[j, i].GetCharacter()} ", boardToPrint[j, i].GetColore()); }

                }
                Console.WriteLine();
            }
        }

        public void PrintBoard(Board board, int x, int y)
        {
            Clear();
            Square[,] boardToPrint = board.Ocean;
            Square s = new Square(11, 11);
            string water = s.GetCharacter().ToString();
            ConsoleColor waterColor = s.GetColore();

            Console.WriteLine("    A B C D E F G H I J ");

            for (int i = 0; i < boardToPrint.GetLength(0); i++)
            {
                if (i >= board.Size - 1)
                {
                    Console.Write($" {i + 1} ");
                }
                else { Console.Write($"  {i + 1} "); }

                for (int j = 0; j < boardToPrint.GetLength(1); j++)
                {
                    if ((j == x) && (i == y))
                    {
                        if (board.Ocean[x, y].SquareStatus == SquareStatusEnum.empty || board.Ocean[x, y].SquareStatus == SquareStatusEnum.ship)
                        {
                            Print("X ", ConsoleColor.Green);
                        }
                        else
                        {
                            Print("X ", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        if (boardToPrint[j, i].SquareStatus == SquareStatusEnum.ship)
                        {
                            Print($"{water} ", waterColor);
                        }
                        else
                        {
                            Print($"{boardToPrint[j, i].GetCharacter()} ", boardToPrint[j, i].GetColore());
                        }


                    }

                }
                Console.WriteLine();
            }
        }

        public void PrintMenu(List<string> options, int light)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (light == i)
                {
                    PrintReverse(options[i]);
                }
                else
                {
                    PrintMessage(options[i]);
                }
            }
        }

        private void PrintReverse(string s)
        {
            Console.BackgroundColor = Foreground;
            Console.ForegroundColor = Background;
            Console.WriteLine(s);
            Console.BackgroundColor = Background;
            Console.ForegroundColor = Foreground;
        }
        private void Print(string s, ConsoleColor col)
        {
            Console.ForegroundColor = col;
            Console.Write(s);
            Console.ForegroundColor = Foreground;
        }
        private void Print(string c)
        {
            Console.Write(c);
        }
        private void Print(char c, ConsoleColor col)
        {
            Console.ForegroundColor = col;
            Console.Write(c);
            Console.ForegroundColor = Foreground;
        }
        private void Print(char c)
        {
            Console.Write(c);
        }

        public void PrintMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public void PrintMessage(string msg, ConsoleColor col)
        {
            Console.ForegroundColor = col;
            Console.WriteLine(msg);
            Console.ForegroundColor = Foreground;
        }

        private (int, int, int, int) GenerateTuple(int rows, int cols, int length, DirectionEnum direction)
        {
            int rowsMax = rows;
            int colsMax = cols;
            int tableLength = length - 1;

            if (direction == DirectionEnum.horizontal)
            {
                rowsMax += tableLength;
            }
            else
            {
                colsMax += tableLength;
            }

            return (rows, rowsMax, cols, colsMax);
        }
    }
}
