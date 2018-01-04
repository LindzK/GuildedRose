using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib.Interfaces
{
    public interface IManageable
    {
       int AddStock(List<IStockable> stock);
        int AddStock(IStockable stock);

       List<IStockable> GetStock();

      
    }
}
