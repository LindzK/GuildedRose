using GuildedRoseLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib.Items
{
    public class NoSuchItem : IStockable
    {
        protected int? _sellin = null;
        public int? Sellin
        {
            get { return _sellin; }
            set { _sellin = null; }
        }
        private int? _quality = null;
        public int? Quality
        {
            get { return _quality; }
            set
            {
                _quality = null;

            }
        }
        private string _name = "NO SUCH ITEM";
        public string Name
        {
            get { return _name; }
            set { _name = "NO SUCH ITEM"; }
        }


        public ITimeMeasureable Timer
        {
            set { }
        }


        public virtual void onDayHasPassed(object sender, EventArgs e)
        {
            return;
        }
        public virtual void increaseQuality(int num)
        {
            return;
        }
        public virtual void decreaseQuality(int num)
        {
            return;
        }

        public string writeStock()
        {
            return this.Name;
        }

    }
}

