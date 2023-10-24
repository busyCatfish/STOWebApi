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

			reportModel.StartDate = start;
			reportModel.EndDate = end;
			var orders = await Object.OrderRepository.GetOrdersByPeriodOfTimeAsync(start, end);
			reportModel.CountOfOrders = orders.Count();
			reportModel.Orders = Mapper.Map<IEnumerable<OrderModel>>(orders);
			var profit = reportModel.Orders.Sum(o => o.Price);
			var sumOfPriceOfDetails = reportModel.Orders.Sum(o => o.PriceOfDetails);
			reportModel.TotalSum = Math.Round(profit - sumOfPriceOfDetails * 0.9m, 2);

			return reportModel;
		}
	}
}
