using System;
using System.Runtime.Serialization;

namespace Michaelsoft.ContentManager.Server.Exceptions
{
    public class MediaNotFoundException : Exception
    {

        public MediaNotFoundException()
        {
        }

        protected MediaNotFoundException(SerializationInfo info,
                                         StreamingContext context) : base(info, context)
        {
        }

        public MediaNotFoundException(string? message) : base(message)
        {
        }

        public MediaNotFoundException(string? message,
                                      Exception? innerException) : base(message, innerException)
        {
        }

    }
}