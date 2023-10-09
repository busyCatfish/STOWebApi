using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Models
{
	public class UserRegistrationModel
	{
		public string UserName { get; set; }

		public string Password { get; set; }
		
		public string Name { get; set; }

		public string Surname { get; set; }

		public RoleEnum Role { get; set; }

		public string Telephone { get; set; }

		public string Email { get; set; }
	}
}
