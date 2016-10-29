// my intension is to follow immutable object pattern which doesn't modified the original object and always return a new object
namespace Clarkson.Task
{
    
    using System.Collections.Generic;
    using static System.Console;

    public class DispenseMoney
    {
        public Dictionary<int, int> Notes { get; private set; }
        public Dictionary<int, int> Coins { get; private set; }
        
        DispenseMoney()
        {
            this.Notes = new Dictionary<int, int>();
            this.Coins = new Dictionary<int, int>();
        }

        DispenseMoney(Dictionary<int, int> notes , Dictionary<int, int> coins)
        {
            this.Notes = notes;
            this.Coins = coins;
        }
        
        public void Display()
        {
            WriteLine("\n\n.................................");
                      
            foreach (var note in this.Notes)
            {           
                WriteLine($"£{note.Key}*{note.Value}");
            }

            foreach (var coin in this.Coins)
            {           
                WriteLine($"£{coin.Key / 100.00} *{coin.Value}");
            }

            WriteLine(".................................");
            WriteLine($"amount dispense : {HelperMethods.GetBalance(this.Notes, this.Coins)}");         
        }

        public static DispenseMoney CreateDispenseMoneyObject() => new DispenseMoney();
        public static DispenseMoney CreateDispenseMoneyObject(Dictionary<int, int> notes, Dictionary<int, int> coins) => new DispenseMoney(notes, coins);
    }
}
