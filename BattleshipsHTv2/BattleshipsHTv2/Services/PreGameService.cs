using BattleshipsHTv2.Constants;
using BattleshipsHTv2.Entities.Players;
using BattleshipsHTv2.Entities.Ships;
using BattleshipsHTv2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsHTv2.Services
{
    public class PreGameService
    {
        readonly DisplayService _displayService;
        readonly InputHelper _input;
        readonly MainMenuService _mainMenuService;
        readonly BoardService _boardService;
        ASCII _ascii;
        readonly List<(int length, int count)> _shipsTemplate = new List<(int length, int count)>
                {
                    (4, 1), (3, 2), (2, 3), (1, 4)
                };

        public PreGameService(MainMenuService menu, DisplayService display, InputHelper input)
        {
            _displayService = display;
            _input = input;
            _mainMenuService = menu;
            _boardService = new BoardService(_displayService, _input);
            _ascii = new ASCII(_displayService);
        }
        public void Run()
        {
            bool isRunning = true;

            do
            {
                Player player1 = new Player("Player1", _displayService, _input);
                Player player2 = new Player("Player2", _displayService, _input);

                int option = _mainMenuService.Menu(new List<string>() { "Human vs Human", "Human vs AI", "AI vs AI", "High Scores", "Exit" });

                switch (option)
                {
                    case 0:
                        player1.Name = AskForName("Player 1");
                        player2.Name = AskForName("Player 2");
                        PlaceShips(_shipsTemplate, player1);
                        PlaceShips(_shipsTemplate, player2);

                        break;
                    case 1:
                        player1.Name = AskForName("Player 1");
                        player2 = new ComputerPlayer("Computer 1", _displayService, _input); // TODO why cannot cast here?
                        PlaceShips(_shipsTemplate, player1);
                        PlaceComputerShips(_shipsTemplate, player2);
                        break;
                    case 2:
                        player1 = new ComputerPlayer("Computer 1", _displayService, _input);
                        player2 = new ComputerPlayer("Computer 2", _displayService, _input); // TODO why cannot cast here?
                        PlaceComputerShips(_shipsTemplate, player1);
                        PlaceComputerShips(_shipsTemplate, player2);
                        break;
                    case 3:
                        _displayService.PrintMessage(_ascii.PressAnyKey());
                        _input.ReadKey();
                        continue;
                    case 4:
                        isRunning = false;
                        break;
                    default:
                        break;
                }

                if (!isRunning) break;

                var game = new GameService(player1, player2);
                game.ConfigureUI(_displayService, _input);
                game.Run();

            } while (isRunning);

        }

        private void PlaceComputerShips(List<(int length, int count)> shipsTemplate, Player player2)
        {
            GeneratePlayerShips(shipsTemplate, player2);
            _boardService.RandomPlacement(player2.Board, player2.Ships);
        }

        private void PlaceShips(List<(int length, int count)> shipsTemplate, Player player)
        {
            bool runningPlacement = true;
            do
            {
                int optionsPlacement = _mainMenuService.Menu(new List<string>() { "Auto placement", "Manual Placement", "Back to main menu" }, $"{player.Name} ship placement turn.");
                switch (optionsPlacement)
                {
                    case 0:
                        GeneratePlayerShips(shipsTemplate, player);
                        _boardService.RandomPlacement(player.Board, player.Ships);
                        runningPlacement = false;
                        break;
                    case 1:
                        GeneratePlayerShips(shipsTemplate, player);
                        _boardService.ManualPlacement(player.Board, player.Ships);
                        runningPlacement = false;
                        break;
                    case 2:
                        runningPlacement = false;
                        break;
                    default:
                        break;
                }
            } while (runningPlacement);
        }

        private void GeneratePlayerShips(List<(int length, int count)> shipsTemplate, Player player)
        {
            foreach (var shipTuple in shipsTemplate)
            {
                for (int i = 0; i < shipTuple.count; i++)
                {
                    player.Ships.Add(new Ship(shipTuple.length));
                }
            }
        }

        private string AskForName(string playerID)
        {
            Console.Clear();
            _displayService.PrintMessage($"Type name for {playerID}. (A-Z, a-z, 0-9, 3 - 10 characters)");
            string userInput = _input.ReadLine();
            return userInput.Length > 2 && userInput.Length < 11 ? userInput : AskForName(playerID);
        }
    }
}
