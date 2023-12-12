using Business;
using Business.Mapping;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices(builder.Configuration);

//builder.Services.AddAutoMapper(typeof(ProductMappingProfiles)); business katman�nda handle edece�iz.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.Run();
