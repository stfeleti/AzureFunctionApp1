using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp1.Models;

namespace FunctionApp1
{
    public static class TurkeyTime
    {
        [FunctionName("get_fooditems")]
        public static async Task<IActionResult> GetFoodItems(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "fooditem")] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("Called get_fooditems with GET request");

            return new OkObjectResult(FoodItemStore.fooditems);
        }
        [FunctionName("get_fooditems_byID")]
        public static async Task<IActionResult> GetFoodItemsById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "fooditem/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("Called get_fooditems_byID with GET request");

            var foodItem = FoodItemStore.fooditems.FirstOrDefault(f => f.Id.Equals(id));
            if (foodItem == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(foodItem);
        }
        [FunctionName("add_fooditem")]
        public static async Task<IActionResult> AddFoodItem(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "fooditem")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Called add_fooditem with POST request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var fooditem = JsonConvert.DeserializeObject<FoodItem>(requestBody);

            FoodItemStore.fooditems.Add(fooditem);

            return new OkObjectResult(fooditem);
        }
        [FunctionName("delete_fooditem")]
        public static async Task<IActionResult> DeleteFoodItem(
           [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "fooditem/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            log.LogInformation("Called delete_fooditem with DELETE request.");

            var fooditem = FoodItemStore.fooditems.FirstOrDefault(f => f.Id.Equals(id));

            if (fooditem == null)
            {
                return new NotFoundResult();
            }

            FoodItemStore.fooditems.Remove(fooditem);
            return new OkResult();
        }
    }
}
