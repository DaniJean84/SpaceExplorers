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


namespace SpaceExplorers.States
{
    class NewGameOptionsState : GameState
    {
        private AnoleEngine.Engine_Base.UI.Button_Obsolete NewGameButton;
        private AnoleEngine.Engine_Base.UI.Button_Obsolete ExitGameButton;
        private AnoleEngine.Engine_Base.UI.Button_Obsolete OptionsButton;

        public NewGameOptionsState() { }

        public NewGameOptionsState(Engine engine)
        {
            InitializeState();
        }

        // BASE OVERRIDES
        public override void InitializeState()
        {
            Vector2f vecViewCenter = new Vector2f((Engine.Instance.GameWindow.Size.X) / 2, (Engine.Instance.GameWindow.Size.Y) / 2);
            View = new View(vecViewCenter, (Vector2f)Engine.Instance.GameWindow.Size);
            Engine.Instance.GameWindow.SetView(View);

            NewGameButton = new AnoleEngine.Engine_Base.UI.Button_Obsolete(200, 50, ((Engine.Instance.GameWindow.Size.X / 2) - 100), ((Engine.Instance.GameWindow.Size.Y / 2)));
            OptionsButton = new AnoleEngine.Engine_Base.UI.Button_Obsolete(200, 50, ((Engine.Instance.GameWindow.Size.X / 2) - 100), ((Engine.Instance.GameWindow.Size.Y / 2) + 75));
            ExitGameButton = new AnoleEngine.Engine_Base.UI.Button_Obsolete(200, 50, ((Engine.Instance.GameWindow.Size.X / 2) - 100), ((Engine.Instance.GameWindow.Size.Y / 2) + 150));

            NewGameButton.SetButtonText("New Game", Color.Black);
            OptionsButton.SetButtonText("Options", Color.Black);
            ExitGameButton.SetButtonText("Exit", Color.Red);

            IsStateActive = true;
            IsStateAlive = true;

            StateName = "Main Menu State";
            PopulateButtonArray();
            InitializeStateEvents();
        }

        public override void DrawState(SFML.Graphics.RenderWindow objRenderTarget)
        {
            //foreach (var currButton in Buttons)
            //{
            //    objRenderTarget.Draw(currButton.recBody);
            //    objRenderTarget.Draw(currButton.Button_ObsoleteText);
            //    Engine.Instance.GameWindow.SetView(View);
            //}
        }

        public override void InitializeStateEvents()
        {
            Engine.Instance.GameWindow.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(CreateNewGameEvent);
            Engine.Instance.GameWindow.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(ExitGameEvent);
        }

        public override void HandleSystemEvents()
        { }

        private void PopulateButtonArray()
        {
            //Buttons = new List<AnoleEngine.Engine_Base.UI.Button_Obsolete>() { NewGameButton, ExitGameButton, OptionsButton };
        }

        private void CreateNewGameEvent(object sender, MouseButtonEventArgs args)
        {
            //if (NewGameButton.IsMouseOverButton() && args.Button_Obsolete == Mouse.Button_Obsolete.Left && IsStateActive == true)
            //{
            //    // TODO - Move to post galaxy options state
            //    GalaxyGenerator.GenerateStars(GlobalConstants.GalaxySize.GALACTIC, GlobalConstants.GalaxyType.DISK);
            //    Engine.Instance.GameStates.Push(new GalaxyViewState());
            //    IsStateActive = false;
            //}
        }

        private void ExitGameEvent(object sender, MouseButtonEventArgs args)
        {
            //if (ExitGameButton.IsMouseOverButton() && args.Button_Obsolete == Mouse.Button_Obsolete.Left && IsStateActive == true)
            //{
            //    Engine.Instance.GameWindow.Close();
            //}
            //else if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && IsStateActive == true)
            //{
            //    Engine.Instance.GameWindow.Close();
            //}
        }
    }
}
