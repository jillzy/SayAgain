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

        CircleShape circle = new CircleShape(20);

        public GameTimer(string name, double howLong, Action T)
		{ //in seconds
			initTime = howLong-1; //0 till 9 = 10 seconds
			countDown = howLong;
			timerEvent = T;
            circle.Position = new Vector2f(SCREEN_WIDTH - 100, SCREEN_HEIGHT - 200);
            timerRead = new Text(Convert.ToString(countDown), adore64);
            timerRead.Position = new Vector2f(SCREEN_WIDTH - 100, SCREEN_HEIGHT - 200);
            timerRead.Color = Color.White;
            start = false;
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
                timerRead.DisplayedString = Convert.ToString(countDown);
                circle.Radius = 20 * (float)(countDown / initTime);
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

		double oldTimeSeconds = 0;
		double pauseTime = 0;
		double newTimeSeconds = 0;
		double timeDiff = 0;
		double countDown = 1;
		double currentTime = 0;
		double initTime = 0; //needed to restart
		bool start = false;
        bool pause = false;
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

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            // Change radius to match time elapsed and draw it
            
            //target.Draw(circle);
            target.Draw(timerRead);
        }
    }
}
