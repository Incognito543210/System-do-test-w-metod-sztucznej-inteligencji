using Microsoft.EntityFrameworkCore;
using DAL;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using System_do_testów_metod_sztucznej_inteligencji.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configuration = builder.Configuration;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IParamInfoService, ParamInfoService>();
builder.Services.AddScoped<IDllService, DllService>();
builder.Services.AddScoped<IDllReader, DllReader>();
builder.Services.AddScoped<ISolveService,SolveService>();
builder.Services.AddScoped<IStateReader, StateReaderService>();
builder.Services.AddScoped<IStateWriter, StateWriterService>();


builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});




var app = builder.Build();
app.UseCors("CorsPolicy");
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
