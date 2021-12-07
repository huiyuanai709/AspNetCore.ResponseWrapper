namespace AspNetCore.ResponseWrapper.Abstractions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DisableWrapperAttribute : Attribute, IDisableWrapperMetadata
{
}