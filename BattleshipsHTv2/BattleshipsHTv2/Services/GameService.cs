using BattleshipsHTv2.Constants;
using BattleshipsHTv2.Entities.Players;
using BattleshipsHTv2.Enums;
using BattleshipsHTv2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Services
{
    public class GameService
    {
        DisplayService _displayService;
        InputHelper _input;
        ASCII _ascii;
        public Player CurrentPlayer { get; private set; }
        public Player NextPlayer { get; private set; }

        public GameService(Player player1, Player player2)
        {
            CurrentPlayer = player1;
            NextPlayer = player2;
        }

        public void ConfigureUI(DisplayService displayService, InputHelper input)
        {
            _displayService = displayService;
            _input = input;
            _ascii = new ASCII(_displayService);
        }

        public void Run()
        {
            while (CurrentPlayer.IsAlive() && NextPlayer.IsAlive())
            {
                TakeTurn();
                SwitchPlayers();
            }

            var winner = NextPlayer;
            int score = winner.GetScore();

            _displayService.PrintMessage($"{winner.Name} has won. Score: {score}");
            _displayService.PrintMessage(_ascii.PressAnyKey());
            _input.ReadKey();
        }

        private void SwitchPlayers()
        {
            Player temp = CurrentPlayer;
            CurrentPlayer = NextPlayer;
            NextPlayer = temp;
        }

        private void TakeTurn()
        {
            bool lastShot = true;
            while (lastShot)
            {
                CurrentPlayer.OneShot(NextPlayer.Board);
                if (CurrentPlayer.LastShot.SquareStatus != SquareStatusEnum.hit)
                {
                    lastShot = false;
                }
            }

        }
    }
}
