namespace STOWebApi.Business.Models
{
	public class ReportModel
	{
		public decimal TotalSum { get; set; }

		public IEnumerable<OrderModel> Orders { get; set; }

		public int CountOfOrders { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
	}
}
