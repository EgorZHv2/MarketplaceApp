using Data;
using Data.Repositories;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddScoped<IRepositoryWrapper, PostgreRepositoryWrapper>();

var app = builder.Build();

using(var servicescope = app.Services.CreateScope())
{
    var serviceprovider = servicescope.ServiceProvider;
    try
    {
         var context = serviceprovider.GetRequiredService<ApplicationDbContext>();
         DbInitializer.Initialize(context);
    }
    catch(Exception e)
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

app.UseAuthorization();

app.MapControllers();

app.Run();
