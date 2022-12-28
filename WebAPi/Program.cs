using Data;
using Data.Repositories;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using Logic.Interfaces;
using Logic.Services;
using AutoMapper;
using Logic.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Logic.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostreSQL"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddScoped<IRepositoryWrapper, PostgreRepositoryWrapper>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IImageConverter, ImageConverter>();
builder.Services.AddScoped<IHttpService, HttpService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
        }
    );
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new[] { string.Empty }
            }
        }
    );
});


builder.Services.AddAuthentication(options =>
    {
       options.DefaultAuthenticateScheme = "JwtBearer";
       options.DefaultChallengeScheme = "JwtBearer";
    })
    .AddJwtBearer("JwtBearer",jwtbeareroptions =>
    {
        jwtbeareroptions.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = JwtAuthOptions.GetKey(),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            
           
        };
    });

var app = builder.Build();

using (var servicescope = app.Services.CreateScope())
{
    var serviceprovider = servicescope.ServiceProvider;
    try
    {
        var context = serviceprovider.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
        var repository = serviceprovider.GetRequiredService<IRepositoryWrapper>();
        DataSeed.SeedData(repository);
    }
    catch (Exception e)
    {
        app.Logger.LogError(e.Message, "Db initializing error");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
