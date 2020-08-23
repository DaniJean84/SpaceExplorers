using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using TGUI;
using AnoleEngine.Engine_Base.Game_Code.GalaxyGen;

namespace AnoleEngine.Engine_Base.UI.Controls
{
    class StarMenuControl
    {
        private List<Drawable> BodyComponentList { get; set; }
        private Vertex[] HorizontalLine1 { get; set; }
        private Vertex[] HorizontalLine2 { get; set; }
        private Vertex[] VerticalLine1 { get; set; }

        public StarMenuControl(Star objStar)
        {
            BodyComponentList = new List<Drawable>();

            float fltStarRad = objStar.Radius;
            Vector2f vecStarCenter = objStar.Position;
            float fltStarCenterX = vecStarCenter.X + fltStarRad;
            float fltStarCenterY = vecStarCenter.Y + fltStarRad;

            CircleShape objCenterCirc = new CircleShape(fltStarRad + 10);
            objCenterCirc.OutlineColor = Color.Blue;
            objCenterCirc.OutlineThickness = 2;
            objCenterCirc.FillColor = Color.Transparent;

            float fltCenterCircX = fltStarCenterX - objCenterCirc.Radius;
            float fltCenterCircY = fltStarCenterY - objCenterCirc.Radius;
            objCenterCirc.Position = new Vector2f(fltCenterCircX, fltCenterCircY);
            this.BodyComponentList.Add(objCenterCirc);

            float fltCenterCircOffset = fltStarRad + 10;
            float fltHorizontalBarX = 50;

            float fltLineOffsetX = fltCenterCircOffset + fltHorizontalBarX;
            Vector2f vecVertex1 = new Vector2f(fltStarCenterX, fltStarCenterY - fltCenterCircOffset);
            Vector2f vecVertex2 = new Vector2f(fltStarCenterX - fltLineOffsetX, fltStarCenterY - 100);
            Vector2f vecVertex3 = new Vector2f(fltStarCenterX - fltLineOffsetX, fltStarCenterY + 100);
            Vector2f vecVertex4 = new Vector2f(fltStarCenterX, fltStarCenterY + fltCenterCircOffset);

            HorizontalLine1 = new Vertex[]
            {
                new Vertex(vecVertex1, Color.Blue),
                new Vertex(vecVertex2, Color.Blue),
                new Vertex(vecVertex3, Color.Blue),
                new Vertex(vecVertex4, Color.Blue)
            };

            float fltRecHeight = 200;
            RectangleShape recDetails = new RectangleShape(new Vector2f(200, fltRecHeight));
            recDetails.FillColor = new Color(50, 170, 255, 50);
            recDetails.OutlineColor = Color.Blue;
            recDetails.OutlineThickness = 2;

            recDetails.Position = new Vector2f(vecVertex2.X - 200, vecVertex2.Y);
            BodyComponentList.Add(recDetails);
            int intOrbitalCount = 0;
            
            if (objStar.ChildOrbits != null)
            {
                intOrbitalCount = objStar.ChildOrbits.Length;
            }

            string strText = $"Name: {objStar.SystemName}\r\nType: {objStar.StarType}\r\nMajor Orbitals: {intOrbitalCount}";
            Text objDetailText = new Text(strText, new Font(Engine.Instance.fontStream));
            objDetailText.Position = new Vector2f(recDetails.Position.X + 5, recDetails.Position.Y + 5);
            objDetailText.CharacterSize = 11;
            BodyComponentList.Add(objDetailText);

        }

        private void BuildDetailText(Star objStar)
        {

        }

        public void Draw(RenderTarget objTarget)
        {
            foreach (Drawable currShape in BodyComponentList)
            {
                
                objTarget.Draw(currShape);
            }

            objTarget.Draw(HorizontalLine1,0, 4, PrimitiveType.LineStrip);
        }

    }
}
