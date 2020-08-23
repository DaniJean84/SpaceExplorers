using AnoleEngine.Engine_Base;
using AnoleEngine.Engine_Base.Game_Code.GalaxyGen;
using AnoleEngine.Engine_Base.States;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceExplorers.Game_Code.Constants;
using System;
using System.Linq;

namespace SpaceExplorers.States
{
    class SystemViewState : GameState
    {
        public Vector2u ViewPosition = new Vector2u();
        public Vector2f ViewSize = new Vector2f();
        public FloatRect ViewRect;
        public bool DisplaySystemInformation;

        public Star HostStar { get; set; }
        public CircleShape HostOutLine = new CircleShape();
        private Sprite StarSprite { get; set; }

        public SystemViewState(Star HostStar)
        {
            InitializeState(HostStar);
        }

        // BASE OVERRIDES
        public override void InitializeState()
        {

        }

        public override void DrawState(RenderWindow objRenderTarget)
        {
            ScrollViewPort();    
            objRenderTarget.Draw(StarSprite);

            if (HostStar.ChildOrbits != null && HostStar.ChildOrbits.Count() > 0)
            {
                foreach (var Planet in HostStar.ChildOrbits)
                {
                    objRenderTarget.Draw(Planet.Orbit);                   
                    Vector2i vecMousePosition = Mouse.GetPosition();
                    Vector2f vecRelativeMousePosition = (Vector2f)Engine.Instance.GameWindow.MapPixelToCoords(vecMousePosition, Engine.Instance.GameWindow.GetView());

                    objRenderTarget.Draw(Planet.OutLine);                  
                    objRenderTarget.Draw(Planet.Body);
                    Engine.Instance.GameWindow.SetView(View);
                }
            }
        }

        public override void InitializeStateEvents()
        {
            Engine.Instance.GameWindow.KeyReleased += new EventHandler<KeyEventArgs>(KillStateEvent);
            Engine.Instance.GameWindow.MouseWheelMoved += new EventHandler<MouseWheelEventArgs>(ZoomViewEvent);
            Engine.Instance.GameWindow.MouseMoved += new EventHandler<MouseMoveEventArgs>(MouseHoverEvent);
        }

        public override void HandleSystemEvents()
        { }

        public void InitializeState(Star objHostStar)
        {
            ViewSize = Engine.Instance.GameWindow.GetView().Size;
            ViewPosition = Engine.Instance.GameWindow.Size + Engine.Instance.GameWindow.Size / 2;
            ViewRect = new FloatRect((Vector2f)ViewPosition, (Vector2f)ViewSize);

            View = new View(ViewRect);
            View.Center = new Vector2f(0,0);
            IsStateActive = true;
            IsStateAlive = true;
            StateName = "System View State";

            float fltHostRadius = objHostStar.Radius + 25;
            Vector2f vecHostPosition = new Vector2f(View.Center.X - fltHostRadius, View.Center.Y - fltHostRadius);
            
            HostStar = new Star(fltHostRadius, vecHostPosition, 0);
            HostStar.Body.FillColor = objHostStar.Body.FillColor;
            HostStar.ChildOrbits = objHostStar.ChildOrbits;
            HostStar.HasPanets = objHostStar.HasPanets;
           // HostStar.starText.DisplayedString = objHostStar.starText.DisplayedString;


            if (HostStar.HasPanets == true)
            {
                CalculatePlanetoidCoordinatesRelativeToHostStar();
            }

            HostOutLine = new CircleShape();
            HostOutLine.FillColor = Color.Transparent;
            HostOutLine.OutlineColor = Color.White;
            HostOutLine.OutlineThickness = 2.0f;

            HostOutLine.Position = HostStar.Body.Position - new Vector2f(5, 5);
            HostOutLine.Radius = HostStar.Body.Radius + 5f;

            DisplaySystemInformation = false;
            Engine.Instance.GameWindow.SetView(View);
            InitializeStateEvents();

            StarSprite = new Sprite(objHostStar.StarSprite);
            StarSprite.Scale = new Vector2f(5, 5);
            StarSprite.Position = new Vector2f(View.Center.X - ((5 * 64) / 2), View.Center.Y - ((5 * 64) / 2));
        }

        private void CalculatePlanetoidCoordinatesRelativeToHostStar()
        {
            for (int currIndex = 0; currIndex < HostStar.ChildOrbits.Count(); currIndex++)
            {
                int _XCoordinate = (int)(HostStar.ChildOrbits[currIndex].OrbitalRadius * (Math.Cos(HostStar.ChildOrbits[currIndex].AngleFromZero)));
                int _YCoordinate = (int)(HostStar.ChildOrbits[currIndex].OrbitalRadius * (Math.Sin(HostStar.ChildOrbits[currIndex].AngleFromZero)));

                HostStar.ChildOrbits[currIndex].Body.Position = new Vector2f(_XCoordinate, _YCoordinate);
                HostStar.ChildOrbits[currIndex].Position = new Vector2f(_XCoordinate, _YCoordinate);
                HostStar.ChildOrbits[currIndex].starText.DisplayedString = "Planet position: X" + _XCoordinate.ToString() + " Y" + _YCoordinate.ToString();
                HostStar.ChildOrbits[currIndex].starText.Position = HostStar.ChildOrbits[currIndex].Body.Position;

                float x = _XCoordinate + HostStar.ChildOrbits[currIndex].Body.Radius;
                float y = _YCoordinate + HostStar.ChildOrbits[currIndex].Body.Radius;

                double _distance = Math.Sqrt((x - View.Center.X)* (x - View.Center.X) + (y - View.Center.Y)* (y - View.Center.Y));
                HostStar.ChildOrbits[currIndex].Orbit.SetPointCount(100);

                HostStar.ChildOrbits[currIndex].Orbit.Radius = (float)_distance;
                HostStar.ChildOrbits[currIndex].Orbit.Position = View.Center - new Vector2f(HostStar.ChildOrbits[currIndex].Orbit.Radius, HostStar.ChildOrbits[currIndex].Orbit.Radius);
                HostStar.ChildOrbits[currIndex].OutLine.Radius = HostStar.ChildOrbits[currIndex].Body.Radius + 10f;
                HostStar.ChildOrbits[currIndex].OutLine.Position = HostStar.ChildOrbits[currIndex].Body.Position - new Vector2f(10f, 10f);
            }
        }

        // STATE MEMBERS
        private bool MouseOverBody()
        {
            if (IsStateActive == true)
            {
                Vector2i vecMousePosition = Mouse.GetPosition();
                Vector2f vecRelativeMousePosition = (Vector2f)Engine.Instance.GameWindow.MapPixelToCoords(vecMousePosition, Engine.Instance.GameWindow.GetView());

                foreach (Planetoid currPlanet in HostStar.ChildOrbits)
                {
                    float fltStarCenterX = currPlanet.Position.X + (int)currPlanet.Radius;
                    float fltStarCenterY = currPlanet.Position.Y + (int)currPlanet.Radius;
                    double dblMouseDistFromStarCenter = Math.Sqrt(((vecRelativeMousePosition.X - fltStarCenterX) * (vecRelativeMousePosition.X - fltStarCenterX)) + ((vecRelativeMousePosition.Y - fltStarCenterY) * (vecRelativeMousePosition.Y - fltStarCenterY)));

                    if (dblMouseDistFromStarCenter <= (currPlanet.Radius + 5))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void MouseHoverEvent(object sender, MouseMoveEventArgs args)
        {
            if (IsStateActive == true && IsStateAlive == true && HostStar.ChildOrbits != null)
            {
                if (HostStar.IsMouseOverStar())
                {
                    HostStar.DisplayStarInformation = true;
                }
                else
                {
                    HostStar.DisplayStarInformation = false;
                }

                foreach (Planetoid currPlanetoid in HostStar.ChildOrbits)
                {
                    if (currPlanetoid.IsMouseOverPlanetoid())
                    {
                        currPlanetoid.OutLine.OutlineColor = Color.White;
                        currPlanetoid.OutLine.FillColor = Color.Black;
                    }
                    else
                    {
                        currPlanetoid.OutLine.OutlineColor = Color.Transparent;
                        currPlanetoid.OutLine.FillColor = Color.Transparent;
                    }
                }
            }
        }

        public void PlanetClickEvent(object sender, MouseButtonEventArgs args)
        {

        }

        private void ScrollViewPort()
        {
            if (IsStateActive == true && Engine.Instance.GameWindow.HasFocus() == true)
            {
                Vector2i vecMousePositon = Mouse.GetPosition(Engine.Instance.GameWindow);
                if (vecMousePositon.X < Engine.Instance.GameWindow.Position.X + 20)
                {
                    View.Move(new Vector2f(-1, 0));
                }
                if (vecMousePositon.X > (Engine.Instance.GameWindow.Position.X + Engine.Instance.GameWindow.Size.X) - 20)
                {
                    View.Move(new Vector2f(1, 0));
                }
                if (vecMousePositon.Y < Engine.Instance.GameWindow.Position.Y + 20)
                {
                    View.Move(new Vector2f(0, -1));
                }
                if (vecMousePositon.Y > (Engine.Instance.GameWindow.Position.Y + Engine.Instance.GameWindow.Size.Y) - 20)
                {
                    View.Move(new Vector2f(0, 1));
                }
            }
        }

        private void ZoomViewEvent(object sender, MouseWheelEventArgs args)
        {
            if (Engine.Instance.GameWindow.HasFocus() == true)
            {
                if (args.Delta < 0 && IsStateActive == true)
                {
                    View.Zoom(0.95f);
                }
                if (args.Delta > 0 && IsStateActive == true)
                {
                    View.Zoom(1.05f);
                }
            }
        }

        private void KillStateEvent(object sender, KeyEventArgs args)
        {
            if (Engine.Instance.GameWindow.HasFocus() == true)
            {
                if (args.Code == Keyboard.Key.Escape)
                {
                    if (IsStateActive == true && IsStateAlive == true)
                    {
                        Engine.Instance.GameWindow.KeyReleased -= new EventHandler<KeyEventArgs>(KillStateEvent);
                        Engine.Instance.GameWindow.MouseWheelMoved -= new EventHandler<MouseWheelEventArgs>(ZoomViewEvent);
                        Engine.Instance.KillCurrentState(this);
                    }
                }
                else if (args.Code == Keyboard.Key.I)
                {
                    DisplaySystemInformation = !DisplaySystemInformation;
                }
            }
        }
    }
}
