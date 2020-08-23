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
using AnoleEngine.Engine_Base.UI.Controls;

namespace AnoleEngine.Engine_Base.Game_Code.GalaxyGen
{
    class Star
    {
        public const string ObjectName = "star";
        public GlobalConstants.StarType StarType { get; set; }
        public float Radius { get; set; }
        public Vector2f Position { get; set; }
        public int ZLayer { get; set; }
        public CircleShape Body { get; set; }
        public Guid ID { get; set; }

        public Planetoid[] ChildOrbits { get; set; }
        public int SystemRadius { get; set; }
        public bool HasPanets { get; set; }
        public Color _Color { get; set; }
        public Vector2i Origin { get; set; }

        public bool DisplayStarInformation = false;
        public string SystemName { get; set; }
        public StarMenuControl objStarMenu { get; set; }
        public Sprite StarSprite { get; set; }

        public bool Home { get; set; }

        public bool HasPlayer { get; set; }

        public Star(float Rad, Vector2f vecPos, int Z)
        {
            ID = new Guid();
            Radius = Rad;
            ZLayer = Z;
            Position = vecPos;

            Body = new CircleShape(Rad);
            Body.Position = Position;
            SetStartTypeAndColorByRadius();
            HasPlanets();
            BuildStarSystem();

            Origin = new Vector2i((int)(vecPos.X + Radius), (int)(vecPos.Y + Radius));

            try
            {
                Random rand = new Random();
                int intTexNum = rand.Next(1, 3);
                string strSmallKey = $"starSm{intTexNum}";
                string strMedKey = $"starMed{intTexNum}";
                string strBigKey = $"starBig{intTexNum}";

                if (StarType == GlobalConstants.StarType.K || StarType == GlobalConstants.StarType.M)
                {
                    StarSprite = new Sprite(Engine.Instance.SpriteImages[strSmallKey]);
                }
                else if (StarType == GlobalConstants.StarType.G || StarType == GlobalConstants.StarType.F)
                {
                    StarSprite = new Sprite(Engine.Instance.SpriteImages[strMedKey]);
                }
                else if (StarType == GlobalConstants.StarType.A || StarType == GlobalConstants.StarType.B || StarType == GlobalConstants.StarType.O)
                {
                    StarSprite = new Sprite(Engine.Instance.SpriteImages[strBigKey]);
                }
                else
                {
                    StarSprite = new Sprite(Engine.Instance.SpriteImages["star"]);
                }

                StarSprite.Color = _Color;
                //StarSprite.Scale = new Vector2f((Radius/10), (Radius / 10));
                StarSprite.Position = new Vector2f((Origin.X - (/*StarSprite.Scale.X **/ 32)),(Origin.Y - (/*StarSprite.Scale.Y **/ 32)));
                
                SystemName = TextGen.StarSystemNameGenerator.GenerateStarSystemName();
                this.objStarMenu = new StarMenuControl(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Dispose()
        {

        }


        public void MouseHoverEvent(object sender, MouseMoveEventArgs args)
        {
            if (this.IsMouseOverStar())
            {
                Body.OutlineColor = Color.White;
                DisplayStarInformation = true;
            }
            else
            {
                Body.OutlineColor = Color.Transparent;
                DisplayStarInformation = false;
            }  
        }

        private void SetStartTypeAndColorByRadius()
        {
            if (Radius < 8)
            {
                StarType = GlobalConstants.StarType.K;
                _Color = new Color(244, 79, 65);
            }
            else if (Radius >= 8 && Radius < 12)
            {
                StarType = GlobalConstants.StarType.M;
                _Color = new Color(244, 166, 65);
            }
            else if (Radius >= 12 && Radius < 15)
            {
                StarType = GlobalConstants.StarType.G;
                _Color = new Color(244, 238, 65);
                Body.Radius -= 5;
                Radius = Body.Radius;
            }
            else if (Radius >= 15 && Radius < 15.85)
            {
                StarType = GlobalConstants.StarType.F;
                _Color = new Color(193, 65, 244);
                Body.Radius -= 5;
                Radius = Body.Radius;
            }
            else if (Radius >= 15.85 && Radius < 16)
            {
                StarType = GlobalConstants.StarType.A;
                _Color = new Color(65, 211, 244);
            }
            else if (Radius >= 16.1 && Radius < 16.25)
            {
                StarType = GlobalConstants.StarType.B;
                _Color = new Color(66, 244, 212);
                Body.Radius += 5;
                Radius = Body.Radius;
            }
            else if (Radius > 16.25 && Radius < 16.3)
            {
                StarType = GlobalConstants.StarType.O;
                _Color = new Color(66, 244, 217);
                Body.Radius += 10;
                Radius = Body.Radius;
            }
            else
            {
                StarType = GlobalConstants.StarType.Unknown;
            }
        }

        private void HasPlanets()
        {
            int intNum = DiscreteUniform.Sample(1, 3);

            if ((intNum == 2 || intNum == 1) && StarType != GlobalConstants.StarType.O && StarType != GlobalConstants.StarType.B)
            {
                HasPanets = true;
            }
            else
            {
                HasPanets = false;
            }
        }

        private void BuildStarSystem()
        {
            if (HasPanets == true)
            {
                SystemRadius = (int)ContinuousUniform.Sample(500, 1800);
                List<Planetoid> lstPlanets = new List<Planetoid>();
                int NumOfPlanetoids = 1;

                if (SystemRadius < 700)
                {
                    NumOfPlanetoids = DiscreteUniform.Sample(1, 4);
                }
                else
                {
                    NumOfPlanetoids = DiscreteUniform.Sample(1, 7);
                }

                for (int i = 0; i < NumOfPlanetoids; i++)
                {
                    bool blnShouldBuildPlanet = true;
                    int intOrbitalRadus = 0;
                    int intAngleFromZero = 0;

                    GetPlanetoidOrbitalRadiusAndAngle(ref lstPlanets, out intOrbitalRadus, out intAngleFromZero);

                    if (blnShouldBuildPlanet == true)
                    {
                        Planetoid objPlanetoid = new Planetoid(0, 0, 0, this);
                        objPlanetoid.OrbitalRadius = intOrbitalRadus;
                        objPlanetoid.Orbit.Radius = intOrbitalRadus;
                        objPlanetoid.AngleFromZero = intAngleFromZero;
                        lstPlanets.Add(objPlanetoid);
                    }
                }

                ChildOrbits = lstPlanets.ToArray();
            }
        }

        private void GetPlanetoidOrbitalRadiusAndAngle(ref List<Planetoid> lstPlanetoids, out int _intOrbitalRadius, out int _intAngleFromZero)
        {
            int intOrbitRadius = (int)ContinuousUniform.Sample(100.0d, SystemRadius);
            bool blnGoodOrbit = false;
            int intReTryCount = 0;
            _intAngleFromZero = (int)ContinuousUniform.Sample(0, 359);
                  
            while (blnGoodOrbit == false)
            {
                if (!lstPlanetoids.Exists(plt => intOrbitRadius < plt.OrbitalRadius + 50 && intOrbitRadius > plt.OrbitalRadius - 50))
                {
                    blnGoodOrbit = true;
                }
                else
                {
                    intReTryCount++;
                    intOrbitRadius = (int)ContinuousUniform.Sample(100.0d, SystemRadius);
                }
                if (intReTryCount >= 10)
                {
                    _intOrbitalRadius = 0;
                    _intAngleFromZero = 0;
                    return;
                }
            }

            _intOrbitalRadius = intOrbitRadius;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(StarSprite);

            //if (DisplayStarInformation == true)
            //{
            //    this.objStarMenu.Draw(target);
            //}       
        }

        public bool IsMouseOverStar()
        {
            Vector2i vecMousePosition = Mouse.GetPosition(Engine.Instance.GameWindow);
            Vector2f vecRelativeMousePosition = (Vector2f)Engine.Instance.GameWindow.MapPixelToCoords(vecMousePosition, Engine.Instance.GameWindow.GetView());

            if (Body.GetGlobalBounds().Contains(vecRelativeMousePosition.X, vecRelativeMousePosition.Y))
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
