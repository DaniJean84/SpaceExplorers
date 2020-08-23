using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using AnoleEngine.Engine_Base.States;
using AnoleEngine.Engine_Base;
using SpaceExplorers;

namespace SpaceExplorers.States
{
    class IntroState : GameState
    {
        private Text introText;
        private Vector2u textPosition = new Vector2u();
        //// BASE OVERRIDES
        
        public override void InitializeState()
        {
            introText = new Text();
            textPosition.X = Engine.Instance.GameWindow.Size.X / 2;
            textPosition.Y = Engine.Instance.GameWindow.Size.Y / 2;

            introText.Position = (Vector2f)textPosition;
            introText.Color = Color.Green;         
        }

        public override void DrawState(RenderWindow objRenderTarget)
        { }


        public override void HandleSystemEvents()
        { }

        //// STATE MEMBERS

    }
}
