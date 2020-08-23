using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceExplorers.Game_Code.Constants;
using System;
using System.Collections.Generic;

namespace AnoleEngine.Engine_Base.Game_Code.Character
{
    class Player
    {
        public Vector<float> Position { get; private set; }
        public RectangleShape Body { get; }
        public Sprite Sprite { get; }

        public Player()
        {

        }

        public void UpdatePosition(Vector<float> newPosition)
        {
            this.Position = newPosition;
        }

        public void UpdatePlayerHealth(float newHealth)
        {

        }

        public void Draw()
        {

        }
    }
}
