using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STOWebApi.Data.Entity
{
	public class Car
	{
		[Key]
		[MaxLength(17)]
		public string Vincode { get; set; }

		public int? UserId { get; set; }

		public User? User { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}
