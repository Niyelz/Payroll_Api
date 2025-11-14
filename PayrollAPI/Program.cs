using Microsoft.EntityFrameworkCore;
using PayrollApi.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "server=localhost;database=payroll_system;user=root;password=;";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors();

app.UseDefaultFiles();  // auto-detect index.html
app.UseStaticFiles();   // serve from wwwroot folder

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payroll API V1");
        c.RoutePrefix = "swagger"; 
    });
}
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
