using System;
using System.Collections;
using System.Runtime.Serialization;

namespace FreeCourse.Web.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException()
        {
        }

        public UnAuthorizedException(string message) : base(message)
        {
        }

        public UnAuthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}