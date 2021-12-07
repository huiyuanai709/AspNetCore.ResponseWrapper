# AspNetCore.ResponseWrapper
AspNetCore.ResponseWrapper is a HTTP API response wrapper, It supports various Action return type, Model invalid wrapper, Swagger response display and also supports custom response wrapper.

## Features:

1. ModelInvalid wrapper by <code>ActionFilter</code>
2. ObjectResult/EmptyResult wrapper by <code>ActionFilter</code>
3. Swagger supported by <code>ProducesResponseTypeAttribute</code>
4. Custom response wrapper supported
5. Disable specified response wrapper by <code>[DisableWrapper]</code>

## Usage

1. Basic
```c#
builder.Services.AddControllers().AddResponseWrapper();
```

See samples...

## Installation
```shell
dotnet add Package AspNetCore.ResponseWrapper
```