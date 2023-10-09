using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STOWebApi.Data.Entity
{
	public class Master
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int WorkerId { get; set; }

		[Required]
		[EnumDataType(typeof(PositionEnum))]
		public MasterTypeEnum Type { get; set; }

		public string? Description { get; set; }

		public Worker? Worker { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}
