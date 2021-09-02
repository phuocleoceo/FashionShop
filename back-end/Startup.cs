using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using back_end.Extension;
using back_end.Data;
using Microsoft.EntityFrameworkCore;
using back_end.Repository.Interface;
using back_end.Repository.Implement;
using back_end.Mapper;

namespace back_end
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Cors
			services.AddCors(c =>
				c.AddDefaultPolicy(options =>
				{
					options.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				})
			);

			//Controller and JSON
			services.AddControllersWithViews()
			.AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
			.AddNewtonsoftJson(options =>
				options.SerializerSettings.ContractResolver = new DefaultContractResolver());

			services.AddControllers();

			// Database
			services.AddDbContext<FSContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});

			// DI
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAutoMapper(typeof(FSMapping));

			// JWT
			services.AddAuthentication();
			services.ConfigureIdentity();
			services.ConfigureJWT(this.Configuration);

			// Swagger
			services.ConfigureSwaggerWithAuth();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors(options => options.AllowAnyOrigin()
										  .AllowAnyMethod()
										  .AllowAnyHeader());

			if (env.IsDevelopment())
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
		}
	}
}
