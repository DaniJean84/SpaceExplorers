using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using TGUI;

namespace AnoleEngine.Engine_Base.States
{
    class GameState
    {
        public virtual void InitializeState() { }

        public virtual void DrawState(SFML.Graphics.RenderWindow objRenderTarget) { }

        public virtual void InitializeStateEvents() { }

        public virtual void HandleSystemEvents() { }

        //protected List<AnoleEngine.Engine_Base.UI.Button> Buttons;

        protected Gui UI { get; set; }
        public bool IsStateAlive { get; set; }
        public bool IsStateActive { get; set; }
        public string StateName { get; set; }

        public View View { get; set; }
    }
}
