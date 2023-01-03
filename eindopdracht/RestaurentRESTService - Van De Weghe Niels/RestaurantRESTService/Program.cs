using RestaurantBL.Interfaces;
using RestaurantBL.Services;
using RestaurantDL.Repositories;
using RestautantRESTServiceUser.Middleware;

var builder = WebApplication.CreateBuilder(args);
string connectionString = @"Data Source=FRENK\SQLEXPRESS;Initial Catalog=RestaurantRESTdef;Integrated Security=True";
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<IReservationRepository>(r => new ReservationRepositoryADO(connectionString));
builder.Services.AddSingleton<IRestautantRepository>(r => new RestaurantRepositoryADO(connectionString));
builder.Services.AddSingleton<IUserRepository>(r => new UserRepositoryADO(connectionString));
builder.Services.AddSingleton<ITableRepository>(r => new TableRepositoryADO(connectionString));

builder.Services.AddSingleton<ReservationService>();
builder.Services.AddSingleton<RestaurantService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<TableService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<LogMiddlewareUser>();

app.Run();
