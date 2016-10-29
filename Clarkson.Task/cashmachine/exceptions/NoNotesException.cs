// this will help in unit testing to understand the intend of the error
namespace Clarkson.Task
{
    using System;

    public class NoNotesException : Exception
    {
        public NoNotesException() : base()
        {

        }

        public NoNotesException(string message) : base(message)
        {

        }
    }
}
