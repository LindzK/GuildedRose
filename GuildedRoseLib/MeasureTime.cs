using GuildedRoseLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib
{
    public class MeasureTime : ITimeMeasureable
    {
        public event EventHandler dayHasPassed;

        public virtual void OnDayPassed(EventArgs e)
        {
            EventHandler dhp = dayHasPassed;
            if (dhp != null)
            {
                dhp(this, e);
            }
        }

        /// <summary>
        /// Time management of consumer of library would  instigate this event normally, 
        /// however for testing purposes, this method will instigate the dayHasPassed event.
        /// </summary>
        public void advanceDay()
        {
            OnDayPassed(EventArgs.Empty);
        }
    }
}
