using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using AnoleEngine.Engine_Base;

namespace SpaceExplorers
{
    static class Program
    {
        public static Stack<AnoleEngine.Engine_Base.States.GameState> StateStack = new Stack<AnoleEngine.Engine_Base.States.GameState>();

        static void Main()
        {
            try
            {
                // Request a 32-bits depth buffer when creating the window
                string strGameName = "SpaceExplorers";
                string strGameVersion = "0.0.1.0000";

                Engine.GameName = strGameName + " " + strGameVersion;
                InitializeBaseStates();

                Console.OpenStandardOutput();
                Console.WriteLine("Game loaded, entering loop.");
                Console.WriteLine();
                Console.WriteLine("Window size: X:" + Engine.Instance.GameWindow.Size.X.ToString() + " Y: " + Engine.Instance.GameWindow.Size.Y.ToString());
                Console.WriteLine();

                while (Engine.Instance.GameWindow.IsOpen)
                {
                    Engine.Instance.HandleGameWindowEvents();
                    Engine.Instance.GameWindow.Clear();
                    Engine.Instance.DrawGameStates();
                    Engine.Instance.GameWindow.Display();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        static void InitializeBaseStates()
        {
            States.MainMenuState MainMenuState = new States.MainMenuState(Engine.Instance);
            Engine.Instance.GameStates.Push(MainMenuState);
        }

    }
}

