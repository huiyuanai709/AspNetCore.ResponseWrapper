using AspNetCore.ResponseWrapper.Abstractions;

namespace CustomResponseWrapper.ResponseWrapper;

public class CustomResponseWrapper<TResponse> : CustomResponseWrapper, IResponseWrapper<TResponse>
{
    public TResponse? Result { get; set; }

    public CustomResponseWrapper()
    {
    }
    
    public CustomResponseWrapper(int code, string? message, TResponse? result) : base(code, message)
    {
        Result = result;
    }

    public IResponseWrapper<TResponse> Ok(TResponse response)
    {
        return new CustomResponseWrapper<TResponse>(0, null, response);
    }
}