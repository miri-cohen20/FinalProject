using Dal.Api;
using Dal.models;
using Microsoft.EntityFrameworkCore;
using Bl.Api;
using Bl.Services;
using Dal.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddScoped<ICarService, RentingService>();
builder.Services.AddScoped<IBlRenting, BlRenting>();
builder.Services.AddScoped<IRenting, RentingService>();
builder.Services.AddScoped<ITime, RentingService>();
builder.Services.AddScoped<IPrice, RentingService>();
builder.Services.AddScoped<IRenting, RentingService>();
builder.Services.AddScoped<ICustomerServise, CustomerService>();
builder.Services.AddScoped<IWorkerService, CustomerService>();
builder.Services.AddScoped<IUser, CustomerService>();
builder.Services.AddScoped<IRoleService, CustomerService>();
builder.Services.AddScoped<ISighIn, SighIn>();
builder.Services.AddScoped<ISighUp, SighUp>();
builder.Services.AddDbContext<dbClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
