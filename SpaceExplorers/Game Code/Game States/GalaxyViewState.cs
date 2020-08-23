using AnoleEngine.Engine_Base;
using AnoleEngine.Engine_Base.Engine_Base.UI.UserInterfaces;
using AnoleEngine.Engine_Base.Game_Code.GalaxyGen;
using AnoleEngine.Engine_Base.States;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceExplorers.Game_Code.Galaxy;
using System;
using TGUI;

namespace SpaceExplorers.States
{
    class GalaxyViewState : GameState
    {
        public Vector2i ViewPosition = new Vector2i();
        public FloatRect ViewRect;
        
        private Vertex[] PlayerPath { get; set; }

        public GalaxyViewState()
        {
            InitializeState();
        }

        // BASE OVERRIDES
        public override void InitializeState()
        {
            this.UI = UI_GalaxyView.CreateMainMenuInterface(ref Engine.Instance, this);
            View fixedView = Engine.Instance.GameWindow.GetView();
            View = new View(fixedView.Center, fixedView.Size);
            IsStateActive = true;
            IsStateAlive = true;
            StateName = "Galaxy View State";

            View.Center = (Vector2f)GalaxyGenerator.Stars[GalaxyGenerator.HomeStarIndex].Origin;
            InitializeStateEvents();

            PlayerPath = new Vertex[0];
        }

        public override void DrawState(RenderWindow objRenderTarget)
        {
            ScrollViewPort();
            FloatRect objViewPort = Engine.Instance.GameWindow.GetView().Viewport;

            if (PlayerPath.Length > 0)
            {
                objRenderTarget.Draw(PlayerPath, 0, 2, PrimitiveType.Lines);
            }

            foreach (var star in GalaxyGenerator.Stars)
            {            
                star.Draw(objRenderTarget);
                Engine.Instance.GameWindow.SetView(View);
            }

            foreach (var star in GalaxyGenerator.Stars)
            {
                if (star.DisplayStarInformation == true)
                {
                    star.objStarMenu.Draw(objRenderTarget);
                }

                Engine.Instance.GameWindow.SetView(View);
            }



            UI.Draw();
        }

        public override void InitializeStateEvents()
        {
            Engine.Instance.GameWindow.KeyReleased += new EventHandler<KeyEventArgs>(KillStateEvent);
            Engine.Instance.GameWindow.MouseWheelScrolled += new EventHandler<MouseWheelScrollEventArgs>(ZoomViewEvent);
            Engine.Instance.GameWindow.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(StarClickEvent);

            foreach (Star currStar in GalaxyGenerator.Stars)
            {
                Engine.Instance.GameWindow.MouseMoved += new EventHandler<MouseMoveEventArgs>(currStar.MouseHoverEvent);
            }          
        }

        public override void HandleSystemEvents()
        { }

        // STATE MEMBERS
        public void StarClickEvent(object sender, MouseButtonEventArgs args)
        {
            if (args.Button == Mouse.Button.Left && this.IsStateActive == true)
            {
                foreach (Star currStar in GalaxyGenerator.Stars)
                {
                    if (currStar.IsMouseOverStar())
                    {
                        Engine.Instance.GameStates.Peek().IsStateActive = false;
                        SystemViewState objSystemView = new SystemViewState(currStar);

                        if (Engine.Instance.GameStates.Contains(objSystemView))
                        {
                            objSystemView = null;
                        }
                        else
                        {
                            Engine.Instance.GameStates.Push(objSystemView);
                        }                                      
                    }
                }
            }
            if (args.Button == Mouse.Button.Right && this.IsStateActive == true)
            {
                bool blnStarSelected = false;
                Vertex vtxStart = new Vertex();
                Vertex vtxEnd = new Vertex();

                foreach (Star currStar in GalaxyGenerator.Stars)
                {
                    if (currStar.HasPlayer == true)
                    {
                        vtxStart = new Vertex((Vector2f)currStar.Origin, Color.Green);
                    }
                   else if (currStar.IsMouseOverStar())
                    {
                        blnStarSelected = true;
                        vtxEnd = new Vertex((Vector2f)currStar.Origin, Color.Green);
                    }
                }

                if (blnStarSelected == false)
                {
                    PlayerPath = new Vertex[0];
                }
                else
                {
                    PlayerPath = new Vertex[] { vtxStart, vtxEnd };
                }
            }
        }

        public void MouseHoverEvent(object sender, MouseMoveEventArgs args)
        {
            if (this.IsStateActive == true)
            {
                foreach (Star currStar in GalaxyGenerator.Stars)
                {
                    if (currStar.IsMouseOverStar())
                    {
                        currStar.Body.OutlineColor = Color.White;
                        currStar.DisplayStarInformation = true;                        
                    }
                    else
                    {
                        currStar.Body.OutlineColor = Color.Transparent;
                        currStar.DisplayStarInformation = false;
                    }
                }
            }
        }

        public void ZoomViewEvent(object sender, MouseWheelScrollEventArgs args)
        {
            if (Engine.Instance.GameWindow.HasFocus() == true && IsStateActive == true)
            {
                if (args.Delta < 0 && View.Size.X <= 7000f)
                {
                    View.Zoom(1.05f);
                }
                if (args.Delta > 0 && View.Size.X >= 1000f)
                {
                    View.Zoom(0.95f);
                }
            }
        }

        public void ScrollViewPort()
        {
            if (IsStateActive == true && Engine.Instance.GameWindow.HasFocus() == true)
            {
                Vector2i vecMousePositon = Mouse.GetPosition(Engine.Instance.GameWindow);
                if (vecMousePositon.X < Engine.Instance.GameWindow.Position.X + 20)
                {
                    View.Move(new Vector2f(-5, 0));
                }
                if (vecMousePositon.X > (Engine.Instance.GameWindow.Position.X + Engine.Instance.GameWindow.Size.X) - 20)
                {
                    View.Move(new Vector2f(5, 0));
                }
                if (vecMousePositon.Y < Engine.Instance.GameWindow.Position.Y + 20)
                {
                    View.Move(new Vector2f(0, -5));
                }
                if (vecMousePositon.Y > (Engine.Instance.GameWindow.Position.Y + Engine.Instance.GameWindow.Size.Y) - 20)
                {
                    View.Move(new Vector2f(0, 5));
                }
            }
        }
        public void KillStateEvent(object sender, SignalArgsVector2f args)
        {
            if (IsStateActive == true && IsStateAlive == true)
            {
                Engine.Instance.KillCurrentState(this);
            }
        }

        public void KillStateEvent(object sender, KeyEventArgs args)
        {
            if (IsStateActive == true && IsStateAlive == true)
            {
                Engine.Instance.KillCurrentState(this);
            }                       
        }

    }
}
