using AnoleEngine.Engine_Base;
using AnoleEngine.Engine_Base.Game_Code.GalaxyGen;
using AnoleEngine.Engine_Base.States;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceExplorers.Game_Code.Galaxy;
using System;

namespace AnoleEngine.Engine_Base.Game_Code.Game_States
{
    class PlanetDetailState : GameState 
    {
        public Vector2i ViewPosition = new Vector2i();
        public Vector2u ViewSize = new Vector2u();
        public FloatRect ViewRect;

        public override void InitializeState()
        {
            ViewSize = new Vector2u(((uint)GalaxyGenerator.GalaxyMajorRadius * 2) + 1000, ((uint)GalaxyGenerator.GalaxyMajorRadius * 2) + 1000);
            View fixedView = Engine.Instance.GameWindow.GetView();
            View = new View(fixedView.Center, fixedView.Size);
            IsStateActive = true;
            IsStateAlive = true;

            StateName = "Planetary Detail State";

            Engine.Instance.GameWindow.SetView(View);
            InitializeStateEvents();
        }

        public override void DrawState(SFML.Graphics.RenderWindow objRenderTarget)
        {

        }

        public override void InitializeStateEvents()
        {

        }

        public override void HandleSystemEvents()
        {

        }
    }
}
