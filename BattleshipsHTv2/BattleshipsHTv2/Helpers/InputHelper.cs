using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Helpers
{
    public class InputHelper
    {
        public ConsoleKey ReadKey()
        {
            return Console.ReadKey().Key;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
