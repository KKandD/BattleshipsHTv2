using BattleshipsHTv2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Services
{
    public class MainMenuService
    {
        protected DisplayService _display;
        protected InputHelper _input;

        public MainMenuService(DisplayService display, InputHelper input)
        {
            _display = display;
            _input = input;
        }

        public string AskForGameMode()
        {
            Console.Clear();
            Console.WriteLine("Please Select game mode:\n");
            Console.WriteLine("1. Player vs Player");
            Console.WriteLine("2. Player vs Computer");
            Console.WriteLine("3. Exit");

            return Console.ReadLine();
        }

        public int Menu(List<string> options, string title = "")
        {
            int x = 0;
            ConsoleKey key;
            do
            {
                Console.Clear();
                if (!String.IsNullOrEmpty(title)) _display.PrintMessage(title);
                _display.PrintMenu(options, x);
                key = _input.ReadKey();
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        x = (x == 0 ? options.Count - 1 : x - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        x = (x == options.Count - 1 ? 0 : x + 1);
                        break;
                    case ConsoleKey.Enter:
                        return x;
                    default:
                        break;
                }
            }
            while (key != ConsoleKey.Enter);
            return -1;
        }
    }
}
