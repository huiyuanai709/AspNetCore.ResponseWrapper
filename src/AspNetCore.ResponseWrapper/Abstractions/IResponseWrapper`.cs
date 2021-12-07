namespace AspNetCore.ResponseWrapper.Abstractions;

public interface IResponseWrapper<in TResponse> : IResponseWrapper
{
    IResponseWrapper<TResponse> Ok(TResponse response);
}