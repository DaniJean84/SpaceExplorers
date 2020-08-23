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
using System.IO;
using AnoleEngine.Engine_Base.Game_Code.Graphics.Shader;
using SpaceExplorers.Game_Code.Galaxy;
using AnoleEngine.Engine_Base.Game_Code.GalaxyGen;

namespace AnoleEngine.Engine_Base
{
    class Engine
    {
        // PUBLIC
        public static Engine Instance = new Engine();
        public static string GameName { get; set; }
        public static Dictionary<string, FileStream> Shaders { get; private set; }

        public FileStream fontStream;
        public ContextSettings contextSettings;
        public RenderWindow GameWindow;
        public Stack<States.GameState> GameStates;

        public Vector2f DistanceOfMouseFromStar;
        public Text devText;
        public bool blnShouldDisplayDevMode =  false;       
        public Clock GameClock { get; private set; }
        public Dictionary<string, Texture> SpriteImages { get; private set; }

        private VideoSettings vidSettings;
        private string EngineVersion = "1.0.0.0000";

        public Engine()
        {
            GameClock = new Clock();           
            contextSettings = new ContextSettings();
            contextSettings.DepthBits = 32;
            contextSettings.AntialiasingLevel = 4;

            VideoMode DisplayMode = new VideoMode(1920, 1080);        
            
            if (string.IsNullOrEmpty(GameName))
            {
                GameName = "AnoleEngine " + EngineVersion;
            }

            GameWindow = new RenderWindow(DisplayMode, GameName, Styles.Fullscreen);
            GameStates = new Stack<States.GameState>();
            GameWindow.SetKeyRepeatEnabled(true);
            InitializeEvents();
            LoadDefaultFonts();
            LoadDefaultShaders();
            LoadDefaultSprites();
            InitializeDevTextDisplay();
        }

        public void LoadDefaultFonts()
        {
            Console.WriteLine("Loading default fonts...");
            fontStream = new FileStream(@"Assets\fonts\arial.ttf", FileMode.Open);
        }

        public void LoadDefaultSprites()
        {
            Console.WriteLine("Loading default fonts...");
            SpriteImages = new Dictionary<string, Texture>();

            FileStream fsImage = new FileStream(@"Assets\Sprites\Star_1.png", FileMode.Open);
            SpriteImages.Add("star", new Texture(new Image(fsImage)));
            SpriteImages["star"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_small_1.png", FileMode.Open);
            SpriteImages.Add("starSm1", new Texture(new Image(fsImage)));
            SpriteImages["starSm1"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_small_2.png", FileMode.Open);
            SpriteImages.Add("starSm2", new Texture(new Image(fsImage)));
            SpriteImages["starSm2"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_small_3.png", FileMode.Open);
            SpriteImages.Add("starSm3", new Texture(new Image(fsImage)));
            SpriteImages["starSm3"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_med_1.png", FileMode.Open);
            SpriteImages.Add("starMed1", new Texture(new Image(fsImage)));
            SpriteImages["starMed1"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_med_2.png", FileMode.Open);
            SpriteImages.Add("starMed2", new Texture(new Image(fsImage)));
            SpriteImages["starMed2"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_med_3.png", FileMode.Open);
            SpriteImages.Add("starMed3", new Texture(new Image(fsImage)));
            SpriteImages["starMed3"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_big_1.png", FileMode.Open);
            SpriteImages.Add("starBig1", new Texture(new Image(fsImage)));
            SpriteImages["starBig1"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_big_2.png", FileMode.Open);
            SpriteImages.Add("starBig2", new Texture(new Image(fsImage)));
            SpriteImages["starBig2"].Smooth = true;

            fsImage = new FileStream(@"Assets\Sprites\Star_big_3.png", FileMode.Open);
            SpriteImages.Add("starBig3", new Texture(new Image(fsImage)));
            SpriteImages["starBig3"].Smooth = true;
        }

        public void LoadDefaultShaders()
        {
            Console.WriteLine("Loading default shaders...");
            Shaders = new Dictionary<string, FileStream>();

            //(@"Assets\Shaders\star.vert", "", @"Assets\Shaders\star.frag");
            FileStream fsStarVert = new FileStream(@"Assets\Shaders\star.vert", FileMode.Open);
            Shaders.Add("starVert", fsStarVert);

            FileStream fsStarFrag = new FileStream(@"Assets\Shaders\star.frag", FileMode.Open);
            Shaders.Add("starFrag", fsStarFrag);
        }


        public void UpdateVieoSettings(VideoSettings Settings)
        {
            contextSettings.AntialiasingLevel = Settings.AALevel;
            GameWindow.SetVerticalSyncEnabled(Settings.VertSyncEnabled);
        }

        public void KillCurrentState(States.GameState CurrState)
        {
            GameStates.Peek().IsStateAlive = false;
            GameStates.Peek().IsStateActive = false;

            GameStates.Pop();

            GameStates.Peek().IsStateAlive = true;
            GameStates.Peek().IsStateActive = true; 
            GameWindow.SetView(GameStates.Peek().View);
        }

        public void DrawGameStates()
        {
            foreach (var currState in GameStates)
            {
                if (currState.IsStateAlive == true && currState.IsStateActive == true)
                {          
                    currState.DrawState(GameWindow);
                }              
            }

            if (blnShouldDisplayDevMode)
            {
                SetDevModeText();

                float fltLeft = GameWindow.GetView().Viewport.Left - GameWindow.GetView().Viewport.Width / 2;
                float fltTop = GameWindow.GetView().Viewport.Top - GameWindow.GetView().Viewport.Height / 2;

                devText.Position = new Vector2f(fltLeft,fltTop);
                GameWindow.Draw(devText);               
            }

        }

        public void HandleSystemEvents()
        {

        }

        public void HandleGameWindowEvents()
        {
            GameWindow.DispatchEvents();           
        }

        public static void InitializeStateEvents()
        {
            Console.WriteLine("Initializing game state events. . .");
            uint StateCount = 0;

            foreach (var currState in Instance.GameStates)
            {
                currState.InitializeStateEvents();
                StateCount++;
                Console.WriteLine(currState.StateName + " events loaded. . .");
            }

            Console.WriteLine(StateCount.ToString() +  " states loaded. . .");
        }

        public void WriteToConsole(string strMessage)
        {
            Console.WriteLine(strMessage);
        }

        public void WriteToConsole(string strMessage, bool MoveCursorUpByOne, bool MoveCursorRightByOne, bool MoveCursorBeforeWrite)
        {
            if (MoveCursorBeforeWrite == false)
            {
                Console.WriteLine(strMessage);

                if (MoveCursorUpByOne == true)
                {
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                }
                if (MoveCursorRightByOne == true)
                {
                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                }
            }
            else
            {
                if (MoveCursorUpByOne == true)
                {
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                }
                if (MoveCursorRightByOne == true)
                {
                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                }

                Console.WriteLine(strMessage);
            }

            
        }

        // TODO - Refactor this method for the general case rather than star shader specific.
        private void InitializeEvents()
        {
            Console.WriteLine("Loading window events. . .");

            GameWindow.Closed += new EventHandler(OnClosed);
            GameWindow.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            GameWindow.Resized += new EventHandler<SizeEventArgs>(OnResized);

            Console.WriteLine("Events loaded. . .");
        }

        private void OnClosed(object sender, EventArgs args)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        private void OnKeyPressed(object sender, KeyEventArgs args)
        {
            if (args.Code == Keyboard.Key.Tilde)
            {
                if (blnShouldDisplayDevMode == true)
                {
                    blnShouldDisplayDevMode = false;
                }
                else
                {
                    blnShouldDisplayDevMode = true;
                }
            }
        }

        public void SetDevModeText()
        {
            Vector2i mousePosition = Mouse.GetPosition();
            string strDevText = "Mouse X: " + mousePosition.X + " Y: " + mousePosition.Y + Environment.NewLine;
            strDevText = strDevText + " Window Position: " + GameWindow.Position.ToString() + Environment.NewLine;
            strDevText = strDevText + "View Position: " + GameWindow.GetView().Center.ToString() + Environment.NewLine;
            devText.DisplayedString = strDevText;
        }

        private void OnResized(object sender, SizeEventArgs args)
        {
            GameWindow.Size = new Vector2u(args.Width, args.Height);           
            Console.WriteLine("Window resized to: " + args.Width.ToString() + " by:" + args.Height.ToString());
        }

        private void InitializeDevTextDisplay()
        {
            devText = new Text();
            devText.Font = new Font(fontStream);
            devText.Color = Color.Yellow;
            devText.Position = (Vector2f)GameWindow.Position;
            devText.CharacterSize = 16;
            devText.Style = Text.Styles.Bold;     
        }
        
    }

    struct VideoSettings
    {
        public bool FullScreen { get; set; }
        public bool VertSyncEnabled { get; set; }
        public uint AALevel { get; set; }
    }
}
