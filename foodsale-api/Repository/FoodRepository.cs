using foodsale_api.Context;
using foodsale_api.Interfaces;
using foodsale_api.Models;

namespace foodsale_api.Repository
{
    public class FoodRepository : GenericRepository<Food>, IFoodRepository
    {
        public FoodRepository(FoodContext context) : base(context)
        {

        }
    }
}
