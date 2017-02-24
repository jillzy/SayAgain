﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Test
{
	class GameTimer : Drawable
	{
        protected UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        protected UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        public GameTimer(string name, double howLong, Action T)
		{ //in seconds
            width = SCREEN_WIDTH / 10;
            height = SCREEN_HEIGHT / 15;
            x = SCREEN_WIDTH - 400;
            y = SCREEN_HEIGHT - 200;
            initTime = howLong-1; //0 till 9 = 10 seconds
			countDown = howLong;
            timerRead = new Text("SPEAK", adore64);
            timerRead.Position = new Vector2f((x + width/2) - timerRead.GetGlobalBounds().Width/2, (y + height/2) - timerRead.GetGlobalBounds().Height/2);
            timerRead.Color = Color.White;
            start = false;
            timerBG = new RectangleShape(new Vector2f(width, height));
            timerBG.Position = new Vector2f(x, y);
            timerLevel = new RectangleShape(new Vector2f(width, height));
            timerLevel.Position = new Vector2f(x, y);
            timerBG.FillColor = Color.Blue;
            timerLevel.FillColor = Color.Green;
            timerEvent = T;
        }

		public void updateTimer()
		{
			if (start == true)
			{

                //timer update
                if (countDown > 0)
				{
					//as long as you are not out of time
					newTimeSeconds = ((DateTime.Now.Ticks / 10000000) - timeDiff);
					countDown = (initTime - (newTimeSeconds - oldTimeSeconds));  

                }
				else if (countDown <= 0)
				{
					start = false;
				}
                //circle.Radius = 20 * (float)(countDown / initTime);
                timerLevel.Size = new Vector2f(width * (float)(countDown / initTime), height);
            }
		}

		public void stopTimer()
		{
			start = false;
		}


        public void restartTimer()
		{
            //timerFinished = false;
            //Console.WriteLine("oh baby");
            start = true;
			countDown = initTime + 1;
			timeDiff = 0;
			oldTimeSeconds = (DateTime.Now.Ticks / 10000000);
		}

        public void resetTimer()
        {
            countDown = initTime + 1;
        }

        public double getInitTime()
		{
			return initTime;
		}

		public void startTimer()
		{
            //Console.WriteLine("plz do not poop here");
			start = true;
            timeDiff = 0;
            oldTimeSeconds = (DateTime.Now.Ticks / 10000000);
        }

		public void PauseTimer()
		{
            if (start)
            {
                //Console.WriteLine("I AM HERE");
                pauseTime = newTimeSeconds;
                double a = pauseTime;
                double b = DateTime.Now.Ticks / 10000000;
                timeDiff = b - a;
            }
		}
        float height, width;
        float x, y;
		double oldTimeSeconds = 0;
		double pauseTime = 0;
		double newTimeSeconds = 0;
		double timeDiff = 0;
		double countDown = 1;
		double currentTime = 0;
		double initTime = 0; //needed to restart
		bool start = false;
        bool pause = false;
        RectangleShape timerBG;
        RectangleShape timerLevel;
		//bool timerFinished = false;
		Action timerEvent;
        Font adore64 = new Font("../../Fonts/Adore64.ttf");
        Text timerRead;


		public bool getStart()
		{
			return start;
		}

		public void doTask()
		{

			timerEvent();
		}

		public void setCurrentTime(double newTimeSeconds)
		{
			currentTime = newTimeSeconds;
		}

		public double getCurrentTime()
		{
			return currentTime;
		}

		public void setOldGameTime(double newTimeSeconds)
		{
			oldTimeSeconds = newTimeSeconds;
		}

		public double getOldGameTime()
		{
			return oldTimeSeconds;
		}

		public double getPauseTime()
		{
			return pauseTime;
		}

		public void setPauseTime(double newTime)
		{
			pauseTime = newTime;
		}

		public double getNewTime()
		{
			return newTimeSeconds;
		}

		public void setNewTime(double newTime)
		{
			newTimeSeconds = newTime;
		}

		public void setTimeDiff(double newTime)
		{
			timeDiff = newTime;
		}

		public double getTimeDiff()
		{
			return timeDiff;
		}

		public void setCountDown(double cd)
		{
			countDown = cd;
		}

		public double getCountDown()
		{
			return countDown;
		}

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = timerBG.GetGlobalBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            // Change radius to match time elapsed and draw it

            //target.Draw(circle);
            target.Draw(timerBG);
            target.Draw(timerLevel);
            target.Draw(timerRead);
        }
    }
}
