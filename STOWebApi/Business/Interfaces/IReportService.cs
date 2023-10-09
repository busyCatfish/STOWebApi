using STOWebApi.Business.Models;

namespace STOWebApi.Business.Interfaces
{
	public interface IReportService
	{
		Task<ReportModel> GenerateReportByPeriodAsync(DateTime start, DateTime end);
	}
}
