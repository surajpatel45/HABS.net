using HABS.API;
using HABS.Data;
using HABS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DoctorServices>();
builder.Services.AddScoped<PatientServices>();
builder.Services.AddScoped<AppointmentServices>();

builder.Services.AddScoped<RabbitMQProducer>();
builder.Services.AddScoped<RabbitMQService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<MyDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DB")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
