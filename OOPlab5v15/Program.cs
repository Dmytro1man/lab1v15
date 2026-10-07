using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8V15
{
    // Базовий клас для всіх фінансових інструментів
    public class FinancialInstrument
    {
        public string Name { get; set; }

        public FinancialInstrument(string name)
        {
            Name = name;
        }

        // Віртуальний метод для визначення поточної вартості
        public virtual decimal GetCurrentValue()
        {
            return 0m;
        }
    }

    // Похідний клас: Акція
    public class Stock : FinancialInstrument
    {
        public string TickerSymbol { get; set; }
        public decimal SharePrice { get; set; }
        public int NumShares { get; set; }

        public Stock(string name, string tickerSymbol, decimal sharePrice, int numShares = 1) 
            : base(name)
        {
            TickerSymbol = tickerSymbol;
            SharePrice = sharePrice;
            NumShares = numShares;
        }

        // Перевизначення віртуального методу
        public override decimal GetCurrentValue()
        {
            return SharePrice * NumShares;
        }
    }

    // Похідний клас: Облігація
    public class Bond : FinancialInstrument
    {
        public decimal FaceValue { get; set; }
        public decimal InterestRate { get; set; } // Вiдсоткова ставка (%)

        public Bond(string name, decimal faceValue, decimal interestRate) 
            : base(name)
        {
            FaceValue = faceValue;
            InterestRate = interestRate;
        }

        // Перевизначення віртуального методу (номінал + відсоток)
        public override decimal GetCurrentValue()
        {
            return FaceValue + (FaceValue * (InterestRate / 100m));
        }
    }

    // Похідний клас: Інвестиційний / Пайовий фонд
    public class MutualFund : FinancialInstrument
    {
        public decimal NAV { get; set; } // Net Asset Value (Чиста вартість активів за пай)
        public decimal NumShares { get; set; } // Кількість паїв

        public MutualFund(string name, decimal nav, decimal numShares) 
            : base(name)
        {
            NAV = nav;
            NumShares = numShares;
        }

        
        public override decimal GetCurrentValue()
        {
            return NAV * NumShares;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=================================================================");
            Console.WriteLine("    Лабораторна робота №5 | Варіант 15: Фінансові інструменти");
            Console.WriteLine("=================================================================\n");

            
            List<FinancialInstrument> portfolio = new List<FinancialInstrument>
            {
                new Stock("Акції Apple Inc.", "AAPL", 225.50m, 10),
                new Stock("Акції Microsoft Corp.", "MSFT", 415.20m, 5),
                new Bond("Державна облігація (ОВДП)", 10000.00m, 15.5m),
                new Bond("Корпоративна облігація TechCorp", 5000.00m, 8.0m),
                new MutualFund("Фонд Vanguard 500 Index", 480.30m, 12.5m),
                new MutualFund("Фонд Fidelity Growth", 150.75m, 20.0m)
            };

            Console.WriteLine("Перелік об'єктів та результати поліморфного виклику GetCurrentValue():");
            Console.WriteLine(new string('-', 65));

           
            foreach (var instrument in portfolio)
            {
               
                decimal value = instrument.GetCurrentValue();
                Console.WriteLine($"• {instrument.Name,-35} | Вартість: {value,12:C2}");
            }

            
            decimal totalValue = portfolio.Sum(item => item.GetCurrentValue());
            decimal maxValue = portfolio.Max(item => item.GetCurrentValue());
            var topInstrument = portfolio.First(item => item.GetCurrentValue() == maxValue);

            Console.WriteLine(new string('=', 65));
            Console.WriteLine("ПІДСУМКОВА АГРЕГАЦІЯ ПОРТФЕЛЯ:");
            Console.WriteLine($"• Кількість фінансових інструментів : {portfolio.Count}");
            Console.WriteLine($"• ЗАГАЛЬНА ВАРТІСТЬ ПОРТФЕЛЯ        : {totalValue:C2}");
            Console.WriteLine($"• Найцінніший інструмент            : {topInstrument.Name} ({maxValue:C2})");
            Console.WriteLine($"• Середня вартість інструменту      : {(totalValue / portfolio.Count):C2}");
            Console.WriteLine(new string('=', 65));
        }
    }
}