using BattleshipsHTv2.Entities.BoardElements;
using BattleshipsHTv2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Entities.Ships
{
    public class Ship
    {
        public int Length { get; set; }
        public (int x, int y) OriginPoint { get; set; }
        public DirectionEnum Direction { get; set; }
        public ShipTypeEnum ShipType { get; set; }
        public List<Square> Squares { get; private set; } = new List<Square>();

        public Ship(int x, int y, int length, DirectionEnum direction)
        {
            OriginPoint = (x, y);
            Length = length;
            Direction = direction;
            SetTypeShip();
        }

        public Ship(int length)
        {
            Length = length;
            SetTypeShip();
        }

        void SetTypeShip()
        {
            switch (Length)
            {
                case 1:
                    ShipType = ShipTypeEnum.Carrier;
                    break;
                case 2:
                    ShipType = ShipTypeEnum.Cruiser;
                    break;
                case 3:
                    ShipType = ShipTypeEnum.Battleship;
                    break;
                case 4:
                    ShipType = ShipTypeEnum.Submarine;
                    break;
                default:
                    break;
            }
        }

        public string GetShipType()
        {
            return ShipType.ToString();
        }

        internal void AddSquare(Square square)
        {
            Squares.Add(square);
        }

        public bool IsAlive()
        {
            foreach (var square in Squares)
            {
                if (square.SquareStatus == SquareStatusEnum.ship)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
