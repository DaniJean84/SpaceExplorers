using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using TGUI;
using AnoleEngine.Engine_Base.States;
using SpaceExplorers.States;
using AnoleEngine.Engine_Base.UI.Renderers;

namespace AnoleEngine.Engine_Base.UI.UserInterfaces
{
    class UI_MainMenu
    {

        public static Gui CreateMainMenuInterface(ref AnoleEngine.Engine_Base.Engine objEngineInstance, MainMenuState state)
        {
            float fltGameWindowWidth = objEngineInstance.GameWindow.Size.X;
            float fltGameWindowHeight = objEngineInstance.GameWindow.Size.Y;

            Gui UI = new Gui(objEngineInstance.GameWindow);

            Button closeButton = new Button("CLOSE");
            closeButton.Size = new Vector2f(200, 50);
            float fltXPos = (fltGameWindowWidth / 2) - 100;
            float fltYPos = (fltGameWindowHeight / 2) - 25;

            closeButton.Position = new Vector2f(fltXPos, fltYPos);
            closeButton.SetRenderer(UI_Renderers.UIBackButtonRenderer.Data);
            UI.Add(closeButton, "closeButton");

            Button objSettingsButton = new Button("SETTINGS");
            objSettingsButton.Size = new Vector2f(200, 50);
            float fltSettingsXPos = ((fltGameWindowWidth / 2) - 100);
            float fltSettingsYPos = ((fltGameWindowHeight / 2) - 75) - UI_Constants.ControlSpacer;

            objSettingsButton.Position = new Vector2f(fltSettingsXPos, fltSettingsYPos);
            objSettingsButton.SetRenderer(UI_Renderers.UIButtonRenderer.Data);
            UI.Add(objSettingsButton, "Settings");

            Button newGameButton = new Button("NEW SOLO GAME");
            newGameButton.Size = new Vector2f(200, 50);
            float fltNewGameXPos = ((fltGameWindowWidth / 2) - 100);
            float fltNewGameYPos = ((fltGameWindowHeight / 2) - 125) - (UI_Constants.ControlDoubleSpacer);

            newGameButton.Position = new Vector2f(fltNewGameXPos, fltNewGameYPos);
            newGameButton.SetRenderer(UI_Renderers.UIButtonRenderer.Data);
            UI.Add(newGameButton, "NewGameButton");

            ((Button)UI.Get("NewGameButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.CreateNewGameEvent);
            ((Button)UI.Get("Settings")).Clicked += new EventHandler<SignalArgsVector2f>(state.SettingsEvent);
            ((Button)UI.Get("closeButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.ExitGameEvent);

            return UI;
        }


    }
}
