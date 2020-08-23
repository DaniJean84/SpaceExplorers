using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using TGUI;
//using System.Drawing;


namespace AnoleEngine.Engine_Base.UI.Renderers
{
    public class UI_Renderers
    {
        public static PanelRenderer UIGalaxyMenuLayoutRenderer = new PanelRenderer()
        {
            BackgroundColor = new Color(66, 69, 244, 75),
            Opacity = 1f,
            TransparentTexture = false
        };

        public static TextBoxRenderer UITextBoxRenderer = new TextBoxRenderer()
        {
            BackgroundColor = new Color(66, 69, 244, 75),
            TextColor = Color.White,
            CaretColor = Color.Cyan
        };

        public static ButtonRenderer UIGalaxyViewButtonRenderer = new ButtonRenderer()
        {
            BackgroundColor = new Color(50,220,125),
            TextColor = Color.Black,
            BackgroundColorHover = Color.Green,
            BorderColor = Color.Black,
            TextStyle = Text.Styles.Bold
        };

        public static ButtonRenderer UIButtonRenderer = new ButtonRenderer()
        {
            BackgroundColor = Color.Blue,//new Color(50,220,125),
            TextColor = Color.Black,
            BackgroundColorHover = Color.Cyan,
            TextStyle = Text.Styles.Bold
        };

        public static ButtonRenderer UIBackButtonRenderer = new ButtonRenderer()
        {
            BackgroundColor = Color.Red,
            TextColor = Color.Black,
            BackgroundColorHover = Color.Magenta,
            TextStyle = Text.Styles.Bold
            
        };

        public static ListViewRenderer UIListViewRenderer = new ListViewRenderer()
        {
            BackgroundColor = new Color(66, 69, 244, 75),
            BorderColor = Color.Blue,
            HeaderBackgroundColor = Color.Blue,
            HeaderTextColor = Color.White,      
            SeparatorColor = Color.Blue,
            SelectedBackgroundColor = Color.Cyan,
            SelectedTextColorHover = Color.Red,
            BackgroundColorHover = Color.Cyan,
            TextColor = Color.White,
            TextColorHover = Color.Black,
            SelectedTextColor = Color.Black
        };

        public static ComboBoxRenderer UIComboBoxRenderer = new ComboBoxRenderer()
        {
            BackgroundColor = new Color(66, 69, 244, 200),
            TextStyle = Text.Styles.Bold,
            BorderColor = Color.Blue,            
            TextColor = Color.White,
            ArrowColor = Color.Blue,
            ArrowColorHover = Color.Cyan,
            ArrowBackgroundColor = new Color(66, 69, 244, 200),
            Opacity =1
        };

        public static ListBoxRenderer UIListBoxRenderer = new ListBoxRenderer()
        {
            BackgroundColor = new Color(66, 69, 244, 200),
            Opacity = 1,
            BorderColor = Color.Blue,
            TextColor = Color.White,
            BackgroundColorHover = Color.Cyan,
            TextColorHover = Color.Black,
            SelectedBackgroundColor = Color.Blue,
            SelectedTextColor = Color.Black,
            TextStyle = Text.Styles.Bold             
        };

        

        public static LabelRenderer UILabelRenderer = new LabelRenderer()
        {
            TextColor = Color.Cyan,
            TextStyle = Text.Styles.Bold
        };

    }
}
