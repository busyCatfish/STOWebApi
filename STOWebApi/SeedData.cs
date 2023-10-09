using Microsoft.EntityFrameworkCore;
using STOWebApi.Data;
using STOWebApi.Data.Entity;

namespace STOWebApi.Models
{
	public static class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			STODbContext context = app.ApplicationServices
						.CreateScope().ServiceProvider.GetRequiredService<STODbContext>();

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}

			if (!context.Users.Any())
			{
				context.Users.AddRange(
					new User
					{
						UserName = "user1",
						Password = "password1",
						Role = RoleEnum.Client,
						FirstName = "John",
						LastName = "Doe",
						Telephone = "123-456-7890",
						Email = "john.doe@example.com"
					},
					new User
					{
						UserName = "user2",
						Password = "password2",
						Role = RoleEnum.Client,
						FirstName = "Alice",
						LastName = "Smith",
						Telephone = "987-654-3210",
						Email = "alice.smith@example.com"
					},
					new User
					{
						UserName = "user3",
						Password = "password3",
						Role = RoleEnum.Client,
						FirstName = "Bob",
						LastName = "Johnson",
						Telephone = "555-555-5555",
						Email = "bob.johnson@example.com"
					},
					new User
					{
						UserName = "user4",
						Password = "password4",
						Role = RoleEnum.Client,
						FirstName = "Sarah",
						LastName = "Williams",
						Telephone = "111-222-3333",
						Email = "sarah.williams@example.com"
					},
					new User
					{
						UserName = "user5",
						Password = "password5",
						Role = RoleEnum.Client,
						FirstName = "Michael",
						LastName = "Brown",
						Telephone = "777-888-9999",
						Email = "michael.brown@example.com"
					},
					new User
					{
						UserName = "admin1",
						Password = "adminpassword1",
						Role = RoleEnum.Administrator,
						FirstName = "Admin",
						LastName = "Adminson",
						Telephone = "555-555-5555",
						Email = "admin@example.com"
					},
					new User
					{
						UserName = "admin2",
						Password = "adminpassword2",
						Role = RoleEnum.Administrator,
						FirstName = "Super",
						LastName = "Admin",
						Telephone = "123-123-1234",
						Email = "super.admin@example.com"
					},
					new User
					{
						UserName = "moderator1",
						Password = "moderatorpassword1",
						Role = RoleEnum.Manager,
						FirstName = "Moderator",
						LastName = "Mod",
						Telephone = "555-123-4567",
						Email = "moderator@example.com"
					},
					new User
					{
						UserName = "guest1",
						Password = "guestpassword1",
						Role = RoleEnum.Client,
						FirstName = "Guest",
						LastName = "User",
						Telephone = "999-888-7777",
						Email = "guest@example.com"
					},
					new User
					{
						UserName = "guest2",
						Password = "guestpassword2",
						Role = RoleEnum.Client,
						FirstName = "Guest2",
						LastName = "User",
						Telephone = "444-333-2222",
						Email = "guest2@example.com"
					}
				);



				context.SaveChanges();
			}

			if (!context.Workers.Any())
			{
				context.Workers.AddRange(
					new Worker
					{
						FirstName = "John",
						LastName = "Doe",
						Telephone = "123-456-7890",
						Email = "john.doe@example.com",
						Salary = 100000.00m,
						Position = PositionEnum.Owner
					},
					new Worker
					{
						FirstName = "Alice",
						LastName = "Smith",
						Telephone = "987-654-3210",
						Email = "alice.smith@example.com",
						Salary = 45000.00m,
						Position = PositionEnum.Manager
					},
					new Worker
					{
						FirstName = "Bob",
						LastName = "Johnson",
						Telephone = "555-555-5555",
						Email = "bob.johnson@example.com",
						Salary = 40000.00m,
						Position = PositionEnum.Master
					},
					new Worker
					{
						FirstName = "Sarah",
						LastName = "Williams",
						Telephone = "111-222-3333",
						Email = "sarah.williams@example.com",
						Salary = 35000.00m,
						Position = PositionEnum.Manager
					},
					new Worker
					{
						FirstName = "Michael",
						LastName = "Brown",
						Telephone = "777-888-9999",
						Email = "michael.brown@example.com",
						Salary = 55000.00m,
						Position = PositionEnum.Master
					},
					new Worker
					{
						FirstName = "Eva",
						LastName = "White",
						Telephone = "888-999-7777",
						Email = "eva.white@example.com",
						Salary = 42000.00m,
						Position = PositionEnum.Master
					},
					new Worker
					{
						FirstName = "Daniel",
						LastName = "Jones",
						Telephone = "333-444-5555",
						Email = "daniel.jones@example.com",
						Salary = 38000.00m,
						Position = PositionEnum.Master
					},
					new Worker
					{
						FirstName = "Olivia",
						LastName = "Anderson",
						Telephone = "222-333-4444",
						Email = "olivia.anderson@example.com",
						Salary = 48000.00m,
						Position = PositionEnum.Manager
					},
					new Worker
					{
						FirstName = "Liam",
						LastName = "Davis",
						Telephone = "666-777-8888",
						Email = "liam.davis@example.com",
						Salary = 41000.00m,
						Position = PositionEnum.Master
					},
					new Worker
					{
						FirstName = "Ava",
						LastName = "Wilson",
						Telephone = "444-555-6666",
						Email = "ava.wilson@example.com",
						Salary = 36000.00m,
						Position = PositionEnum.Master
					}
				);


				context.SaveChanges();
			}

			if (!context.Cars.Any())
			{
				context.Cars.AddRange(
					new Car
					{
						Vincode = "1HGCM82633A123456",
						UserId = 1
					},
					new Car
					{
						Vincode = "2T3RWRFV3EW123456",
						UserId = 2
					},
					new Car
					{
						Vincode = "5XYZW3LF0EG123456",
						UserId = 3
					},
					new Car
					{
						Vincode = "JH4DB1650MS123456",
						UserId = 4
					},
					new Car
					{
						Vincode = "3VW5T7AU0HM123456",
						UserId = 5
					},
					new Car
					{
						Vincode = "KM8SRDHF1DU123456",
						UserId = 6
					},
					new Car
					{
						Vincode = "WDCYC7BF9HX123456",
						UserId = 7
					},
					new Car
					{
						Vincode = "1G1BC5M5G7W123456",
						UserId = 8
					},
					new Car
					{
						Vincode = "4T1BK1EB3FU123456",
						UserId = 2
					},
					new Car
					{
						Vincode = "YV1RS592592123456",
						UserId = 1
					}
				);

				context.SaveChanges();
			}

			if (!context.Orders.Any() && !context.Masters.Any())
			{
				var master1 = new Master
				{
					WorkerId = 3,
					Type = MasterTypeEnum.Electrician,
					Description = "Висококваліфікований майстер у справах з електрикою і проводкою.",
				};
				var master2 = new Master
				{
					WorkerId = 5,
					Type = MasterTypeEnum.Mechanic,
					Description = "Досвідчений механік, який володіє ремонтом автомобілів та їхніми системами.",
				};
				var master3 = new Master
				{
					WorkerId = 6,
					Type = MasterTypeEnum.DiagnosticsSpecialist,
					Description = "Експерт з комп'ютерної діагностики автомобілів та виявлення несправностей.",
				};
				var master4 = new Master
				{
					WorkerId = 7,
					Type = MasterTypeEnum.Mechanic,
					Description = "Досвідчений механік, який володіє ремонтом автомобілів та їхніми системами.",
				};
				var master5 = new Master
				{
					WorkerId = 9,
					Type = MasterTypeEnum.Welder,
					Description = "Кваліфікований зварник, який володіє різними видами зварювальних робіт.",
				};
				var master6 = new Master
				{
					WorkerId = 10,
					Type = MasterTypeEnum.MasterOfWorkWithEngines,
					Description = "Експерт з ремонту і підтримки двигунів різних типів і марок автомобілів.",
				};
				context.Masters.AddRange(
					master1, master2, master3, master4, master5, master6
				);

				context.Orders.AddRange(
					new Order
					{
						UserId = 1,
						CarVincode = "1HGCM82633A123456",
						Price = 1500.00m,
						StartDate = DateTime.Now.AddDays(-10),
						FinisheDate = DateTime.Now.AddDays(-5),
						Description = "Заміна масла та фільтра",
						Details = "Масло Mobil 1, фільтр Fram",
						PriceOfDetails = 100.00m,
						State = StateEnum.Ready,
						Masters = new List<Master>() {master1, master2 }
					},
					new Order
					{
						UserId = 2,
						CarVincode = "2T3RWRFV3EW123456",
						Price = 1200.00m,
						StartDate = DateTime.Now.AddDays(-15),
						FinisheDate = DateTime.Now.AddDays(-10),
						Description = "Заміна гальмівних колодок",
						Details = "Гальмівні колодки Brembo",
						PriceOfDetails = 80.00m,
						State = StateEnum.Ready,
						Masters = new List<Master>() { master2, master6 }
					},
					new Order
					{
						UserId = 3,
						CarVincode = "5XYZW3LF0EG123456",
						Price = 800.00m,
						StartDate = DateTime.Now.AddDays(-5),
						Description = "Діагностика двигуна",
						Details = "Проведено комп'ютерну діагностику",
						PriceOfDetails = 50.00m,
						State = StateEnum.Repair,
						Masters = new List<Master>() { master1, master2 }
					},
					new Order
					{
						UserId = 4,
						CarVincode = "JH4DB1650MS123456",
						Price = 500.00m,
						StartDate = DateTime.Now.AddDays(-3),
						Description = "",
						Details = "",
						PriceOfDetails = 0,
						State = StateEnum.Diagnostic,
						Masters = new List<Master>() { master3 }
					},
					new Order
					{
						UserId = 5,
						CarVincode = "3VW5T7AU0HM123456",
						Price = 2500.00m,
						StartDate = DateTime.Now.AddDays(-20),
						FinisheDate = DateTime.Now.AddDays(-15),
						Description = "Ремонт трансмісії",
						Details = "Заміна передач",
						PriceOfDetails = 800.00m,
						State = StateEnum.Ready,
						Masters = new List<Master>() { master2, master3, master4 }
					},
					new Order
					{
						UserId = 6,
						CarVincode = "KM8SRDHF1DU123456",
						Price = 1800.00m,
						StartDate = DateTime.Now.AddDays(-12),
						FinisheDate = DateTime.Now.AddDays(-8),
						Description = "Заміна ременя ГРМ",
						Details = "Ремінь Gates",
						PriceOfDetails = 120.00m,
						State = StateEnum.Ready,
						Masters = new List<Master>() { master4, master5 }
					},
					new Order
					{
						UserId = 7,
						CarVincode = "WDCYC7BF9HX123456",
						Price = 2200.00m,
						StartDate = DateTime.Now.AddDays(-8),
						Description = "Ремонт трансмісії",
						Details = "Заміна передач",
						PriceOfDetails = 800.00m,
						State = StateEnum.Repair,
						Masters = new List<Master>() { master1, master4 }
					},
					new Order
					{
						UserId = 8,
						CarVincode = "1G1BC5M5G7W123456",
						Price = 900.00m,
						StartDate = DateTime.Now.AddDays(-7),
						Description = "Заміна стартера",
						Details = "Стартер Bosch",
						PriceOfDetails = 150.00m,
						State = StateEnum.Repair,
						Masters = new List<Master>() { master6 }
					},
					new Order
					{
						UserId = 2,
						CarVincode = "4T1BK1EB3FU123456",
						Price = 1300.00m,
						StartDate = DateTime.Now.AddDays(-10),
						FinisheDate = DateTime.Now.AddDays(-6),
						Description = "Заміна заднього бампера",
						Details = "Оригінальний бампер",
						PriceOfDetails = 300.00m,
						State = StateEnum.Ready,
						Masters = new List<Master>() { master1, master2 }
					},
					new Order
					{
						UserId = 1,
						CarVincode = "YV1RS592592123456",
						Price = 750.00m,
						StartDate = DateTime.Now.AddDays(-5),
						Description = "Діагностика системи вприску",
						Details = "",
						PriceOfDetails = 0,
						State = StateEnum.Diagnostic,
						Masters = new List<Master>() { master3 }
					}
				);

				context.SaveChanges();
			}
		}
	}
}
