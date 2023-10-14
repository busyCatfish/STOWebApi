using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STOWebApi.Data.Entity
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public int? UserId { get; set; }

		public string? CarVincode { get; set; }

		[Required]
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Price { get; set; }

		[Required]
		public DateTime StartDate { get; set; }
		
		public DateTime FinisheDate { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Details { get; set; }

		[Required]
		[Column(TypeName = "decimal(18, 2)")]
		public decimal PriceOfDetails { get; set; }

		[Required]
		[EnumDataType(typeof(StateEnum))]
		public StateEnum State { get; set; }

		public User? User { get; set; }

		public Car? Car { get; set; }

		public ICollection<Master> Masters { get; set; } 
	}
}
