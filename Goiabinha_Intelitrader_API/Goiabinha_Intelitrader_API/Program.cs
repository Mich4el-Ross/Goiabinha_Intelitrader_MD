global using Goiabinha_Intelitrader_API.Data;
global using Goiabinha_Intelitrader_API.Services;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var SERVER       = Environment.GetEnvironmentVariable("DB_SERVER"); 
var PORT         = Environment.GetEnvironmentVariable("DB_PORT"); 
var SA_PASSWORD  = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); 
var NAME         = Environment.GetEnvironmentVariable("DB_NAME"); 

Console.WriteLine($"SERVER   ---:> {SERVER}");          // goiabinha-api-database
Console.WriteLine($"PORT     ---:> {PORT}");            // 1433
Console.WriteLine($"PASSWORD ---:> {SA_PASSWORD}");     // Pa55w0rd#2022
Console.WriteLine($"NAME     ---:> {NAME}");            // UsersAPI

var connectionString = $"Server={SERVER},{PORT};Initial Catalog={NAME};User ID=SA;Password={SA_PASSWORD}";

builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseSqlServer(connectionString);
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

DatabaseManagementService.MigrationInitialisation(app);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();