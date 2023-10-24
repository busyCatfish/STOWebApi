using AutoMapper;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Services;
using STOWebApi.Data.Interfaces;
using STOWebApi.Data;
using STOWebApi.Business;
using STOWebApi.Middleware;
using STOWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace STOWebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
				};
			});

			builder.Services.AddAuthorization();

			builder.Services.AddDbContext<STODbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			var mapperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AutomapperProfile());
			});
			IMapper mapper = mapperConfig.CreateMapper();
			builder.Services.AddSingleton(mapper);

			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<ICarService, CarService>();
			builder.Services.AddScoped<IMasterService, MasterService>();
			builder.Services.AddScoped<IWorkerService, WorkerService>();
			builder.Services.AddScoped<IOrderService, OrderService>();
			builder.Services.AddScoped<IReportService, ReportService>();

			builder.Services.AddTransient<ErrorHandlingMiddleware>();

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please provide a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
			});

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowOrigin",
				builder => builder
					.WithOrigins("http://localhost:5173")
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials()
				);
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
					c.DocExpansion(DocExpansion.None);
				});
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.UseCors("AllowOrigin");

			SeedData.EnsurePopulated(app);

			app.Run();
		}
	}
}