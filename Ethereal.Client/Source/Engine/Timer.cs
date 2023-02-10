using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ethereal.Client.Source.Engine
{
    public delegate void Handler(object sender, EventArgs e);
    public class Timer
    {
        public bool IsReady;
        protected int mSec;
        protected TimeSpan timer = new TimeSpan();
        public double Interval;

        public Timer(int m)
        {
            IsReady = false;
            mSec = m;
        }
        public Timer(int m, bool STARTLOADED)
        {
            IsReady = STARTLOADED;
            mSec = m;
        }
        public Timer()
        {
        }

        public event Handler Tick;
        public int MSec
        {
            get { return mSec; }
            set { mSec = value; }
        }
        public int GetTimer
        {
            get { return (int)timer.TotalMilliseconds; }
        }

        public void UpdateTimer()
        {
            timer += Globals.GameTime.ElapsedGameTime;
        }

        public void UpdateTimer(float SPEED)
        {
            timer += TimeSpan.FromTicks((long)(Globals.GameTime.ElapsedGameTime.Ticks * SPEED));
        }

        public virtual void AddToTimer(int MSEC)
        {
            timer += TimeSpan.FromMilliseconds((long)(MSEC));
        }

        public bool Test()
        {
            if (timer.TotalMilliseconds >= mSec || IsReady)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            timer = timer.Subtract(new TimeSpan(0, 0, mSec / 60000, mSec / 1000, mSec % 1000));
            if (timer.TotalMilliseconds < 0)
            {
                timer = TimeSpan.Zero;
            }
            IsReady = false;
        }

        public void Reset(int NEWTIMER)
        {
            timer = TimeSpan.Zero;
            MSec = NEWTIMER;
            IsReady = false;
        }

        public void ResetToZero()
        {
            timer = TimeSpan.Zero;
            IsReady = false;
        }

        public virtual XElement ReturnXML()
        {
            XElement xml = new XElement("Timer",
                           new XElement("mSec", mSec),
                           new XElement("timer", GetTimer));



            return xml;
        }

        public void SetTimer(TimeSpan TIME)
        {
            timer = TIME;
        }

        public virtual void SetTimer(int MSEC)
        {
            timer = TimeSpan.FromMilliseconds((long)(MSEC));
        }
    }
}
