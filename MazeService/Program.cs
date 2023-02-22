using System.Diagnostics;
using MazeService.Data;
using MazeService.SyncDataServices;
using Microsoft.EntityFrameworkCore;
Debug.WriteLine("tutaj");
var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("tutaj");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMazeRepo, MazeRepo>();
Console.WriteLine("tutaj");
builder.Services.AddHttpClient<IBlobStorageClient, HttpBlobStorageDataClient>();
builder.Services.AddHttpClient<IMazeGenDataClient, HttpMazeGenDataClient>();
builder.Services.AddHttpClient<IMazeSolveDataClient, HttpMazeSolveDataClient>();


Console.WriteLine("USING IN MEMORY DATABASE");
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMem"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app);

app.Run();
