using BattleshipsHTv2.Entities.Ships;
using BattleshipsHTv2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Entities.BoardElements
{
    public class Board
    {
        public Square[,] Ocean { get; set; }
        public int Size { get; set; }


        public Board(int size)
        {
            Ocean = NewOcean(Size);
            Size = size;
        }
        void AddWatter(Square[,] o)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    o[j, i] = new Square(j, i);
                }
            }
        }
        Square[,] NewOcean(int size)
        {
            Square[,] res = new Square[size, size];
            AddWatter(res);
            return res;
        }

        public void AddShip(Ship ship)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                Square square;
                if (ship.Direction == DirectionEnum.horizontal)
                {
                    square = Ocean[ship.OriginPoint.x + i, ship.OriginPoint.y];
                    square.SquareStatus = SquareStatusEnum.ship;
                    square.CurrentShip = ship;

                    ship.AddSquare(square);
                }
                else
                {
                    square = Ocean[ship.OriginPoint.x, ship.OriginPoint.y + i];
                    square.SquareStatus = SquareStatusEnum.ship;
                    square.CurrentShip = ship;
                    ship.AddSquare(square);
                }
                MarkAdjacentSquares(ship);
            }
        }

        public void MarkAdjacentSquares(Ship ship)
        {
            foreach (Square square in ship.Squares)
            {
                int x = square.Position.x;
                int y = square.Position.y;
                Square currentSquare;

                currentSquare = Ocean[x > 0 ? x - 1 : x, y > 0 ? y - 1 : y];
                MarkSquareIfEmpty(currentSquare);
                currentSquare = Ocean[x > 0 ? x - 1 : x, y];
                MarkSquareIfEmpty(currentSquare);
                currentSquare = Ocean[x > 0 ? x - 1 : x, y < Size - 1 ? y + 1 : y];
                MarkSquareIfEmpty(currentSquare);
                currentSquare = Ocean[x < Size - 1 ? x + 1 : x, y > 0 ? y - 1 : y];
                MarkSquareIfEmpty(currentSquare);
                currentSquare = Ocean[x < Size - 1 ? x + 1 : x, y];
                MarkSquareIfEmpty(currentSquare);
                currentSquare = Ocean[x < Size - 1 ? x + 1 : x, y < Size - 1 ? y + 1 : y];
                MarkSquareIfEmpty(currentSquare);
                currentSquare = Ocean[x, y > 0 ? y - 1 : y];
                MarkSquareIfEmpty(currentSquare);
                currentSquare = Ocean[x, y < Size - 1 ? y + 1 : y];
                MarkSquareIfEmpty(currentSquare);
            }
        }

        private void MarkSquareIfEmpty(Square currentSquare)
        {
            if (currentSquare.SquareStatus == SquareStatusEnum.empty)
                currentSquare.SquareStatus = SquareStatusEnum.buoy;
        }

        public bool possibleShip(Ship ship)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                if (ship.Direction == DirectionEnum.horizontal)
                {
                    if (Ocean[ship.OriginPoint.x + i, ship.OriginPoint.y].SquareStatus != SquareStatusEnum.empty)
                    {
                        return false;
                    }
                }
                else
                {
                    if (Ocean[ship.OriginPoint.x, ship.OriginPoint.y + i].SquareStatus != SquareStatusEnum.empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void DeleteBouys()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (Ocean[x, y].SquareStatus == SquareStatusEnum.buoy)
                    {
                        Ocean[x, y].SquareStatus = SquareStatusEnum.empty;
                    }
                }
            }
        }
    }
}
