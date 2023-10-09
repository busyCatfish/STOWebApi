using STOWebApi.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace STOWebApi.Business.Models
{
	public class CarModel
	{
		public string Vincode { get; set; }

		public string UserName { get; set; }
		
		public string Name { get; set; }
		
		public string Surname { get; set; }

		public IEnumerable<int> OrdersId { get; set; }
	}
}
