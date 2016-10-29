namespace Clarkson.Task
{
    using System.Collections.Generic;

    public interface ICoinSelecotr
    {
        int GetCoin(int amount, Dictionary<int, int> availableCoins);
    }
}
