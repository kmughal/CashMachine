// this will help in unit testing to understand the intend of the error
namespace Clarkson.Task
{
    using System;

    public class NotEnoughtCashException : Exception
    {
        public NotEnoughtCashException() : base()
        {

        }

        public NotEnoughtCashException(string message) : base(message)
        {

        }

    }
}
