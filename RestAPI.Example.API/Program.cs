
using RestAPI.Example.Application;
using RestAPI.Example.Application.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("SQLDatabase")!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var dbInit = app.Services.GetRequiredService<DBInitilizer>();
await dbInit.Initilizer();

app.Run();
