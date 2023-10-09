using AutoMapper;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Services;
using STOWebApi.Data.Interfaces;
using STOWebApi.Data;
using STOWebApi.Business;
using STOWebApi.Middleware;
using STOWebApi.Models;

namespace STOWebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

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
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowOrigin",
					builder => builder.WithOrigins("http://localhost:4200") //  адресу React додатка
					.AllowAnyHeader()
					.AllowAnyMethod());
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			SeedData.EnsurePopulated(app);

			app.Run();
		}
	}
}