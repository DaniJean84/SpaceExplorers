using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML;
using System.Collections;
using System.IO;

namespace AnoleEngine.Engine_Base.UI
{
    class Button_Obsolete
    {
        // PRIVATE MEMBERS     
        private SFML.System.Vector2f Position;
        private SFML.System.Vector2f Size;

        // PUBLIC MEMBERS
        public int zLayer { get; set; }
        public string Name { get; set; }
        public bool HasFocus { get; set; }
        public int yOffset { get; set; }

        public States.GameState ParentState { get; set; }
        public RectangleShape recBody;
        public Text buttonText;
        public RenderStates renderState = new RenderStates();

        public Button_Obsolete()
        {
            Position.X = 100;
            Position.Y = 100;
            zLayer = 0;

            Size.X = 50;
            Size.Y = 50;

            recBody = new RectangleShape(Size);
            recBody.Position = Position;

            SetButtonTexture(null);
        }

        public Button_Obsolete(float fltBodyWidth, float fltBodyHeight)
        {
            Position.X = 100;
            Position.Y = 100;
            zLayer = 0;
            
            Size.X = fltBodyWidth;
            Size.Y = fltBodyHeight;
            
            recBody = new RectangleShape(Size);
            recBody.Position = Position;
            recBody.FillColor = Color.Blue;
            SetButtonTexture(null);
        }

        public Button_Obsolete(float fltBodyWidth, float fltBodyHeight, float fltXPos, float fltYPos)
        {
            Position.X = fltXPos;
            Position.Y = fltYPos;
            zLayer = 0;

            Size.X = fltBodyWidth;
            Size.Y = fltBodyHeight;
            
            recBody = new RectangleShape(Size);
            recBody.Position = Position;
            recBody.FillColor = Color.Blue;
        }

        public Button_Obsolete(float fltBodyWidth, float fltBodyHeight, float fltXPos, float fltYPos, int ZLayer)
        {
            Position.X = fltXPos;
            Position.Y = fltYPos;
            zLayer = ZLayer;

            Size.X = fltBodyWidth;
            Size.Y = fltBodyHeight;

            recBody = new RectangleShape(Size);
            recBody.Position = Position;
            recBody.OutlineColor = Color.White;
            recBody.OutlineThickness = 2;
            recBody.FillColor = Color.Blue;
            
            SetButtonTexture(null);
        }

        public void UpdatePosition(float fltXPos, float fltYPos)
        {
            Position.X = fltXPos;
            Position.Y = fltYPos;
            recBody.Position = Position;
            buttonText.Position = Position;
        }

        public void UpdatePosition(Vector2f vecNewPosition)
        {
            Position = vecNewPosition;
            recBody.Position = vecNewPosition;
            buttonText.Position = vecNewPosition;
        }

        public void SetButtonTexture(Texture txTexture)
        {
            if (txTexture == null)
            {
                recBody.FillColor = Color.Green;
            }
            else
            {
                recBody.Texture = txTexture;
            }
        }

        public bool IsMouseOverButton()
        {
            Vector2i mousePosition = Mouse.GetPosition(Engine.Instance.GameWindow);

            if (recBody.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
            {
                HasFocus = true;
                return true;
            }
            else
            {
                HasFocus = false;
                return false;
            }
        }

        public void SetButtonText(string strText, Color color)
        {
            buttonText = new Text();

            if (! string.IsNullOrEmpty(strText))
            {
                buttonText.DisplayedString = strText;
            }
            else
            {
                buttonText.DisplayedString = "Default text.";
            }

            buttonText.Color = color;

            Font defaultFont = new Font(Engine.Instance.fontStream);              
            buttonText.Font = defaultFont;

            buttonText.Position = Position;
            buttonText.CharacterSize = 25;
        }
    }
}
