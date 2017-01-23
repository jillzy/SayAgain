﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Test
{
    class UIButton:UIElement
    {
        //constructor
        public UIButton(float size, float x, float y, string content,string newDialogue) {

            this.x = x;
            this.y = y;
            buttonTextFont = new Font("Content/ARCADECLASSIC.ttf");
            buttonText = new Text(content, buttonTextFont);
            buttonText.Position = new SFML.System.Vector2f(x, y);

            rect = new RectangleShape(new SFML.System.Vector2f(size, size));
            rect.Position = new SFML.System.Vector2f(x, y);
            rect.FillColor = Color.Black;
            Color myColor = new Color(177, 177, 177);
            rect.FillColor = myColor;
            this.newDialogue = newDialogue;
        }

        //fields
        float x, y;
        Font buttonTextFont;
        Text buttonText;
        RectangleShape rect;
        string newDialogue;
        bool selected = false;
        int mouseOffsetX = 0;
        int mouseOffsetY = 0;

        //methods
        String eventHandler;

        public RectangleShape getUIButtonRect()
        {
            return rect;
        }

        public float getX() {
            return x;
        }

        public float getY() {
            return y;
        }

        public void setX(float newX) {
            x = newX;
        }

        public void setY(float newY)
        {
            y = newY;
        }
        public Text getUIButtonText()
        {
            return buttonText;
        }

        public FloatRect getRectBounds()
        {
            return rect.GetGlobalBounds();
        }

        public void SetMouseOffset(int x, int y)
        {
            mouseOffsetX = x;
            mouseOffsetY = y;
        }

        public void SetSelected(bool val)
        {
            selected = val;
        }

        public bool GetSelected()
        {
            return selected;
        }

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = getRectBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }

        public void snapBack() {
            rect.Position = new SFML.System.Vector2f(x, y);
            buttonText.Position = new SFML.System.Vector2f(x, y);
        }
        public void translate(int x, int y)
        {
            var bounds = getRectBounds();
            var newXPos = x - mouseOffsetX;
            var newYPos = y - mouseOffsetY;
            if (x - mouseOffsetX < 0)
            {
                newXPos = 0;
            }
            else if (x - mouseOffsetX + (int)bounds.Height > 800)
            {
                newXPos = 800 - (int)bounds.Width;
            }

            if (y - mouseOffsetY < 0)
            {
                newYPos = 0;
            }
            else if (y - mouseOffsetY + (int)bounds.Width > 600)
            {
                newYPos = 600 - (int)bounds.Height;
            }

            rect.Position = new SFML.System.Vector2f(newXPos, newYPos);
            buttonText.Position = new SFML.System.Vector2f(newXPos, newYPos);

        }

        public string getNewDialogue() {
            return newDialogue;
        }
    }
}
