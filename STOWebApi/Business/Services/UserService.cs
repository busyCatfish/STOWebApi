using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;


namespace STOWebApi.Business.Services
{
	public class UserService : IUserService
	{
		public UserService(IUnitOfWork @object, IMapper mapper)
		{
			Object = @object;
			Mapper = mapper;
		}

		public IUnitOfWork Object { get; }

		public IMapper Mapper { get; }

		public async Task AddAsync(UserRegistrationModel model)
		{
			if (model == null)
			{
				throw new STOSystemException("User cannot be null!");
			}

			User user = Mapper.Map<User>(model);

			this.CheckUserModel(user);

			await Object.UserRepository.AddAsync(user);

			await Object.SaveAsync();
		}

		public async Task DeleteByIdAsync(int modelId)
		{
			if(modelId <= 0)
			{
				throw new STOSystemException("UserId should be more than 0!");
			}

			await Object.UserRepository.DeleteByIdAsync(modelId);

			await Object.SaveAsync();
		}

		public async Task<IEnumerable<UserModel>> GetAllAsync()
		{
			var allUsers = await Object.UserRepository.GetAllWithDetailsAsync();

			var usersModel = Mapper.Map<IEnumerable<UserModel>>(allUsers);

			return usersModel;
		}

		public async Task<UserModel> GetByIdAsync(int modelId)
		{
			if (modelId <= 0)
			{
				throw new STOSystemException("UserId should be more than 0!");
			}

			var user = await Object.UserRepository.GetByIdWithDetailsAsync(modelId);

			var userModel = Mapper.Map<UserModel>(user);

			return userModel;
		}

		public async Task<IEnumerable<UserModel>> GetUsersByRollAsync(RoleEnum role)
		{
			var users = await Object.UserRepository.GetUsersByRoleAsync(role);

			var usersModel = Mapper.Map<IEnumerable<UserModel>>(users);

			return usersModel;
		}

		public async Task UpdateAsync(UserRegistrationModel model, int modelId)
		{
			if (model == null)
			{
				throw new STOSystemException("User cannot be null!");
			}

			User user = Mapper.Map<User>(model);

			user.Id = modelId;

			CheckUserModel(user);

			User? oldUser = await Object.UserRepository.GetByIdAsync(user.Id);

			if(oldUser == null)
			{
				throw new STOSystemException("Incorrect user id!");
			}

			if(oldUser.Id != modelId)
			{
				throw new STOSystemException("Don`t change id!");
			}

			if(oldUser.Password.IsNullOrEmpty() || oldUser.Password == "")
			{
				user.Password = oldUser.Password;
			}

			user.Role = oldUser.Role;

			await Object.UserRepository.UpdateAsync(user);

			await Object.SaveAsync();
		}

		private void CheckUserModel(User user)
		{
			if (user == null)
			{
				throw new STOSystemException("User cannot be null!");
			}

			if (user.Id <= 0)
			{
				throw new STOSystemException("UserId should be more than 0!");
			}

			if (string.IsNullOrEmpty(user.UserName))
			{
				throw new STOSystemException("UserName cannot be null or empty");
			}

			if (string.IsNullOrEmpty(user.FirstName))
			{
				throw new STOSystemException("Name cannot be null or empty");
			}

			if (string.IsNullOrEmpty(user.LastName))
			{
				throw new STOSystemException("Surname cannot be null or empty");
			}

			if (string.IsNullOrEmpty(user.Email))
			{
				throw new STOSystemException("Email cannot be null or empty");
			}

			if (string.IsNullOrEmpty(user.Telephone))
			{
				throw new STOSystemException("Telephone cannot be null or empty");
			}
		}
	}
}
