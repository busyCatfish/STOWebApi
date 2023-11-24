using System.Runtime.Serialization;

namespace STOWebApi.Data.Validation
{
	public class STODataBaseException : Exception
	{
		public STODataBaseException()
		{
		}

		public STODataBaseException(string? message) : base(message)
		{
		}

		public STODataBaseException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
