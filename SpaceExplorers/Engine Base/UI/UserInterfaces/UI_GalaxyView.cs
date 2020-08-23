using AnoleEngine.Engine_Base;
using AnoleEngine.Engine_Base.Game_Code.GalaxyGen;
using AnoleEngine.Engine_Base.States;
using AnoleEngine.Engine_Base.UI;
using AnoleEngine.Engine_Base.UI.Renderers;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceExplorers.Game_Code.Galaxy;
using SpaceExplorers.States;
using System;
using TGUI;

namespace AnoleEngine.Engine_Base.Engine_Base.UI.UserInterfaces
{
    class UI_GalaxyView
    {
        public static Gui CreateMainMenuInterface(ref AnoleEngine.Engine_Base.Engine objEngineInstance, GalaxyViewState state)
        {
            float fltGameWindowWidth = objEngineInstance.GameWindow.Size.X;
            float fltGameWindowHeight = objEngineInstance.GameWindow.Size.Y;

            Gui UI = new Gui(objEngineInstance.GameWindow);

            Panel objGalaxyMenuLayout = new Panel();
            objGalaxyMenuLayout.SetRenderer(UI_Renderers.UIGalaxyMenuLayoutRenderer.Data);
            objGalaxyMenuLayout.Size = new Vector2f(fltGameWindowWidth, fltGameWindowHeight / 7);
            objGalaxyMenuLayout.ShowWithEffect(ShowAnimationType.SlideFromBottom, Time.FromMilliseconds(800));
            objGalaxyMenuLayout.Position = new Vector2f((fltGameWindowWidth/2) - (objGalaxyMenuLayout.Size.X / 2), fltGameWindowHeight - objGalaxyMenuLayout.Size.Y);

          
            Button closeButton = new Button("CLOSE");
            closeButton.Size = new Vector2f(50f, 45);
            closeButton.Position = new Vector2f(objGalaxyMenuLayout.Size.X - 60, 5);
            closeButton.SetRenderer(UI_Renderers.UIGalaxyViewButtonRenderer.Data);
            closeButton.Clicked += new EventHandler<SignalArgsVector2f>(state.KillStateEvent);
            objGalaxyMenuLayout.Add(closeButton, "closeButton");

            UI.Add(objGalaxyMenuLayout, "GalaxyLayout");
            return UI;
        }

    }
}
