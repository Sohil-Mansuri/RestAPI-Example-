
using RestAPI.Example.API.Mapping;
using RestAPI.Example.Application;
using RestAPI.Example.Application.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("SQLDatabase")!);

builder.Services.AddSingleton<ValidationMappingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ValidationMappingMiddleware>();

app.MapControllers();

var dbInit = app.Services.GetRequiredService<DBInitilizer>();
await dbInit.Initilizer();

app.Run();
