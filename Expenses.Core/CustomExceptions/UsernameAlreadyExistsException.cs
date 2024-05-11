using System;
using System.Runtime.Serialization;

namespace Expenses.Core.CustomExceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException()
        {
        }

        public UsernameAlreadyExistsException(string message) : base(message)
        {
        }

        public UsernameAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsernameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
