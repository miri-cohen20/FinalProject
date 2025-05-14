using Dal.Api;
using Dal.models;
using Microsoft.EntityFrameworkCore;
using Bl.Api;
using Bl.Services;
using Dal.Services;

var builder = WebApplication.CreateBuilder(args);

// הוספת Controllers עם JsonOptions
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// הוספת שירותים
builder.Services.AddScoped<ICarService, RentingService>();
builder.Services.AddScoped<IBlRenting, BlRenting>();
builder.Services.AddScoped<IRenting, RentingService>();
builder.Services.AddScoped<ITime, RentingService>();
builder.Services.AddScoped<IPrice, RentingService>();
builder.Services.AddScoped<ICustomerServise, CustomerService>();
builder.Services.AddScoped<IWorkerService, CustomerService>();
builder.Services.AddScoped<IUser, CustomerService>();
builder.Services.AddScoped<IRoleService, CustomerService>();
builder.Services.AddScoped<ISighIn, SighIn>();
builder.Services.AddScoped<ISighUp, SighUp>();



// הוספת DbContext
builder.Services.AddDbContext<dbClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// הוספת CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// הוספת Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// קונפיגורציה עבור סביבות פיתוח
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// קונפיגורציה כללית של האפליקציה
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins"); // הפעלת CORS
app.UseAuthorization();
app.MapControllers();

app.Run();
