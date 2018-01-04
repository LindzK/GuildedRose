using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GuildedRoseLib;
using GuildedRoseLib.Items;
using System;
using GuildedRoseLib.Interfaces;

namespace UnitTestProject1
{
    
    [TestClass]
    public class StockTest
    {
      
        private class StockComparer : Comparer<IStockable>
        {
            public override int Compare(IStockable x, IStockable y)
            {
                int retval = 0;

                if (x.Name != y.Name) retval = 1;
                if (x.Quality != y.Quality) retval = 1;
                if (x.Sellin != y.Sellin) retval = 1;

                return retval;
            }
        }

       
        [TestMethod]
        public void RetrieveUpdatedStockAfter1Day()
        {
            // Arrange
            var currentStock = new List<IStockable>();
            currentStock.Add(ItemFactory.getStock("Aged Brie",1,1));
            currentStock.Add(ItemFactory.getStock("Backstage Passes",-1,2));
            currentStock.Add(ItemFactory.getStock("Backstage Passes",9,2));
            currentStock.Add(ItemFactory.getStock("Sulfuras",2,2));
            currentStock.Add(ItemFactory.getStock("Normal Item", -1, 55));
            currentStock.Add(ItemFactory.getStock("Normal Item",2,2));
            currentStock.Add(ItemFactory.getStock("INVALID ITEM", 2, 2));
            currentStock.Add(ItemFactory.getStock("Conjured",2, 2));
            currentStock.Add(ItemFactory.getStock("Conjured", -1,5));

            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new AgedBrie { Name = "Aged Brie", Sellin = 0, Quality = 2 });
            expectedStockAfter1Day.Add(new BackStagePass { Name = "Backstage Passes", Sellin = -2, Quality = 0 });
            expectedStockAfter1Day.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 8, Quality = 4 });
            expectedStockAfter1Day.Add(new LegendaryItem { Name = "Sulfuras", Sellin = 2, Quality = 2 });
            expectedStockAfter1Day.Add(new StandardItem { Name = "Normal Item", Sellin = -2, Quality = 50 });
            expectedStockAfter1Day.Add(new StandardItem { Name = "Normal Item", Sellin = 1, Quality = 1 });
            expectedStockAfter1Day.Add(new NoSuchItem { Name = "NO SUCH ITEM" });
            expectedStockAfter1Day.Add(new ConjuredItem { Name = "Conjured", Sellin = 1, Quality = 0 });
            expectedStockAfter1Day.Add(new ConjuredItem { Name = "Conjured", Sellin = -2, Quality = 1 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            

            // Act
            ItemFactory.timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void standardItemSellinAndQualityDecreaseOnDayPassed()
        {
            // Arrange
            var currentStock = new List<IStockable>();
                       
            currentStock.Add(ItemFactory.getStock("Normal Item", 2, 2));
            
            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new StandardItem { Name = "Normal Item", Sellin = 1, Quality = 1 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            ItemFactory.timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void standardItemSellinAndQualityDecreaseOnDayPassedAndSellByPassed()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new StandardItem { Name = "Normal Item", Sellin = 1, Quality = 2, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new StandardItem { Name = "Normal Item", Sellin = 0, Quality = 0 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void itemWithNegativeQualityIsSetToZero()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new StandardItem { Name = "Normal Item", Sellin = 1, Quality = -1, Timer = timer });

            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new StandardItem { Name = "Normal Item", Sellin = 0, Quality = 0 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void agedBrieIncreasesInQualityWithAge()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new AgedBrie { Name = "Aged Brie", Sellin = 2, Quality = 2, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new AgedBrie { Name = "Aged Brie", Sellin = 1, Quality = 3 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void backStagePassIncreasesInQualityWithAge()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new BackStagePass{ Name = "Backstage Passes", Sellin = 15, Quality = 2, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 14, Quality = 3 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void qualityOfAgedBrieNeverMoreThan50WhenSetFirstTime()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new AgedBrie { Name = "Aged Brie", Sellin = 2, Quality = 51, Timer = timer });

            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new AgedBrie { Name = "Aged Brie", Sellin = 1, Quality = 50 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());

        }

        [TestMethod]
        public void qualityOfAgedBrieNeverMoreThan50()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new AgedBrie { Name = "Aged Brie", Sellin = 2, Quality = 50, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new AgedBrie { Name = "Aged Brie", Sellin = 1, Quality = 50 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void qualityOfSulfurasNeverMoreThan50()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new LegendaryItem { Name = "Sulfuras", Sellin = 2, Quality = 50, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new LegendaryItem { Name = "Sulfuras", Sellin = 2, Quality = 50 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void qualityOfBackStagePassNeverMoreThan50()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 2, Quality = 50, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 1, Quality = 50 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
         public void qualityOfStandardItemNeverMoreThan50WhenSetFirstTime()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new StandardItem { Name = "Normal Item", Sellin = 2, Quality = 51, Timer = timer });

            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new StandardItem { Name = "Normal Item", Sellin = 1, Quality = 50 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());

        }

        [TestMethod]
        public void sulfurasNeverDecreasesInQuality()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new LegendaryItem { Name = "Sulfuras", Sellin = 2, Quality = 50, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new LegendaryItem { Name = "Sulfuras", Sellin = 2, Quality = 50 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void backStagePassesIncreaseBy2With10DaysOrLess()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 11, Quality = 47, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 10, Quality = 49 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void backStagePassesIncreaseBy3With5DaysOrLess()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 6, Quality = 40, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 5, Quality = 43 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void backStagePassesDropToZeroAfterConcert()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 1, Quality = 50, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new BackStagePass { Name = "Backstage Passes", Sellin = 0, Quality = 0 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void conjuredItemsdegradeInQualityTwiceAsFastAsNormalItems()
        {
            // Arrange
            var timer = new MeasureTime();

            var currentStock = new List<IStockable>();
            currentStock.Add(new ConjuredItem { Name = "Conjured", Sellin = 5, Quality = 50, Timer = timer });


            var expectedStockAfter1Day = new List<IStockable>();
            expectedStockAfter1Day.Add(new ConjuredItem { Name = "Conjured", Sellin = 4, Quality = 48 });

            var stockManager = new ManageStock();
            stockManager.AddStock(currentStock);

            // Act
            timer.advanceDay();
            var finalStock = stockManager.GetStock();

            // Assert
            CollectionAssert.AreEqual(finalStock, expectedStockAfter1Day, new StockComparer());
        }

        [TestMethod]
        public void ItemFactoryReturnsCorrectType()
        {
            Assert.IsTrue(ItemFactory.getStock("Conjured", 1, 1) is ConjuredItem);
            Assert.IsTrue(ItemFactory.getStock("Aged Brie", 1, 1) is AgedBrie);
            Assert.IsTrue(ItemFactory.getStock("Backstage passes", 1, 1) is BackStagePass);
            Assert.IsTrue(ItemFactory.getStock("INVALID ITEM", 1, 1) is NoSuchItem);
            Assert.IsTrue(ItemFactory.getStock("Normal Item", 1, 1) is StandardItem);
            Assert.IsTrue(ItemFactory.getStock("Sulfuras", 1, 1) is LegendaryItem);

        }

        
    }
}
