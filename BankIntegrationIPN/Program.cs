using Microsoft.EntityFrameworkCore;
using BankIntegrationIPN.Data;
using BankIntegrationIPN.Services;
using BankIntegrationIPN.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Db Conn
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

// services
builder.Services.AddScoped<IPaymentService, PaymentService>();


// Enable configuration for appsettings.json
builder.Services.Configure<PaymentSettings>(builder.Configuration.GetSection("PaymentSettings"));


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
