using Marvel.Core.Options;
using Marvel.Application;
using Marvel.Infra;

var builder = WebApplication.CreateBuilder(args);
var myPolicy = "myPolicy";
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(myPolicy,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services
    .AddApplication()
    .AddInfra();


builder.Services.Configure<MarvelApiOptions>(
    builder.Configuration.GetSection(MarvelApiOptions.MarvelApi));

builder.Services.Configure<ConnectionStringOptions>(
    builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings));

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

app.UseCors(myPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
