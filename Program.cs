using Microsoft.EntityFrameworkCore;
using UrlShorterer.Models;
using UrlShortererApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));

});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.\
builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

var code = "";
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/{code}", async (string code, AppDbContext _context) =>
{
    var url = await _context.Urls.FirstOrDefaultAsync(s=>s.Code== code);
    if(url == null)
    {
        return Results.NotFound();
    }
    return Results.Redirect(url.LongUrl);
});

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
