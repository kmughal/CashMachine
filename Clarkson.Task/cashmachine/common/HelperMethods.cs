namespace Clarkson.Task
{
    using System;
    using System.Collections.Generic;
    
    public static class HelperMethods
    {
        public static Tuple<int, int> GetWholeAndFractionalPart(this decimal amount)
        {
            var parts = amount.ToString().Split('.');
            var wholePart = int.Parse(parts[0]);
            var fractionalPart = (parts.Length == 1) ? 0 : int.Parse(parts[1]);

            return Tuple.Create<int, int>(wholePart, fractionalPart);
        }

        public static decimal GetBalance(Dictionary<int, int> notes, Dictionary<int, int> coins)
        {
            var balance = default(decimal);

            Func<Dictionary<int,int>, Func<KeyValuePair<int, int>,decimal>,decimal> getSum = (kvp, func) => {
                var returnValue = default(decimal);
                foreach (var item in kvp) {
                    returnValue += func(item);
                }
                return returnValue;
            };

            balance += getSum(notes, (note) => { return note.Key * note.Value; });
            balance += getSum(coins, (coin) => { return (coin.Key * coin.Value) / 100.00m; });
            return balance;
        }
    }
}
