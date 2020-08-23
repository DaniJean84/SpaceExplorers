using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using SpaceExplorers.Game_Code.Constants;
using AnoleEngine.Engine_Base;
using AnoleEngine.Engine_Base.Game_Code.Graphics.Shader;
using System.IO;
using SpaceExplorers.Game_Code.Galaxy;
using SFML.Window;

namespace AnoleEngine.Engine_Base.Game_Code.GalaxyGen
{
    class Planetoid// : Drawable
    {
        public Guid HostStar { get; set; }
        public GlobalConstants.PlanetoidType PlanetType { get; set; }
        public Guid ID { get; set; }
        public float Radius { get; set; }
        public Vector2f Position = new Vector2f();
        public int ZLayer { get; set; }
        public int AngleFromZero { get; set; }
        public int OrbitalRadius { get; set; }
        public Text starText { get; set; }

        public CircleShape Body = new CircleShape();
        public CircleShape Orbit = new CircleShape();
        public CircleShape OutLine = new CircleShape();

        public RectangleShape PlanetDetailBox = new RectangleShape();

        public Planetoid(float X, float Y, int Z, Star objHostStar)
        {
            ID = new Guid();
            HostStar = objHostStar.ID;
            ZLayer = Z;
            Position = new Vector2f(X, Y);

            Body = new CircleShape(Radius, 100);
            SetPlanetTypeAndColor();
            Body.Position = Position;
            Orbit = new CircleShape();

            Orbit.FillColor = Color.Transparent;
            Orbit.OutlineColor = new Color(77, 77, 77);
            Orbit.OutlineThickness = 1.5f;
            OutLine = new CircleShape();

            OutLine.OutlineThickness = 1.5f;
            OutLine.OutlineColor = Color.Transparent;
            OutLine.FillColor = Color.Transparent;            
            starText = new Text(Engine.Instance.devText);

            starText.DisplayedString = "X: " + (int)Position.X + " Y: " + (int)Position.Y;
            starText.Color = Color.Green;
        }

        public void SetOrbitOrigin(Vector2f vecPosition)
        {
            Orbit.Origin = vecPosition;
        }

        private void SetPlanetTypeAndColor()
        {
            GlobalConstants.PlanetoidType PType = (GlobalConstants.PlanetoidType)Normal.Sample(4, 2.16);

            switch (PType)
            {
                case GlobalConstants.PlanetoidType.GAS_GIANT:
                    Body.FillColor = new Color(226, 130, 106);
                    Body.Radius = DiscreteUniform.Sample(12, 25);
                    break;

                case GlobalConstants.PlanetoidType.FROZEN:
                    Body.FillColor = Color.Cyan;
                    Body.Radius = DiscreteUniform.Sample(4, 9);

                    break;
                case GlobalConstants.PlanetoidType.ROCKY:
                    Body.FillColor = new Color(86, 74, 71);
                    Body.Radius = DiscreteUniform.Sample(4, 9);

                    break;
                case GlobalConstants.PlanetoidType.TERRAN:
                    Body.FillColor = Color.Green;
                    Body.Radius = DiscreteUniform.Sample(4, 8);

                    break;
                case GlobalConstants.PlanetoidType.WATER_WORLD:
                    Body.FillColor = Color.Blue;
                    Body.Radius = DiscreteUniform.Sample(4, 8);
                    break;

                case GlobalConstants.PlanetoidType.VOLCANIC:
                    Body.FillColor = Color.Red + Color.Yellow;
                    Body.Radius = DiscreteUniform.Sample(4, 6);
                    break;

                case GlobalConstants.PlanetoidType.TOXIC:
                    Body.FillColor = Color.Green + Color.Yellow;
                    Body.Radius = DiscreteUniform.Sample(4, 8);
                    break;

                default:
                    Body.FillColor = new Color(226, 130, 106);
                    Body.Radius = DiscreteUniform.Sample(12, 25);
                    break;
            }
        }

        public bool IsMouseOverPlanetoid()
        {
            Vector2i vecMousePosition = Mouse.GetPosition(Engine.Instance.GameWindow);
            Vector2f vecRelativeMousePosition = (Vector2f)Engine.Instance.GameWindow.MapPixelToCoords(vecMousePosition, Engine.Instance.GameWindow.GetView());

            if (OutLine.GetGlobalBounds().Contains(vecRelativeMousePosition.X, vecRelativeMousePosition.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
