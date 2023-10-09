using STOWebApi.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace STOWebApi.Business.Models
{
	public class CarRegistrationModel
	{
		public string Vincode { get; set; }

		public string UserName { get; set; }
	}
}
