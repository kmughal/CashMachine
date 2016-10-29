// this will help in unit testing to understand the intend of the error
namespace Clarkson.Task
{
    using System;
    public class NoCoinsException : Exception
    {
        public NoCoinsException() : base()
        {

        }

        public NoCoinsException(string message) : base(message)
        {

        }
    }
}
