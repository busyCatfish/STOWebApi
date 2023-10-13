using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;

namespace STOWebApi.Business
{
	public static class StaticTools
	{
		public static RoleEnum GetRoleEnumByRoleString(string role)
		{
			RoleEnum roleEnum;

			switch (role)
			{
				case "Client":
					roleEnum = RoleEnum.Client;
					break;
				case "Administrator":
					roleEnum = RoleEnum.Administrator;
					break;
				case "Manager":
					roleEnum = RoleEnum.Manager;
					break;
				default:
					throw new STOSystemException("Такої ролі не існує!");
			}

			return roleEnum;
		}

		public static StateEnum GetStateEnumByStateString(string state)
		{
			StateEnum stateEnum;

			switch (state)
			{
				case "Diagnostic":
					stateEnum = StateEnum.Diagnostic;
					break;
				case "Repair":
					stateEnum = StateEnum.Repair;
					break;
				case "Ready":
					stateEnum = StateEnum.Ready;
					break;
				default:
					throw new STOSystemException("Такого стану не існує!");
			}

			return stateEnum;
		}

		public static MasterTypeEnum GetMasterTypeEnumByMasterTypeString(string masterType)
		{
			MasterTypeEnum masterTypeEnum;

			switch (masterType)
			{
				case "Mechanic":
					masterTypeEnum = MasterTypeEnum.Mechanic;
					break;
				case "Welder":
					masterTypeEnum = MasterTypeEnum.Welder;
					break;
				case "Electrician":
					masterTypeEnum = MasterTypeEnum.Electrician;
					break;
				case "DiagnosticsSpecialist":
					masterTypeEnum = MasterTypeEnum.DiagnosticsSpecialist;
					break;
				case "MasterOfWorkWithEngines":
					masterTypeEnum = MasterTypeEnum.MasterOfWorkWithEngines;
					break;
				case "AirConditioningSpecialist":
					masterTypeEnum = MasterTypeEnum.AirConditioningSpecialist;
					break;
				default:
					throw new STOSystemException("Такого типу майстра не існує!");
			}

			return masterTypeEnum;
		}

		public static PositionEnum GetPositionEnumByPositionString(string position)
		{
			PositionEnum positionEnum;

			switch (position)
			{
				case "Master":
					positionEnum = PositionEnum.Master;
					break;
				case "Manager":
					positionEnum = PositionEnum.Manager;
					break;
				case "Owner":
					positionEnum = PositionEnum.Owner;
					break;
				default:
					throw new STOSystemException("Такої позиції не існує!");
			}

			return positionEnum;
		}
	}
}
