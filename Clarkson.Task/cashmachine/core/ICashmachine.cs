namespace Clarkson.Task {
    public interface ICashmachine
    {
        DispenseMoney WithdrawCash(decimal amount);
    }
}