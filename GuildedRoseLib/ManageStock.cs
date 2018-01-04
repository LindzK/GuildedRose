using GuildedRoseLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseLib
{
    public class ManageStock : IManageable
    {

        private List<IStockable> _currentStock = new List<IStockable>();

        /// <summary>
        /// Adds a list of IStockable items to the stock list
        /// If stock already exists, it will ammend to what is already there
        /// </summary>
        /// <param name="stock">stock item, consisting of Sellin, Quality and name</param>
        /// <returns>Quantity of items currently stored within the stock</returns>
        public int AddStock(List<IStockable> stock)
        {
            _currentStock.AddRange(stock);
            return _currentStock.Count;
        }
        public int AddStock(IStockable stock)
        {
            _currentStock.Add(stock);
            return _currentStock.Count;
        }

        /// <summary>
        /// Returns the list of current stock, reporting current Sellin and Quality values for all
        /// </summary>
        /// <returns></returns>
        public List<IStockable> GetStock()
        {
            return _currentStock;
        }

        
    }
}
