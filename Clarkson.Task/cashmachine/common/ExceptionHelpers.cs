namespace Clarkson.Task
{ 
    public static class ExceptionHelpers
    {
        public static NoNotesException ThrowNoNotesException() => new NoNotesException();
        public static NoCoinsException ThrowNoCoinsException() => new NoCoinsException();
        public  static NotEnoughtCashException ThrowNotEnoughCashException() => new NotEnoughtCashException();
        public static InvalidAmountException ThrowInvalidAmountException() => new InvalidAmountException();
    }   
}
