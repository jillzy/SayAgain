using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Window;
using SFML.Graphics;


namespace Test
{
    abstract class Game
    {
        protected RenderWindow window;
        protected Color clearColor;

        //////////////////////////////////////////////////////////////////////////////////////////////
        //Screen defaults
        static protected UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static protected UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;
        //Input Manager
        protected InputManager ManagerOfInput = new InputManager();
        //User Inferface Manager
        protected UIManager ui_man = new UIManager();

        //Cutscene Manager
        protected CutsceneManager cman = new CutsceneManager();
        //Menus
        protected StartMenu startMenu = new StartMenu("start");
        protected StartMenu settingsMenu = new StartMenu("settings");
        protected StartMenu pauseMenu = new StartMenu("pause");
        //Matrices
        protected ToneEffects tfx = new ToneEffects();
        protected ContextFilter cf;
        protected Relationships rs = new Relationships();
        //Font
        static protected Font Adore64 = new Font(new FileStream("../../Fonts/Ticketing.ttf", FileMode.Open));
        //Jill's fields and variables
        //protected DialogueBox dialogueBox;
        //protected Boolean init;
        //protected View fullScreenView, scrollview;

        /////////////////////////////////////////////////////////////////////////////////////////////

        protected GameState State = new GameState();

        public Game(uint width, uint height, string title, Color clearColor)
        {
            window = new RenderWindow(new VideoMode(width, height), title, Styles.Close);
            this.clearColor = clearColor;

            // Set-up Events
            window.Closed += onClosed;
            window.KeyPressed += onKeyPressed;
        }

        public void Run()
        {
            LoadContent();
            Initialize();

            /***********************************************/
            /*                                             */
            /*        framerate lock                       */
            /*        incase logic is bound to frames      */
            /*        being drawn,                         */
            /*        animation won't go insanely fast     */
            /*                                             */
            /***********************************************/

            DateTime time = DateTime.Now;
            float framerate = 60f;
            while (window.IsOpen)
            {
                window.DispatchEvents();
                Update();

                if ((DateTime.Now - time).TotalMilliseconds > (1000f / framerate))
                {
                    time = DateTime.Now;
                    Draw();
                    window.Display();
                }
            }
        }

        protected abstract void LoadContent();
        protected abstract void Initialize();
        protected abstract void Update();
        protected abstract void Draw();

        private void onClosed(object sender, EventArgs e)
        {
            window.Close();
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Code);
            if (e.Code.Equals(Keyboard.Key.Escape)) {
                window.Close();
            }
        }
    }
}
