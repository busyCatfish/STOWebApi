using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Models
{
	public class OrderFilterSearchModel
	{
		public int? UserId { get; set;}

		public string? CarVincode { get; set; }

		public string State { get; set;}

		public DateTime? StartDate { get; set; }

		public DateTime? FinisheDate { get; set; }
	}
}
