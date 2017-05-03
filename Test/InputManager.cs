using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Test {
    class InputManager {
        public InputManager() {

        }

        //fields
        int MouseX;
        int MouseY;
        bool MouseDown = false;
        bool MouseRelease = false;
        bool MouseMove = false;

        /////////////////////////////////////////////////BUILT-IN
        private void SetMousePos(int x, int y) {
            MouseX = x;
            MouseY = y;
        }

        public int[] GetMousePos() {
            return new int[] { MouseX, MouseY };
        }

        private void SetMouseDown(bool value) {
            MouseDown = value;
        }

        public bool GetMouseDown() {
            return MouseDown;
        }

        private void SetMouseRelease(bool value) {
            MouseRelease = value;
        }

        private bool GetMouseRelease() {
            return MouseRelease;
        }

        private void SetMouseMove(bool value) {
            MouseMove = value;
        }

        private bool GetMouseMove() {
            return MouseMove;
        }

        private void printMouseStuff() {
            //Console.WriteLine(MouseDown + ", " + MouseMove + ", " + MouseRelease);

        }

        private bool CheckCollision(FloatRect bounds) {
            if (MouseX >= bounds.Left && MouseX <= bounds.Left + bounds.Width && MouseY >= bounds.Top && MouseY <= bounds.Top + bounds.Height) {
                return true;
            }
            return false;
        }
        //////////////////////////////////////////////////////////////////////BUILT-IN

        #region SA_OnMouseMoved
        //
        public void OnMouseMoved(GameState State, int x, int y) {
            if (State.GetState() == "game" || State.GetState() == "tutorial") {
                if (this.GetMouseDown()) {
                    this.SetMouseMove(true);
                    this.SetMousePos(x, y);
                }
            }
        }
        #endregion
       
        #region SA_onMouseButtonReleased
        public void onMouseButtonReleased() {
            this.SetMouseMove(false);
            this.SetMouseDown(false);
            this.SetMouseRelease(true);
        }
        #endregion

        #region SA_checkTargets
        public void checkTargets(GameState State, DramaManager d) {
            if (State.GetState() == "game") {
                if (d.getAlex().Contains(MouseX, MouseY)) {

                }
                
                //d.getAlex().targetCheck(MouseX, MouseY);
                //d.getMom().targetCheck(MouseX, MouseY);
                //d.getDad().targetCheck(MouseX, MouseY);

            }
        }
        #endregion

        #region SA_onMouseButtonPressed
        public void onMouseButtonPressed(int x, int y) {
            this.SetMousePos(x, y);
            this.SetMouseRelease(false);
            this.SetMouseDown(true);
        }
        #endregion

        #region SA_GamePlay
        public void GamePlay(GameState s, List<UIButton> b, int x, int y, double sx, double sy) {
            if (s.GetState() == "game" || s.GetState() == "tutorial") {
                for (var i = 0; i < b.Count; i++) {
                    if (b[i].Contains((int)(x*sx), (int)(y*sy))) {
                        var bounds = b[i].getRectBounds();
                        s.sound_man.playSFX("button");
                        b[i].SetMouseOffset((int)(x - bounds.Left), (int)(y - bounds.Top));
                        b[i].SetSelected(true);
                    }
                }

                this.SetMousePos(x, y);
                this.SetMouseRelease(false);
                this.SetMouseDown(true);
            }
        }
        #endregion

        #region SA_MenuPlay
        public void MenuPlay(GameState s,List<Menu> m, int x, int y) {
            var startMenu = m[0]; var settingsMenu = m[1]; var pauseMenu = m[2];
            if (s.GetState() == "menu") {
                // Menu Traversal Logic
                if (s.GetMenuState() == "start") //If Current Menu State is the Start Menu
                {
                    // Pass the current menu's buttons, along with a list of tuples symbolizing:
                    //      Tuple(ButtonText, TargetState, AnonymousFunction)
                    s.updateMenuState(this.GetMousePos(), startMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Start", "tutorial", new Task(() => { s.sound_man.playSFX("button");})),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => { s.sound_man.playSFX("button"); }))
                    });

                } else if (s.GetMenuState() == "settings") //If Current Menu State is the Settings Menu
                  {
                    s.updateMenuState(this.GetMousePos(), settingsMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Sound", "settings", new Task(() => { settingsMenu.getMenuButtons()[0].toggleon = !settingsMenu.getMenuButtons()[0].toggleon; s.sound_man.playSFX("button"); })),
                        new Tuple<string, string, Task>("Back", "start", new Task(() => { s.sound_man.playSFX("button"); }))
                    });

                }
            } else if (s.GetState() == "pause") {
                if (s.GetMenuState() == "pause") {
                    s.updateMenuState(this.GetMousePos(), pauseMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Back", "game", new Task(() => { s.sound_man.playSFX("button"); })),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => { s.sound_man.playSFX("button"); })),
                        new Tuple<string, string, Task>("Quit", "menu", new Task(() => { s.sound_man.playSFX("button"); }))
                    });
                } else if (s.GetMenuState() == "settings") {
                    s.updateMenuState(this.GetMousePos(), settingsMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Sound", "settings", new Task(() => { settingsMenu.getMenuButtons()[0].toggleon = !settingsMenu.getMenuButtons()[0].toggleon; s.sound_man.playSFX("button"); })),
                        new Tuple<string, string, Task>("Back", "pause", new Task(() => { s.sound_man.playSFX("button"); }))
                    });
                }
            }
        }
        #endregion
    }
}
