using OrderAPI.Core.DIs;
using OrderAPI.Infrastructure.DIs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCoreService(builder.Configuration); // added extension
builder.Services.AddInfrastructureService(builder.Configuration); // added extension

var app = builder.Build();

app.UserInfrastructureMiddleware(); // added extension

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
