using foodsale_api.Interfaces;
using foodsale_api.Models;
using foodsale_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace foodsale_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly ILogger<FoodController> _logger;
        private readonly IFoodRepository _foodRepository;
        public FoodController (ILogger<FoodController> logger, IFoodRepository foodRepository)
        {
            _logger = logger;
            _foodRepository = foodRepository;
        }
        [HttpGet(Name = "GetFoodSale")]
        public IActionResult GetFoodSale() 
        {
            try
            {
                var result = _foodRepository.GetAll().AsEnumerable();
                // init data from excel;
                if (result.Count() == 0)
                {
                    ExcelHelper excel = new ExcelHelper();
                    string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var foods = excel.ReadExcel(Path.Combine(rootPath, "Files"));
                    // save data
                    foreach (var row in foods)
                    {
                        _foodRepository.Insert(row);
                    }
                    if (foods.Count() > 0) 
                        _foodRepository.Save();
                    // query
                    result = _foodRepository.GetAll().AsEnumerable();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPost(Name = "AddFoodSale")]
        public IActionResult AddFoodSale([FromBody] Food reqFood)
        {
            try
            {
                var row = new Food() { OrderDate = reqFood.OrderDate, Region = reqFood.Region, Category = reqFood.Category, City = reqFood.City,  Product = reqFood.Product, Quantity = reqFood.Quantity, UnitPrice = reqFood.UnitPrice };
                _foodRepository.Insert(row);
                _foodRepository.Save();
                return StatusCode(StatusCodes.Status201Created, row);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut(Name = "UpdateFoodSale")]
        public IActionResult UpdateFoodSale([FromBody] Food reqFood)
        {
            try
            {
                var row = new Food() { Id = reqFood.Id, OrderDate = reqFood.OrderDate, Region = reqFood.Region, Category = reqFood.Category, City = reqFood.City, Product = reqFood.Product, Quantity = reqFood.Quantity, UnitPrice = reqFood.UnitPrice };
                _foodRepository.Update(row);
                _foodRepository.Save();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteFoodSale(Guid Id)
        {
           try
            {
                _foodRepository.Delete(Id);
                _foodRepository.Save();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
