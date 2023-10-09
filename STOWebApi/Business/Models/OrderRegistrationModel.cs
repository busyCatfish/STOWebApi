using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Models
{
	public class OrderRegistrationModel
	{
		public string UserName { get; set; }

		public string CarVincode { get; set; }

		public decimal Price { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime FinisheDate { get; set; }

		public string Description { get; set; }

		public string Details { get; set; }

		public decimal PriceOfDetails { get; set; }

		public StateEnum State { get; set; }

		public IEnumerable<int> MastersId { get; set; }
	}
}
