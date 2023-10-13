using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Models
{
	public class UserModel
	{
		public int UserId { get; set; }

		public string UserName { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Telephone { get; set; }

		public string Email { get; set; }

		public string Role { get; set; }

		public IEnumerable<string> CarsVincode { get; set; }

		public IEnumerable<int> OrdersId { get; set; }
	}
}
