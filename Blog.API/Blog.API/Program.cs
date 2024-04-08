using Blog.API.Data;
using Blog.API.Repositories.Implemetation;
using Blog.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Depency injection of the  DbContext connetion string 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnectionString")); //Injecting the connection connection string
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();  // Injecting Repositories (Interface and implementation)
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();  // Injecting Repositories (Interface and implementation)
builder.Services.AddScoped<IImagesRespository, imagesRespository>(); // Injecting Repositories (Interface and implementation)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseAuthorization();
app.UseAuthentication();
app.UseStaticFiles(new StaticFileOptions
{


    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});
app.MapControllers();

app.Run();
