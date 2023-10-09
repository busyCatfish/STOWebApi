using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Models
{
	public class WorkerRegistrationModel
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Telephone { get; set; }

		public string Email { get; set; }

		public decimal Salary { get; set; }

		public PositionEnum Position { get; set; }
	}
}
