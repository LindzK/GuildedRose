using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib.Items
{
    public class ConjuredItem : Stock
    {
        public override void onDayHasPassed(object sender, EventArgs e)
        {
            Sellin = Sellin - 1;
            if (Sellin <= 0)
            {
                decreaseQuality(4);
            }
            else
            {
                decreaseQuality(2);
            }
        }
    }
}
