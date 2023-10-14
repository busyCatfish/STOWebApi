using AutoMapper;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Business
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<User, UserRegistrationModel>()
				.ForMember(urm => urm.UserName, u => u.MapFrom(x => x.UserName))
				.ForMember(urm => urm.Password, u => u.MapFrom(x => string.Empty))
				.ForMember(urm => urm.Email, u => u.MapFrom(x => x.Email))
				.ForMember(urm => urm.Name, u => u.MapFrom(x => x.FirstName))
				.ForMember(u => u.Role, urm => urm.MapFrom(x => x.Role.ToString()))
				.ForMember(urm => urm.Surname, u => u.MapFrom(x => x.LastName))
				.ForMember(urm => urm.Telephone, u => u.MapFrom(x => x.Telephone))
				.ReverseMap()
				.ForMember(u => u.Role, urm => urm.MapFrom(x => StaticTools.GetRoleEnumByRoleString(x.Role)));

			CreateMap<User, UserModel>()
				.ForMember(um => um.UserId, u => u.MapFrom(x => x.Id))
				.ForMember(um => um.UserName, u => u.MapFrom(x => x.UserName))
				.ForMember(um => um.Email, u => u.MapFrom(x => x.Email))
				.ForMember(um => um.Name, u => u.MapFrom(x => x.FirstName))
				.ForMember(um => um.Surname, u => u.MapFrom(x => x.LastName))
				.ForMember(um => um.Telephone, u => u.MapFrom(x => x.Telephone))
				.ForMember(um => um.Role, u => u.MapFrom(x => x.Role.ToString()))
				.ForMember(um => um.CarsVincode, u => u.MapFrom(x => x.Cars.Select(c => c.Vincode)))
				.ForMember(um => um.OrdersId, u => u.MapFrom(x => x.Orders.Select(o => o.Id)))
				.ReverseMap()
				.ForMember(u => u.Role, urm => urm.MapFrom(x => StaticTools.GetRoleEnumByRoleString(x.Role)));

			//CreateMap<User, UserRegistrationModel>()
			//	.ForMember(urm => urm.UserName, u => u.MapFrom(x => x.UserName))
			//	.ForMember(urm => urm.Password, u => u.MapFrom(x => x.Password))
			//	.ReverseMap();

			CreateMap<Worker, WorkerModel>()
				.ForMember(wm => wm.WorkerId, w => w.MapFrom(x => x.Id))
				.ForMember(wm => wm.Email, w => w.MapFrom(x => x.Email))
				.ForMember(wm => wm.Name, w => w.MapFrom(x => x.FirstName))
				.ForMember(wm => wm.Surname, w => w.MapFrom(x => x.LastName))
				.ForMember(wm => wm.Telephone, w => w.MapFrom(x => x.Telephone))
				.ForMember(wm => wm.Position, w => w.MapFrom(x => x.Position.ToString()))
				.ForMember(wm => wm.Salary, w => w.MapFrom(x => x.Salary))
				.AfterMap((w, wm) => wm.MasterId = w.Master != null ? w.Master.Id : 0)
				.ReverseMap()
				.ForMember(w => w.Position, wm => wm.MapFrom(x => StaticTools.GetPositionEnumByPositionString(x.Position)));

			CreateMap<Worker, WorkerRegistrationModel>()
				.ForMember(wm => wm.Name, w => w.MapFrom(x => x.FirstName))
				.ForMember(wm => wm.Surname, w => w.MapFrom(x => x.LastName))
				.ForMember(wm => wm.Email, w => w.MapFrom(x => x.Email))
				.ForMember(wm => wm.Telephone, w => w.MapFrom(x => x.Telephone))
				.ForMember(wm => wm.Position, w => w.MapFrom(x => x.Position.ToString()))
				.ForMember(wm => wm.Salary, w => w.MapFrom(x => x.Salary))
				.ReverseMap()
				.ForMember(w => w.Position, wm => wm.MapFrom(x => StaticTools.GetPositionEnumByPositionString(x.Position)));

			CreateMap<Master, MasterModel>()
				.ForMember(mm => mm.MasterId, m => m.MapFrom(x => x.Id))
				.ForMember(mm => mm.WorkerId, m => m.MapFrom(x => x.WorkerId))
				.ForMember(mm => mm.Type, m => m.MapFrom(x => x.Type.ToString()))
				.ForMember(mm => mm.Description, m => m.MapFrom(x => x.Description))
				.ForMember(mm => mm.OrdersId, m => m.MapFrom(x => x.Orders.Select(o => o.Id)))
				.ReverseMap()
				.ForMember(m => m.Type, mm => mm.MapFrom(x => StaticTools.GetMasterTypeEnumByMasterTypeString(x.Type)));

			CreateMap<Master, MasterRegistrationModel>()
				.ForMember(mm => mm.WorkerId, m => m.MapFrom(x => x.WorkerId))
				.ForMember(mm => mm.Type, m => m.MapFrom(x => x.Type.ToString()))
				.ForMember(mm => mm.Description, m => m.MapFrom(x => x.Description))
				.ReverseMap()
				.ForMember(m => m.Type, mm => mm.MapFrom(x => StaticTools.GetMasterTypeEnumByMasterTypeString(x.Type)));

			CreateMap<Car, CarModel>()
				.ForMember(cm => cm.Vincode, c => c.MapFrom(x => x.Vincode))
				.ForMember(cm => cm.OrdersId, c => c.MapFrom(x => x.Orders.Select(o => o.Id)))
				.AfterMap((c, cm) => cm.UserName = c.User != null ? c.User.UserName : "")
				.AfterMap((c, cm) => cm.Name = c.User != null ? c.User.FirstName : "")
				.AfterMap((c, cm) => cm.Surname = c.User != null ? c.User.LastName : "")
				.ReverseMap();

			CreateMap<Car, CarRegistrationModel>()
				.ForMember(cm => cm.Vincode, c => c.MapFrom(x => x.Vincode))
				.AfterMap((c, cm) => cm.UserName = c.User != null ? c.User.UserName : "")
				.ReverseMap();

			CreateMap<Order, OrderModel>()
				.ForMember(om => om.OrderId, o => o.MapFrom(x => x.Id))
				.ForMember(om => om.CarVincode, o => o.MapFrom(x => x.CarVincode))
				.ForMember(om => om.Description, o => o.MapFrom(x => x.Description))
				.ForMember(om => om.PriceOfDetails, o => o.MapFrom(x => x.PriceOfDetails))
				.ForMember(om => om.Details, o => o.MapFrom(x => x.Details))
				.ForMember(om => om.Price, o => o.MapFrom(x => x.Price))
				.ForMember(om => om.StartDate, o => o.MapFrom(x => x.StartDate))
				.ForMember(om => om.FinisheDate, o => o.MapFrom(x => x.FinisheDate))
				.ForMember(om => om.State, o => o.MapFrom(x => x.State.ToString()))
				.ForMember(om => om.MastersId, o => o.MapFrom(x => x.Masters.Select(m => m.Id)))
				.AfterMap((o, om) => om.UserName = o.User != null ? o.User.UserName : "")
				.AfterMap((o, om) => om.Name = o.User != null ? o.User.FirstName : "")
				.AfterMap((o, om) => om.Surname = o.User != null ? o.User.LastName : "")
				.ReverseMap()
				.ForMember(o => o.State, om => om.MapFrom(x => StaticTools.GetStateEnumByStateString(x.State)));

			CreateMap<Order, OrderRegistrationModel>()
				.ForMember(om => om.CarVincode, o => o.MapFrom(x => x.CarVincode))
				.ForMember(om => om.Description, o => o.MapFrom(x => x.Description))
				.ForMember(om => om.PriceOfDetails, o => o.MapFrom(x => x.PriceOfDetails))
				.ForMember(om => om.Details, o => o.MapFrom(x => x.Details))
				.ForMember(om => om.Price, o => o.MapFrom(x => x.Price))
				.ForMember(om => om.StartDate, o => o.MapFrom(x => x.StartDate))
				.ForMember(om => om.FinisheDate, o => o.MapFrom(x => x.FinisheDate))
				.ForMember(om => om.State, o => o.MapFrom(x => x.State.ToString()))
				.ForMember(om => om.MastersId, o => o.MapFrom(x => x.Masters.Select(m => m.Id)))
				.AfterMap((o, om) => om.UserName = o.User != null ? o.User.UserName : "")
				.ReverseMap()
				.ForMember(o => o.State, om => om.MapFrom(x => StaticTools.GetStateEnumByStateString(x.State)));
		}
	}
}
