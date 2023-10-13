using STOWebApi.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace STOWebApi.Business.Models
{
	public class MasterRegistrationModel
	{
		public int WorkerId { get; set; }

		public string Type { get; set; }

		public string? Description { get; set; }
	}
}
