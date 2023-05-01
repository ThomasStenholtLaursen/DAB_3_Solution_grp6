using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DAB_3_Solution_grp6.Api.Seed;
using DAB_3_Solution_grp6.MongoDb.DataAccess.MongoDbSettings;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;
using DAB_3_Solution_grp6.MSSQL.DataAccess;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Repositories.Canteen;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Repositories.Global;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Repositories.Reservation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("DAB3Database"));

builder.Services.AddSingleton<CanteenService>();

builder.Services.AddDbContext<CanteenAppDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Database"), sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("DAB_2_Solution_grp6.Api");
        });

    });

builder.Services.AddScoped<IGlobalRepository, GlobalRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seeding for MSSQL
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<CanteenAppDbContext>();

        await MssqlDataSeed.Seed(context);
    }

    // Seeding for MongoDB
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var canteenService = services.GetRequiredService<CanteenService>();

        var mongoDbDataSeed = new MongoDbDataSeed(canteenService);
        mongoDbDataSeed.SeedData();
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

