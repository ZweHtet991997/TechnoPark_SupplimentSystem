using Microsoft.EntityFrameworkCore;
using TechnoPark_SupplimentSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<EFDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("TechnoParkDBConnection")));

// Register your services.
builder.Services.AddScoped<UserServiecs>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<GlobalSupplierServices>();
builder.Services.AddScoped<LocalSupplierServices>();
builder.Services.AddScoped<CurrencyExchangeRateServices>();
builder.Services.AddScoped<SalesServices>();
builder.Services.AddScoped<CommissionServices>();

// Add CORS services and define a specific policy.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS with the defined policy.
app.UseCors("AllowAll");

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