using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STOWebApi.Data.Entity
{
	public class Worker
	{
		[Key]
		public int Id { get; set; }

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

		[Required]
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Salary { get; set; }

		[Required]
		[EnumDataType(typeof(PositionEnum))]
		public PositionEnum Position { get; set; }

		public Master? Master { get; set; }
	}
}
