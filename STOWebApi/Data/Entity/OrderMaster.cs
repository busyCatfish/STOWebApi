using Microsoft.EntityFrameworkCore;

namespace STOWebApi.Data.Entity
{
	[Keyless]
	public class OrderMaster
	{
		public int MasterId { get; set; }

		public int OrderId { get; set; }

		//public Master Master { get; set; }

		//public Order Order { get; set; }
	}
}
