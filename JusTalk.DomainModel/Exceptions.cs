using System;
using System.Runtime.Serialization;

namespace JusTalk.DomainModel
{
    public class EntityAccessException : ApplicationException
    {
        public EntityAccessException() { }

        public EntityAccessException(string message) : base(message) { }

        public EntityAccessException(string message, Exception inner) : base(message, inner) { }

        protected EntityAccessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}