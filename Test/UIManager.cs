﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;

//holds UI elements such as buttons, input fields, TextBoxes, etc
namespace Test
{
    class UIManager
    {
        //constructor
        public UIManager() {
            
        }


        List<UIButton> buttons = new List<UIButton>(); //our tone buttons

        //methods
        public List<UIButton> getButtons() {
            return buttons;
        }

        public void addButton(UIButton b) {
            buttons.Add(b);
        }
    }
}