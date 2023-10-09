using AutoMapper;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;
using System.Data;

namespace STOWebApi.Business.Services
{
	public class CarService : ICarService
	{
		public CarService(IUnitOfWork @object, IMapper mapper)
		{
			Object = @object;
			Mapper = mapper;
		}

		public IUnitOfWork Object { get; }

		public IMapper Mapper { get; }

		public async Task AddAsync(CarRegistrationModel model)
		{
			if (model == null)
			{
				throw new STOSystemException("User cannot be null!");
			}

			Car car = Mapper.Map<Car>(model);

			car.UserId = await this.GetUserIdByUserName(model.UserName);

			this.CheckCarModel(car);

			await Object.CarRepository.AddAsync(car);

			await Object.SaveAsync();
		}

		public async Task DeleteByIdAsync(string vincode)
		{
			if (string.IsNullOrEmpty(vincode))
			{
				throw new STOSystemException("Vincode cannot be null or empty");
			}

			await Object.CarRepository.DeleteByIdAsync(vincode);

			await Object.SaveAsync();
		}

		public async Task<IEnumerable<CarModel>> GetAllAsync()
		{
			var allCars = await Object.CarRepository.GetAllWithDetailsAsync();

			var carsModel = Mapper.Map<IEnumerable<CarModel>>(allCars);

			return carsModel;
		}

		public async Task<CarModel> GetByIdAsync(string vincode)
		{
			if (string.IsNullOrEmpty(vincode))
			{
				throw new STOSystemException("Vincode cannot be null or empty");
			}

			var car = await Object.CarRepository.GetByIdWithDetailsAsync(vincode);

			var carModel = Mapper.Map<CarModel>(car);

			return carModel;
		}

		public async Task<IEnumerable<CarModel>> GetCarsByUserIdAsync(int userId)
		{
			if (userId <= 0)
			{
				throw new STOSystemException("UserId should be more than 0");
			}

			var allCars = await Object.CarRepository.GetCarsByUserIdAsync(userId);

			var carsModel = Mapper.Map<IEnumerable<CarModel>>(allCars);

			return carsModel;
		}

		public async Task UpdateAsync(CarModel model)
		{
			if (model == null)
			{
				throw new STOSystemException("User cannot be null!");
			}

			Car car = Mapper.Map<Car>(model);

			CheckCarModel(car);

			await Object.CarRepository.UpdateAsync(car);

			await Object.SaveAsync();
		}

		private async Task<int> GetUserIdByUserName(string userName)
		{
			var user = await Object.UserRepository.GetUserByUserNameAsync(userName);

			if (user == null)
			{
				throw new STOSystemException($"Не існує користувача з таким username: {userName}");
			}

			return user.Id;
		}

		private void CheckCarModel(Car car)
		{
			if (car == null)
			{
				throw new STOSystemException("User cannot be null!");
			}

			if (string.IsNullOrEmpty(car.Vincode))
			{
				throw new STOSystemException("Vincode cannot be null or empty");
			}

			if (car.UserId <= 0)
			{
				throw new STOSystemException("UserId should be more than 0");
			}
		}
	}
}
