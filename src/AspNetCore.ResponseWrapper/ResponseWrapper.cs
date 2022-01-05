using AspNetCore.ResponseWrapper.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.ResponseWrapper
{
    /// <summary>
    /// Default wrapper for <see cref="EmptyResult"/> or error occured
    /// </summary>
    public class ResponseWrapper : IResponseWrapper
    {
        public int Code { get; }

        public string? Message { get; }

        public ResponseWrapper()
        {
        }

        protected ResponseWrapper(int code, string? message)
        {
            Code = code;
            Message = message;
        }

        public IResponseWrapper Ok()
        {
            return new ResponseWrapper(ResponseWrapperDefaults.OkCode, null);
        }

        public IResponseWrapper BusinessError(string message)
        {
            return new ResponseWrapper(ResponseWrapperDefaults.BusinessErrorCode, message);
        }

        public IResponseWrapper ClientError(string message)
        {
            return new ResponseWrapper(ResponseWrapperDefaults.ClientErrorCode, message);
        }
    }
}