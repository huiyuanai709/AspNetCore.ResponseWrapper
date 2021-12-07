using AspNetCore.ResponseWrapper.Abstractions;

namespace CustomResponseWrapper.ResponseWrapper;

public class CustomResponseWrapper : IResponseWrapper
{
    public bool Success => Code == 0;

    public int Code { get; set; }

    public string? Message { get; set; }

    public CustomResponseWrapper()
    {
    }

    public CustomResponseWrapper(int code, string? message)
    {
        Code = code;
        Message = message;
    }

    public IResponseWrapper Ok()
    {
        return new CustomResponseWrapper(0, null);
    }

    public IResponseWrapper BusinessError(string message)
    {
        return new CustomResponseWrapper(1, message);
    }

    public IResponseWrapper ClientError(string message)
    {
        return new CustomResponseWrapper(400, message);
    }
}