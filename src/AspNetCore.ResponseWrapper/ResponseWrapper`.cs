using AspNetCore.ResponseWrapper.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.ResponseWrapper
{
    /// <summary>
    /// Default wrapper for <see cref="ObjectResult"/>
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class ResponseWrapper<TResponse> : ResponseWrapper, IResponseWrapper<TResponse>
    {
        public TResponse? Data { get; }

        public ResponseWrapper()
        {
        }

        private ResponseWrapper(int code, string? message, TResponse? data) : base(code, message)
        {
            Data = data;
        }

        public IResponseWrapper<TResponse> Ok(TResponse response)
        {
            return new ResponseWrapper<TResponse>(ResponseWrapperDefaults.OkCode, null, response);
        }
    }
}