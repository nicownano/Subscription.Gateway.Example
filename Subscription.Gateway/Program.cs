using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("redis:6379"))
    .AddGraphQLServer()
    .AddSubscriptionType<Subscriptions>()
    .AddQueryType<Query>()
    .AddRedisSubscriptions(c => c.GetRequiredService<IConnectionMultiplexer>())
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

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
app.UseWebSockets();
app.MapGraphQL();
app.Run();


public class Query
{
    public string Me() => "Nico";
}