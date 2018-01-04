using GuildedRoseLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib
{
    public abstract class Stock : IStockable
    {
        protected int? _sellin;
        public int? Sellin
        {
           get { return _sellin; }
           set { _sellin = value; }
        }
        private int? _quality;
        public int? Quality
        {
            get
            {
                //NOTE : I dont like this, one rule states that quality can not be more than 50
                // yet when the test item goes in at 55, it expects 50 after one day ( I believe it should be 49 )
                // I had originally set the quality to add 50 if a higher number was input, but upon viewing the test results
                // I have had to maintain the actual passed in quality, but return a lower or higher number if that is the case
                // This does not feel logical to me.
                var retval = _quality;
                if (_quality <= 50 && _quality >= 0)
                {
                    retval = _quality;
                }
                else
                {
                    retval = (_quality <= 0) ? 0 : 50;
                }
                return retval;
            }
            set
            {
                _quality = value;
                
            }
        }
        public string Name
        {
            get; set;
        }


        public ITimeMeasureable Timer
        {
            set { value.dayHasPassed += onDayHasPassed; }
        }

       
        public virtual void onDayHasPassed(object sender, EventArgs e)
        {
            _sellin--;
            if (_sellin <= 0)
            {
                decreaseQuality(2);
            }
            else
            {
                decreaseQuality(1);
            }
        }

        public virtual void increaseQuality(int num)
        {
            _quality = _quality + num;
        }
        public virtual void decreaseQuality(int num)
        {
            _quality = _quality - num;
        }

        public string writeStock()
        {
            return this.Name + " " + ((this.Sellin != null) ? this.Sellin.ToString() : "") + " " + ((this.Quality != null) ? this.Quality.ToString() : "");
        }

    }
}
