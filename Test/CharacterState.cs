using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace Test
{
    class CharacterState : Drawable
    {
        //fields
        private string who;
        private double mood;
        private double volatility;
        private double goal;
        double talkedTo = 0;

        static bool[] targets = { false,false,false }; // 0 = alex 1 = mom 2 = dad
        List<String> memory = new List<String>();

        Color color;

        RectangleShape characterRect = new RectangleShape(new Vector2f(75, 75));

        //fields made for drawing character box, temp
        int x = 0;
        int y = 10;

        //methods

        public CharacterState()
        {
            this.mood = 0;
            this.volatility = 0;
        }

        public List<String> getMemories()
        {
            return memory;
        }

        public void addMemory(string s)
        {
            memory.Add(s);
        }

        public void setMood(double m)
        {
            mood = m;
        }

        public double getMood()
        {
            return mood;
        }

        public void setVolatility(double v)
        {
            volatility = v;
        }

        public double getVolatility() {
            return volatility;
        }


        public void SetTalked(char f, double amount)
        {
            if (f == 'i')
            {
                talkedTo += amount;
            }
            else if (f == 'd')
            {
                talkedTo -= amount;
            }
        }

        public bool CheckTalked()
        {
            if (talkedTo == 10000000.0)
            {
                return true;
            }
            return false;
        }

        public void setHover(bool b)
        {
            if (b)
            {
                characterRect.FillColor = Color.White;
            }
            else
            {
                characterRect.FillColor = this.color;
                //Console.WriteLine("i forgot what i said");
            }
        }

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = characterRect.GetGlobalBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }

        //#region InputManager_targetCheck
        //public void targetCheck(int x, int y) {
        //    if (this.Contains(x, y)) this.setTargets(this.who);
        //}
        //#endregion

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(characterRect);

        }

    }
}
