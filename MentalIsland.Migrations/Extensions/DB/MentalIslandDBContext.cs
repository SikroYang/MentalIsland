// using Furion.DatabaseAccessor;
// using Microsoft.EntityFrameworkCore;

namespace MentalIsland.Migrations.Extensions.DB;

// [AppDbContext("Sqlite", DbProvider.Sqlite)]
// public class MentalIslandDBContext : AppDbContext<MentalIslandDBContext>
// {
//     public MentalIslandDBContext(DbContextOptions<MentalIslandDBContext> options) : base(options)
//     {
//         InsertOrUpdateIgnoreNullValues = true;
//     }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseLazyLoadingProxies();
//         base.OnConfiguring(optionsBuilder);
//     }
// }