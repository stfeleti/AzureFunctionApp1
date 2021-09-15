using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OurProject.Model;

namespace OurProject
{
    public static class QuestionFunction
    {

        [FunctionName("get_All_Questions")]
        public static async Task<IActionResult> GetQuestions(
             [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "question")] HttpRequest req,
             ILogger log)
        {
            log.LogInformation("Called GetQuestions with GET request");

            return new OkObjectResult(QuestionStore.Questions);
        }

        [FunctionName("get_Question_byID")]
        public static async Task<IActionResult> GetQuestionById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ftQuestion/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("Called GetQuestionById with GET request");
            var Question = QuestionStore.Questions.FirstOrDefault(f => f.Id.Equals(id));
            if (Question == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(Question);
        }
        [FunctionName("add_Question")]
        public static async Task<IActionResult> AddQuestion(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Question")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Called add_Question with POST request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var Question = JsonConvert.DeserializeObject<Question>(requestBody);

            QuestionStore.Questions.Add(Question);

            return new OkObjectResult(Question);
        }
        [FunctionName("delete_Question")]
        public static async Task<IActionResult> DeleteQuestion(
           [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Question/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            log.LogInformation("Called delete_Question with DELETE request.");

            var Question = QuestionStore.Questions.FirstOrDefault(f => f.Id.Equals(id));

            if (Question == null)
            {
                return new NotFoundResult();
            }

            QuestionStore.Questions.Remove(Question);
            return new OkResult();
        }
    }
}
