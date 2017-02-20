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
        protected double scaleFactorX;
        protected double scaleFactorY;


        //Input Manager
        protected InputManager ManagerOfInput = new InputManager();

        //User Inferface Manager
        protected UIManager ui_man = new UIManager();
        protected List<UIButton> buttons;


        //Menus
        protected Menu startMenu = new Menu("start");
        protected Menu settingsMenu = new Menu("settings");
        protected Menu pauseMenu = new Menu("pause");
        protected List<Menu> menus = new List<Menu>();


        //Matrices
        protected ToneEffects tfx = new ToneEffects();
        protected ContextFilter cf;
        protected Relationships rs = new Relationships();

        //Font
        static protected Font Adore64 = new Font(new FileStream("../../Fonts/Adore64.ttf", FileMode.Open));

        //Character States
        // Deprecated: protected CharacterState Alex, Mom, Dad;
        // Replaced with DramaManager that holds each characterstate
        protected DramaManager D_Man = new DramaManager();
  
        //Jill's fields and variables
        protected DialogueBox dialogueBox;
        protected Boolean init;
        protected View fullScreenView, scrollview;

        #region AI_FIELDS
        protected List<string> currentMadeMemories = new List<string>();
        protected List<DialogueObj> responseList = new List<DialogueObj>();
        protected List<DialogueObj> responseListAlex = new List<DialogueObj>();
        protected List<string> currentMilestones = new List<string>();
        protected int FNC = 0;
        protected string currentContext = "";
        protected tone currentTone = tone.Root;
        protected Loader Load = new Loader();
        protected Selector s = new Selector();
    #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////

        protected GameState State = new GameState();

        public Game(uint width, uint height, string title, Color clearColor)
        {
            window = new RenderWindow(new VideoMode(width, height), title, Styles.Default);

            this.clearColor = clearColor;

            // Set-up Events
            window.Closed += onClosed;
            window.KeyPressed += onKeyPressed;
            window.Resized += onResized;

        }

        public void Run()
        {
            Initialize();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                Update();
                
                Draw();
                window.Display();
            }
        }

        //protected abstract void LoadContent();
        protected abstract void Initialize();
        protected abstract void Update();
        protected abstract void Draw();

        private void onResized(object sender, EventArgs e) {

        }

        private void onClosed(object sender, EventArgs e)
        {
            window.Close();
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code.Equals(Keyboard.Key.Escape)) {
                window.Close();
            }
        }
    }
}
