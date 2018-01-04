using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib.Items
{
    public class AgedBrie : Stock
    {
        public override void onDayHasPassed(object sender, EventArgs e)
        {
            
            Sellin = Sellin - 1;

            increaseQuality(1);
                        
        }
    }
}
