using System.ComponentModel.DataAnnotations;

namespace STOWebApi.Data.Entity
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(20)]
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[EnumDataType(typeof(RoleEnum))]
		public RoleEnum Role { get; set; }

		[Required]
		[MaxLength(20)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(20)]
		public string LastName { get; set; }

		[Required]
		[MaxLength(12)]
		public string Telephone { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public ICollection<Car> Cars { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}
