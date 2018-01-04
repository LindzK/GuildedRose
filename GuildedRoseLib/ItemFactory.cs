using GuildedRoseLib.Interfaces;
using GuildedRoseLib.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib
{
    public static class ItemFactory
    {
        private static MeasureTime _timer = new MeasureTime();
        public static MeasureTime timer { get { return _timer; } }

        public static IStockable getStock(string name, int sellin, int quality)
        {
            switch (name.ToLower())
            {
                case "aged brie":
                    return new AgedBrie { Name = name, Quality = quality, Sellin = sellin, Timer = timer };
                    break;
                case "backstage passes":
                    return new BackStagePass { Name = name, Quality = quality, Sellin = sellin, Timer = timer };
                    break;
                case "sulfuras":
                    return new LegendaryItem { Name = name, Quality = quality, Sellin = sellin, Timer = timer };
                    break;
                case "normal item":
                    return new StandardItem { Name = name, Quality = quality, Sellin = sellin, Timer = timer };
                    break;
                case "conjured":
                    return new ConjuredItem { Name = name, Quality = quality, Sellin = sellin, Timer = timer };
                    break;
                default:
                    return new NoSuchItem();
                    break;
            }
        }

        
    }
}
