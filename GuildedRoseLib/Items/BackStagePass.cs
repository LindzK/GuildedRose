using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib.Items
{
    public class BackStagePass : Stock
    {
        public override void onDayHasPassed(object sender, EventArgs e)
        {
            Sellin = Sellin - 1;
           
            if (Sellin < 1)
            {
                Quality = 0;
            }
            else if (Sellin <=5)
            {
                increaseQuality(3);
            }
            else if (Sellin <= 10)
            {
                increaseQuality(2);
            }
            else
            {
                increaseQuality(1);
            }
            
            
        }
    }
}
