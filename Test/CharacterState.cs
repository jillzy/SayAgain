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
    class CharacterState: Drawable{
        //fields
        private string who;
        //private double mood;
        //private double volatility;
        //private double goal;
        double talkedTo = 0;
        static bool[] targets = { false,false,false }; // 0 = alex 1 = mom 2 = dad
        static Relationships ship = new Relationships();
        Color color;

        RectangleShape characterRect = new RectangleShape(new Vector2f(75, 75));
        CircleShape targeted = new CircleShape(15);

        //fields made for drawing character box, temp
        int x = 0;
        int y = 10;

        //methods
        public CharacterState(string who) {
            this.who = who;
            if (who == "alex")
            {
                this.x = 10;
               
                this.color = Color.Red;
            }
            else if (who == "mom")
            {
                this.x = 110;

                this.color = Color.Black;
            }
            else if (who == "dad") {
                this.x = 210;

                this.color = Color.Green;
            }

            characterRect.Position = new Vector2f(x, y);
            characterRect.FillColor = this.color;

            targeted.Position = new Vector2f(x + characterRect.GetGlobalBounds().Width/2 - targeted.GetGlobalBounds().Width/2, y + characterRect.GetGlobalBounds().Height/2 - targeted.GetGlobalBounds().Width / 2);
            targeted.FillColor = Color.White;

            //this.mood = mood;
            //this.volatility = volatility;
        }

        public void setTargets(string who) {
            //targets[0] = alex; targets[1] = mom; targets[2] = dad;
            if (who == "alex") {
                targets[0] = !targets[0];
            }

            else if (who == "mom")
            {
                targets[1] = !targets[1];
            }

            else if (who == "dad")
            {
                targets[2] = !targets[2];
            }

        }

        public bool[] getTargets() {
            return targets;
        }

        public void SetTalked(char f, double amount) {
            if (f == 'i') {
                talkedTo += amount;
            } else if (f == 'd') {
                talkedTo -= amount;
            }
        }

        public bool CheckTalked() {
            if (talkedTo == 10000000.0) {
                return true;
            }
            return false;
        }

        public void DecreaseMood() {

        }

        public double getDadFNC() {
            return ship.getDadFNC();
        }
        public double getAlexFNC() {
            return ship.getAlexFNC();
        }
        public double getMomFNC() {
            return ship.getMomFNC();
        }

        ///lol wat, turnary ?
        public void Draw(RenderTarget target, RenderStates states) {
            target.Draw(characterRect);
            if (targets[(who == "alex" ? 0 : who == "mom" ? 1 : 2)]) target.Draw(targeted);
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

        #region InputManager_targetCheck
        public void targetCheck(int x, int y) {
            if (this.Contains(x, y)) this.setTargets(this.who);
        }
        #endregion


    }
}
