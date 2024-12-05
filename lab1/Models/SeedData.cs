using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace lab1.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(DataContext context) {

            context.Database.Migrate();
            var entrance = new[]
            {
                new Entrance {ProductName = "Ярик дурка", BarchSize = 1, BarchPrice = 1309, BarchDate = new DateTime(2024,12,1) },
                new Entrance {ProductName = "дурка", BarchSize = 2, BarchPrice = 131139, BarchDate = new DateTime(2024,12,1) },
                new Entrance {ProductName = "11 11", BarchSize = 3, BarchPrice = 130139, BarchDate = new DateTime(2024,12,1) },
                new Entrance {ProductName = "1000-7?", BarchSize = 4, BarchPrice = 1321309, BarchDate = new DateTime(2024,12,1) },
                new Entrance {ProductName = "Концерт кишлака", BarchSize = 5, BarchPrice = 13124309, BarchDate = new DateTime(2024,12,1) },
                new Entrance {ProductName = "тмов", BarchSize = 6, BarchPrice = 141309, BarchDate = new DateTime(2024,12,1) },
                new Entrance {ProductName = "адлута", BarchSize = 7, BarchPrice = 141309, BarchDate = new DateTime(2024,12,1) },
                new Entrance {ProductName = "щшлмтвд", BarchSize = 8, BarchPrice = 1342109, BarchDate = new DateTime(2024,12,1) },
            };
            if (!context.entrances.Any())
            {
                context.entrances.AddRange(entrance);
            }

            context.SaveChanges();
            var Tariffses = new[]
            {
                new Tariffs{ProductName = "Ярики", CostTariff= 1},
                new Tariffs{ProductName = "не ярики", CostTariff= 8134},
                new Tariffs{ProductName = "тема", CostTariff= 14},
                new Tariffs{ProductName = "не тема", CostTariff= 325},
                new Tariffs{ProductName = "аболтус", CostTariff= 14564},
                new Tariffs{ProductName = "404 ерор", CostTariff= 145},
                new Tariffs{ProductName = "лок", CostTariff= 14434245}
            };

            if (!context.Tariffses.Any())
            {
                context.Tariffses.AddRange(Tariffses);
            
            }
            context.SaveChanges();

            var users = new[]
            {
                new Person { Login = "root", Password = "toor"},
            };
            if (!context.Users.Any())
            {
                context.Users.AddRange(users);
            }
            context.SaveChanges();

        }

    }
}
