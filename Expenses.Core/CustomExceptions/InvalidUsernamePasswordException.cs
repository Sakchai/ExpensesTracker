using System;
using System.Runtime.Serialization;

namespace Expenses.Core.CustomExceptions
{
    public class InvalidUsernamePasswordException : Exception
    {
        public InvalidUsernamePasswordException()
        {
        }

        public InvalidUsernamePasswordException(string message) : base(message)
        {
        }

        public InvalidUsernamePasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidUsernamePasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
