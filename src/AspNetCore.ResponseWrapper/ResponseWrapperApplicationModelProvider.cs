using System.Reflection;
using AspNetCore.ResponseWrapper.Abstractions;
using AspNetCore.ResponseWrapper.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace AspNetCore.ResponseWrapper;

public class ResponseWrapperApplicationModelProvider : IApplicationModelProvider
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IResponseWrapper _responseWrapper;
    private readonly Type _responseWrapperType;
    private readonly IResponseWrapper<object?> _genericResponseWrapper;
    private readonly Type _genericWrapperType;
    private readonly bool _suppressModelInvalidWrapper;
    private readonly bool _onlyAvailableInApiController;

    public ResponseWrapperApplicationModelProvider(IOptions<ResponseWrapperOptions> responseWrapperOptions,
        ILoggerFactory loggerFactory)
    {
        var options = responseWrapperOptions.Value;
        _loggerFactory = loggerFactory;
        _responseWrapper = options.ResponseWrapper;
        _responseWrapperType = options.ResponseWrapper.GetType();
        _genericResponseWrapper = options.GenericResponseWrapper;
        _genericWrapperType = options.GenericResponseWrapper.GetType().GetGenericTypeDefinition();
        _suppressModelInvalidWrapper = options.SuppressModelInvalidWrapper;
        _onlyAvailableInApiController = options.OnlyAvailableInApiController;
    }

    public int Order => -1000 + 20;

    public void OnProvidersExecuted(ApplicationModelProviderContext context)
    {
        // Intentionally empty.
    }

    public void OnProvidersExecuting(ApplicationModelProviderContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        foreach (var controllerModel in context.Result.Controllers)
        {
            if (_onlyAvailableInApiController && IsApiController(controllerModel))
            {
                continue;
            }
            
            if (controllerModel.Attributes.OfType<IDisableWrapperMetadata>().Any())
            {
                if (!_suppressModelInvalidWrapper)
                {
                    foreach (var actionModel in controllerModel.Actions)
                    {
                        actionModel.Filters.Add(new ModelInvalidWrapperFilter(_responseWrapper, _loggerFactory));
                    }
                }

                continue;
            }

            foreach (var actionModel in controllerModel.Actions)
            {
                if (!_suppressModelInvalidWrapper)
                {
                    actionModel.Filters.Add(new ModelInvalidWrapperFilter(_responseWrapper, _loggerFactory));
                }

                if (actionModel.Attributes.OfType<IDisableWrapperMetadata>().Any()) continue;
                actionModel.Filters.Add(new ResultWrapperFilter(_responseWrapper, _genericResponseWrapper));
                AddResponseWrapperFilter(actionModel);
            }
        }
    }

    private void AddResponseWrapperFilter(ActionModel actionModel)
    {
        const int statusCode = StatusCodes.Status200OK;
        var responseType = actionModel.ActionMethod.ReturnType;
        if (responseType.IsAssignableTo(typeof(IConvertToActionResult)))
        {
            AddIActionResultWrapperFilter(responseType);
            return;
        }

        if (responseType == typeof(void) || responseType == typeof(Task))
        {
            AddWrapperFilter();
            return;
        }

        if (responseType.BaseType == typeof(Task))
        {
            var genericArgument = responseType.GetGenericArguments()[0];
            if (genericArgument.IsAssignableTo(typeof(IConvertToActionResult)))
            {
                AddIActionResultWrapperFilter(genericArgument);
                return;
            }

            AddGenericWrapperFilter(genericArgument);
            return;
        }

        AddGenericWrapperFilter(responseType);

        void AddWrapperFilter()
        {
            actionModel.Filters.Add(new ProducesResponseTypeAttribute(_responseWrapperType, statusCode));
        }

        void AddGenericWrapperFilter(Type type)
        {
            actionModel.Filters.Add(
                new ProducesResponseTypeAttribute(_genericWrapperType.MakeGenericType(type), statusCode));
        }

        // Add wrapper filter for the type is assignable to IConvertToActionResult
        void AddIActionResultWrapperFilter(Type type)
        {
            if (type.GetGenericArguments().Any())
            {
                var genericType = type.GetGenericArguments()[0];
                AddGenericWrapperFilter(genericType);
                return;
            }

            AddWrapperFilter();
        }
    }
    
    private static bool IsApiController(ControllerModel controller)
    {
        if (controller.Attributes.OfType<IApiBehaviorMetadata>().Any())
        {
            return true;
        }

        var controllerAssembly = controller.ControllerType.Assembly;
        var assemblyAttributes = controllerAssembly.GetCustomAttributes();
        return assemblyAttributes.OfType<IApiBehaviorMetadata>().Any();
    }
}