using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib.Interfaces
{
    public interface ITimeMeasureable
    {
        event EventHandler dayHasPassed;

        void OnDayPassed(EventArgs e);

        void advanceDay();
    }
}
