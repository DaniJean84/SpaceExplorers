using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using TGUI;
using System.Drawing;
using StrategyGame.States;
using StrategyGame.GameBase.Network.HttpModels;

namespace StrategyGame.UI.UserInterfaces
{
    internal class UI_JoinGame
    {
        public static Gui CreateJoinGameInterface(ref AnoleEngine.Engine objEngineInstance, JoinGameState state)
        {
            Gui UI = new Gui(objEngineInstance.GameWindow);

            float fltX = objEngineInstance.GameWindow.Size.X;
            float fltY = objEngineInstance.GameWindow.Size.Y;

            ListView objServerList = new ListView();
            objServerList.Size = new Vector2f(1505f, 600f);
            objServerList.Position = new Vector2f(100, 150);
            objServerList.SetRenderer(UI_Renderers.UIListViewRenderer.Data);
            objServerList.AddColumn("NAME:", 800f, HorizontalAlignment.Left);
            objServerList.AddColumn("PLAYERS:", 100f, HorizontalAlignment.Left);
            objServerList.AddColumn("MAP:", 500f, HorizontalAlignment.Left);
            objServerList.AddColumn("IP:PORT", 100f, HorizontalAlignment.Left);

            objServerList.AddItem(new List<string>() { "Test server name", "2/8", "Super awesome map 5" });

            UI.Add(objServerList, "ServerList");

            Button JoinGameButton = new Button("JOIN GAME");
            JoinGameButton.Size = new Vector2f(200, 50);
            float fltJoinGameXPos = (1000);
            float fltJoinGameYPos = (50);

            JoinGameButton.Position = new Vector2f(fltJoinGameXPos, fltJoinGameYPos);
            JoinGameButton.SetRenderer(UI_Renderers.UIButtonRenderer.Data);

            UI.Add(JoinGameButton, "JoinGameButton");

            Button RefreshListButton = new Button("REFRESH");
            RefreshListButton.Size = new Vector2f(200, 50);
            float fltRefreshXPos = (1300);
            float fltRefreshYPos = (50);

            RefreshListButton.Position = new Vector2f(fltRefreshXPos, fltRefreshYPos);
            RefreshListButton.SetRenderer(UI_Renderers.UIButtonRenderer.Data);
            UI.Add(RefreshListButton, "RefreshListButton");

            Button BackButton = new Button("BACK");
            BackButton.Size = new Vector2f(200, 50);
            float fltJBackButtonXPos = (100);
            float fltBackButtonYPos = (50);

            BackButton.Position = new Vector2f(fltJBackButtonXPos, fltBackButtonYPos);
            BackButton.SetRenderer(UI_Renderers.UIBackButtonRenderer.Data);
            UI.Add(BackButton, "BackButton");

            Button CreateGameButton = new Button("CreateGame");
            CreateGameButton.Size = new Vector2f(200, 50);
            float fltJCreateGameButtonXPos = (400);
            float fltCreateGameButtonYPos = (50);

            CreateGameButton.Position = new Vector2f(fltJCreateGameButtonXPos, fltCreateGameButtonYPos);
            CreateGameButton.SetRenderer(UI_Renderers.UIButtonRenderer.Data);
            UI.Add(CreateGameButton, "CreateGameButton");

            ((ListView)UI.Get("ServerList")).ItemSelected += new EventHandler<SignalArgsInt>(state.ServiceListSelectEvent);
            ((Button)UI.Get("JoinGameButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.JoinGameEvent);
            ((Button)UI.Get("RefreshListButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.RefreshServerListEvent);
            ((Button)UI.Get("BackButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.BackToMainMenuEvent);
            ((Button)UI.Get("CreateGameButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.CreateGameEvent);

            return UI;
        }



    }
}
