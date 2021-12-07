using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.ResponseWrapper.Mvc.Abstractions;

/// <summary>
/// A filter that allows response wrap.
/// </summary>
public interface IResultWrapperFilter : IActionFilter
{
}