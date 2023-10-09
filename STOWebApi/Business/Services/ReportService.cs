using AutoMapper;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Business.Services
{
	public class ReportService : IReportService
	{
		public ReportService(IUnitOfWork @object, IMapper mapper)
		{
			Object = @object;
			Mapper = mapper;
		}

		public IUnitOfWork Object { get; }

		public IMapper Mapper { get; }

		public async Task<ReportModel> GenerateReportByPeriodAsync(DateTime start, DateTime end)
		{
			ReportModel reportModel = new ReportModel();

			reportModel.Start = start;
			reportModel.End = end;
			var orders = await Object.OrderRepository.GetOrdersByPeriodOfTimeAsync(start, end);
			reportModel.Orders = Mapper.Map<IEnumerable<OrderModel>>(orders);
			reportModel.TotalSum = reportModel.Orders.Sum(o => o.Price);

			return reportModel;
		}
	}
}
