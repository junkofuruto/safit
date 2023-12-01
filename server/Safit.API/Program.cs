using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Safit.Core.Services.Auth;
using Safit.Core.Services.Auth.Token;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBearerTokenDispatcherService, BearerTokenDispatcherService>();
builder.Services.AddScoped<IBearerTokenGeneratorService, BearerTokenGeneratorService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Safit:Bearer:ValidIssuer"],
        ValidAudience = builder.Configuration["Safit:Bearer:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Safit:Bearer:IssuerSigningKey"]!))
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
