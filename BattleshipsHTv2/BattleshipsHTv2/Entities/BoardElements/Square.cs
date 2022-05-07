using BattleshipsHTv2.Entities.Ships;
using BattleshipsHTv2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Entities.BoardElements
{
    public class Square
    {
        public (int x, int y) Position { get; set; }
        public SquareStatusEnum SquareStatus { get; set; }
        public Ship CurrentShip { get; set; }

        public Square(int x, int y)
        {
            Position = (x, y);
            SquareStatus = SquareStatusEnum.empty;
        }

        public char GetCharacter()
        {
            switch ((int)SquareStatus)
            {
                case 0:
                    return '~';
                case 1:
                    return '0';
                case 2:
                    return 'x';
                case 3:
                    return '*';
                case 4:
                    return 'o';
                default:
                    return 'e';

            }
        }

        public void ChangeStatus(SquareStatusEnum status)
        {
            SquareStatus = status;
        }

        public ConsoleColor GetColore()
        {
            switch ((int)SquareStatus)
            {
                case 0:
                    return ConsoleColor.Blue;
                case 1:
                    return ConsoleColor.Red;
                case 2:
                    return ConsoleColor.Red;
                case 3:
                    return ConsoleColor.Gray;
                case 4:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.DarkMagenta;
            }
        }
    }
}
