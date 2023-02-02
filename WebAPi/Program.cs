using Data;
using Data.Extensions;
using Data.IRepositories;
using Logic.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPi.Middleware;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "../ProjectResources"
});

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.WriteIndented = true;
//    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appsettings = builder.Configuration;

builder.Services.AddAppDbContext(appsettings);
builder.Services.AddRepositories();
builder.Services.AddServices();

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
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwtbeareroptions =>
    {
        jwtbeareroptions.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtAuthKey").Value)),
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

app.UseStaticFiles();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<UserCheckMiddleware>();

app.MapControllers();

app.Run();