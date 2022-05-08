using BattleshipsHTv2.Entities.BoardElements;
using BattleshipsHTv2.Entities.Ships;
using BattleshipsHTv2.Enums;
using BattleshipsHTv2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Services
{
    public class BoardService
    {
        DisplayService _displayService { get; }
        InputHelper _input { get; }
        public BoardService(DisplayService display, InputHelper input)
        {
            _displayService = display;
            _input = input;
        }
        public void RandomPlacement(Board board, List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                AddOneShipRandom(board, ship);
            }
            board.DeleteBouys();
        }

        void AddOneShipRandom(Board board, Ship ship)
        {
            do
            {
                NewRandomPosition(ship, board.Size);
            }
            while (!board.PossibleShip(ship));

            board.AddShip(ship);
        }
        void NewRandomPosition(Ship ship, int sizeMap)
        {
            Random rnd = new Random();
            ship.Direction = rnd.Next(2) == 0 ? DirectionEnum.horizontal : DirectionEnum.vertical;
            ship.OriginPoint = (ship.Direction == DirectionEnum.horizontal ? (rnd.Next(sizeMap - ship.Length + 1), rnd.Next(sizeMap)) : (rnd.Next(sizeMap), rnd.Next(sizeMap - ship.Length + 1)));
        }

        public void ManualPlacement(Board board, List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                AddOneShipManual(board, ship);
            }
            board.DeleteBouys();
        }
        void AddOneShipManual(Board board, Ship ship)
        {
            int x = 0;
            int y = 0;
            int size = board.Size;
            DirectionEnum dir = DirectionEnum.horizontal;
            ConsoleKey key;
            bool CordsNotSet = true;
            bool wrongPositionMessage = false;
            do
            {
                ship.OriginPoint = (x, y);
                ship.Direction = dir;
                _displayService.PrintBoard(board, ship);
                if (wrongPositionMessage)
                {
                    _displayService.PrintMessage("Invalid Ship Position", ConsoleColor.Red);
                    wrongPositionMessage = false;
                }
                key = _input.ReadKey();

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (y == 0 && dir == DirectionEnum.vertical) { y = size - ship.Length; }
                        else if (y == 0 && dir == DirectionEnum.horizontal) { y = size - 1; }
                        else { y--; }
                        break;
                    case ConsoleKey.DownArrow:
                        if ((y == size - ship.Length && dir == DirectionEnum.vertical) || (y == size - 1)) { y = 0; }
                        else { y++; }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x == 0 && dir == DirectionEnum.horizontal) { x = size - ship.Length; }
                        else if (x == 0 && dir == DirectionEnum.vertical) { x = size - 1; }
                        else { x--; }
                        break;
                    case ConsoleKey.RightArrow:
                        if ((x == size - ship.Length && dir == DirectionEnum.horizontal) || (x == size - 1)) { x = 0; }
                        else { x++; }
                        break;
                    case ConsoleKey.Spacebar:
                        if (dir == DirectionEnum.horizontal)
                        {
                            dir = DirectionEnum.vertical;
                            if (y >= size - ship.Length) { y = size - ship.Length; }
                        }
                        else
                        {
                            dir = DirectionEnum.horizontal;
                            if (x >= size - ship.Length) { x = size - ship.Length; }
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (board.PossibleShip(ship))
                        {
                            board.AddShip(ship);
                            CordsNotSet = false;
                        }
                        else
                        {
                            wrongPositionMessage = true;
                        }
                        break;
                    default:
                        break;
                }
            } while (CordsNotSet);
        }
    }
}
