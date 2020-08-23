using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using AnoleEngine.Engine_Base.States;
using AnoleEngine.Engine_Base;
using SpaceExplorers;
using SpaceExplorers.Game_Code.Galaxy;
using SpaceExplorers.Game_Code.Constants;
using System.IO;
using System.Windows.Forms;
using AnoleEngine.Engine_Base.UI.UserInterfaces;

namespace SpaceExplorers.States
{
    class MainMenuState : GameState
    {
        public Texture BGTexture;
        public Sprite BGSprite;

        public MainMenuState() { }

        public MainMenuState(Engine engine)
        {
            InitializeState();
            engine.GameStates.Push(this);
        }

        // BASE OVERRIDES
        public override void InitializeState()
        {        
            Vector2f vecViewCenter = new Vector2f((Engine.Instance.GameWindow.Size.X) / 2, (Engine.Instance.GameWindow.Size.Y) / 2);
            View = new SFML.Graphics.View(vecViewCenter, (Vector2f)Engine.Instance.GameWindow.Size);
            Engine.Instance.GameWindow.SetView(View);

            this.UI = UI_MainMenu.CreateMainMenuInterface(ref Engine.Instance, this);
            this.IsStateActive = true;
            this.IsStateAlive = true;
            this.StateName = nameof(MainMenuState);
            FileStream fsImageStream = new FileStream(@"Assets\Backgrounds\MMBG01.png", FileMode.Open);

            BGTexture = new Texture(fsImageStream);
            BGSprite = new Sprite(BGTexture);
        }

        public override void DrawState(SFML.Graphics.RenderWindow objRenderTarget)
        {
            objRenderTarget.Draw(BGSprite);
            UI.Draw();
        }

        public void CreateNewGameEvent(object sender, TGUI.SignalArgsVector2f args)
        {
            // TODO - Move to post galaxy options state
            GalaxyGenerator.GenerateStars(GlobalConstants.GalaxySize.SMALL, GlobalConstants.GalaxyType.DISK);
            Engine.Instance.GameStates.Push(new GalaxyViewState());
            IsStateActive = false;
        }

        public void ExitGameEvent(object sender, TGUI.SignalArgsVector2f args)
        {
            if (IsStateActive == true)
            {
                Engine.Instance.GameWindow.Close();
            }
        }

        public void SettingsEvent(object sender, TGUI.SignalArgsVector2f args)
        {

        }
    }

    public class testForm : Form
    {
        public testForm()
        {
            this.Size = new System.Drawing.Size(200, 200);
        }
    }
}
