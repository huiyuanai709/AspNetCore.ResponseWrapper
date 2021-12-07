using AspNetCore.ResponseWrapper.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.ResponseWrapper;

public class ResponseWrapperOptions
{
    /// <summary>
    /// A wrapper for no business data return, Set to change the default wrapper
    /// </summary>
    public IResponseWrapper ResponseWrapper { get; set; } = new ResponseWrapper();

    /// <summary>
    /// A wrapper for business data return, Set to change the default wrapper
    /// </summary>
    public IResponseWrapper<object?> GenericResponseWrapper { get; set; } = new ResponseWrapper<object?>();

    /// <summary>
    /// Gets or sets a value that determines if the filter that returns an <see cref="BadRequestObjectResult"/> when
    /// <see cref="ActionContext.ModelState"/> is invalid is suppressed.
    /// </summary>
    public bool SuppressModelInvalidWrapper { get; set; }

    /// <summary>
    /// Gets or sets a value that determines response wrapper only available in ApiController.
    /// </summary>
    public bool OnlyAvailableInApiController { get; set; }
}