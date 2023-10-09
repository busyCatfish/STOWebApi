using Microsoft.AspNetCore.Mvc;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;
using System.Reflection;

namespace STOWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportsController : ControllerBase
	{
		private readonly IReportService _reportService;

		public ReportsController(IReportService reportService)
		{
			this._reportService = reportService;
		}


		//GET: api/reports?startDate=YourStartDate&finisheDate=YourFinisheDate
		[HttpGet]
		public async Task<ActionResult<ReportModel>> GetByRollAsync([FromQuery] DateTime start, DateTime end)
		{
			ReportModel report = await _reportService.GenerateReportByPeriodAsync(start, end);

			if (report is null)
			{
				return NotFound();
			}

			return Ok(report);
		}

	}
}
