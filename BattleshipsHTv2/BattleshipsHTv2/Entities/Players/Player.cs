using BattleshipsHTv2.Entities.BoardElements;
using BattleshipsHTv2.Entities.Ships;
using BattleshipsHTv2.Enums;
using BattleshipsHTv2.Helpers;
using BattleshipsHTv2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Entities.Players
{
    public class Player
    {
        protected DisplayService _displayService;
        protected InputHelper _input;
        public string Name { get; set; }
        public List<Ship> Ships { get; set; } = new List<Ship>();
        public Board Board { get; }
        public Square LastShot { get; private set; }

        public Player(string name, DisplayService displayService, InputHelper input)
        {
            Board = new Board(10);
            Name = name;
            _displayService = displayService;
            _input = input;
        }

        public bool IsAlive()
        {

            foreach (var ship in Ships)
            {
                if (ship.IsAlive())
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void OneShot(Board board)
        {
            int x = 0;
            int y = 0;
            int size = board.Size;
            ConsoleKey key;
            bool wrongPositionMessage = false;
            bool shoot = true;
            do
            {
                _displayService.PrintBoard(board, x, y);
                _displayService.PrintMessage($"\n{Name} turn.");
                if (wrongPositionMessage)
                {
                    _displayService.PrintMessage("Invalid Shot", ConsoleColor.Red);
                    wrongPositionMessage = false;
                }
                key = _input.ReadKey();

                switch (key)
                {
                    case ConsoleKey.UpArrow:

                        if (y == 0)
                            y = size - 1;
                        else 
                            y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if ((y == size - 1)) 
                            y = 0;
                        else 
                            y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x == 0) 
                            x = size - 1;
                        else 
                            x--;
                        break;
                    case ConsoleKey.RightArrow:
                        if ((x == size - 1)) 
                            x = 0;
                        else 
                            x++;
                        break;
                    case ConsoleKey.Enter:
                        if (CoordsAreValid(board, x, y))
                        {
                            Shoot(board, x, y);
                            shoot = false;
                        }
                        else
                        {
                            wrongPositionMessage = true;
                        }
                        break;
                    default:
                        break;
                }
            } while (shoot);
        }

        protected bool CoordsAreValid(Board board, int x, int y)
        {
            return board.Ocean[x, y].SquareStatus == SquareStatusEnum.empty || board.Ocean[x, y].SquareStatus == SquareStatusEnum.ship;
        }

        protected void Shoot(Board board, int x, int y)
        {
            board.Ocean[x, y].SquareStatus = board.Ocean[x, y].SquareStatus == SquareStatusEnum.ship ? SquareStatusEnum.hit : SquareStatusEnum.missed;
            if (board.Ocean[x, y].SquareStatus == SquareStatusEnum.hit)
            {
                if (!board.Ocean[x, y].CurrentShip.IsAlive())
                {
                    board.MarkAdjacentSquares(board.Ocean[x, y].CurrentShip);
                }
            }
            LastShot = board.Ocean[x, y];

            _displayService.PrintBoard(board);
            _displayService.PrintMessage($"{Name}'s turn.");
            _displayService.WaitForTime(1000);
        }

        public int GetScore()
        {
            int score = 100;
            score += GetSquaresCount() * 10;
            return score;
        }

        private int GetSquaresCount()
        {
            int result = 0;

            foreach (var ship in Ships)
            {
                foreach (var square in ship.Squares)
                {
                    if (square.SquareStatus == SquareStatusEnum.ship) result++;
                }
            }

            return result;
        }
    }
}
