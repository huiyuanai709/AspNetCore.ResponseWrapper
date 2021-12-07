using AspNetCore.ResponseWrapper;
using CustomResponseWrapper.ResponseWrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddResponseWrapper(options =>
{
    options.ResponseWrapper = new CustomResponseWrapper.ResponseWrapper.CustomResponseWrapper();
    options.GenericResponseWrapper = new CustomResponseWrapper<object?>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();