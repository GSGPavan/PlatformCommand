using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncComm;
using PlatformService.Data;
using PlatformService.Server;
using PlatformService.SyncComm.Grpc;
using PlatformService.SyncComm.Http;
using RabbitMQ.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment env = builder.Environment;
IConfiguration configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if(env.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemory"));
}
else if(env.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
        configuration.GetConnectionString("PlatformSql")));
}

ConnectionFactory connectionFactory = new ConnectionFactory()
{
    HostName = configuration["RabbitMq:Host"]!,
    Port = int.Parse(configuration["RabbitMq:port"]!)
};

IConnection rabbitMqConnection = await connectionFactory.CreateConnectionAsync();
builder.Services.AddSingleton<IConnection>(serviceProvider =>
{
    return rabbitMqConnection;
});
builder.Services.AddTransient<IMessageBusClient, MessageBusClient>();
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddScoped<IPlatformServer, PlatformServer>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<IApiClient, ApiClient>();

var app = builder.Build();

PrepDb.InitializeLogger(app);
PrepDb.PrepPopulation(app, env.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<GrpcPlatformController>();
app.MapGet("/protos/platforms.proto", async context => await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto")));

app.MapControllers();

app.Run();
