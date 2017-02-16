﻿using System;
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
        public void SetMousePos(int x, int y) {
            MouseX = x;
            MouseY = y;
        }

        public int[] GetMousePos() {
            return new int[] { MouseX, MouseY };
        }

        public void SetMouseDown(bool value) {
            MouseDown = value;
        }

        public bool GetMouseDown() {
            return MouseDown;
        }

        public void SetMouseRelease(bool value) {
            MouseRelease = value;
        }

        public bool GetMouseRelease() {
            return MouseRelease;
        }

        public void SetMouseMove(bool value) {
            MouseMove = value;
        }

        public bool GetMouseMove() {
            return MouseMove;
        }

        private void printMouseStuff() {
            Console.WriteLine(MouseDown + ", " + MouseMove + ", " + MouseRelease);
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
            if (State.GetState() == "game") {
                if (this.GetMouseDown()) {
                    this.SetMouseMove(true);
                    this.SetMousePos(x, y);
                }
            }
        }
        #endregion

        //SA: onMouseButtonReleased


        #region SA_onMouseButtonReleased
        public void onMouseButtonReleased() {
            this.SetMouseMove(false);
            this.SetMouseDown(false);
            this.SetMouseRelease(true);
        }
        #endregion

        #region SA_checkTargets
        public void checkTargets(GameState State, CharacterState Alex, CharacterState Mom, CharacterState Dad) {
            if (State.GetState() == "game") {
                Alex.targetCheck(MouseX, MouseY);
                Mom.targetCheck(MouseX, MouseY);
                Dad.targetCheck(MouseX, MouseY);
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
        public void GamePlay(GameState s, List<UIButton> b, int x, int y) {
            if (s.GetState() == "game") {

                for (var i = 0; i < b.Count; i++) {
                    if (b[i].Contains(x, y)) {
                        var bounds = b[i].getRectBounds();
                        b[i].SetMouseOffset(x - (int)bounds.Left, y - (int)bounds.Top);
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
                        new Tuple<string, string, Task>("Game Start", "game", new Task(() => {})),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => {}))
                    });

                } else if (s.GetMenuState() == "settings") //If Current Menu State is the Settings Menu
                  {
                    s.updateMenuState(this.GetMousePos(), settingsMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("8K GAMING", "settings", new Task(() => {})),
                        new Tuple<string, string, Task>("<- Back", "start", new Task(() => {}))
                    });

                }
            } else if (s.GetState() == "pause") {
                if (s.GetMenuState() == "pause") {
                    s.updateMenuState(this.GetMousePos(), pauseMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Back to Game", "game", new Task(() => {})),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => {})),
                        new Tuple<string, string, Task>("Quit", "menu", new Task(() => {}))
                    });
                } else if (s.GetMenuState() == "settings") {
                    s.updateMenuState(this.GetMousePos(), settingsMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("8K GAMING", "settings", new Task(() => {})),
                        new Tuple<string, string, Task>("<- Back", "pause", new Task(() => {}))
                    });
                }
            }
        }
        #endregion


    }
}
