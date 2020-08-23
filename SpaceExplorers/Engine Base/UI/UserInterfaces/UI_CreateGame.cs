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
    public class UI_CreateGame
    {
        public static Gui InitializeCreateGameInterface(ref AnoleEngine.Engine objEngineInstance, CreateGameState state)
        {
            Gui UI = new Gui(objEngineInstance.GameWindow);
            float fltX = objEngineInstance.GameWindow.Size.X;
            float fltY = objEngineInstance.GameWindow.Size.Y;

            TextBox txbGameName = new TextBox();
            txbGameName.Size = new Vector2f(600, 25);
            float fltGameNameXPos = 200;
            float fltGameNameYPos = 250;
            txbGameName.Position = new Vector2f(fltGameNameXPos, fltGameNameYPos);

            txbGameName.SetRenderer(UI_Renderers.UITextBoxRenderer.Data);
            UI.Add(txbGameName, "txbGameName");


            ComboBox cmbRuleSet = new ComboBox();
            cmbRuleSet.Size = new Vector2f(300, 25);
            float fltRuleSetXPos = 200;
            float fltRuleSetYPos = 300;
            cmbRuleSet.Position = new Vector2f(fltRuleSetXPos, fltRuleSetYPos);

            cmbRuleSet.SetRenderer(UI_Renderers.UIComboBoxRenderer.Data);
            cmbRuleSet.Renderer.ListBox = UI_Renderers.UIListBoxRenderer.Data;
            cmbRuleSet.AddItem("Default 1", "Default1");
            cmbRuleSet.AddItem("Default 2", "Default2");
            UI.Add(cmbRuleSet, "RuleSetSelect");
            
            Label lblRuleSetLabel = new Label("Select Rule Set:");
            lblRuleSetLabel.Position = new Vector2f(200, 275);
            lblRuleSetLabel.Size = new Vector2f(200, 25);
            lblRuleSetLabel.SetRenderer(UI_Renderers.UILabelRenderer.Data);
            UI.Add(lblRuleSetLabel, "lblRuleSetLabel");

            ComboBox cmbSelectMap = new ComboBox();
            cmbSelectMap.Size = new Vector2f(300, 25);
            float fltSelectMapXPos = 200;
            float fltSelectMapYPos = 375;
            cmbSelectMap.Position = new Vector2f(fltSelectMapXPos, fltSelectMapYPos);
           
            cmbSelectMap.SetRenderer(UI_Renderers.UIComboBoxRenderer.Data);
            cmbSelectMap.Renderer.ListBox = UI_Renderers.UIListBoxRenderer.Data;
            cmbSelectMap.AddItem("Desert Canyon", "Default1");
            cmbSelectMap.AddItem("Artcic", "Default2");
            UI.Add(cmbSelectMap, "MapSelect");

            Label lblSelectMapLabel = new Label("Select Map:");
            lblSelectMapLabel.Position = new Vector2f(200, 350);
            lblSelectMapLabel.Size = new Vector2f(200, 25);
            lblSelectMapLabel.SetRenderer(UI_Renderers.UILabelRenderer.Data);
            UI.Add(lblSelectMapLabel, "lblSelectMapLabel");

            Button BackButton = new Button("BACK");
            BackButton.Size = new Vector2f(200, 50);
            float fltJBackButtonXPos = 100;
            float fltBackButtonYPos = 50;

            BackButton.Position = new Vector2f(fltJBackButtonXPos, fltBackButtonYPos);
            BackButton.SetRenderer(UI_Renderers.UIBackButtonRenderer.Data);
            UI.Add(BackButton, "BackButton");

            Button CreateGameButton = new Button("CREATE GAME");
            CreateGameButton.Size = new Vector2f(200, 50);
            float fltJCreateGameButtonXPos = 1300;
            float fltCreateGameButtonYPos = 900;

            CreateGameButton.Position = new Vector2f(fltJCreateGameButtonXPos, fltCreateGameButtonYPos);
            CreateGameButton.SetRenderer(UI_Renderers.UIButtonRenderer.Data);
            UI.Add(CreateGameButton, "CreateGameButton");

            //((Button)UI.Get("JoinGameButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.JoinGameEvent);
            //((Button)UI.Get("RefreshListButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.RefreshServerListEvent);
            //((Button)UI.Get("BackButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.BackToMainMenuEvent);
            //((Button)UI.Get("CreateGameButton")).Clicked += new EventHandler<SignalArgsVector2f>(state.CreateGameEvent);

            return UI;
        }
    }
}
