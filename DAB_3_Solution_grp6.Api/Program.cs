using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DAB_3_Solution_grp6.Api.Seed;
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

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CanteenAppDbContext>();

    await DataSeed.Seed(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

