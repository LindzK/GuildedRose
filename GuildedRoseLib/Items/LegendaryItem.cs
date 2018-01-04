using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib.Items
{
    public class LegendaryItem : Stock
    {
        public override void onDayHasPassed(object sender, EventArgs e)
        {
            // it does not have to be sold, or decrease in quality
            return;          
        }
    }
}
