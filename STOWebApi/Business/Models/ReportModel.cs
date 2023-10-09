namespace STOWebApi.Business.Models
{
	public class ReportModel
	{
		public decimal TotalSum { get; set; }

		public IEnumerable<OrderModel> Orders { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }
	}
}
