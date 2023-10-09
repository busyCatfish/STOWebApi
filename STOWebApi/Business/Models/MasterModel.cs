using STOWebApi.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace STOWebApi.Business.Models
{
	public class MasterModel
	{
		public int MasterId { get; set; }

		public int WorkerId { get; set; }

		public MasterTypeEnum Type { get; set; }

		public string? Description { get; set; }

		public IEnumerable<int> OrdersId { get; set; }
	}
}
