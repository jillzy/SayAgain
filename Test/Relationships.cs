using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Relationships
    {
        public Relationships() {
            AlexFNC = -3.0;
            MomFNC = 0.0;
            DadFNC = 4.0;
        }

        double AlexFNC;
        double MomFNC;
        double DadFNC;

        public int getAlexFNC() {
            return (int)AlexFNC;
        }
        public double getMomFNC()
        {
            return MomFNC;
        }
        public double getDadFNC()
        {
            return DadFNC;
        }

        public void setAlexFNC(double value)
        {
            AlexFNC = value;
        }
        public void setDadFNC(double value)
        {
            DadFNC = value;
        }
        public void setMomFNC(double value)
        {
            MomFNC = value;
        }
    }
}
