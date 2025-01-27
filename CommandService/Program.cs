using CommandService.AsyncComm;
using CommandService.Data;
using CommandService.Server;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemory"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPlatformServer, PlatformServer>();
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddScoped<ICommandServer, CommandServer>();
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

builder.Services.AddHostedService<MessageBusSubscriber>();

ConnectionFactory connectionFactory = new ConnectionFactory()
{
    HostName = configuration["RabbitMq:Host"]!,
    Port = int.Parse(configuration["RabbitMq:Port"]!)
};
IConnection rabbitMqConnection = await connectionFactory.CreateConnectionAsync();
builder.Services.AddSingleton<IConnection>(serviceProvider => rabbitMqConnection);

var app = builder.Build();

PrepDb.PrepPopulation(app);

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
