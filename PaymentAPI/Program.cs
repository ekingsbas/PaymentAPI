using Microsoft.AspNetCore.Authentication;
using PaymentAPI.Auth;
using PaymentAPI.BLL.Contracts;
using PaymentAPI.BLL.Services;
using PaymentAPI.DAL;
using PaymentAPI.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DI things

//Adding Repos
builder.Services.AddDbContext<PaymentContext>();
builder.Services.AddScoped<CreditCardRepository>(r => new CreditCardRepository(r.GetRequiredService<PaymentContext>()));
builder.Services.AddScoped<PaymentRepository>(r => new PaymentRepository(r.GetRequiredService<PaymentContext>()));

//Adding services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton(new  UFEService());
builder.Services.AddScoped<CardManagementService>(s => new CardManagementService(s.GetRequiredService<CreditCardRepository>(), s.GetRequiredService<PaymentRepository>(), s.GetRequiredService<UFEService>()));

//Swagger things
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Auth things
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
