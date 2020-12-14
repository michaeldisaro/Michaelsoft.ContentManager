using System;
using System.Runtime.Serialization;

namespace Michaelsoft.ContentManager.Server.Exceptions
{
    public class ContentNotFoundException : Exception
    {

        public ContentNotFoundException()
        {
        }

        protected ContentNotFoundException(SerializationInfo info,
                    StreamingContext context) : base(info, context)
        {
        }

        public ContentNotFoundException(string? message) : base(message)
        {
        }

        public ContentNotFoundException(string? message,
                 Exception? innerException) : base(message, innerException)
        {
        }

    }
}