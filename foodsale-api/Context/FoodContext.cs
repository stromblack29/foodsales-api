using foodsale_api.Models;
using Microsoft.EntityFrameworkCore;

namespace foodsale_api.Context
{
    public class FoodContext : DbContext
    {
        public FoodContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase("food-db");

        public DbSet<Food> foods { get; set; }
    }
}
