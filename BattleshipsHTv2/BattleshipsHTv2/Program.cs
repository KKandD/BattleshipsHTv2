using BattleshipsHTv2.Helpers;
using BattleshipsHTv2.Services;


var display = new DisplayService();
var input = new InputHelper();
var mainMenu = new MainMenuService(display, input);
var battleApp = new PreGameService(mainMenu, display, input);

battleApp.Run();