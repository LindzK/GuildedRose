using GuildedRoseLib;
using GuildedRoseLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildedRoseConsole
{
    public class Program
    {
        
        private static void EnterCmd()
        {
            Console.WriteLine("Enter stock : ");
        }

        private static void writeHelp()
        {
            Console.WriteLine("H : Help");
            Console.WriteLine("C : Close");
            Console.WriteLine("A : Advanced Day");
            Console.WriteLine("L : List current stock");
            Console.WriteLine("To enter a stock item the format is [name of stock] [sellin] [quality]");
            Console.WriteLine("Press return after each stock item is added.");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var stockManager = new ManageStock();

            Console.WriteLine("Welcome to the Guilded Rose Stock Managmenet Program..  ");
            writeHelp();
            bool cont = true;
            while (cont)
            {
                string line = Console.ReadLine();
                switch(line.ToLower())
                {
                    case "h":
                        writeHelp();
                        break;
                    case "c":
                        cont = false;
                        break;
                    case "a":
                        ItemFactory.timer.advanceDay();
                        Console.WriteLine("day advanced");
                        EnterCmd();
                        break;
                    case "l":
                        var stock = stockManager.GetStock();
                        Console.WriteLine("Stock found : " + stock.Count);
                        foreach (var item in stock)
                        {
                            Console.WriteLine(item.writeStock());
                        }
                        break;
                    default:
                        var list = line.Split(' ');
                        if (list.Length <= 0)
                        {
                            Console.WriteLine("Invalid stock structure - please re-enter");
                        }
                        else
                        {
                            string name = "";
                            int sellin;
                            int qual;
                            try
                            {

                                if (list.Length > 3)
                                {
                                    // assume our title has a space or two in.
                                    qual = int.Parse(list[list.Length - 1]);
                                    sellin = int.Parse(list[list.Length - 2]);
                                    var sb = new StringBuilder();
                                    for(var i = 0; i < list.Length-2; i++)
                                    {
                                        sb.AppendFormat("{0} ", list[i]);
                                    }
                                    name = sb.ToString().TrimEnd();
                                }
                                else
                                {
                                    qual = int.Parse(list[2]);
                                    sellin = int.Parse(list[1]);
                                    name = list[0].TrimEnd();
                                }
                              
                                stockManager.AddStock(ItemFactory.getStock(name, sellin, qual));
                                
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Invalid stock structure - please re-enter");
                            }
                        }
                        break;
                }
                //if (cont) EnterCmd();
            };
        }
    }
}