using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using back_end.Repository.Implement;
using back_end.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using back_end.Authentication;
using back_end.Extension;
using Newtonsoft.Json;
using back_end.Mapper;
using back_end.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Cors
builder.Services.AddCors(c =>
    c.AddDefaultPolicy(options =>
    {
        options.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    })
);

//Controller and JSON
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<FSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
builder.Services.AddAutoMapper(typeof(FSMapping));

// JWT
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

// Swagger
builder.Services.ConfigureSwaggerWithAuth();



var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "back_end v1");
        c.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
    RequestPath = "/Photos"
});

app.Run();
