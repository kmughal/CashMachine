// this will help in unit testing to understand the intend of the error
namespace Clarkson.Task
{
    using System;

    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() : base()
        {

        }

        public InvalidAmountException(string message) : base(message)
        {

        }
    }
}
