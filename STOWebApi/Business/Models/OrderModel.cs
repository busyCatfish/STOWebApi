using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Models
{
	public class OrderModel
	{
		public int OrderId { get; set; }

		public string UserName { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string CarVincode { get; set; }

		public decimal Price { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime FinisheDate { get; set; }

		public string Description { get; set; }

		public string Details { get; set; }

		public decimal PriceOfDetails { get; set; }

		public string State { get; set; }

		public IEnumerable<int> MastersId { get; set; }
	}
}
