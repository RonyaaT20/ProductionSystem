using System;
using System.Net;
using ProductionSystem.Application.Api;
using ProductionSystem.Application.Exceptions;

namespace ProductionSystem.Application.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException()
            : base(ApiResultStatusCode.Forbidden,null,HttpStatusCode.Forbidden)
        {
        }

        public ForbiddenException(string message)
            : base(ApiResultStatusCode.Forbidden, message, HttpStatusCode.Forbidden)
        {
        }

        public ForbiddenException(object additionalData)
            : base(ApiResultStatusCode.Forbidden, additionalData)
        {
        }

        public ForbiddenException(string message, object additionalData)
            : base(ApiResultStatusCode.Forbidden, message, additionalData)
        {
        }

        public ForbiddenException(string message, Exception exception)
            : base(ApiResultStatusCode.Forbidden, message, exception)
        {
        }

        public ForbiddenException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.Forbidden, message, exception, additionalData)
        {
        }
    }
}