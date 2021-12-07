namespace AspNetCore.ResponseWrapper;

public static class ResponseWrapperDefaults
{
    #region Code constants

    /// <summary>
    /// This indicate work normally
    /// </summary>
    public const int OkCode = 0;

    /// <summary>
    /// This indicate business error occured
    /// </summary>
    public const int BusinessErrorCode = 1;

    /// <summary>
    /// This indicate client bad request, model invalid mostly
    /// </summary>
    public const int ClientErrorCode = 400;

    /// <summary>
    /// The indicate server error occured, unhandled exception mostly
    /// </summary>
    public const int ServerErrorCode = 500;

    #endregion

}