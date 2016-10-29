namespace Clarkson.Task
{
    using System.Collections.Generic;

    public class CoinSelector : ICoinSelecotr
    {
        public int GetCoin(int amount, Dictionary<int, int> availableCoins)
        {
            foreach (var kvp in availableCoins)
            {
                if (amount >= kvp.Key && kvp.Value > 0)
                    return kvp.Key;
            }
            throw ExceptionHelpers.ThrowNoCoinsException();            
        }
    }
}
