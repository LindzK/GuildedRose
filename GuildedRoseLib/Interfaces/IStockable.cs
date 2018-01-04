using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib.Interfaces
{
    public delegate void dayPassedEventHandler();

    public interface IStockable
    {
        int? Sellin { get; set; }
        int? Quality { get; set; }
        string Name { get; set; }

        void onDayHasPassed(object sender, EventArgs e);

        ITimeMeasureable Timer { set; }

        void increaseQuality(int num);
        void decreaseQuality(int num);

        string writeStock();
     
    }
}
