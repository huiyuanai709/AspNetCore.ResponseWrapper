namespace AspNetCore.ResponseWrapper.Abstractions;

public interface IResponseWrapper
{
    IResponseWrapper Ok();

    IResponseWrapper BusinessError(string message);

    IResponseWrapper ClientError(string message);
}